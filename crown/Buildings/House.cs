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
            }
        }

        public override void Initialize() {
            base.Initialize();

            Name = "House";
            Description = "It's a small and cozy house. \n A wooden place called home.";
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
