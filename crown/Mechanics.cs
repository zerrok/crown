using System.Collections.Generic;
using static crown.Game1;
using Microsoft.Xna.Framework;
using crown.Terrain;

namespace crown {

    public class Mechanics {

        int gold;
        int wood;
        int stone;
        int population;
        int food;
        int maxPop;

        int stoneStorage;
        int foodStorage;
        int woodStorage;

        int foodDelta;
        int goldDelta;
        int woodDelta;
        int stoneDelta;

        int workers;
        float doomClock;
        int taxClock;

        List<Building> buildings;

        public int Gold { get => gold; set => gold = value; }
        public int Wood { get => wood; set => wood = value; }
        public int Stone { get => stone; set => stone = value; }
        public int Population { get => population; set => population = value; }
        public int Food { get => food; set => food = value; }
        public int MaxPop { get => maxPop; set => maxPop = value; }
        public int StoneStorage { get => stoneStorage; set => stoneStorage = value; }
        public int FoodStorage { get => foodStorage; set => foodStorage = value; }
        public int WoodStorage { get => woodStorage; set => woodStorage = value; }
        public int FoodDelta { get => foodDelta; set => foodDelta = value; }
        public int GoldDelta { get => goldDelta; set => goldDelta = value; }
        public int WoodDelta { get => woodDelta; set => woodDelta = value; }
        public int StoneDelta { get => stoneDelta; set => stoneDelta = value; }
        public int Workers { get => workers; set => workers = value; }
        public float DoomClock { get => doomClock; set => doomClock = value; }
        public List<Building> Buildings { get => buildings; set => buildings = value; }
        public int TaxClock { get => taxClock; set => taxClock = value; }

        public Mechanics() {
            Gold = 500;
            Wood = 0;
            Stone = 0;
            Population = 0;
            Food = 0;

            FoodDelta = 0;
            GoldDelta = 0;
            WoodDelta = 0;
            StoneDelta = 0;

            StoneStorage = 0;
            FoodStorage = 0;
            WoodStorage = 0;

            DoomClock = 0;
            Buildings = new List<Building>();
            Workers = 0;

            MaxPop = 0;
        }


        public void UpdateMechanics() {
            DoomClock += 0.02f;

            CitizenUpdates();
            UIUpdates();

            // Things are happening every doomlock tick
            if (DoomClock > 1f) {
                BuildingUpdates();
                ResourceUpdates();
                InteractiveUpdates();
                DoomClock = 0f;
            }
        }

        private void UIUpdates() {
            foreach (UIElement ui in uiElements) {
                if (ui.Type == UIElement.ElementType.Resources) {
                    if (ui.Resource1 == UIElement.Resource.Population) {
                        ui.TextBottom = mechanics.Population + " / " + mechanics.MaxPop;
                    }
                    if (ui.Resource1 == UIElement.Resource.Workers) {
                        ui.TextBottom = mechanics.Workers.ToString();
                    }
                    if (ui.Resource1 == UIElement.Resource.Gold) {
                        ui.TextTop = mechanics.Gold.ToString();
                        ui.TextBottom = mechanics.GoldDelta.ToString();
                    }
                    if (ui.Resource1 == UIElement.Resource.Wood) {
                        ui.TextTop = mechanics.Wood + " / " + mechanics.WoodStorage;
                        ui.TextBottom = mechanics.WoodDelta.ToString();
                    }
                    if (ui.Resource1 == UIElement.Resource.Stone) {
                        ui.TextTop = mechanics.Stone + " / " + mechanics.StoneStorage;
                        ui.TextBottom = mechanics.StoneDelta.ToString();
                    }
                    if (ui.Resource1 == UIElement.Resource.Food) {
                        ui.TextTop = mechanics.Food + " / " + mechanics.FoodStorage;
                        ui.TextBottom = mechanics.FoodDelta.ToString();
                    }
                }
                if (ui.Type == UIElement.ElementType.MenuSelection) {
                    Costs costs = null;
                    if (mouseAction == MouseAction.House)
                        costs = Costs.HouseCosts();
                    if (mouseAction == MouseAction.Farm)
                        costs = Costs.FarmCosts();
                    if (mouseAction == MouseAction.Woodcutter)
                        costs = Costs.WoodcutterCosts();
                    if (mouseAction == MouseAction.Quarry)
                        costs = Costs.QuarryCosts();
                    if (mouseAction == MouseAction.Scientist)
                        costs = Costs.ScientistCosts();
                    if (costs != null) {
                        if (ui.Resource1 == UIElement.Resource.Food) {
                            ui.TextTop = costs.Food.ToString();
                            ui.TextBottom = costs.FoodUpkeep.ToString();
                        }
                        if (ui.Resource1 == UIElement.Resource.Stone) {
                            ui.TextTop = costs.Stone.ToString();
                            ui.TextBottom = costs.StoneUpkeep.ToString();
                        }
                        if (ui.Resource1 == UIElement.Resource.Wood) {
                            ui.TextTop = costs.Wood.ToString();
                            ui.TextBottom = costs.WoodUpkeep.ToString();
                        }
                        if (ui.Resource1 == UIElement.Resource.Workers) {
                            ui.TextTop = "";
                            ui.TextBottom = costs.Workers.ToString();
                        }
                        if (ui.Resource1 == UIElement.Resource.Gold) {
                            ui.TextTop = costs.Gold.ToString();
                            ui.TextBottom = costs.GoldUpkeep.ToString();
                        }
                    }
                }
            }
        }

        private void InteractiveUpdates() {
            foreach (Interactive inter in interactives) {
                // When trees are chopped they will regrow after a while
                if (inter.Type == Interactive.IntType.TREE) {
                    inter.RespawnInteractive();
                }
            }
        }

        private void ResourceUpdates() {
            if (taxClock >= 15) {
                if (Food + FoodDelta <= FoodStorage)
                    Food += FoodDelta;
                else
                    Food = FoodStorage;

                if (Wood + WoodDelta <= WoodStorage)
                    Wood += WoodDelta;
                else
                    Wood = WoodStorage;

                if (Stone + StoneDelta <= StoneStorage)
                    Stone += StoneDelta;
                else
                    Stone = StoneStorage;

                Gold += GoldDelta;
                taxClock = 0;
            } else {
                taxClock++;
            }
        }

        private void BuildingUpdates() {
            // Building updates
            foreach (Building bld in Buildings) {
                bld.Updates();
            }
        }

        private void CitizenUpdates() {
            if (citizens != null) {
                Vector2 position = new Vector2();
                // Always spawn as much people as there is population
                if (citizens.Count < Population)
                    // Get the position of a random road and spawn the people there
                    foreach (Road road in roads) {
                        if (road != null)
                            if (random.Next(1, 1000) > 990) {
                                position = road.Coords;
                                // Middle of the road tile calculation
                                position.X += tileSize / 2 - 16;
                                position.Y += tileSize / 2 - 16;
                                Citizen citizen = new Citizen(position, GetPeopleSprite());
                                citizens.Add(citizen);
                                break;
                            }
                    }

                foreach (Citizen citizen in citizens)
                    citizen.Movement();
            }
        }

        private static TexturePackerLoader.SpriteFrame GetPeopleSprite() {
            int rand = random.Next(0, 3);
            if (rand == 0)
                return peopleTileSheet.Sprite(TexturePackerMonoGameDefinitions.peopleAtlas.Peop1);
            if (rand == 1)
                return peopleTileSheet.Sprite(TexturePackerMonoGameDefinitions.peopleAtlas.Peop2);
            if (rand == 2)
                return peopleTileSheet.Sprite(TexturePackerMonoGameDefinitions.peopleAtlas.Peop3);
            return null;
        }

        public int GetMaxPop() {
            int count = 0;
            foreach (Building building in Buildings) {
                if (building.Type == Building.BuildingTypes.House)
                    count++;
            }
            return count * 2;
        }



    }
}



