﻿using System.Collections.Generic;
using crown.Terrain;
using static crown.Game1;
using Microsoft.Xna.Framework;

namespace crown {
    public class InteractiveGenerator {

        public static void PlaceInteractives(Tile[,] tileMap) {
            PlaceTrees(tileMap);
            PlaceStones(tileMap);
        }

        private static void PlaceTrees(Tile[,] tileMap) {
            // First add sources for the forests
            int treeSources = random.Next(100, 120);
            string sprite = getRandomTreeSprite();
            PlaceSources(tileMap, treeSources, 64, 128, "Tree", Interactive.IntType.TREE, sprite);

            // Grow the sources
            GrowForest(tileMap);

            // Add single trees
            treeSources = random.Next(120, 150);
            PlaceSources(tileMap, treeSources, 64, 128, "Tree", Interactive.IntType.TREE, sprite);
        }

        private static void PlaceStones(Tile[,] tileMap) {
            // First add sources for the forests
            int stoneSources = random.Next(100, 120);
            string sprite = TexturePackerMonoGameDefinitions.interactiveAtlas.Stones;
            PlaceSources(tileMap, stoneSources, 128, 128, "Some stones", Interactive.IntType.STONE, sprite);
        }

        private static void PlaceSources(Tile[,] tileMap, int treeSources, int sizeX, int sizeY, string text, Interactive.IntType type, string sprite) {
            while (treeSources != 0) {
                int randomX = random.Next(0, tileMap.GetUpperBound(0));
                int randomY = random.Next(0, tileMap.GetUpperBound(1));

                if (tileMap[randomX, randomY].IsClear && !tileMap[randomX, randomY].Type.Contains("sand")) {
                    Rectangle rect = new Rectangle(randomX * tileSize, randomY * tileSize, sizeX, sizeY);

                    bool isAllowed = true;
                    if (type == Interactive.IntType.STONE) {
                        foreach (Interactive interactive in interactives) {
                            // Stones may not intersect trees
                            if (interactive.Type == Interactive.IntType.TREE && rect.Intersects(interactive.Rect)) {
                                isAllowed = false;
                                break;
                            }
                        }
                    }
                    if (isAllowed) {
                        // Stones will make the tile under it unbuildable (except for quarries)
                        if (type == Interactive.IntType.STONE)
                            tileMap[randomX, randomY].IsClear = false;

                        Interactive inter = new Interactive(type
                                                         , text
                                                         , 1
                                                         , rect
                                                         , new Vector2(randomX * tileSize, randomY * tileSize)
                                                         , interactiveTileSheet.Sprite(sprite));
                        interactives.Add(inter);

                        treeSources--;
                    }
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
                    int initX = random.Next(-16, -8);
                    int initY = random.Next(-16, -8);

                    int maxX = random.Next(8, 16);
                    int maxY = random.Next(8, 16);

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
                            if (SurroundingTilesClearAndNoSand(tileMap, xTile, yTile)) {
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

        private static bool SurroundingTilesClearAndNoSand(Tile[,] tileMap, int xTile, int yTile) {
            return tileMap[xTile, yTile + 1].IsClear && !tileMap[xTile, yTile + 1].Type.Contains("sand")
                            && tileMap[xTile + 1, yTile].IsClear && !tileMap[xTile + 1, yTile].Type.Contains("sand")
                            && tileMap[xTile, yTile - 1].IsClear && !tileMap[xTile, yTile - 1].Type.Contains("sand")
                            && tileMap[xTile - 1, yTile].IsClear && !tileMap[xTile - 1, yTile].Type.Contains("sand")
                            && tileMap[xTile - 1, yTile - 1].IsClear && !tileMap[xTile - 1, yTile - 1].Type.Contains("sand")
                            && tileMap[xTile + 1, yTile + 1].IsClear && !tileMap[xTile + 1, yTile + 1].Type.Contains("sand")
                            && tileMap[xTile - 1, yTile + 1].IsClear && !tileMap[xTile - 1, yTile + 1].Type.Contains("sand")
                            && tileMap[xTile + 1, yTile - 1].IsClear && !tileMap[xTile + 1, yTile - 1].Type.Contains("sand");
        }
    }
}
