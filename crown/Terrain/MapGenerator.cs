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

      // Stone
      GeneratorParameters stoneParameters = new GeneratorParameters(300, 950, 5, 10);
      string[] forbiddenForStone = { TexturePackerMonoGameDefinitions.texturePackerSpriteAtlas.Stone1 };
      PutTerrainOnMap(tileMap, stoneParameters, TexturePackerMonoGameDefinitions.texturePackerSpriteAtlas.Stone1, forbiddenForStone);

      // Water
      GeneratorParameters waterParameters = new GeneratorParameters(120, 910, 15, 25);
      string[] forbiddenForWater = { TexturePackerMonoGameDefinitions.texturePackerSpriteAtlas.Stone1 };
      PutTerrainOnMap(tileMap, waterParameters, TexturePackerMonoGameDefinitions.texturePackerSpriteAtlas.Water1, forbiddenForWater);

      // Sand
      GeneratorParameters sandParameters = new GeneratorParameters(350, 920, 1, 1);
      string[] forbiddenForSand = { TexturePackerMonoGameDefinitions.texturePackerSpriteAtlas.Water1, TexturePackerMonoGameDefinitions.texturePackerSpriteAtlas.Stone1 };
      PutTerrainOnMap(tileMap, sandParameters, TexturePackerMonoGameDefinitions.texturePackerSpriteAtlas.Sand1, forbiddenForSand);

      SetTerrainBorders(tileMap);

      return tileMap;
    }

    private void SetTerrainBorders(Tile[,] tileMap) {
      for (int x = 0; x < tileMap.GetUpperBound(0); x++) {
        for (int y = 0; y < tileMap.GetUpperBound(1); y++) {
          AddSandBorders(tileMap, x, y);
          AddGrassBorders(tileMap, x, y);
          AddWaterBorders(tileMap, x, y);
        }
      }
    }

    private static void AddWaterBorders(Tile[,] tileMap, int x, int y) {
      // Set water borders
      if (tileMap[x, y].Type == TexturePackerMonoGameDefinitions.texturePackerSpriteAtlas.Water1) {
        // Grass borders
        if (tileMap[x + 1, y].Type == TexturePackerMonoGameDefinitions.texturePackerSpriteAtlas.Grass1) {
          tileMap[x, y].Type = TexturePackerMonoGameDefinitions.texturePackerSpriteAtlas.WaterGrassRight;
        }
        if (x != 0 && tileMap[x - 1, y].Type == TexturePackerMonoGameDefinitions.texturePackerSpriteAtlas.Grass1) {
          tileMap[x, y].Type = TexturePackerMonoGameDefinitions.texturePackerSpriteAtlas.WaterGrassLeft;
        }
        if (y != 0 && tileMap[x, y - 1].Type.Contains("grass")) {
          tileMap[x, y].Type = TexturePackerMonoGameDefinitions.texturePackerSpriteAtlas.WaterGrassTop;
        }

        // Sand borders
        if (y != 0 && tileMap[x, y - 1].Type.Contains("sand")) {
          tileMap[x, y].Type = TexturePackerMonoGameDefinitions.texturePackerSpriteAtlas.WaterSandTop;
        }

        // Stone borders
        if (y != 0 && tileMap[x, y - 1].Type.Contains("stone")) {
          tileMap[x, y].Type = TexturePackerMonoGameDefinitions.texturePackerSpriteAtlas.WaterStoneTop;
        }
      }
    }

    private static void AddGrassBorders(Tile[,] tileMap, int x, int y) {
      if (tileMap[x, y].Type == TexturePackerMonoGameDefinitions.texturePackerSpriteAtlas.Grass1) {
        // Stone on 1 side
        const string stone1 = TexturePackerMonoGameDefinitions.texturePackerSpriteAtlas.Stone1;
        if (tileMap[x + 1, y].Type == stone1) {
          tileMap[x, y].Type = TexturePackerMonoGameDefinitions.texturePackerSpriteAtlas.GrassStoneRight;
        }
        if (x != 0 && tileMap[x - 1, y].Type == stone1) {
          tileMap[x, y].Type = TexturePackerMonoGameDefinitions.texturePackerSpriteAtlas.GrassStoneLeft;
        }
        if (y != 0 && tileMap[x, y - 1].Type == stone1) {
          tileMap[x, y].Type = TexturePackerMonoGameDefinitions.texturePackerSpriteAtlas.GrassStoneTop;
        }

        // Stone on 2 sides
        if (tileMap[x + 1, y].Type == stone1 && y != 0 && tileMap[x, y - 1].Type == stone1) {
          tileMap[x, y].Type = TexturePackerMonoGameDefinitions.texturePackerSpriteAtlas.GrassStoneTopRight;
        }
        if (x != 0 && tileMap[x - 1, y].Type == stone1 && y != 0 && tileMap[x, y - 1].Type == stone1) {
          tileMap[x, y].Type = TexturePackerMonoGameDefinitions.texturePackerSpriteAtlas.GrassStoneTopLeft;
        }

        // Stone on 3 sides
        if (x != 0 && tileMap[x - 1, y].Type == stone1 && tileMap[x + 1, y].Type == stone1 && y != 0 && tileMap[x, y - 1].Type == stone1) {
          tileMap[x, y].Type = TexturePackerMonoGameDefinitions.texturePackerSpriteAtlas.GrassStoneTopRightLeft;
        }
      }
    }

    public static void AddDirtBorders(Tile[,] tileMap) {
      for (int x = 0; x < tileMap.GetUpperBound(0); x++) {
        for (int y = 0; y < tileMap.GetUpperBound(1); y++) {
          if (tileMap[x, y].Type.Contains("dirt")) {
            const string grass1 = TexturePackerMonoGameDefinitions.texturePackerSpriteAtlas.Grass1;
            // Grass borders 1 side
            if (tileMap[x + 1, y].Type == grass1) {
              tileMap[x, y].Type = TexturePackerMonoGameDefinitions.texturePackerSpriteAtlas.DirtGrassRight;
            }
            if (x != 0 && tileMap[x - 1, y].Type == grass1) {
              tileMap[x, y].Type = TexturePackerMonoGameDefinitions.texturePackerSpriteAtlas.DirtGrassLeft;
            }
            if (y != 0 && tileMap[x, y - 1].Type == grass1) {
              tileMap[x, y].Type = TexturePackerMonoGameDefinitions.texturePackerSpriteAtlas.DirtGrassTop;
            }
            if (tileMap[x, y + 1].Type == grass1) {
              tileMap[x, y].Type = TexturePackerMonoGameDefinitions.texturePackerSpriteAtlas.DirtGrassDown;
            }

            // Grass on 2 sides
            if (tileMap[x + 1, y].Type == grass1 && tileMap[x, y + 1].Type == grass1) {
              tileMap[x, y].Type = TexturePackerMonoGameDefinitions.texturePackerSpriteAtlas.DirtGrassDownRight;
            }
            if (tileMap[x + 1, y].Type == grass1 && y != 0 && tileMap[x, y - 1].Type == grass1) {
              tileMap[x, y].Type = TexturePackerMonoGameDefinitions.texturePackerSpriteAtlas.DirtGrassTopRight;
            }
            if (x != 0 && tileMap[x - 1, y].Type == grass1 && tileMap[x, y + 1].Type == grass1) {
              tileMap[x, y].Type = TexturePackerMonoGameDefinitions.texturePackerSpriteAtlas.DirtGrassDownLeft;
            }
            if (x != 0 && tileMap[x - 1, y].Type == grass1 && y != 0 && tileMap[x, y - 1].Type == grass1) {
              tileMap[x, y].Type = TexturePackerMonoGameDefinitions.texturePackerSpriteAtlas.DirtGrassTopLeft;
            }
            if (x != 0 && tileMap[x - 1, y].Type == grass1 && tileMap[x + 1, y].Type == grass1) {
              tileMap[x, y].Type = TexturePackerMonoGameDefinitions.texturePackerSpriteAtlas.DirtGrassLeftRight;
            }
            if (tileMap[x, y + 1].Type == grass1 && y != 0 && tileMap[x, y - 1].Type == grass1) {
              tileMap[x, y].Type = TexturePackerMonoGameDefinitions.texturePackerSpriteAtlas.DirtGrassTopDown;
            }

            // Grass on 3 sides
            if (x != 0 && tileMap[x - 1, y].Type == grass1 && tileMap[x + 1, y].Type == grass1 && tileMap[x, y + 1].Type == grass1) {
              tileMap[x, y].Type = TexturePackerMonoGameDefinitions.texturePackerSpriteAtlas.DirtGrassDownLeftRight;
            }
            if (x != 0 && tileMap[x - 1, y].Type == grass1 && tileMap[x + 1, y].Type == grass1 && y != 0 && tileMap[x, y - 1].Type == grass1) {
              tileMap[x, y].Type = TexturePackerMonoGameDefinitions.texturePackerSpriteAtlas.DirtGrassTopLeftRight;
            }
            if (x != 0 && tileMap[x - 1, y].Type == grass1 && tileMap[x, y + 1].Type == grass1 && y != 0 && tileMap[x, y - 1].Type == grass1) {
              tileMap[x, y].Type = TexturePackerMonoGameDefinitions.texturePackerSpriteAtlas.DirtGrassTopDownLeft;
            }
            if (tileMap[x, y + 1].Type == grass1 && y != 0 && tileMap[x, y - 1].Type == grass1 && tileMap[x + 1, y].Type == grass1) {
              tileMap[x, y].Type = TexturePackerMonoGameDefinitions.texturePackerSpriteAtlas.DirtGrassTopDownRight;
            }

            if (x != 0 && tileMap[x - 1, y].Type == grass1 && y != 0 && tileMap[x, y - 1].Type == grass1 && tileMap[x, y + 1].Type == grass1 && tileMap[x + 1, y].Type == grass1) {
              tileMap[x, y].Type = TexturePackerMonoGameDefinitions.texturePackerSpriteAtlas.DirtGrassAll;
            }

            // Dirt on all sides
            if (x != 0 && tileMap[x - 1, y].Type.Contains("dirt") && tileMap[x + 1, y].Type.Contains("dirt") && y != 0 && tileMap[x, y - 1].Type.Contains("dirt") && tileMap[x, y + 1].Type.Contains("dirt")) {
              tileMap[x, y].Type = TexturePackerMonoGameDefinitions.texturePackerSpriteAtlas.Dirt1;
            }
          }
        }
      }
    }

    private static void AddSandBorders(Tile[,] tileMap, int x, int y) {
      // Set sand borders
      if (tileMap[x, y].Type == TexturePackerMonoGameDefinitions.texturePackerSpriteAtlas.Sand1) {
        const string grass1 = TexturePackerMonoGameDefinitions.texturePackerSpriteAtlas.Grass1;

        // Grass borders 1 side
        if (tileMap[x + 1, y].Type == grass1) {
          tileMap[x, y].Type = TexturePackerMonoGameDefinitions.texturePackerSpriteAtlas.SandGrassRight;
        }
        if (x != 0 && tileMap[x - 1, y].Type == grass1) {
          tileMap[x, y].Type = TexturePackerMonoGameDefinitions.texturePackerSpriteAtlas.SandGrassLeft;
        }
        if (y != 0 && tileMap[x, y - 1].Type == grass1) {
          tileMap[x, y].Type = TexturePackerMonoGameDefinitions.texturePackerSpriteAtlas.SandGrassTop;
        }
        if (tileMap[x, y + 1].Type == grass1) {
          tileMap[x, y].Type = TexturePackerMonoGameDefinitions.texturePackerSpriteAtlas.SandGrassDown;
        }

        // Grass on 2 sides
        if (tileMap[x + 1, y].Type == grass1 && tileMap[x, y + 1].Type == grass1) {
          tileMap[x, y].Type = TexturePackerMonoGameDefinitions.texturePackerSpriteAtlas.SandGrassDownRight;
        }
        if (tileMap[x + 1, y].Type == grass1 && y != 0 && tileMap[x, y - 1].Type == grass1) {
          tileMap[x, y].Type = TexturePackerMonoGameDefinitions.texturePackerSpriteAtlas.SandGrassTopRight;
        }
        if (x != 0 && tileMap[x - 1, y].Type == grass1 && tileMap[x, y + 1].Type == grass1) {
          tileMap[x, y].Type = TexturePackerMonoGameDefinitions.texturePackerSpriteAtlas.SandGrassDownLeft;
        }
        if (x != 0 && tileMap[x - 1, y].Type == grass1 && y != 0 && tileMap[x, y - 1].Type == grass1) {
          tileMap[x, y].Type = TexturePackerMonoGameDefinitions.texturePackerSpriteAtlas.SandGrassTopLeft;
        }

        // Grass on 3 sides
        if (x != 0 && tileMap[x - 1, y].Type == grass1 && tileMap[x + 1, y].Type == grass1 && tileMap[x, y + 1].Type == grass1) {
          tileMap[x, y].Type = TexturePackerMonoGameDefinitions.texturePackerSpriteAtlas.SandGrassDownLeftRight;
        }
        if (x != 0 && tileMap[x - 1, y].Type == grass1 && tileMap[x + 1, y].Type == grass1 && y != 0 && tileMap[x, y - 1].Type == grass1) {
          tileMap[x, y].Type = TexturePackerMonoGameDefinitions.texturePackerSpriteAtlas.SandGrassTopLeftRight;
        }
        if (x != 0 && tileMap[x - 1, y].Type == grass1 && tileMap[x, y + 1].Type == grass1 && y != 0 && tileMap[x, y - 1].Type == grass1) {
          tileMap[x, y].Type = TexturePackerMonoGameDefinitions.texturePackerSpriteAtlas.SandGrassTopDownLeft;
        }
        if (x != 0 && tileMap[x - 1, y].Type == grass1 && y != 0 && tileMap[x, y - 1].Type == grass1 && tileMap[x + 1, y].Type == grass1) {
          tileMap[x, y].Type = TexturePackerMonoGameDefinitions.texturePackerSpriteAtlas.SandGrassTopDownRight;
        }

        if (x != 0 && tileMap[x - 1, y].Type == grass1 && y != 0 && tileMap[x, y - 1].Type == grass1 && tileMap[x, y + 1].Type == grass1 && tileMap[x + 1, y].Type == grass1) {
          tileMap[x, y].Type = TexturePackerMonoGameDefinitions.texturePackerSpriteAtlas.SandGrassAll;
        }

        // Stone on 1 side
        const string stone1 = TexturePackerMonoGameDefinitions.texturePackerSpriteAtlas.Stone1;
        if (tileMap[x + 1, y].Type == stone1) {
          tileMap[x, y].Type = TexturePackerMonoGameDefinitions.texturePackerSpriteAtlas.SandStoneRight;
        }
        if (x != 0 && tileMap[x - 1, y].Type == stone1) {
          tileMap[x, y].Type = TexturePackerMonoGameDefinitions.texturePackerSpriteAtlas.SandStoneLeft;
        }
        if (y != 0 && tileMap[x, y - 1].Type == stone1) {
          tileMap[x, y].Type = TexturePackerMonoGameDefinitions.texturePackerSpriteAtlas.SandStoneTop;
        }

        // Stone on 2 sides
        if (tileMap[x + 1, y].Type == stone1 && y != 0 && tileMap[x, y - 1].Type == stone1) {
          tileMap[x, y].Type = TexturePackerMonoGameDefinitions.texturePackerSpriteAtlas.SandStoneTopRight;
        }
        if (x != 0 && tileMap[x - 1, y].Type == stone1 && y != 0 && tileMap[x, y - 1].Type == stone1) {
          tileMap[x, y].Type = TexturePackerMonoGameDefinitions.texturePackerSpriteAtlas.SandStoneTopLeft;
        }

        // Stone on 3 sides
        if (x != 0 && tileMap[x - 1, y].Type == stone1 && tileMap[x + 1, y].Type == stone1 && y != 0 && tileMap[x, y - 1].Type == stone1) {
          tileMap[x, y].Type = TexturePackerMonoGameDefinitions.texturePackerSpriteAtlas.SandStoneTopRightLeft;
        }

      }
    }

    private static void InitializeMap(Tile[,] tileMap) {
      // First, fill everything with grassland
      for (int x = 0; x <= tileMap.GetUpperBound(0); x++) {
        for (int y = 0; y <= tileMap.GetUpperBound(1); y++) {
          tileMap[x, y] = new Tile(x, y, tileSize);
        }
      }
    }

    private static void PutTerrainOnMap(Tile[,] tileMap, GeneratorParameters parameters, string sourceTexture, string[] forbiddenTextures) {
      PutTerrainSources(tileMap, sourceTexture, parameters.MinSourceAmount, parameters.MaxSourceAmount, forbiddenTextures);
      GrowTerrainType(tileMap, parameters.Size, parameters.GrowChance, sourceTexture, forbiddenTextures);

      for (int i = 0; i < 2; i++)
        SmoothTerrain(tileMap, sourceTexture);
    }

    private static void PutTerrainSources(Tile[,] tileMap, string texture, int lowBound, int upBound, string[] forbiddenTextures) {
      int xStart = 0;
      int yStart = 0;

      // Put some random starting points for sources on the map
      for (int i = 0; i < random.Next(lowBound, upBound); i++) {
        xStart = random.Next(0, tileMap.GetUpperBound(0));
        yStart = random.Next(0, tileMap.GetUpperBound(1));
        if (tileMap[xStart, yStart].Type == TexturePackerMonoGameDefinitions.texturePackerSpriteAtlas.Grass1 && !forbiddenTextures.Contains(tileMap[xStart, yStart].Type)) {
          tileMap[xStart, yStart].IsClear = false;
          tileMap[xStart, yStart].Type = texture;
        } else {
          i--;
        }
      }
    }

    private static void GrowTerrainType(Tile[,] tileMap, int lakeSize, int chance, string growingTexture, string[] forbiddenTextures) {
      // Now iterate over the map and add to the size of the terrain spots
      for (int count = 0; count < lakeSize; count++) {
        for (int x = 0; x < tileMap.GetUpperBound(0); x++) {
          for (int y = 0; y < tileMap.GetUpperBound(1); y++) {
            // But only spots which are adjacent to same type
            int adjacent = 0;
            if (tileMap[x + 1, y].Type == growingTexture)
              adjacent++;
            if (tileMap[x, y + 1].Type == growingTexture)
              adjacent++;
            if (x != 0 && tileMap[x - 1, y].Type == growingTexture)
              adjacent++;
            if (y != 0 && tileMap[x, y - 1].Type == growingTexture)
              adjacent++;

            if (adjacent > 0) {
              // Low chance of terrain type getting bigger, when not on forbidden texture
              if (random.Next(0, 1000) > chance && !forbiddenTextures.Contains(tileMap[x, y].Type)) {
                tileMap[x, y].IsClear = false;
                tileMap[x, y].Type = growingTexture;
              }
            }

          }
        }
      }
    }

    private static void SmoothTerrain(Tile[,] tileMap, string texture) {
      // Fill holes in the water so it does look better
      for (int x = 0; x < tileMap.GetUpperBound(0); x++) {
        for (int y = 0; y < tileMap.GetUpperBound(1); y++) {
          // But only spots which are adjacent to 3 water spaces
          int sum = 0;
          if (tileMap[x + 1, y].Type == texture)
            sum++;
          if (tileMap[x, y + 1].Type == texture)
            sum++;
          if (x != 0 && tileMap[x - 1, y].Type == texture)
            sum++;
          if (y != 0 && tileMap[x, y - 1].Type == texture)
            sum++;

          if (sum >= 3) {
            tileMap[x, y].IsClear = false;
            tileMap[x, y].Type = texture;
          }
          // The same for lonely bits of terrain sticking out - turn em into grass again
          sum = 0;
          if (tileMap[x + 1, y].Type == TexturePackerMonoGameDefinitions.texturePackerSpriteAtlas.Grass1)
            sum++;
          if (tileMap[x, y + 1].Type == TexturePackerMonoGameDefinitions.texturePackerSpriteAtlas.Grass1)
            sum++;
          if (x != 0 && tileMap[x - 1, y].Type == TexturePackerMonoGameDefinitions.texturePackerSpriteAtlas.Grass1)
            sum++;
          if (y != 0 && tileMap[x, y - 1].Type == TexturePackerMonoGameDefinitions.texturePackerSpriteAtlas.Grass1)
            sum++;

          if (sum >= 3) {
            tileMap[x, y].IsClear = false;
            tileMap[x, y].Type = TexturePackerMonoGameDefinitions.texturePackerSpriteAtlas.Grass1;
          }

        }
      }
    }

  }
}
