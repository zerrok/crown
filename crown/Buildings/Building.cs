using Microsoft.Xna.Framework;
using TexturePackerLoader;

namespace crown {
    public class Building {

        // TODO: Statt spriteFrame reingeben, einen Enum anlegen mit allen gebäudetypen und dann entsprechend die werte füllen - factory-artig
        SpriteFrame spriteFrame;
        Vector2 position;
        Rectangle rect;

        public Building(SpriteFrame spriteFrame, Vector2 position, Rectangle rect) {
            this.spriteFrame = spriteFrame;
            this.position = position;
            this.rect = rect;
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
    }
}
