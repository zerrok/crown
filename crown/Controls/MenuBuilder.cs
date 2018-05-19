using Microsoft.Xna.Framework;
using static crown.Game1;

namespace crown {
    public class MenuBuilder {
        public static int menuYPosition = graphics.GraphicsDevice.Viewport.Height - 300;
        static int xOffset = 80;
        static int yOffset = 80;

        public static void BuildGameMenu() {
            int xPos = 0;
            int yPos = menuYPosition;

            Button buttonTownhall = new Button();
            buttonTownhall.Pos = new Vector2(xPos + 20, yPos + 20);
            buttonTownhall.Rect = new Rectangle(xPos + 20, yPos + 20, (int)menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Buttonfarmland).Size.X, (int)menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Buttonfarmland).Size.Y);
            buttonTownhall.Type = Button.ButtonType.TOWNHALL;
            buttonTownhall.SpriteFrame = menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Buttontownhall);
            menuButtons.Add(buttonTownhall);
        }

        public static void MenuTierOne() {
            int xPos = 100;
            int yPos = menuYPosition + 20;

            // Town hall has been built
            Button buttonHouse = new Button();
            buttonHouse.Pos = new Vector2(xPos, yPos);
            buttonHouse.Rect = new Rectangle(xPos, yPos, (int)menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Buttonfarmland).Size.X, (int)menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Buttonfarmland).Size.Y);
            buttonHouse.Type = Button.ButtonType.HOUSE;
            buttonHouse.SpriteFrame = menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Buttonhouse);
            menuButtons.Add(buttonHouse);

            xPos += xOffset;

            Button buttonRoads = new Button();
            buttonRoads.Pos = new Vector2(xPos, yPos);
            buttonRoads.Rect = new Rectangle(xPos, yPos, (int)menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Buttonfarmland).Size.X, (int)menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Buttonfarmland).Size.Y);
            buttonRoads.Type = Button.ButtonType.ROAD;
            buttonRoads.SpriteFrame = menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Buttonroad);
            menuButtons.Add(buttonRoads);

            xPos += xOffset;

            Button buttonFarm = new Button();
            buttonFarm.Pos = new Vector2(xPos, yPos);
            buttonFarm.Rect = new Rectangle(xPos, yPos, (int)menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Buttonfarmland).Size.X, (int)menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Buttonfarmland).Size.Y);
            buttonFarm.Type = Button.ButtonType.FARM;
            buttonFarm.SpriteFrame = menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Buttonfarm);
            menuButtons.Add(buttonFarm);

            xPos += xOffset;

            Button buttonWoodcutter = new Button();
            buttonWoodcutter.Pos = new Vector2(xPos, yPos);
            buttonWoodcutter.Rect = new Rectangle(xPos, yPos, (int)menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Buttonfarmland).Size.X, (int)menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Buttonfarmland).Size.Y);
            buttonWoodcutter.Type = Button.ButtonType.WOODCUTTER;
            buttonWoodcutter.SpriteFrame = menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Buttonwoodcutter);
            menuButtons.Add(buttonWoodcutter);

            xPos = 20;
            yPos += yOffset;

            Button buttonStorage = new Button();
            buttonStorage.Pos = new Vector2(xPos, yPos);
            buttonStorage.Rect = new Rectangle(xPos, yPos, (int)menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Buttonfarmland).Size.X, (int)menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Buttonfarmland).Size.Y);
            buttonStorage.Type = Button.ButtonType.STORAGE;
            buttonStorage.SpriteFrame = menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Buttonstorage);
            menuButtons.Add(buttonStorage);

            xPos += xOffset;

            Button buttonQuarry = new Button();
            buttonQuarry.Pos = new Vector2(xPos, yPos);
            buttonQuarry.Rect = new Rectangle(xPos, yPos, (int)menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Buttonfarmland).Size.X, (int)menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Buttonfarmland).Size.Y);
            buttonQuarry.Type = Button.ButtonType.QUARRY;
            buttonQuarry.SpriteFrame = menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Buttonquarry);
            menuButtons.Add(buttonQuarry);

            xPos += xOffset;

            Button buttonScientist = new Button();
            buttonScientist.Pos = new Vector2(xPos, yPos);
            buttonScientist.Rect = new Rectangle(xPos, yPos, (int)menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Buttonfarmland).Size.X, (int)menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Buttonfarmland).Size.Y);
            buttonScientist.Type = Button.ButtonType.SCIENTIST;
            buttonScientist.SpriteFrame = menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Buttonscientist);
            menuButtons.Add(buttonScientist);
        }

    }
}
