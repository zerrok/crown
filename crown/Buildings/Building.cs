using Microsoft.Xna.Framework;
using TexturePackerLoader;
using static crown.Game1;

namespace crown {
    public class Building {

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
                if (Type == BuildingTypes.HOUSE) {
                    UpdateHouse();
                }
                if (Type == Building.BuildingTypes.TOWNHALL) {
                    UpdateTownhall();
                }
                if (Type == Building.BuildingTypes.FARM) {
                    UpdateFarm();
                }
                BuildingTick = 0;
            }
        }

        private void UpdateFarm() {
            if (BuildingState == 4) {
                InitializeFarm();
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

        private void UpdateHouse() {
            if (BuildingState == 4) {
                // People move in and are ready to work
                InitializeHouse();
                BuildingState = 5;
            } else if (BuildingState == 5) {

                // Update resources
                if (ActionTick > 5) {
                    mechanics.Food -= Inhabitants / 2;
                    mechanics.Gold += 4;
                    ActionTick = 0;
                }

                ActionTick++;
            }
        }

        private void UpdateTownhall() {
            if (BuildingState == 4) {
                InitializeTownhall();
                BuildingState = 5;
            }
        }

        private void InitializeHouse() {
            Inhabitants = 2;
            mechanics.Population += Inhabitants;
            mechanics.Workers += Inhabitants;
            mechanics.GoldDelta += 4;
            mechanics.FoodDelta -= Inhabitants / 2;
        }

        private void InitializeFarm() {
            Inhabitants = 5;
            ResourcesProduced = Inhabitants * 3;
            GoldUpkeep = 10;
            mechanics.Workers -= 5;
            mechanics.GoldDelta -= GoldUpkeep;
            mechanics.FoodDelta += ResourcesProduced;
        }

        private void InitializeTownhall() {
            mechanics.WoodStorage = 200;
            mechanics.StoneStorage = 200;
            mechanics.FoodStorage = 1000;
            mechanics.Wood = 200;
            mechanics.Food = 300;
        }

        private void UpdateBuildingSprites() {
            // For gradually building buildings
            UpdateSprite();
            BuildingState++;
        }

        public void UpdateSprite() {
            if (BuildingState == 0) {
                if (Type == BuildingTypes.HOUSE)
                    SpriteFrame = buildingTileSheet.Sprite(TexturePackerMonoGameDefinitions.buildingAtlas.House0);
                if (Type == BuildingTypes.TOWNHALL)
                    SpriteFrame = buildingTileSheet.Sprite(TexturePackerMonoGameDefinitions.buildingAtlas.LargeSelect);
                if (Type == BuildingTypes.FARM)
                    SpriteFrame = buildingTileSheet.Sprite(TexturePackerMonoGameDefinitions.buildingAtlas.Farm0);
            }
            if (BuildingState == 1) {
                if (Type == BuildingTypes.HOUSE)
                    SpriteFrame = buildingTileSheet.Sprite(TexturePackerMonoGameDefinitions.buildingAtlas.House1);
                if (Type == BuildingTypes.TOWNHALL)
                    SpriteFrame = buildingTileSheet.Sprite(TexturePackerMonoGameDefinitions.buildingAtlas.LargeSelect);
                if (Type == BuildingTypes.FARM)
                    SpriteFrame = buildingTileSheet.Sprite(TexturePackerMonoGameDefinitions.buildingAtlas.Farm1);
            }
            if (BuildingState == 2) {
                if (Type == BuildingTypes.HOUSE)
                    SpriteFrame = buildingTileSheet.Sprite(TexturePackerMonoGameDefinitions.buildingAtlas.House2);
                if (Type == BuildingTypes.TOWNHALL)
                    SpriteFrame = buildingTileSheet.Sprite(TexturePackerMonoGameDefinitions.buildingAtlas.LargeSelect);
                if (Type == BuildingTypes.FARM)
                    SpriteFrame = buildingTileSheet.Sprite(TexturePackerMonoGameDefinitions.buildingAtlas.Farm2);
            }
            if (BuildingState == 3) {
                if (Type == BuildingTypes.FARM)
                    SpriteFrame = buildingTileSheet.Sprite(TexturePackerMonoGameDefinitions.buildingAtlas.Farm4);
                if (Type == BuildingTypes.HOUSE) {
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
        public int ActionTick { get => actionTick; set => actionTick = value; }
        public int ResourcesProduced { get => resourcesProduced; set => resourcesProduced = value; }
        public int GoldUpkeep { get => goldUpkeep; set => goldUpkeep = value; }
    }
}
