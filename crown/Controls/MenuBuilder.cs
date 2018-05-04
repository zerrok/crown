using Microsoft.Xna.Framework;
using static crown.Game1;

namespace crown {
    public class MenuBuilder {

        public static void BuildGameMenu() {
            Menu menuBackground = new Menu();
            int menuSizeX = 500;
            int menuSizeY = (int)menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Maincontrols).Size.Y;
            int xPos = 0;
            int yPos = graphics.GraphicsDevice.Viewport.Height - menuSizeY;

            menuBackground.MainPos = new Vector2(xPos, yPos);
            menuBackground.MainRect = new Rectangle(xPos, yPos, menuSizeX, menuSizeY);
            menuBackground.Type = Menu.MenuType.MAIN;
            menu.Add(menuBackground);

            Menu buttonTownhall = new Menu();
            buttonTownhall.MainPos = new Vector2(xPos + 20, yPos + 20);
            buttonTownhall.MainRect = new Rectangle(xPos + 20, yPos + 20, (int)menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Buttonfarmland).Size.X, (int)menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Buttonfarmland).Size.Y);
            buttonTownhall.Type = Menu.MenuType.BUTTON_TOWNHALL;
            menu.Add(buttonTownhall);
        }

        public static void MenuTierOne() {
            int menuSizeY = (int)menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Maincontrols).Size.Y;
            int xPos = 0;
            int yPos = graphics.GraphicsDevice.Viewport.Height - menuSizeY;

            // Town hall has been built
            Menu buttonHouse = new Menu();
            buttonHouse.MainPos = new Vector2(xPos + 100, yPos + 20);
            buttonHouse.MainRect = new Rectangle(xPos + 100, yPos + 20, (int)menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Buttonfarmland).Size.X, (int)menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Buttonfarmland).Size.Y);
            buttonHouse.Type = Menu.MenuType.BUTTON_HOUSE;
            menu.Add(buttonHouse);

            Menu buttonRoads = new Menu();
            buttonRoads.MainPos = new Vector2(xPos + 180, yPos + 20);
            buttonRoads.MainRect = new Rectangle(xPos + 180, yPos + 20, (int)menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Buttonfarmland).Size.X, (int)menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Buttonfarmland).Size.Y);
            buttonRoads.Type = Menu.MenuType.BUTTON_ROAD;
            menu.Add(buttonRoads);

            Menu buttonFarm = new Menu();
            buttonFarm.MainPos = new Vector2(xPos + 20, yPos + 100);
            buttonFarm.MainRect = new Rectangle(xPos + 20, yPos + 100, (int)menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Buttonfarmland).Size.X, (int)menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Buttonfarmland).Size.Y);
            buttonFarm.Type = Menu.MenuType.BUTTON_FARM;
            menu.Add(buttonFarm);

            Menu buttonWoodcutter = new Menu();
            buttonWoodcutter.MainPos = new Vector2(xPos + 100, yPos + 100);
            buttonWoodcutter.MainRect = new Rectangle(xPos + 100, yPos + 100, (int)menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Buttonfarmland).Size.X, (int)menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Buttonfarmland).Size.Y);
            buttonWoodcutter.Type = Menu.MenuType.BUTTON_WOODCUTTER;
            menu.Add(buttonWoodcutter);

            Menu buttonStorage = new Menu();
            buttonStorage.MainPos = new Vector2(xPos + 180, yPos + 100);
            buttonStorage.MainRect = new Rectangle(xPos + 180, yPos + 100, (int)menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Buttonfarmland).Size.X, (int)menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Buttonfarmland).Size.Y);
            buttonStorage.Type = Menu.MenuType.BUTTON_STORAGE;
            menu.Add(buttonStorage);
        }
    }
}
