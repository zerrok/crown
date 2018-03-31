using Microsoft.Xna.Framework;
using System;
using TexturePackerLoader;
using static crown.Game1;
using System.Collections.Generic;
using crown.Terrain;

namespace crown {
  class Drawing {

    public static void DrawTerrain(SpriteRender spriteRender) {
      int startCol, endCol, startRow, endRow;
      GetRenderableTilesAndCenterTile(out startCol, out endCol, out startRow, out endRow);
      for (int x = startCol; x < endCol && x < tileMap.GetUpperBound(0); x++)
        for (int y = startRow; y < endRow && y < tileMap.GetUpperBound(1); y++) {
          int yPos = tileMap[x, y].Type != TexturePackerMonoGameDefinitions.texturePackerSpriteAtlas.Stone1 ? y * tileSize : y * tileSize - 64;
          int xPos = x * tileSize;
          Vector2 coord = new Vector2(xPos, yPos);
          spriteRender.Draw(mapTileSheet.Sprite(tileMap[x, y].Type), coord);
        }
    }

    public static void DrawMouseSelection(SpriteRender spriteRender, Vector2 mousePosition, MouseAction mouseAction) {
      SpriteFrame spriteframe = null;

      if (mouseAction == MouseAction.TOWNHALL)
        spriteframe = buildingTileSheet.Sprite(TexturePackerMonoGameDefinitions.buildingAtlas.Townhall);
      if (mouseAction == MouseAction.HOUSE)
        spriteframe = buildingTileSheet.Sprite(TexturePackerMonoGameDefinitions.buildingAtlas.House);
      if (mouseAction == MouseAction.FARMLAND)
        spriteframe = mapTileSheet.Sprite(TexturePackerMonoGameDefinitions.texturePackerSpriteAtlas.DirtGrassAll);

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

    public static void DrawMenu(SpriteRender spriteRender, Menu menu) {
      // TODO: Muss noch skaliert werden für verschiedene auflösungen
      spriteRender.Draw(menu.MainControls, menu.MainPos);
      spriteRender.Draw(menu.ButtonTownHall, menu.HallPos);
      spriteRender.Draw(menu.ButtonHouse, menu.HousePos);
      spriteRender.Draw(menu.ButtonFarmland, menu.FarmlandPos);
    }

    public static void GetRenderableTilesAndCenterTile(out int startCol, out int endCol, out int startRow, out int endRow) {
      var width = graphics.GraphicsDevice.Viewport.Width;
      var height = graphics.GraphicsDevice.Viewport.Height;
      Rectangle renderTangle = new Rectangle((int)Math.Ceiling(cam.Pos.X) - width / 2, (int)Math.Ceiling(cam.Pos.Y) - height / 2, width, height);

      startCol = (int)(renderTangle.X / tileSize) - 3 < 1 ? 0 : (int)(renderTangle.X / tileSize) - 3;
      endCol = (int)((renderTangle.X + renderTangle.Width) / tileSize) + 3;
      startRow = (int)(renderTangle.Y / tileSize) - 3 < 0 ? 0 : (int)(renderTangle.Y / tileSize) - 3;
      endRow = (int)((renderTangle.Y + renderTangle.Height) / tileSize + 3);
    }

    internal static void DrawInteractives(SpriteRender spriteRender, List<Interactive> interactives) {
      foreach (Interactive interactive in interactives) {
        if (interactive.Type == Interactive.IntType.TREE)
          spriteRender.Draw(interactiveTileSheet.Sprite(TexturePackerMonoGameDefinitions.interactiveAtlas.Tree), interactive.Coords);
      }
    }
  }
}
