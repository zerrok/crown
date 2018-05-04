using Microsoft.Xna.Framework;
using TexturePackerLoader;
using static crown.Game1;

namespace crown.Buildings {
    class Townhall : Building {

        public Townhall(SpriteFrame spriteFrame, Vector2 position, Rectangle rect, BuildingTypes type, Costs costs) : base(spriteFrame, position, rect, type, costs) {
        }

        public override void Initialize() {
            mechanics.WoodStorage = 200;
            mechanics.StoneStorage = 200;
            mechanics.FoodStorage = 1000;
            mechanics.Wood = 500;
            mechanics.Food = 300;
        }

        public override void Update() {
            if (BuildingState == 4) {
                Initialize();
                BuildingState = 5;
            }
        }

        public override void UpdateSprite() {
            if (BuildingState == 0)
                SpriteFrame = buildingTileSheet.Sprite(TexturePackerMonoGameDefinitions.buildingAtlas.LargeSelect);
            if (BuildingState == 1)
                SpriteFrame = buildingTileSheet.Sprite(TexturePackerMonoGameDefinitions.buildingAtlas.LargeSelect);
            if (BuildingState == 2)
                SpriteFrame = buildingTileSheet.Sprite(TexturePackerMonoGameDefinitions.buildingAtlas.LargeSelect);
            if (BuildingState == 3)
                SpriteFrame = buildingTileSheet.Sprite(TexturePackerMonoGameDefinitions.buildingAtlas.Townhall);
        }

    }
}
