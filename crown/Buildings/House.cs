using Microsoft.Xna.Framework;
using TexturePackerLoader;
using static crown.Game1;

namespace crown.Buildings {
    class House : Building {

        public House(SpriteFrame spriteFrame, Vector2 position, Rectangle rect, BuildingTypes type, Costs costs) : base(spriteFrame, position, rect, type, costs) {
        }

        public override void Update() {
            if (BuildingState == 4) {
                // People move in and are ready to work
                Initialize();
                BuildingState = 5;
            } else if (BuildingState == 5) {
                // Update resources
                if (ActionTick > 5) {
                    // Food
                    if (mechanics.Food + Costs.FoodUpkeep < mechanics.FoodStorage)
                        mechanics.Food += Costs.FoodUpkeep;
                    else {
                        mechanics.Food = mechanics.FoodStorage;
                    }

                    mechanics.Gold += Costs.GoldUpkeep;

                    // Stone
                    if (mechanics.Stone + Costs.StoneUpkeep < mechanics.StoneStorage)
                        mechanics.Stone += Costs.StoneUpkeep;
                    else {
                        mechanics.Stone = mechanics.StoneStorage;
                    }
                    // Wood
                    if (mechanics.Wood + Costs.WoodUpkeep < mechanics.WoodStorage)
                        mechanics.Wood += Costs.WoodUpkeep;
                    else {
                        mechanics.Wood = mechanics.WoodStorage;
                    }
                    ActionTick = 0;
                }

                ActionTick++;
            }
        }

        public override void UpdateSprite() {
            if (BuildingState == 0)
                SpriteFrame = buildingTileSheet.Sprite(TexturePackerMonoGameDefinitions.buildingAtlas.House0);
            if (BuildingState == 1)
                SpriteFrame = buildingTileSheet.Sprite(TexturePackerMonoGameDefinitions.buildingAtlas.House1);
            if (BuildingState == 2)
                SpriteFrame = buildingTileSheet.Sprite(TexturePackerMonoGameDefinitions.buildingAtlas.House2);
            if (BuildingState == 3) {
                int randomHouse = random.Next(1, 7);
                if (randomHouse == 1)
                    SpriteFrame = buildingTileSheet.Sprite(TexturePackerMonoGameDefinitions.buildingAtlas.House);
                else if (randomHouse == 2)
                    SpriteFrame = buildingTileSheet.Sprite(TexturePackerMonoGameDefinitions.buildingAtlas.House4);
                else if (randomHouse == 3)
                    SpriteFrame = buildingTileSheet.Sprite(TexturePackerMonoGameDefinitions.buildingAtlas.House5);
                else if (randomHouse == 4)
                    SpriteFrame = buildingTileSheet.Sprite(TexturePackerMonoGameDefinitions.buildingAtlas.House6);
                else if (randomHouse == 5)
                    SpriteFrame = buildingTileSheet.Sprite(TexturePackerMonoGameDefinitions.buildingAtlas.House7);
                else if (randomHouse == 6)
                    SpriteFrame = buildingTileSheet.Sprite(TexturePackerMonoGameDefinitions.buildingAtlas.House8);
            }
        }

    }
}
