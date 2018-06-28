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
        double inhabitants;

        int buildingState;
        int buildingTick;
        int actionTick;
        Actions type;

        string name;
        string description;

        Costs costs;

        public SpriteFrame SpriteFrame { get => spriteFrame; set => spriteFrame = value; }
        public Vector2 Position { get => position; set => position = value; }
        public Rectangle Rect { get => rect; set => rect = value; }
        public double Inhabitants { get => inhabitants; set => inhabitants = value; }
        public int BuildingState { get => buildingState; set => buildingState = value; }
        public int BuildingTick { get => buildingTick; set => buildingTick = value; }
        public int ActionTick { get => actionTick; set => actionTick = value; }
        public Actions Type { get => type; set => type = value; }
        public Costs Costs { get => costs; set => costs = value; }
        public string Description { get => description; set => description = value; }
        public string Name { get => name; set => name = value; }

        public virtual void Initialize() {
            Inhabitants = Costs.Population;
            mechanics.Population += Costs.Population;
            mechanics.GoldDelta += Costs.GoldUpkeep;
            mechanics.FoodDelta += Costs.FoodUpkeep;
            mechanics.WoodDelta += Costs.WoodUpkeep;
            mechanics.StoneDelta += Costs.StoneUpkeep;
            mechanics.BeerDelta += Costs.BeerUpkeep;
            mechanics.WheatDelta += Costs.WheatUpkeep;
        }

        public Building(SpriteFrame spriteFrame, Vector2 position, Rectangle rect, Actions type, Costs costs) {
            SpriteFrame = spriteFrame;
            Position = position;
            Rect = rect;
            Type = type;

            // For slowly building or degrading the building
            BuildingState = 0;
            if (type == Actions.Townhall)
                BuildingState = 2;
            BuildingTick = 0;
            ActionTick = 0;

            Costs = costs;

            DeductCosts(costs);

            description = "Is being built";
            name = "Something";
        }

        public void DeductCosts(Costs costs) {
            // After building subtract the costs
            mechanics.Gold += costs.Gold;
            mechanics.Stone += costs.Stone;
            mechanics.Wood += costs.Wood;
            mechanics.Food += costs.Food;
            if (Type != Actions.House)
                mechanics.Workers += costs.Workers;
        }

        public void Updates() {
            if (BuildingTick < 4)
                BuildingTick++;

            if (BuildingTick >= 4) {
                if (BuildingState <= 3) {
                    // Go through the different build phases for every type
                    UpdateBuildingSprites();
                    BuildingState++;
                }
                Update();
                BuildingTick = 0;
            }
        }

        private void UpdateBuildingSprites() {
            // For gradually building buildings
            UpdateSprite();
        }


    }
}
