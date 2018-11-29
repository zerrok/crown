using System.Collections.Generic;
using static crown.Game1;
using Microsoft.Xna.Framework;
using crown.Terrain;

namespace crown
{

    public class Mechanics
    {

        double gold;
        double wood;
        double stone;
        double wheat;
        double beer;
        double population;
        double food;
        double maxPop;

        double stoneStorage;
        double foodStorage;
        double woodStorage;
        double wheatStorage;
        double beerStorage;

        double foodDelta;
        double goldDelta;
        double woodDelta;
        double stoneDelta;
        double wheatDelta;
        double beerDelta;
        double workers;

        float doomClock;
        int taxClock;

        List<Building> buildings;

        public double Gold { get => gold; set => gold = value; }
        public double Wood { get => wood; set => wood = value; }
        public double Stone { get => stone; set => stone = value; }
        public double Population { get => population; set => population = value; }
        public double Food { get => food; set => food = value; }
        public double MaxPop { get => maxPop; set => maxPop = value; }
        public double StoneStorage { get => stoneStorage; set => stoneStorage = value; }
        public double FoodStorage { get => foodStorage; set => foodStorage = value; }
        public double WoodStorage { get => woodStorage; set => woodStorage = value; }
        public double FoodDelta { get => foodDelta; set => foodDelta = value; }
        public double GoldDelta { get => goldDelta; set => goldDelta = value; }
        public double WoodDelta { get => woodDelta; set => woodDelta = value; }
        public double StoneDelta { get => stoneDelta; set => stoneDelta = value; }
        public double Workers { get => workers; set => workers = value; }
        public float DoomClock { get => doomClock; set => doomClock = value; }
        public List<Building> Buildings { get => buildings; set => buildings = value; }
        public int TaxClock { get => taxClock; set => taxClock = value; }
        public double Wheat { get => wheat; set => wheat = value; }
        public double Beer { get => beer; set => beer = value; }
        public double WheatDelta { get => wheatDelta; set => wheatDelta = value; }
        public double BeerDelta { get => beerDelta; set => beerDelta = value; }
        public double WheatStorage { get => wheatStorage; set => wheatStorage = value; }
        public double BeerStorage { get => beerStorage; set => beerStorage = value; }

        public Mechanics() {
            Gold = 500;
            Wood = 0;
            Stone = 0;
            Population = 0;
            Food = 0;
            Wheat = 0;
            Beer = 0;

            FoodDelta = 0;
            GoldDelta = 0;
            WoodDelta = 0;
            StoneDelta = 0;
            WheatDelta = 0;
            BeerDelta = 0;

            StoneStorage = 0;
            FoodStorage = 0;
            WoodStorage = 0;
            BeerStorage = 0;
            WheatStorage = 0;

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
                    if (ui.Resource1 == UIElement.Resource.Beer) {
                        ui.TextTop = mechanics.Beer + " / " + mechanics.BeerStorage;
                        ui.TextBottom = mechanics.BeerDelta.ToString();
                    }
                    if (ui.Resource1 == UIElement.Resource.Wheat) {
                        ui.TextTop = mechanics.Wheat + " / " + mechanics.WheatStorage;
                        ui.TextBottom = mechanics.WheatDelta.ToString();
                    }
                }
                if (ui.Type == UIElement.ElementType.MenuSelection) {
                    Costs costs = null;
                    if (mouseAction == Actions.House)
                        costs = Costs.HouseCosts();
                    if (mouseAction == Actions.Farm)
                        costs = Costs.FarmCosts();
                    if (mouseAction == Actions.Woodcutter)
                        costs = Costs.WoodcutterCosts();
                    if (mouseAction == Actions.Quarry)
                        costs = Costs.QuarryCosts();
                    if (mouseAction == Actions.Scientist)
                        costs = Costs.ScientistCosts();
                    if (mouseAction == Actions.Brewery)
                        costs = Costs.BreweryCosts();
                    if (mouseAction == Actions.Tavern)
                        costs = Costs.TavernCosts();
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
                        if (ui.Resource1 == UIElement.Resource.Beer) {
                            ui.TextTop = costs.Beer.ToString();
                            ui.TextBottom = costs.BeerUpkeep.ToString();
                        }
                        if (ui.Resource1 == UIElement.Resource.Wheat) {
                            ui.TextTop = costs.Wheat.ToString();
                            ui.TextBottom = costs.WheatUpkeep.ToString();
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

                if (Beer + BeerDelta <= BeerStorage)
                    Beer += BeerDelta;
                else
                    Beer = BeerStorage;

                if (Wheat + WheatDelta <= WheatStorage)
                    Wheat += WheatDelta;
                else
                    Wheat = WheatStorage;

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
                if (building.Type == Actions.House)
                    count++;
            }
            return count * 2;
        }



    }
}



