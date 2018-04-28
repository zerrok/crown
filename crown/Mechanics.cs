using System.Collections.Generic;

namespace crown
{

    public class Mechanics
    {

        int gold;
        int wood;
        int stone;
        int population;
        int food;
        int workers;
        float doomClock;
        List<Building> buildings;

        int foodTick;

        public Mechanics()
        {
            gold = 500;
            wood = 0;
            stone = 0;
            population = 0;
            food = 100;
            doomClock = 0;
            buildings = new List<Building>();
            workers = 0;
            foodTick = 0;
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

        public void UpdateMechanics()
        {
            doomClock += 0.02f;

            // Things are happening every doomlock tick
            if (DoomClock > 1f) {
                // Building updates
                foreach (Building bld in buildings) {
                    bld.BuildingTick++;
                    UpdateBuildingSprites(bld);
                }

                // People gotta eat
                foodTick++;
                if (foodTick > 20) {
                    foodTick = 0;
                    food -= population / 2;

                    // TODO: Wenn food < 0 dann sterben erst arbeitslose, dann worker
                }

                DoomClock = 0f;
            }
        }

        private static void UpdateBuildingSprites(Building bld)
        {
            // For gradually building buildings
            if (bld.BuildingState <= 3) {
                bld.UpdateSprite();
                if (bld.BuildingTick >= 5) {
                    bld.BuildingTick = 0;
                    bld.BuildingState++;
                }
            }
        }
    }



}
