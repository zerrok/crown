using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using TexturePackerLoader;

namespace crown {
  public class Building {

    SpriteFrame spriteFrame;
    Vector2 position;
    Rectangle rect;

    public Building(SpriteFrame spriteFrame, Vector2 position, Rectangle rect) {
      this.spriteFrame = spriteFrame;
      this.position = position;
      this.rect = rect;
    }

    public SpriteFrame SpriteFrame {
      get {
        return spriteFrame;
      }

      set {
        spriteFrame = value;
      }
    }

    public Vector2 Position {
      get {
        return position;
      }

      set {
        position = value;
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
  }
}
