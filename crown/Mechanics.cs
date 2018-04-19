using System.Collections.Generic;

namespace crown {

    public class Mechanics {
        int gold;
        int wood;
        int stone;
        int population;
        int food;
        float doomClock;
        List<Building> buildings;

        public Mechanics() {
            gold = 0;
            wood = 0;
            stone = 0;
            population = 0;
            food = 0;
            doomClock = 0;
            buildings = new List<Building>();
        }

        public int Gold { get => gold; set => gold = value; }
        public int Wood { get => wood; set => wood = value; }
        public int Stone { get => stone; set => stone = value; }
        public int Population { get => population; set => population = value; }
        public int Food { get => food; set => food = value; }
        public float DoomClock { get => doomClock; set => doomClock = value; }
        public List<Building> Buildings { get => buildings; set => buildings = value; }
    }

}
