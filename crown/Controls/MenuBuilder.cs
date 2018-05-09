using Microsoft.Xna.Framework;
using static crown.Game1;

namespace crown {
    public class MenuBuilder {

        public static void BuildGameMenu() {
            Button menuBackground = new Button();
            int menuSizeX = 500;
            int menuSizeY = (int)menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Maincontrols).Size.Y;
            int xPos = 0;
            int yPos = graphics.GraphicsDevice.Viewport.Height - menuSizeY;

            menuBackground.MainPos = new Vector2(xPos, yPos);
            menuBackground.MainRect = new Rectangle(xPos, yPos, menuSizeX, menuSizeY);
            menuBackground.Type = Button.ButtonType.MAIN;
            menuButtons.Add(menuBackground);

            Button buttonTownhall = new Button();
            buttonTownhall.MainPos = new Vector2(xPos + 20, yPos + 20);
            buttonTownhall.MainRect = new Rectangle(xPos + 20, yPos + 20, (int)menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Buttonfarmland).Size.X, (int)menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Buttonfarmland).Size.Y);
            buttonTownhall.Type = Button.ButtonType.BUTTON_TOWNHALL;
            menuButtons.Add(buttonTownhall);
        }

        public static void MenuTierOne() {
            int menuSizeY = (int)menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Maincontrols).Size.Y;
            int xPos = 0;
            int yPos = graphics.GraphicsDevice.Viewport.Height - menuSizeY;

            // Town hall has been built
            Button buttonHouse = new Button();
            buttonHouse.MainPos = new Vector2(xPos + 100, yPos + 20);
            buttonHouse.MainRect = new Rectangle(xPos + 100, yPos + 20, (int)menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Buttonfarmland).Size.X, (int)menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Buttonfarmland).Size.Y);
            buttonHouse.Type = Button.ButtonType.BUTTON_HOUSE;
            menuButtons.Add(buttonHouse);

            Button buttonRoads = new Button();
            buttonRoads.MainPos = new Vector2(xPos + 180, yPos + 20);
            buttonRoads.MainRect = new Rectangle(xPos + 180, yPos + 20, (int)menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Buttonfarmland).Size.X, (int)menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Buttonfarmland).Size.Y);
            buttonRoads.Type = Button.ButtonType.BUTTON_ROAD;
            menuButtons.Add(buttonRoads);

            Button buttonFarm = new Button();
            buttonFarm.MainPos = new Vector2(xPos + 20, yPos + 100);
            buttonFarm.MainRect = new Rectangle(xPos + 20, yPos + 100, (int)menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Buttonfarmland).Size.X, (int)menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Buttonfarmland).Size.Y);
            buttonFarm.Type = Button.ButtonType.BUTTON_FARM;
            menuButtons.Add(buttonFarm);

            Button buttonWoodcutter = new Button();
            buttonWoodcutter.MainPos = new Vector2(xPos + 100, yPos + 100);
            buttonWoodcutter.MainRect = new Rectangle(xPos + 100, yPos + 100, (int)menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Buttonfarmland).Size.X, (int)menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Buttonfarmland).Size.Y);
            buttonWoodcutter.Type = Button.ButtonType.BUTTON_WOODCUTTER;
            menuButtons.Add(buttonWoodcutter);

            Button buttonStorage = new Button();
            buttonStorage.MainPos = new Vector2(xPos + 180, yPos + 100);
            buttonStorage.MainRect = new Rectangle(xPos + 180, yPos + 100, (int)menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Buttonfarmland).Size.X, (int)menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Buttonfarmland).Size.Y);
            buttonStorage.Type = Button.ButtonType.BUTTON_STORAGE;
            menuButtons.Add(buttonStorage);
        }
    }
}
