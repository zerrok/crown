using Microsoft.Xna.Framework;
using TexturePackerLoader;

namespace crown {
    public class Button {

        Vector2 pos;
        Rectangle rect;
        ButtonType type;
        SpriteFrame spriteFrame;

        public Vector2 Pos { get => pos; set => pos = value; }
        public Rectangle Rect { get => rect; set => rect = value; }
        public ButtonType Type { get => type; set => type = value; }
        public SpriteFrame SpriteFrame { get => spriteFrame; set => spriteFrame = value; }

        public enum ButtonType {
            Main, House, Townhall, Road, Farm, Woodcutter, Storage, Quarry, Scientist, Start, Quit, Continue
        }


    }
}
