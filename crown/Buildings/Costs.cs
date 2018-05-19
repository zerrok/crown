using System;

namespace crown {
    public class Costs {

        public static Costs HouseCosts() {
            return new Costs(0,     // Stone 
                             -10,    // Wood
                             -10,   // Gold
                             2,     // Workers
                             2,     // Population
                             0,     // Food
                             2,     // GoldUpkeep
                             -1,     // WoodUpkeep
                             0,     // StoneUpkeep
                             -2);   // FoodUpkeep
        }

        public static Costs WoodcutterCosts() {
            return new Costs(0,     // Stone 
                             0,     // Wood
                             -30,    // Gold
                             -10,    // Workers
                             0,     // Population
                             0,     // Food
                             -5,     // GoldUpkeep
                             20,     // WoodUpkeep
                             0,     // StoneUpkeep
                             0);    // FoodUpkeep
        }

        public static Costs FarmCosts() {
            return new Costs(0,     // Stone 
                             -5,    // Wood
                             -30,    // Gold
                             -10,    // Workers
                             0,     // Population
                             0,     // Food
                             -5,     // GoldUpkeep
                             0,     // WoodUpkeep
                             0,     // StoneUpkeep
                             30);    // FoodUpkeep
        }

        public static Costs QuarryCosts() {
            return new Costs(0,     // Stone 
                             -20,    // Wood
                             -50,   // Gold
                             -30,     // Workers
                             0,     // Population
                             0,     // Food
                             -30,     // GoldUpkeep
                             -1,     // WoodUpkeep
                             5,     // StoneUpkeep
                             0);   // FoodUpkeep
        }

        internal static Costs ScientistCosts() {
            return new Costs(0,     // Stone 
                             -100,    // Wood
                             -500,   // Gold
                             0,     // Workers
                             0,     // Population
                             0,     // Food
                             -50,     // GoldUpkeep
                             -10,     // WoodUpkeep
                             -8,     // StoneUpkeep
                             -5);   // FoodUpkeep
        }

        int stone;
        int wood;
        int gold;
        int workers;
        int population;
        int food;

        int goldUpkeep;
        int woodUpkeep;
        int stoneUpkeep;
        int foodUpkeep;

        public Costs(int stone, int wood, int gold, int workers, int population, int food, int goldUpkeep, int woodUpkeep, int stoneUpkeep, int foodUpkeep) {
            this.stone = stone;
            this.wood = wood;
            this.gold = gold;
            this.workers = workers;
            this.population = population;
            this.food = food;
            this.goldUpkeep = goldUpkeep;
            this.woodUpkeep = woodUpkeep;
            this.stoneUpkeep = stoneUpkeep;
            this.foodUpkeep = foodUpkeep;
        }

        public int Stone { get => stone; set => stone = value; }
        public int Wood { get => wood; set => wood = value; }
        public int Gold { get => gold; set => gold = value; }
        public int Workers { get => workers; set => workers = value; }
        public int Population { get => population; set => population = value; }
        public int Food { get => food; set => food = value; }
        public int GoldUpkeep { get => goldUpkeep; set => goldUpkeep = value; }
        public int WoodUpkeep { get => woodUpkeep; set => woodUpkeep = value; }
        public int StoneUpkeep { get => stoneUpkeep; set => stoneUpkeep = value; }
        public int FoodUpkeep { get => foodUpkeep; set => foodUpkeep = value; }

    }
}
