﻿using Microsoft.Xna.Framework;
using TexturePackerLoader;
using static crown.Game1;

namespace crown.Buildings {
    class Quarry : Building {

        public Quarry(SpriteFrame spriteFrame, Vector2 position, Rectangle rect, Actions type, Costs costs) : base(spriteFrame, position, rect, type, costs) {
        }

        public override void Update() {
            if (BuildingState == 4) {
                // People move in and are ready to work
                Initialize();
                BuildingState = 5;
            } else if (BuildingState == 5) {

                // Update resources
                if (ActionTick > 5) {

                    ActionTick = 0;
                }

                ActionTick++;
            }
        }

        public override void Initialize() {
            base.Initialize();

            Name = "Quarry";
            Description = "Lots of workers are extracting stone \n from the ground. \n Masons are making bricks from the stones.";
        }

        public override void UpdateSprite() {
            if (BuildingState == 0)
                SpriteFrame = buildingTileSheet.Sprite(TexturePackerMonoGameDefinitions.buildingAtlas.House0);
            if (BuildingState == 1)
                SpriteFrame = buildingTileSheet.Sprite(TexturePackerMonoGameDefinitions.buildingAtlas.House1);
            if (BuildingState == 2)
                SpriteFrame = buildingTileSheet.Sprite(TexturePackerMonoGameDefinitions.buildingAtlas.House2);
            if (BuildingState == 3)
                SpriteFrame = buildingTileSheet.Sprite(TexturePackerMonoGameDefinitions.buildingAtlas.Quarry);
        }

    }
}
