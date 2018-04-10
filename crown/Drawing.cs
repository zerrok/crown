using Microsoft.Xna.Framework;
using System;
using TexturePackerLoader;
using static crown.Game1;
using System.Collections.Generic;
using crown.Terrain;
using System.Linq;

namespace crown {
  class Drawing {

    public static void DrawTerrain(SpriteRender spriteRender) {
      int startCol, endCol, startRow, endRow;
      GetRenderableTilesAndCenterTile(out startCol, out endCol, out startRow, out endRow);
      for (int x = startCol; x < endCol && x < tileMap.GetUpperBound(0); x++)
        for (int y = startRow; y < endRow && y < tileMap.GetUpperBound(1); y++) {
          // Render coords are tile coords times tilesize 
          int yPos = tileMap[x, y].Type != TexturePackerMonoGameDefinitions.texturePackerSpriteAtlas.Stone1 ? y * tileSize : y * tileSize - (2 * tileSize);
          int xPos = x * tileSize;
          Vector2 coord = new Vector2(xPos, yPos);
          spriteRender.Draw(mapTileSheet.Sprite(tileMap[x, y].Type), coord);
        }
    }

    public static void DrawMouseSelection(SpriteRender spriteRender, Vector2 mousePosition, MouseAction mouseAction) {
      SpriteFrame spriteframe = null;

      if (mouseAction == MouseAction.TOWNHALL)
        spriteframe = buildingTileSheet.Sprite(TexturePackerMonoGameDefinitions.buildingAtlas.LargeSelect);
      if (mouseAction == MouseAction.HOUSE || mouseAction == MouseAction.FARMLAND || mouseAction == MouseAction.ROAD)
        spriteframe = buildingTileSheet.Sprite(TexturePackerMonoGameDefinitions.buildingAtlas.SmallSelect);

      foreach (Tile tile in tileMap)
        if (tile != null && tile.Rect.Contains(mousePosition)) {
          if (spriteframe != null)
            spriteRender.Draw(spriteframe, new Vector2(tile.Rect.X, tile.Rect.Y));
        }
    }

    public static void DrawBuildings(SpriteRender spriteRender, List<Building> buildings) {
      foreach (Building building in buildings) {
        spriteRender.Draw(building.SpriteFrame, building.Position);
      }
    }

    public static void DrawRoads(SpriteRender spriteRender, Road[,] roads) {
      foreach (Road road in roads) {
        if (road != null)
          spriteRender.Draw(road.SpriteFrame, road.Coords);
      }
    }

    public static void DrawMenu(SpriteRender spriteRender, List<Menu> menu) {
      // TODO: Muss noch skaliert werden für verschiedene auflösungen
      SpriteFrame spriteFrame = null;
      foreach (Menu item in menu) {
        if (item.Type == Menu.MenuType.MAIN)
          spriteFrame = menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Maincontrols);
        else if (item.Type == Menu.MenuType.BUTTON_HOUSE)
          spriteFrame = menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Buttonhouse);
        else if (item.Type == Menu.MenuType.BUTTON_FARMLAND)
          spriteFrame = menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Buttonfarmland);
        else if (item.Type == Menu.MenuType.BUTTON_ROAD)
          spriteFrame = menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Buttonfarmland); // TODO add road button in spritesheet
        else if (item.Type == Menu.MenuType.BUTTON_TOWNHALL)
          spriteFrame = menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Buttontownhall);

        if (spriteFrame != null)
          spriteRender.Draw(spriteFrame, item.MainPos);
      }
    }

    public static void GetRenderableTilesAndCenterTile(out int startCol, out int endCol, out int startRow, out int endRow) {
      var width = graphics.GraphicsDevice.Viewport.Width;
      var height = graphics.GraphicsDevice.Viewport.Height;
      Rectangle renderTangle = new Rectangle((int)Math.Ceiling(cam.Pos.X) - width, (int)Math.Ceiling(cam.Pos.Y) - height, width * 2, height * 2);

      startCol = (int)(renderTangle.X / tileSize) - 3 < 1 ? 0 : (int)(renderTangle.X / tileSize) - 3;
      endCol = (int)((renderTangle.X + renderTangle.Width) / tileSize) + 3;
      startRow = (int)(renderTangle.Y / tileSize) - 3 < 0 ? 0 : (int)(renderTangle.Y / tileSize) - 3;
      endRow = (int)((renderTangle.Y + renderTangle.Height) / tileSize + 3);
    }

    internal static void DrawInteractives(SpriteRender spriteRender, IOrderedEnumerable<Interactive> interactives) {
      if (interactives != null)
        foreach (Interactive interactive in interactives) {
          // Only draw interactives with health bigger than 0 - it is harvested if health is <= 0
          if (interactive.Type == Interactive.IntType.TREE && interactive.Health > 0)
            spriteRender.Draw(interactiveTileSheet.Sprite(TexturePackerMonoGameDefinitions.interactiveAtlas.Tree), interactive.Coords);
        }
    }
  }
}
