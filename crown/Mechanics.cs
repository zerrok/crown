using System.Collections.Generic;

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

        int workers;
        float doomClock;
        List<Building> buildings;

        int foodTick;
        int taxTick;

        public Mechanics() {
            gold = 5000;
            wood = 0;
            stone = 0;
            population = 0;
            food = 0;

            StoneStorage = 0;
            FoodStorage = 0;
            WoodStorage = 0;

            doomClock = 0;
            buildings = new List<Building>();
            workers = 0;

            foodTick = 0;
            taxTick = 0;

            maxPop = 0;
        }


        public void UpdateMechanics() {
            doomClock += 0.02f;

            // Things are happening every doomlock tick
            if (DoomClock > 1f) {
                UpdateBuildings();

                // People gotta eat
                foodTick++;
                if (foodTick > 50) {
                    foodTick = 0;
                    food -= population / 2 < 0 ? 0 : population / 2;

                    // TODO: Wenn food < 0 dann sterben erst arbeitslose, dann worker
                }

                // People gotta pay taxes
                taxTick++;
                if (taxTick > 25) {
                    taxTick = 0;
                    gold += population;
                }
                DoomClock = 0f;
            }
        }

        private void UpdateBuildings() {
            // Building updates
            foreach (Building bld in buildings) {
                if (bld.BuildingTick < 5) {
                    bld.BuildingTick++;

                    if (bld.BuildingTick >= 5) {
                        if (bld.BuildingState <= 3) {
                            // Go through the different build phases
                            UpdateBuildingSprites(bld);
                        }
                        if (bld.BuildingState == 4) {
                            if (bld.Type == Building.BuildingTypes.HOUSE) {
                                // People move in and are ready to work
                                InitializeHouse(bld);
                            }
                            // Townhall can store stuff now and brings initial food
                            if (bld.Type == Building.BuildingTypes.HOUSE) {
                                InitializeTownhall();
                            }
                            bld.BuildingState = 5;
                        }
                        if (bld.BuildingState == 5) {
                            // Do something building related, 5 is the final state
                        }
                        bld.BuildingTick = 0;
                    }
                }
            }
        }

        private void InitializeTownhall() {
            woodStorage = 200;
            stoneStorage = 200;
            foodStorage = 1000;
            food = 300;
        }

        public int GetMaxPop() {
            int count = 0;
            foreach (Building building in buildings) {
                if (building.Type == Building.BuildingTypes.HOUSE)
                    count++;
            }
            return count * 2;
        }

        private void InitializeHouse(Building bld) {
            bld.Inhabitants = 2;
            population += bld.Inhabitants;
            workers += bld.Inhabitants;
        }

        private void UpdateBuildingSprites(Building bld) {
            // For gradually building buildings
            bld.UpdateSprite();
            bld.BuildingState++;
        }

        public int Gold {
            get => gold; set => gold = value;
        }
        public int Wood {
            get => wood; set => wood = value;
        }
        public int Stone {
            get => stone; set => stone = value;
        }
        public int Population {
            get => population; set => population = value;
        }
        public int Food {
            get => food; set => food = value;
        }
        public float DoomClock {
            get => doomClock; set => doomClock = value;
        }
        public List<Building> Buildings {
            get => buildings; set => buildings = value;
        }
        public int Workers {
            get => workers; set => workers = value;
        }
        public int FoodTick {
            get => foodTick; set => foodTick = value;
        }
        public int StoneStorage {
            get => stoneStorage;
            set => stoneStorage = value;
        }
        public int FoodStorage {
            get => foodStorage;
            set => foodStorage = value;
        }
        public int WoodStorage {
            get => woodStorage;
            set => woodStorage = value;
        }
        public int MaxPop { get => maxPop; set => maxPop = value; }
    }
}



