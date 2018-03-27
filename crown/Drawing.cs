﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexturePackerLoader;
using static crown.Game1;

namespace crown {
    class Drawing {
        public static void drawTerrain(SpriteRender spriteRender) {
            int startCol, endCol, startRow, endRow;
            GetRenderableTilesAndCenterTile(out startCol, out endCol, out startRow, out endRow);
            for (int x = startCol; x < endCol && x < tileMap.GetUpperBound(0); x++)
                for (int y = startRow; y < endRow && y < tileMap.GetUpperBound(1); y++) {
                    Vector2 coord = new Vector2(x * tileSize, y * tileSize);
                    spriteRender.Draw(mapTileSheet.Sprite(tileMap[x, y].Type), coord);
                }
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
