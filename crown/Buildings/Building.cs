using Microsoft.Xna.Framework;
using TexturePackerLoader;
using static crown.Game1;

namespace crown {
    abstract public class Building {

        abstract public void Update();
        abstract public void Initialize();
        abstract public void UpdateSprite();

        SpriteFrame spriteFrame;
        Vector2 position;
        Rectangle rect;
        int inhabitants;

        int resourcesProduced;
        int goldUpkeep;

        int buildingState;
        int buildingTick;
        int actionTick;

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
            actionTick = 0;
            resourcesProduced = 0;
            goldUpkeep = 0;
        }

        public void Updates() {
            if (BuildingTick < 5)
                BuildingTick++;

            if (BuildingTick >= 5) {
                if (BuildingState <= 3) {
                    // Go through the different build phases for every type
                    UpdateBuildingSprites();
                }
                Update();
                BuildingTick = 0;
            }
        }

        private void UpdateBuildingSprites() {
            // For gradually building buildings
            UpdateSprite();
            BuildingState++;
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
        public int ActionTick { get => actionTick; set => actionTick = value; }
        public int ResourcesProduced { get => resourcesProduced; set => resourcesProduced = value; }
        public int GoldUpkeep { get => goldUpkeep; set => goldUpkeep = value; }
    }
}
