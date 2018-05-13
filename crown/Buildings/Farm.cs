using Microsoft.Xna.Framework;
using TexturePackerLoader;
using static crown.Game1;

namespace crown.Buildings {
    class Farm : Building {

        public Farm(SpriteFrame spriteFrame, Vector2 position, Rectangle rect, BuildingTypes type, Costs costs) : base(spriteFrame, position, rect, type, costs) {
        }

        public override void Update() {
            if (BuildingState == 4) {
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
                SpriteFrame = buildingTileSheet.Sprite(TexturePackerMonoGameDefinitions.buildingAtlas.Farm0);
            if (BuildingState == 1)
                SpriteFrame = buildingTileSheet.Sprite(TexturePackerMonoGameDefinitions.buildingAtlas.Farm1);
            if (BuildingState == 2)
                SpriteFrame = buildingTileSheet.Sprite(TexturePackerMonoGameDefinitions.buildingAtlas.Farm2);
            if (BuildingState == 3)
                SpriteFrame = buildingTileSheet.Sprite(TexturePackerMonoGameDefinitions.buildingAtlas.Farm4);
        }

    }
}
