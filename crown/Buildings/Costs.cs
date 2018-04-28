namespace crown {
    public class Costs {
        int stone;
        int wood;
        int gold;
        int workers;
        int population;
        int food;

        public Costs(int stone, int wood, int gold, int workers, int population, int food) {
            this.stone = stone;
            this.wood = wood;
            this.gold = gold;
            this.workers = workers;
            this.population = population;
            this.food = food;
        }

        public int Stone {
            get => stone; set => stone = value;
        }
        public int Wood {
            get => wood; set => wood = value;
        }
        public int Gold {
            get => gold; set => gold = value;
        }
        public int Workers {
            get => workers; set => workers = value;
        }
        public int Population {
            get => population; set => population = value;
        }
        public int Food {
            get => food; set => food = value;
        }
    }
}
