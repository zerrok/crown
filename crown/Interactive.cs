﻿using Microsoft.Xna.Framework;
using TexturePackerLoader;

namespace crown {
    public class Interactive {

        public enum IntType {
            TREE
        }

        IntType type;
        string text;
        int health;
        int worth;
        Rectangle rect;
        Vector2 coords;
        SpriteFrame spriteFrame;
        bool isSelected;
        int respawnCounter;

        public IntType Type { get => type; set => type = value; }
        public string Text { get => text; set => text = value; }
        public int Health { get => health; set => health = value; }
        public int Worth { get => worth; set => worth = value; }
        public Rectangle Rect { get => rect; set => rect = value; }
        public Vector2 Coords { get => coords; set => coords = value; }
        public SpriteFrame SpriteFrame { get => spriteFrame; set => spriteFrame = value; }
        public bool IsSelected { get => isSelected; set => isSelected = value; }
        public int RespawnCounter { get => respawnCounter; set => respawnCounter = value; }

        public Interactive(IntType type, string text, int health, int worth, Rectangle rect, Vector2 coords, SpriteFrame spriteFrame) {
            Type = type;
            Text = text;
            Health = health;
            Worth = worth;
            Rect = rect;
            Coords = coords;
            IsSelected = false;
            SpriteFrame = spriteFrame;
            respawnCounter = 0;
        }

        public void RespawnInteractive() {
            if (Health <= 0) {
                if (respawnCounter > 25) {
                    respawnCounter = 0;
                    Health = 1;
                }
                respawnCounter++;
            }
        }

    }
}
