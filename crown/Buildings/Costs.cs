namespace crown {
    public class Costs {

        public static Costs HouseCosts() {
            return new Costs(0,     // Stone 
                             -5,    // Wood
                             -10,   // Gold
                             2,     // Workers
                             2,     // Population
                             0,     // Food
                             1,     // GoldUpkeep
                             -1,     // WoodUpkeep
                             0,     // StoneUpkeep
                             -1);   // FoodUpkeep
        }

        public static Costs WoodcutterCosts() {
            return new Costs(0,     // Stone 
                             0,     // Wood
                             15,    // Gold
                             -4,    // Workers
                             0,     // Population
                             0,     // Food
                             -8,     // GoldUpkeep
                             8,     // WoodUpkeep
                             0,     // StoneUpkeep
                             0);    // FoodUpkeep
        }

        public static Costs FarmCosts() {
            return new Costs(0,     // Stone 
                             15,    // Wood
                             20,    // Gold
                             -4,    // Workers
                             0,     // Population
                             0,     // Food
                             -8,     // GoldUpkeep
                             0,     // WoodUpkeep
                             0,     // StoneUpkeep
                             8);    // FoodUpkeep
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
