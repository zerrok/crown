using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crown.Terrain {
    class MapGenerator {
        public Tile[,] GetMap(int xDimension, int yDimension) {
            Tile[,] tileMap = new Tile[xDimension, yDimension];

            for (int x = 0; x < xDimension; x++) {
                for (int y = 0; y < yDimension; y++) {
                    tileMap[x, y] = new Tile();
                    if ((x > xDimension / 3 && y > yDimension / 3) || x < xDimension / 5 && y < yDimension / 5) {
                        tileMap[x, y].IsClear = true;
                        tileMap[x, y].Type = TexturePackerMonoGameDefinitions.texturePackerSpriteAtlas.Grass1;
                    } else {
                        tileMap[x, y].IsClear = true;
                        tileMap[x, y].Type = TexturePackerMonoGameDefinitions.texturePackerSpriteAtlas.Stone1;
                    }
                }
            }
            return tileMap;
        }
    }
}
