using Microsoft.Xna.Framework;
using TexturePackerLoader;
using static crown.Game1;

namespace crown {
  public class Menu {

    public static void BuildGameMenu() {
      Menu menuBackground = new Menu();
      int menuSizeX = (int)menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Maincontrols).Size.X;
      int menuSizeY = (int)menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Maincontrols).Size.Y;
      int xPos = 0;
      int yPos = graphics.GraphicsDevice.Viewport.Height - menuSizeY;

      menuBackground.MainPos = new Vector2(xPos, yPos);
      menuBackground.MainRect = new Rectangle(xPos, yPos, menuSizeX, menuSizeY);
      menuBackground.Type = Menu.MenuType.MAIN;
      menu.Add(menuBackground);

      Menu buttonHouse = new Menu();
      buttonHouse.MainPos = new Vector2(xPos + 20, yPos + 20);
      buttonHouse.MainRect = new Rectangle(xPos + 20, yPos + 20, (int)menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Buttonfarmland).Size.X, (int)menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Buttonfarmland).Size.Y);
      buttonHouse.Type = Menu.MenuType.BUTTON_HOUSE;
      menu.Add(buttonHouse);

      Menu buttonRoads = new Menu();
      buttonRoads.MainPos = new Vector2(xPos + 180, yPos + 20);
      buttonRoads.MainRect = new Rectangle(xPos + 180, yPos + 20, (int)menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Buttonfarmland).Size.X, (int)menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Buttonfarmland).Size.Y);
      buttonRoads.Type = Menu.MenuType.BUTTON_ROAD;
      menu.Add(buttonRoads);

      Menu buttonFarmland = new Menu();
      buttonFarmland.MainPos = new Vector2(xPos + 340, yPos + 20);
      buttonFarmland.MainRect = new Rectangle(xPos + 340, yPos + 20, (int)menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Buttonfarmland).Size.X, (int)menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Buttonfarmland).Size.Y);
      buttonFarmland.Type = Menu.MenuType.BUTTON_FARMLAND;
      menu.Add(buttonFarmland);

      Menu buttonTownhall = new Menu();
      buttonTownhall.MainPos = new Vector2(xPos + 500, yPos + 20);
      buttonTownhall.MainRect = new Rectangle(xPos + 500, yPos + 20, (int)menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Buttonfarmland).Size.X, (int)menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Buttonfarmland).Size.Y);
      buttonTownhall.Type = Menu.MenuType.BUTTON_TOWNHALL;
      menu.Add(buttonTownhall);
    }

    Vector2 mainPos;
    Rectangle mainRect;
    MenuType type;

    public Vector2 MainPos {
      get => mainPos;
      set => mainPos = value;
    }
    public Rectangle MainRect {
      get => mainRect;
      set => mainRect = value;
    }
    public MenuType Type {
      get => type;
      set => type = value;
    }

    public enum MenuType {
      MAIN, BUTTON_HOUSE, BUTTON_TOWNHALL, BUTTON_FARMLAND, BUTTON_ROAD
    }


  }
}
