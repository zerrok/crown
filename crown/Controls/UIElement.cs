using Microsoft.Xna.Framework;
using TexturePackerLoader;

namespace crown {
    public class UIElement {

        public enum ElementType { Resources, MenuSelection, GameSelection }

        Vector2 pos;
        SpriteFrame spriteFrame;
        ElementType type;

        public UIElement(Vector2 pos, SpriteFrame spriteFrame, ElementType type) {
            this.pos = pos;
            this.spriteFrame = spriteFrame;
            this.type = type;
        }

        public Vector2 Pos { get => pos; set => pos = value; }
        public SpriteFrame SpriteFrame { get => spriteFrame; set => spriteFrame = value; }
        public ElementType Type { get => type; set => type = value; }
    }
}
