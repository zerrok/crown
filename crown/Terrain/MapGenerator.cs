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

      // Dirt
      GeneratorParameters dirtParameters = new GeneratorParameters(20, 850, 30, 40);
      string[] forbiddenForDirt = { TexturePackerMonoGameDefinitions.texturePackerSpriteAtlas.Water1, TexturePackerMonoGameDefinitions.texturePackerSpriteAtlas.Stone1 };
      PutTerrainOnMap(tileMap, dirtParameters, TexturePackerMonoGameDefinitions.texturePackerSpriteAtlas.Dirt1, forbiddenForDirt);

      // Sand
      GeneratorParameters sandParameters = new GeneratorParameters(350, 920, 1, 1);
      string[] forbiddenForSand = { TexturePackerMonoGameDefinitions.texturePackerSpriteAtlas.Water1, TexturePackerMonoGameDefinitions.texturePackerSpriteAtlas.Stone1 };
      PutTerrainOnMap(tileMap, sandParameters, TexturePackerMonoGameDefinitions.texturePackerSpriteAtlas.Sand1, forbiddenForSand);

      return tileMap;
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
      PutTerrainSources(tileMap, sourceTexture, parameters.MinSourceAmount, parameters.MaxSourceAmount);
      GrowTerrainType(tileMap, parameters.Size, parameters.GrowChance, sourceTexture, forbiddenTextures);

      for (int i = 0; i < 2; i++)
        SmoothTerrain(tileMap, sourceTexture);
    }

    private static void PutTerrainSources(Tile[,] tileMap, string texture, int lowBound, int upBound) {
      int xStart = 0;
      int yStart = 0;

      // Put some random starting points for lakes on the map
      for (int i = 0; i < random.Next(lowBound, upBound); i++) {
        xStart = random.Next(0, tileMap.GetUpperBound(0));
        yStart = random.Next(0, tileMap.GetUpperBound(1));
        if (tileMap[xStart, yStart].Type == TexturePackerMonoGameDefinitions.texturePackerSpriteAtlas.Grass1) {
          tileMap[xStart, yStart].IsClear = false;
          tileMap[xStart, yStart].Type = texture;
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
