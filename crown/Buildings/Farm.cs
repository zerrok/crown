using Microsoft.Xna.Framework;
using TexturePackerLoader;
using static crown.Game1;

namespace crown.Buildings {
    class Farm : Building {

        public Farm(SpriteFrame spriteFrame, Vector2 position, Rectangle rect, BuildingTypes type, Costs costs) : base(spriteFrame, position, rect, type, costs) {
        }

        public override void Initialize() {
            Inhabitants = 5;
            ResourcesProduced = Inhabitants * 3;
            GoldUpkeep = 10;
            mechanics.GoldDelta -= GoldUpkeep;
            mechanics.FoodDelta += ResourcesProduced;
        }

        public override void Update() {
            if (BuildingState == 4) {
                Initialize();
                BuildingState = 5;
            } else if (BuildingState == 5) {

                // Update resources
                if (ActionTick > 5) {
                    // Food
                    if (mechanics.Food + ResourcesProduced < mechanics.FoodStorage)
                        mechanics.Food += ResourcesProduced;
                    else {
                        mechanics.Food = mechanics.FoodStorage;
                    }
                    // Gold
                    mechanics.Gold -= GoldUpkeep;

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
