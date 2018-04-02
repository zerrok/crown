using Microsoft.Xna.Framework;

namespace crown {
  public class Interactive {

    public enum IntType {
      TREE
    }

    IntType type;
    string text;
    int health;
    int worth;
    Rectangle rect;
    Vector2 coords;
    bool isSelected;

    public Interactive(IntType type, string text, int health, int worth, Rectangle rect, Vector2 coords) {
      this.type = type;
      this.text = text;
      this.health = health;
      this.worth = worth;
      this.rect = rect;
      this.coords = coords;
      isSelected = false;
    }

    public string Text {
      get {
        return text;
      }

      set {
        text = value;
      }
    }

    public int Health {
      get {
        return health;
      }

      set {
        health = value;
      }
    }

    public int Worth {
      get {
        return worth;
      }

      set {
        worth = value;
      }
    }

    public IntType Type {
      get {
        return type;
      }

      set {
        type = value;
      }
    }

    public Rectangle Rect {
      get {
        return rect;
      }

      set {
        rect = value;
      }
    }

    public Vector2 Coords {
      get {
        return coords;
      }

      set {
        coords = value;
      }
    }

    public bool IsSelected {
      get {
        return isSelected;
      }

      set {
        isSelected = value;
      }
    }
  }
}
