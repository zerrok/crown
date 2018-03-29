using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TexturePackerLoader;

namespace crown {
  public class Menu {
    SpriteFrame mainControls;
    SpriteFrame buttonHouse;
    SpriteFrame buttonTownHall;
    SpriteFrame buttonFarmland;

    Vector2 mainPos;
    Vector2 housePos;
    Vector2 hallPos;
    Vector2 farmlandPos;

    Rectangle mainRect;
    Rectangle houseRect;
    Rectangle hallRect;
    Rectangle farmlandRect;

    public Menu(SpriteFrame mainControls, SpriteFrame buttonHouse, SpriteFrame buttonTownHall, SpriteFrame buttonFarmland) {
      this.mainControls = mainControls;
      this.buttonHouse = buttonHouse;
      this.buttonTownHall = buttonTownHall;
      this.buttonFarmland = buttonFarmland;


      int xPos = 0;
      int yPos = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height - (int)this.mainControls.Size.Y;
      mainPos = new Vector2(xPos, yPos);

      hallPos = new Vector2(xPos + 50, yPos + 25);
      housePos = new Vector2(xPos + 220, yPos + 25);
      farmlandPos = new Vector2(xPos + 390, yPos + 25);

      MainRect = new Rectangle((int)mainPos.X, (int)mainPos.Y, (int)mainControls.Size.X, (int)mainControls.Size.Y);
      HouseRect = new Rectangle((int)housePos.X, (int)housePos.Y, (int)buttonHouse.Size.X, (int)buttonHouse.Size.Y);
      HallRect = new Rectangle((int)hallPos.X, (int)hallPos.Y, (int)buttonTownHall.Size.X, (int)buttonTownHall.Size.Y);
      FarmlandRect = new Rectangle((int)farmlandPos.X, (int)farmlandPos.Y, (int)buttonFarmland.Size.X, (int)buttonFarmland.Size.Y);
    }

    public SpriteFrame MainControls {
      get {
        return mainControls;
      }

      set {
        mainControls = value;
      }
    }

    public SpriteFrame ButtonHouse {
      get {
        return buttonHouse;
      }

      set {
        buttonHouse = value;
      }
    }

    public SpriteFrame ButtonTownHall {
      get {
        return buttonTownHall;
      }

      set {
        buttonTownHall = value;
      }
    }

    public SpriteFrame ButtonFarmland {
      get {
        return buttonFarmland;
      }

      set {
        buttonFarmland = value;
      }
    }

    public Vector2 MainPos {
      get {
        return mainPos;
      }

      set {
        mainPos = value;
      }
    }

    public Vector2 HousePos {
      get {
        return housePos;
      }

      set {
        housePos = value;
      }
    }

    public Vector2 HallPos {
      get {
        return hallPos;
      }

      set {
        hallPos = value;
      }
    }

    public Vector2 FarmlandPos {
      get {
        return farmlandPos;
      }

      set {
        farmlandPos = value;
      }
    }

    public Rectangle MainRect {
      get {
        return mainRect;
      }

      set {
        mainRect = value;
      }
    }

    public Rectangle HouseRect {
      get {
        return houseRect;
      }

      set {
        houseRect = value;
      }
    }

    public Rectangle HallRect {
      get {
        return hallRect;
      }

      set {
        hallRect = value;
      }
    }

    public Rectangle FarmlandRect {
      get {
        return farmlandRect;
      }

      set {
        farmlandRect = value;
      }
    }
  }
}
