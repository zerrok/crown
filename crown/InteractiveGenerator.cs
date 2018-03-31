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
      int treeSources = random.Next(35, 45);

      PlaceSources(tileMap, treeSources);
    }

    private static void PlaceSources(Tile[,] tileMap, int treeSources) {
      // Places the sources
      while (treeSources != 0) {
        int randomX = random.Next(0, tileMap.GetUpperBound(0));
        int randomY = random.Next(0, tileMap.GetUpperBound(1));

        if (tileMap[randomX, randomY].Type.Contains("grass")) {
          Interactive tree = new Interactive(Interactive.IntType.TREE
                                           , "Tree", 3, 1, new Rectangle(randomX * (tileSize / 2)
                                           , randomY * tileSize, 16, 32)
                                           , new Vector2(randomX * tileSize, randomY * tileSize));
          interactives.Add(tree);
          treeSources--;
        }
      }
    }

  }
}
