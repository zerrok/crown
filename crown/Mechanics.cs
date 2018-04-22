﻿using System.Collections.Generic;

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
            gold = 500;
            wood = 0;
            stone = 0;
            population = 0;
            food = 100;
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

        public void UpdateMechanics() {
            doomClock += 0.02f;

            if (DoomClock > 1f) {
                DoomClock = 0f;
            }
        }
    }



}
