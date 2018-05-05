using Microsoft.Xna.Framework;
using TexturePackerLoader;
using static crown.Game1;

namespace crown.Buildings {
    class Woodcutter : Building {

        public Woodcutter(SpriteFrame spriteFrame, Vector2 position, Rectangle rect, BuildingTypes type, Costs costs) : base(spriteFrame, position, rect, type, costs) {
        }

        public override void Initialize() {
            Inhabitants = 3;
            GoldUpkeep = 6;
            ResourcesProduced = 10;
            mechanics.GoldDelta -= GoldUpkeep;
            mechanics.WoodDelta += ResourcesProduced;
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
                        && interactive.Rect.Intersects(new Rectangle(Rect.X - 2 * Rect.Width, Rect.Y - 2 * Rect.Height, Rect.Width * 4, Rect.Height * 4))) {
                            if (mechanics.Wood + ResourcesProduced > mechanics.WoodStorage)
                                break;

                            mechanics.Wood += ResourcesProduced;
                            interactive.Health--;
                            break;
                        }
                    }
                    mechanics.Gold -= GoldUpkeep;
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
