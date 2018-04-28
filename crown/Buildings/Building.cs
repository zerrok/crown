using Microsoft.Xna.Framework;
using TexturePackerLoader;
using static crown.Game1;

namespace crown
{
    public class Building
    {

        SpriteFrame spriteFrame;
        Vector2 position;
        Rectangle rect;
        int buildingState;
        int buildingTick;

        public enum Type
        {
            TOWNHALL,
            HOUSE,
            WOODCUTTER,
            FARM,
            STORAGE
        };
        Type type;


        public Building(SpriteFrame spriteFrame, Vector2 position, Rectangle rect, Type type)
        {
            this.SpriteFrame = spriteFrame;
            this.Position = position;
            this.Rect = rect;
            this.type = type;

            // For slowly building or degrading the building
            BuildingState1 = 0;
            BuildingTick1 = 0;
        }

        public void UpdateSprite()
        {
            if (buildingState == 0) {
                if (type == Type.HOUSE)
                    spriteFrame = buildingTileSheet.Sprite(TexturePackerMonoGameDefinitions.buildingAtlas.SmallSelect);
                if (type == Type.TOWNHALL)
                    spriteFrame = buildingTileSheet.Sprite(TexturePackerMonoGameDefinitions.buildingAtlas.LargeSelect);
            }
            if (buildingState == 1) {
                if (type == Type.HOUSE)
                    spriteFrame = buildingTileSheet.Sprite(TexturePackerMonoGameDefinitions.buildingAtlas.SmallSelect);
                if (type == Type.TOWNHALL)
                    spriteFrame = buildingTileSheet.Sprite(TexturePackerMonoGameDefinitions.buildingAtlas.LargeSelect);
            }
            if (buildingState == 2) {
                if (type == Type.HOUSE)
                    spriteFrame = buildingTileSheet.Sprite(TexturePackerMonoGameDefinitions.buildingAtlas.SmallSelect);
                if (type == Type.TOWNHALL)
                    spriteFrame = buildingTileSheet.Sprite(TexturePackerMonoGameDefinitions.buildingAtlas.LargeSelect);
            }
            if (buildingState == 3) {
                if (type == Type.HOUSE)
                    spriteFrame = buildingTileSheet.Sprite(TexturePackerMonoGameDefinitions.buildingAtlas.House);
                if (type == Type.TOWNHALL)
                    spriteFrame = buildingTileSheet.Sprite(TexturePackerMonoGameDefinitions.buildingAtlas.Townhall);
            }
        }

        public Type Type1 {
            get => type; set => type = value;
        }
        public int BuildingState {
            get => BuildingState1; set => BuildingState1 = value;
        }
        public int BuildingTick {
            get => BuildingTick1; set => BuildingTick1 = value;
        }
        public SpriteFrame SpriteFrame {
            get => spriteFrame; set => spriteFrame = value;
        }
        public Vector2 Position {
            get => position; set => position = value;
        }
        public Rectangle Rect {
            get => rect; set => rect = value;
        }
        public int BuildingState1 {
            get => buildingState; set => buildingState = value;
        }
        public int BuildingTick1 {
            get => buildingTick; set => buildingTick = value;
        }
    }
}
