using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexturePackerMonoGameDefinitions;

namespace crown.Terrain {
    public class Tile {
        String type;
        bool isClear;
        Rectangle rect;

        public Tile(int x, int y, int tileSize) {            
            IsClear = true;
            Type = texturePackerSpriteAtlas.Grass1;
            rect = new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize);        
        }

        public bool IsClear {
            get {
                return isClear;
            }

            set {
                isClear = value;
            }
        }

        public String Type {
            get {
                return type;
            }

            set {
                type = value;
            }
        }

        public Rectangle Rect {
            get {
                return rect;
            }

            set {
                rect = value;
            }
        }
    }
}
