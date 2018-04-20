using Microsoft.Xna.Framework;

namespace crown {
    public class Menu {

        Vector2 mainPos;
        Rectangle mainRect;
        MenuType type;

        public Vector2 MainPos {
            get => mainPos;
            set => mainPos = value;
        }
        public Rectangle MainRect {
            get => mainRect;
            set => mainRect = value;
        }
        public MenuType Type {
            get => type;
            set => type = value;
        }

        public enum MenuType {
            MAIN, BUTTON_HOUSE, BUTTON_TOWNHALL, BUTTON_FARMLAND, BUTTON_ROAD, BUTTON_FARM, BUTTON_WOODCUTTER, BUTTON_STORAGE
        }


    }
}
