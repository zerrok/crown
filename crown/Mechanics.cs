﻿using System.Collections.Generic;
using static crown.Game1;
using Microsoft.Xna.Framework;
using crown.Terrain;

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
            gold = 500;
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

            CitizenUpdates();

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

        private void CitizenUpdates() {
            if (citizens != null) {
                Vector2 position = new Vector2();
                // Always spawn as much people as there is population
                if (citizens.Count < population)
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



