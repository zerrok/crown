using Microsoft.Xna.Framework;
using TexturePackerLoader;
using static crown.Game1;

namespace crown {
    public class Building {

        SpriteFrame spriteFrame;
        Vector2 position;
        Rectangle rect;
        int inhabitants;

        int buildingState;
        int buildingTick;

        public enum BuildingTypes {
            TOWNHALL,
            HOUSE,
            WOODCUTTER,
            FARM,
            STORAGE
        };
        BuildingTypes type;



        public Building(SpriteFrame spriteFrame, Vector2 position, Rectangle rect, BuildingTypes type) {
            this.spriteFrame = spriteFrame;
            this.position = position;
            this.rect = rect;
            this.type = type;

            // For slowly building or degrading the building
            BuildingState = 0;
            if (type == BuildingTypes.TOWNHALL)
                BuildingState = 2;
            BuildingTick = 0;
        }

        public void UpdateSprite() {
            if (BuildingState == 0) {
                if (Type == BuildingTypes.HOUSE)
                    SpriteFrame = buildingTileSheet.Sprite(TexturePackerMonoGameDefinitions.buildingAtlas.House0);
                if (Type == BuildingTypes.TOWNHALL)
                    SpriteFrame = buildingTileSheet.Sprite(TexturePackerMonoGameDefinitions.buildingAtlas.LargeSelect);
            }
            if (BuildingState == 1) {
                if (Type == BuildingTypes.HOUSE)
                    SpriteFrame = buildingTileSheet.Sprite(TexturePackerMonoGameDefinitions.buildingAtlas.House1);
                if (Type == BuildingTypes.TOWNHALL)
                    SpriteFrame = buildingTileSheet.Sprite(TexturePackerMonoGameDefinitions.buildingAtlas.LargeSelect);
            }
            if (BuildingState == 2) {
                if (Type == BuildingTypes.HOUSE)
                    SpriteFrame = buildingTileSheet.Sprite(TexturePackerMonoGameDefinitions.buildingAtlas.House2);
                if (Type == BuildingTypes.TOWNHALL)
                    SpriteFrame = buildingTileSheet.Sprite(TexturePackerMonoGameDefinitions.buildingAtlas.LargeSelect);
            }
            if (BuildingState == 3) {
                if (Type == BuildingTypes.HOUSE) {
                    if (random.Next(1, 100) >= 50)
                        SpriteFrame = buildingTileSheet.Sprite(TexturePackerMonoGameDefinitions.buildingAtlas.House);
                    else
                        SpriteFrame = buildingTileSheet.Sprite(TexturePackerMonoGameDefinitions.buildingAtlas.House4);
                }
                if (Type == BuildingTypes.TOWNHALL)
                    SpriteFrame = buildingTileSheet.Sprite(TexturePackerMonoGameDefinitions.buildingAtlas.Townhall);
            }
        }

        public SpriteFrame SpriteFrame {
            get => spriteFrame;
            set => spriteFrame = value;
        }
        public Vector2 Position {
            get => position;
            set => position = value;
        }
        public Rectangle Rect {
            get => rect;
            set => rect = value;
        }
        public int Inhabitants {
            get => inhabitants;
            set => inhabitants = value;
        }
        public int BuildingState {
            get => buildingState;
            set => buildingState = value;
        }
        public int BuildingTick {
            get => buildingTick;
            set => buildingTick = value;
        }
        public BuildingTypes Type {
            get => type;
            set => type = value;
        }
    }
}
