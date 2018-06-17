using Microsoft.Xna.Framework;
using TexturePackerLoader;

namespace crown {
    public class UIElement {

        public enum ElementType { Resources, MenuSelection, GameSelection }
        public enum Resource { Details, Population, Gold, Wood, Workers, Stone, Food }

        Vector2 pos;
        SpriteFrame spriteFrame;
        ElementType type;
        Resource resource;
        string textTop;
        string textBottom;

        public UIElement(Vector2 pos, SpriteFrame spriteFrame, ElementType type, Resource resource) {
            this.pos = pos;
            this.spriteFrame = spriteFrame;
            this.type = type;
            this.resource = resource;
            textTop = "";
            TextBottom = "";
        }

        public Vector2 Pos { get => pos; set => pos = value; }
        public SpriteFrame SpriteFrame { get => spriteFrame; set => spriteFrame = value; }
        public ElementType Type { get => type; set => type = value; }
        public string TextTop { get => textTop; set => textTop = value; }
        public string TextBottom { get => textBottom; set => textBottom = value; }
        public Resource Resource1 { get => resource; set => resource = value; }
    }
}
