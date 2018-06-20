using Microsoft.Xna.Framework;
using TexturePackerLoader;

namespace crown {
    public class Button {

        Vector2 pos;
        Rectangle rect;
        Actions action;
        SpriteFrame spriteFrame;

        public Vector2 Pos { get => pos; set => pos = value; }
        public Rectangle Rect { get => rect; set => rect = value; }
        public Actions Action { get => action; set => action = value; }
        public SpriteFrame SpriteFrame { get => spriteFrame; set => spriteFrame = value; }

    }
}
