using Microsoft.Xna.Framework;
using TexturePackerLoader;
using static crown.Game1;

namespace crown {
    abstract public class Building {

        abstract public void Update();
        abstract public void UpdateSprite();

        SpriteFrame spriteFrame;
        Vector2 position;
        Rectangle rect;
        int inhabitants;

        int buildingState;
        int buildingTick;
        int actionTick;
        BuildingTypes type;

        Costs costs;

        public enum BuildingTypes {
            TOWNHALL,
            HOUSE,
            WOODCUTTER,
            FARM,
            STORAGE
        };

        public SpriteFrame SpriteFrame { get => spriteFrame; set => spriteFrame = value; }
        public Vector2 Position { get => position; set => position = value; }
        public Rectangle Rect { get => rect; set => rect = value; }
        public int Inhabitants { get => inhabitants; set => inhabitants = value; }
        public int BuildingState { get => buildingState; set => buildingState = value; }
        public int BuildingTick { get => buildingTick; set => buildingTick = value; }
        public int ActionTick { get => actionTick; set => actionTick = value; }
        public BuildingTypes Type { get => type; set => type = value; }
        public Costs Costs { get => costs; set => costs = value; }

        public void Initialize() {
            Inhabitants = Costs.Population;
            mechanics.Population += Costs.Population;
            mechanics.GoldDelta += Costs.GoldUpkeep;
            mechanics.FoodDelta += Costs.FoodUpkeep;
            mechanics.WoodDelta += Costs.WoodUpkeep;

            if (Type == BuildingTypes.HOUSE)
                mechanics.Workers += costs.Workers;

            if (Type == BuildingTypes.TOWNHALL) {
                mechanics.WoodStorage = 400;
                mechanics.StoneStorage = 100;
                mechanics.FoodStorage = 1000;
                mechanics.Wood = 200;
                mechanics.Food = 200;
            }
        }

        public Building(SpriteFrame spriteFrame, Vector2 position, Rectangle rect, BuildingTypes type, Costs costs) {
            SpriteFrame = spriteFrame;
            Position = position;
            Rect = rect;
            Type = type;

            // For slowly building or degrading the building
            BuildingState = 0;
            if (type == BuildingTypes.TOWNHALL)
                BuildingState = 2;
            BuildingTick = 0;
            ActionTick = 0;

            Costs = costs;

            DeductCosts(costs);
        }

        public void DeductCosts(Costs costs) {
            // After building subtract the costs
            mechanics.Gold += costs.Gold;
            mechanics.Stone += costs.Stone;
            mechanics.Wood += costs.Wood;
            mechanics.Food += costs.Food;
            if (Type != BuildingTypes.HOUSE)
                mechanics.Workers += costs.Workers;
        }

        public void Updates() {
            if (BuildingTick < 2)
                BuildingTick++;

            if (BuildingTick >= 2) {
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


    }
}
