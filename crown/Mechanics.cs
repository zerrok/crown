using System;
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

        int foodDelta;
        int goldDelta;
        int woodDelta;
        int stoneDelta;

        int workers;
        float doomClock;
        List<Building> buildings;

        public Mechanics() {
            gold = 1000;
            wood = 0;
            stone = 0;
            population = 0;
            food = 0;

            foodDelta = 0;
            goldDelta = 0;
            woodDelta = 0;
            stoneDelta = 0;

            StoneStorage = 0;
            FoodStorage = 0;
            WoodStorage = 0;

            doomClock = 0;
            buildings = new List<Building>();
            workers = 0;

            maxPop = 0;
        }


        public void UpdateMechanics() {
            doomClock += 0.02f;

            // Things are happening every doomlock tick
            if (DoomClock > 1f) {
                BuildingUpdates();
                DoomClock = 0f;
            }
        }

        private void BuildingUpdates() {
            // Building updates
            foreach (Building bld in buildings) {
                bld.Updates();
            }
        }


        public int GetMaxPop() {
            int count = 0;
            foreach (Building building in buildings) {
                if (building.Type == Building.BuildingTypes.HOUSE)
                    count++;
            }
            return count * 2;
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
        public int FoodDelta { get => foodDelta; set => foodDelta = value; }
        public int GoldDelta { get => goldDelta; set => goldDelta = value; }
        public int WoodDelta { get => woodDelta; set => woodDelta = value; }
        public int StoneDelta { get => stoneDelta; set => stoneDelta = value; }
    }
}



