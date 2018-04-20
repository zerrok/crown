using Microsoft.Xna.Framework;
using TexturePackerLoader;

namespace crown {
    public class Building {

        // TODO: Statt spriteFrame reingeben, einen Enum anlegen mit allen gebäudetypen und dann entsprechend die werte füllen - factory-artig
        SpriteFrame spriteFrame;
        Vector2 position;
        Rectangle rect;

        public enum Type  {TOWNHALL, HOUSE, WOODCUTTER, FARM, STORAGE };
        Type type;


        public Building(SpriteFrame spriteFrame, Vector2 position, Rectangle rect, Type type) {
            this.spriteFrame = spriteFrame;
            this.position = position;
            this.rect = rect;
            this.type = type;
        }

        public SpriteFrame SpriteFrame {
            get {
                return spriteFrame;
            }

            set {
                spriteFrame = value;
            }
        }

        public Vector2 Position {
            get {
                return position;
            }

            set {
                position = value;
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

        public Type Type1 { get => type; set => type = value; }
    }
}
