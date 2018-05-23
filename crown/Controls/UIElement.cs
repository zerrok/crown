using Microsoft.Xna.Framework;
using TexturePackerLoader;

namespace crown {
    public class UIElement {

        Vector2 pos;
        SpriteFrame spriteFrame;

        public UIElement(Vector2 pos, SpriteFrame spriteFrame) {
            this.pos = pos;
            this.spriteFrame = spriteFrame;
        }

        public Vector2 Pos { get => pos; set => pos = value; }
        public SpriteFrame SpriteFrame { get => spriteFrame; set => spriteFrame = value; }

    }
}
