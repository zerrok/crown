using System.Collections.Generic;
using crown.Terrain;
using static crown.Game1;
using Microsoft.Xna.Framework;
using System;
using System.Linq;

namespace crown {
  public class InteractiveGenerator {

    public static void PlaceInteractives(Tile[,] tileMap) {
      // Trees can only grow on grass tiles      
      PlaceTrees(tileMap);
    }

    private static void PlaceTrees(Tile[,] tileMap) {
      // First add sources for the forests
      int treeSources = random.Next(35, 45);
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

        if (tileMap[randomX, randomY].Type.Contains("grass")) {
          Interactive tree = new Interactive(Interactive.IntType.TREE
                                           , "Tree"
                                           , 3
                                           , 1
                                           , new Rectangle(randomX * tileSize, randomY * tileSize, 16, 32)
                                           , new Vector2(randomX * tileSize, randomY * tileSize));
          interactives.Add(tree);

          // Make tile unbuildable
          tileMap[randomX, randomY].IsClear = false;
          treeSources--;
        }
      }
    }

    private static void GrowForest(Tile[,] tileMap) {
      List<Interactive> newInteractives = new List<Interactive>();

      foreach (Interactive inter in interactives) {
        if (inter.Type == Interactive.IntType.TREE) {
          // Adds a random amount of trees left and right of the source tree
          for (int x = random.Next(-10, -2); x < random.Next(2, 10); x++)
            for (int y = random.Next(-10, -2); y < random.Next(2, 10); y++) {
              int xTile = ((int)inter.Coords.X / tileSize) + x;
              int yTile = ((int)inter.Coords.Y / tileSize) + y;

              if (xTile < 1)
                xTile = 1;
              if (xTile >= tileMap.GetUpperBound(0))
                xTile = tileMap.GetUpperBound(0) - 1;

              if (yTile < 1)
                yTile = 1;
              if (yTile >= tileMap.GetUpperBound(1))
                yTile = tileMap.GetUpperBound(1) - 1;

              // Only if the surrounding tiles are grass
              if (tileMap[xTile, yTile + 1].Type.Contains("grass")
                && tileMap[xTile + 1, yTile].Type.Contains("grass")
                && tileMap[xTile, yTile - 1].Type.Contains("grass")
                && tileMap[xTile - 1, yTile].Type.Contains("grass")) {
                if (random.Next(0, 100) > 35) {
                  // To shift the trees by a random amount of pixels
                  // Do it twice or thrice to spawn more trees in one spot
                  for (int i = 0; i < random.Next(2, 3); i++) {
                    int randX = random.Next(-16, 16);
                    int randY = random.Next(-32, 32);

                    Interactive tree = new Interactive(Interactive.IntType.TREE
                                                     , "Tree"
                                                     , 3
                                                     , 1
                                                     , new Rectangle(xTile * tileSize + randX, yTile * tileSize + randY, tileSize / 2, tileSize)
                                                     , new Vector2(xTile * tileSize + randX, yTile * tileSize + randY));
                    newInteractives.Add(tree);
                    tileMap[xTile, yTile].IsClear = false;
                  }
                }
              }
            }
        }
      }

      foreach (Interactive tree in newInteractives)
        interactives.Add(tree);
    }

  }
}
