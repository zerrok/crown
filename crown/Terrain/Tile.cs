using Microsoft.Xna.Framework;
using System;
using TexturePackerMonoGameDefinitions;

namespace crown.Terrain {
    public class Tile {

        String type;
        bool isClear;
        Rectangle rect;

        public Tile(int x, int y, int tileSize) {
            IsClear = true;
            Type = texturePackerSpriteAtlas.Grass1;
            Rect = new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize);
        }

        public string Type { get => type; set => type = value; }
        public bool IsClear { get => isClear; set => isClear = value; }
        public Rectangle Rect { get => rect; set => rect = value; }
    }
}
