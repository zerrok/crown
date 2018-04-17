using System.Collections.Generic;
using crown.Terrain;
using static crown.Game1;
using Microsoft.Xna.Framework;
using System;
using System.Linq;

namespace crown
{
    public class InteractiveGenerator
    {

        public static void PlaceInteractives(Tile[,] tileMap)
        {
            // Trees can only grow on grass tiles      
            PlaceTrees(tileMap);
        }

        private static void PlaceTrees(Tile[,] tileMap)
        {
            // First add sources for the forests
            int treeSources = random.Next(35, 45);
            PlaceSources(tileMap, treeSources);

            // Grow the sources
            GrowForest(tileMap);

            // Add single trees
            treeSources = random.Next(50, 80);
            PlaceSources(tileMap, treeSources);
        }

        private static void PlaceSources(Tile[,] tileMap, int treeSources)
        {
            // Places the sources
            while (treeSources != 0) {
                int randomX = random.Next(0, tileMap.GetUpperBound(0));
                int randomY = random.Next(0, tileMap.GetUpperBound(1));

                if (tileMap[randomX, randomY].IsClear) {
                    Interactive tree = new Interactive(Interactive.IntType.TREE
                                                     , "Tree"
                                                     , 3
                                                     , 1
                                                     , new Rectangle(randomX * tileSize, randomY * tileSize, 16, 32)
                                                     , new Vector2(randomX * tileSize, randomY * tileSize));
                    interactives.Add(tree);

                    treeSources--;
                }
            }
        }

        private static void GrowForest(Tile[,] tileMap)
        {
            List<Interactive> newInteractives = new List<Interactive>();

            foreach (Interactive inter in interactives) {
                if (inter.Type == Interactive.IntType.TREE) {
                    // TODO: An den Ecken weniger Bäume wachsen lassen
                    // Adds a random amount of trees left and right of the source tree
                    for (int x = random.Next(-10, -2); x < random.Next(4, 15); x++)
                        for (int y = random.Next(-10, -2); y < random.Next(4, 15); y++) {
                            int xTile = ((int)inter.Coords.X / tileSize) + x;
                            int yTile = ((int)inter.Coords.Y / tileSize) + y;

                            if (xTile < 2)
                                xTile = 2;
                            if (xTile >= tileMap.GetUpperBound(0) - 1)
                                xTile = tileMap.GetUpperBound(0) - 2;

                            if (yTile < 2)
                                yTile = 2;
                            if (yTile >= tileMap.GetUpperBound(1) - 1)
                                yTile = tileMap.GetUpperBound(1) - 2;

                            // Only if the surrounding tiles are grass
                            if (SurroundingTilesAreClear(tileMap, xTile, yTile)) {
                                if (random.Next(0, 100) > 35) {
                                    // To shift the trees by a random amount of pixels
                                    // Do it twice or thrice to spawn more trees in one spot
                                    for (int i = 0; i < random.Next(1, 10); i++) {
                                        int randX = random.Next(-16, 16);
                                        int randY = random.Next(-32, 32);

                                        Interactive tree = new Interactive(Interactive.IntType.TREE
                                                                         , "Tree"
                                                                         , 3
                                                                         , 1
                                                                         , new Rectangle(xTile * tileSize + randX, yTile * tileSize + randY, tileSize / 2, tileSize)
                                                                         , new Vector2(xTile * tileSize + randX, yTile * tileSize + randY));
                                        newInteractives.Add(tree);
                                    }
                                }
                            }
                        }
                }
            }

            foreach (Interactive tree in newInteractives)
                interactives.Add(tree);
        }

        private static bool SurroundingTilesAreClear(Tile[,] tileMap, int xTile, int yTile)
        {
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
