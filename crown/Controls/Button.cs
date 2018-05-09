﻿using Microsoft.Xna.Framework;

namespace crown {
    public class Button {

        Vector2 pos;
        Rectangle rect;
        ButtonType type;

        public Vector2 MainPos {
            get => pos;
            set => pos = value;
        }
        public Rectangle MainRect {
            get => rect;
            set => rect = value;
        }
        public ButtonType Type {
            get => type;
            set => type = value;
        }

        public enum ButtonType {
            MAIN, BUTTON_HOUSE, BUTTON_TOWNHALL, BUTTON_FARMLAND, BUTTON_ROAD, BUTTON_FARM, BUTTON_WOODCUTTER, BUTTON_STORAGE
        }


    }
}