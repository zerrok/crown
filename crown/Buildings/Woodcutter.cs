using Microsoft.Xna.Framework;
using TexturePackerLoader;
using static crown.Game1;

namespace crown.Buildings {
    class Woodcutter : Building {

        public Woodcutter(SpriteFrame spriteFrame, Vector2 position, Rectangle rect, BuildingTypes type, Costs costs) : base(spriteFrame, position, rect, type, costs) {
        }

        public override void Update() {
            if (BuildingState == 4) {
                // People move in and are ready to work
                Initialize();
                BuildingState = 5;
            } else if (BuildingState == 5) {

                // Update resources
                if (ActionTick > 5) {
                    foreach (Interactive interactive in interactives) {
                        if (interactive.Type == Interactive.IntType.TREE
                        && interactive.Health > 0
                        && interactive.Rect.Intersects(new Rectangle(Rect.X - 4 * Rect.Width, Rect.Y - 4 * Rect.Height, Rect.Width * 9, Rect.Height * 9))) {
                            if (mechanics.Wood + Costs.WoodUpkeep > mechanics.WoodStorage)
                                break;

                            mechanics.Wood += Costs.WoodUpkeep;
                            interactive.Health--;
                            break;
                        }
                    }
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
            if (BuildingState == 3)
                SpriteFrame = buildingTileSheet.Sprite(TexturePackerMonoGameDefinitions.buildingAtlas.Woodcutter4);
        }

    }
}
