using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using TexturePackerLoader;

namespace crown.Terrain
{
    public class Road
    {

        Vector2 coords;
        Rectangle rect;
        SpriteFrame spriteFrame;

        public Road(Vector2 coords, Rectangle rect)
        {
            this.coords = coords;
            this.rect = rect;
        }

        public Vector2 Coords {
            get => coords;
            set => coords = value;
        }
        public Rectangle Rect {
            get => rect;
            set => rect = value;
        }
        public SpriteFrame SpriteFrame {
            get => spriteFrame;
            set => spriteFrame = value;
        }
    }
}
