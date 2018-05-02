using System.Collections.Generic;
using crown.Terrain;
using static crown.Game1;
using Microsoft.Xna.Framework;

namespace crown {
    public class InteractiveGenerator {

        public static void PlaceInteractives(Tile[,] tileMap) {
            // Trees can only grow on grass tiles      
            PlaceTrees(tileMap);
        }

        private static void PlaceTrees(Tile[,] tileMap) {
            // First add sources for the forests
            int treeSources = random.Next(45, 65);
            PlaceSources(tileMap, treeSources);

            // Grow the sources
            GrowForest(tileMap);

            // Add single trees
            treeSources = random.Next(50, 80);
            PlaceSources(tileMap, treeSources);
        }

        private static void PlaceSources(Tile[,] tileMap, int treeSources) {
            // Places the sources
            while (treeSources != 0) {
                int randomX = random.Next(0, tileMap.GetUpperBound(0));
                int randomY = random.Next(0, tileMap.GetUpperBound(1));

                string sprite = getRandomTreeSprite();

                if (tileMap[randomX, randomY].IsClear) {
                    Interactive tree = new Interactive(Interactive.IntType.TREE
                                                     , "Tree"
                                                     , 3
                                                     , 1
                                                     , new Rectangle(randomX * tileSize, randomY * tileSize, 16, 32)
                                                     , new Vector2(randomX * tileSize, randomY * tileSize)
                                                     , interactiveTileSheet.Sprite(sprite));
                    interactives.Add(tree);

                    treeSources--;
                }
            }
        }

        private static string getRandomTreeSprite() {
            int randTree = random.Next(1, 4);
            if (randTree == 1)
                return TexturePackerMonoGameDefinitions.interactiveAtlas.Tree;
            else if (randTree == 2)
                return TexturePackerMonoGameDefinitions.interactiveAtlas.Tree2;
            else if (randTree == 3)
                return TexturePackerMonoGameDefinitions.interactiveAtlas.Tree3;
            else
                return "";
        }

        private static void GrowForest(Tile[,] tileMap) {
            List<Interactive> newInteractives = new List<Interactive>();

            foreach (Interactive inter in interactives) {
                if (inter.Type == Interactive.IntType.TREE) {
                    // Adds a random amount of trees left and right of the source tree
                    int initX = random.Next(-12, -6);
                    int initY = random.Next(-15, -6);

                    int maxX = random.Next(6, 12);
                    int maxY = random.Next(6, 12);

                    for (int x = initX; x < maxX; x++) {
                        for (int y = initY; y < maxY; y++) {
                            int xTile = ((int)inter.Coords.X / tileSize) + x;
                            int yTile = ((int)inter.Coords.Y / tileSize) + y;

                            if (xTile <= 1)
                                break;
                            if (xTile >= tileMap.GetUpperBound(0) - 1)
                                break;
                            if (yTile <= 1)
                                break;
                            if (yTile >= tileMap.GetUpperBound(1) - 1)
                                break;

                            // Only if the surrounding tiles are grass
                            if (SurroundingTilesAreClear(tileMap, xTile, yTile)) {
                                int probability = 15;
                                int minTrees = 5;
                                int maxTrees = 7;
                                // Spawn less trees outwards
                                if (x < initX + 3 || y < initY + 3 || x >= maxX - 3 || y >= maxY - 3) {
                                    probability = 45;
                                    minTrees = 3;
                                    maxTrees = 6;
                                }
                                if (x < initX + 2 || y < initY + 2 || x >= maxX - 2 || y >= maxY - 2) {
                                    probability = 65;
                                    minTrees = 2;
                                    maxTrees = 4;
                                }
                                if (x < initX + 1 || y < initY + 1 || x >= maxX - 1 || y >= maxY - 1) {
                                    probability = 85;
                                    minTrees = 1;
                                    maxTrees = 2;
                                }


                                if (random.Next(0, 100) > probability) {
                                    // To shift the trees by a random amount of pixels
                                    // Do it twice or thrice to spawn more trees in one spot
                                    for (int i = 0; i < random.Next(minTrees, maxTrees); i++) {
                                        int randX = random.Next(-64, 64);
                                        int randY = random.Next(-64, 64);

                                        string sprite = getRandomTreeSprite();

                                        Interactive tree = new Interactive(Interactive.IntType.TREE
                                                                         , "Tree"
                                                                         , 1
                                                                         , 1
                                                                         , new Rectangle(xTile * tileSize + randX, yTile * tileSize + randY, tileSize / 2, tileSize)
                                                                         , new Vector2(xTile * tileSize + randX, yTile * tileSize + randY)
                                                                         , interactiveTileSheet.Sprite(sprite));
                                        newInteractives.Add(tree);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            foreach (Interactive tree in newInteractives)
                interactives.Add(tree);
        }

        private static bool SurroundingTilesAreClear(Tile[,] tileMap, int xTile, int yTile) {
            return tileMap[xTile, yTile + 1].IsClear
                            && tileMap[xTile + 1, yTile].IsClear
                            && tileMap[xTile, yTile - 1].IsClear
                            && tileMap[xTile - 1, yTile].IsClear
                            && tileMap[xTile - 1, yTile - 1].IsClear
                            && tileMap[xTile + 1, yTile + 1].IsClear
                            && tileMap[xTile - 1, yTile + 1].IsClear
                            && tileMap[xTile + 1, yTile - 1].IsClear
                            && tileMap[xTile, yTile + 2].IsClear
                            && tileMap[xTile + 2, yTile].IsClear
                            && tileMap[xTile, yTile - 2].IsClear
                            && tileMap[xTile - 2, yTile].IsClear
                            && tileMap[xTile - 2, yTile - 2].IsClear
                            && tileMap[xTile + 2, yTile + 2].IsClear
                            && tileMap[xTile - 2, yTile + 2].IsClear
                            && tileMap[xTile + 2, yTile - 2].IsClear;
        }
    }
}
