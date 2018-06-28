namespace crown {
    public class Costs {

        public static Costs HouseCosts() {
            return new Costs(0,     // Stone 
                             0,     // Wood
                             0,     // Gold
                             2,     // Workers
                             0,     // Population
                             0,     // Food
                             0,     // Wheat
                             0,     // Beer
                             0,     // WheatUpkeep
                             -0.25,     // BeerUpkeep
                             2,     // GoldUpkeep
                             0,     // WoodUpkeep
                             0,     // StoneUpkeep
                             -1);    // FoodUpkeep
        }

        public static Costs WoodcutterCosts() {
            return new Costs(0,     // Stone 
                             0,     // Wood
                             0,     // Gold
                             -5,     // Workers
                             0,     // Population
                             0,     // Food
                             0,     // Wheat
                             0,     // Beer
                             0,     // WheatUpkeep
                             0,     // BeerUpkeep
                             -5,     // GoldUpkeep
                             10,     // WoodUpkeep
                             0,     // StoneUpkeep
                             0);    // FoodUpkeep
        }

        public static Costs FarmCosts() {
            return new Costs(0,     // Stone 
                             0,     // Wood
                             0,     // Gold
                             -10,     // Workers
                             0,     // Population
                             0,     // Food
                             0,     // Wheat
                             0,     // Beer
                             20,     // WheatUpkeep
                             0,     // BeerUpkeep
                             -10,     // GoldUpkeep
                             0,     // WoodUpkeep
                             0,     // StoneUpkeep
                             0);    // FoodUpkeep
        }

        public static Costs QuarryCosts() {
            return new Costs(0,     // Stone 
                             0,     // Wood
                             0,     // Gold
                             -20,     // Workers
                             0,     // Population
                             0,     // Food
                             0,     // Wheat
                             0,     // Beer
                             0,     // WheatUpkeep
                             0,     // BeerUpkeep
                             -15,     // GoldUpkeep
                             0,     // WoodUpkeep
                             5,     // StoneUpkeep
                             0);    // FoodUpkeep
        }

        public static Costs ScientistCosts() {
            return new Costs(0,     // Stone 
                             0,     // Wood
                             0,     // Gold
                             -50,     // Workers
                             0,     // Population
                             0,     // Food
                             0,     // Wheat
                             0,     // Beer
                             0,     // WheatUpkeep
                             0,     // BeerUpkeep
                            -50,     // GoldUpkeep
                             -5,     // WoodUpkeep
                             -5,     // StoneUpkeep
                             0);    // FoodUpkeep
        }
        public static Costs TavernCosts() {
            return new Costs(0,     // Stone 
                             0,     // Wood
                             0,     // Gold
                             -5,     // Workers
                             0,     // Population
                             0,     // Food
                             0,     // Wheat
                             0,     // Beer
                             -20,     // WheatUpkeep
                             0,     // BeerUpkeep
                             -5,     // GoldUpkeep
                             0,     // WoodUpkeep
                             0,     // StoneUpkeep
                             40);    // FoodUpkeep
        }

        public static Costs BreweryCosts() {
            return new Costs(0,     // Stone 
                             0,     // Wood
                             0,     // Gold
                             -8,     // Workers
                             0,     // Population
                             0,     // Food
                             0,     // Wheat
                             0,     // Beer
                             -20,     // WheatUpkeep
                             20,     // BeerUpkeep
                             -8,     // GoldUpkeep
                             0,     // WoodUpkeep
                             0,     // StoneUpkeep
                             0);    // FoodUpkeep
        }

        public static Costs StorageCosts() {
            return new Costs(0,     // Stone 
                             0,     // Wood
                             0,     // Gold
                             -10,     // Workers
                             0,     // Population
                             0,     // Food
                             0,     // Wheat
                             0,     // Beer
                             0,     // WheatUpkeep
                             0,     // BeerUpkeep
                             -10,     // GoldUpkeep
                             0,     // WoodUpkeep
                             0,     // StoneUpkeep
                             0);    // FoodUpkeep
        }

        double stone;
        double wood;
        double gold;
        double workers;
        double population;
        double food;
        double beer;
        double wheat;

        double wheatUpkeep;
        double beerUpkeep;
        double goldUpkeep;
        double woodUpkeep;
        double stoneUpkeep;
        double foodUpkeep;

        public Costs(double stone, double wood, double gold, double workers, double population, double food, double beer, double wheat, double wheatUpkeep, double beerUpkeep, double goldUpkeep, double woodUpkeep, double stoneUpkeep, double foodUpkeep) {
            this.stone = stone;
            this.wood = wood;
            this.gold = gold;
            this.workers = workers;
            this.population = population;
            this.food = food;
            this.beer = beer;
            this.wheat = wheat;
            this.wheatUpkeep = wheatUpkeep;
            this.beerUpkeep = beerUpkeep;
            this.goldUpkeep = goldUpkeep;
            this.woodUpkeep = woodUpkeep;
            this.stoneUpkeep = stoneUpkeep;
            this.foodUpkeep = foodUpkeep;
        }

        public double Stone { get => stone; set => stone = value; }
        public double Wood { get => wood; set => wood = value; }
        public double Gold { get => gold; set => gold = value; }
        public double Workers { get => workers; set => workers = value; }
        public double Population { get => population; set => population = value; }
        public double Food { get => food; set => food = value; }
        public double GoldUpkeep { get => goldUpkeep; set => goldUpkeep = value; }
        public double WoodUpkeep { get => woodUpkeep; set => woodUpkeep = value; }
        public double StoneUpkeep { get => stoneUpkeep; set => stoneUpkeep = value; }
        public double FoodUpkeep { get => foodUpkeep; set => foodUpkeep = value; }
        public double Beer { get => beer; set => beer = value; }
        public double Wheat { get => wheat; set => wheat = value; }
        public double BeerUpkeep { get => beerUpkeep; set => beerUpkeep = value; }
        public double WheatUpkeep { get => wheatUpkeep; set => wheatUpkeep = value; }
    }
}
