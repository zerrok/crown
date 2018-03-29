﻿using Microsoft.Xna.Framework;
using System;
using TexturePackerLoader;
using static crown.Game1;

namespace crown {
  class Drawing {

    public static void drawTerrain(SpriteRender spriteRender) {
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

    public static void drawMouseSelection(SpriteRender spriteRender, Vector2 mousePosition, MouseAction mouseAction) {
      SpriteFrame spriteframe = null;

      if (mouseAction == MouseAction.TOWNHALL)
        spriteframe = buildingTileSheet.Sprite(TexturePackerMonoGameDefinitions.buildingAtlas.Townhall);
      if (mouseAction == MouseAction.HOUSE)
        spriteframe = buildingTileSheet.Sprite(TexturePackerMonoGameDefinitions.buildingAtlas.House);

      if (spriteframe != null)
        spriteRender.Draw(spriteframe, mousePosition);

    }

    public static void drawMenu(SpriteRender spriteRender, Menu menu) {
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

  }
}
