using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static crown.Game1;

namespace crown.Terrain {
    class MapGenerator {
        public Tile[,] GetMap(int xDimension, int yDimension) {
            Tile[,] tileMap = new Tile[xDimension, yDimension];

            InitializeMap(tileMap);
            PutLakesOnMap(tileMap);

            return tileMap;
        }

        private static void PutLakesOnMap(Tile[,] tileMap) {
            PutWaterSources(tileMap);
            GrowWaterBodies(tileMap);
            // TODO: Smooth out the holes in the water
        }

        private static void GrowWaterBodies(Tile[,] tileMap) {
            // Now iterate over the map and add to the size of the water spots
            int lakeSize = random.Next(20, 40);
            for (int count = 0; count < lakeSize; count++) {
                for (int x = 1; x < tileMap.GetUpperBound(0); x++) {
                    for (int y = 1; y < tileMap.GetUpperBound(1); y++) {
                        // But only spots which are adjacent to water
                        if (tileMap[x + 1, y].Type == TexturePackerMonoGameDefinitions.texturePackerSpriteAtlas.Water1 ||
                            tileMap[x, y + 1].Type == TexturePackerMonoGameDefinitions.texturePackerSpriteAtlas.Water1 ||
                            tileMap[x - 1, y].Type == TexturePackerMonoGameDefinitions.texturePackerSpriteAtlas.Water1 ||
                            tileMap[x, y - 1].Type == TexturePackerMonoGameDefinitions.texturePackerSpriteAtlas.Water1) {
                            // Low chance of lake getting bigger
                            if (random.Next(0, 100) > 90) {
                                tileMap[x, y].IsClear = false;
                                tileMap[x, y].Type = TexturePackerMonoGameDefinitions.texturePackerSpriteAtlas.Water1;
                            }
                        }
                    }
                }
            }
        }

        private static void PutWaterSources(Tile[,] tileMap) {
            int xStart = 0;
            int yStart = 0;
            // Put some random starting points for lakes on the map
            for (int i = 0; i < random.Next(10, 20); i++) {
                xStart = random.Next(0, tileMap.GetUpperBound(0));
                yStart = random.Next(0, tileMap.GetUpperBound(1));
                if (tileMap[xStart, yStart].Type == TexturePackerMonoGameDefinitions.texturePackerSpriteAtlas.Grass1) {
                    tileMap[xStart, yStart].IsClear = false;
                    tileMap[xStart, yStart].Type = TexturePackerMonoGameDefinitions.texturePackerSpriteAtlas.Water1;
                }
            }
        }

        private static void InitializeMap(Tile[,] tileMap) {
            // First, fill everything with grassland
            for (int x = 0; x <= tileMap.GetUpperBound(0); x++) {
                for (int y = 0; y <= tileMap.GetUpperBound(1); y++) {
                    tileMap[x, y] = new Tile();
                    // The grassland is clear so you can build on it, and people can walk on it
                    tileMap[x, y].IsClear = true;
                    tileMap[x, y].Type = TexturePackerMonoGameDefinitions.texturePackerSpriteAtlas.Grass1;
                }
            }
        }
    }
}
