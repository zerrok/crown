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
                    int yPos = tileMap[x, y].Type != TexturePackerMonoGameDefinitions.texturePackerSpriteAtlas.Stone1 ? y * tileSize : y * tileSize - tileSize;
                    int xPos = x * tileSize;
                    spriteRender.Draw(mapTileSheet.Sprite(tileMap[x, y].Type), new Vector2(xPos, yPos));
                }
        }

        public static void DrawMouseSelection(SpriteRender spriteRender, Vector2 mousePosition, MouseAction mouseAction) {
            SpriteFrame spriteframe = null;

            if (mouseAction == MouseAction.TOWNHALL || mouseAction == MouseAction.FARM)
                spriteframe = buildingTileSheet.Sprite(TexturePackerMonoGameDefinitions.buildingAtlas.LargeSelect);
            if (mouseAction == MouseAction.HOUSE|| mouseAction == MouseAction.ROAD)
                spriteframe = buildingTileSheet.Sprite(TexturePackerMonoGameDefinitions.buildingAtlas.SmallSelect);

            foreach (Tile tile in tileMap)
                if (tile != null && tile.Rect.Contains(mousePosition)) {
                    if (spriteframe != null)
                        spriteRender.Draw(spriteframe, new Vector2(tile.Rect.X, tile.Rect.Y));
                }
        }

        public static void DrawBuildings(SpriteRender spriteRender, List<Building> buildings) {
            foreach (Building building in buildings) {
                int offset = 0;
                if (building.SpriteFrame != buildingTileSheet.Sprite(TexturePackerMonoGameDefinitions.buildingAtlas.SmallSelect)
                 && building.SpriteFrame != buildingTileSheet.Sprite(TexturePackerMonoGameDefinitions.buildingAtlas.LargeSelect))
                    offset = tileSize / 4;

                spriteRender.Draw(building.SpriteFrame, new Vector2(building.Position.X, building.Position.Y - offset));
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
                    spriteFrame = menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Buttonroad);
                else if (item.Type == Menu.MenuType.BUTTON_TOWNHALL)
                    spriteFrame = menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Buttontownhall);
                else if (item.Type == Menu.MenuType.BUTTON_FARM)
                    spriteFrame = menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Buttonfarm);
                else if (item.Type == Menu.MenuType.BUTTON_STORAGE)
                    spriteFrame = menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Buttonstorage);
                else if (item.Type == Menu.MenuType.BUTTON_WOODCUTTER)
                    spriteFrame = menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Buttonwoodcutter);
                if (spriteFrame != null)

                    spriteRender.Draw(spriteFrame, item.MainPos);
            }
        }

        public static void GetRenderableTilesAndCenterTile(out int startCol, out int endCol, out int startRow, out int endRow) {
            var width = graphics.GraphicsDevice.Viewport.Width;
            var height = graphics.GraphicsDevice.Viewport.Height;
            Rectangle renderTangle = new Rectangle((int)Math.Ceiling(cam.Pos.X) - width, (int)Math.Ceiling(cam.Pos.Y) - height, width * 2, height * 2);
            // Offset of tiles rendered off screen (when zoom == 1f)
            const int offset = 24;
            startCol = renderTangle.X / tileSize - offset < 1 ? 0 : renderTangle.X / tileSize - offset;
            endCol = (renderTangle.X + renderTangle.Width) / tileSize + offset;
            startRow = renderTangle.Y / tileSize - offset < 0 ? 0 : renderTangle.Y / tileSize - offset;
            endRow = (renderTangle.Y + renderTangle.Height) / tileSize + offset;
        }

        internal static void DrawInteractives(SpriteRender spriteRender, List<Interactive> interactives) {
            if (interactives != null) {
                GetRenderableTilesAndCenterTile(out int startCol, out int endCol, out int startRow, out int endRow);

                foreach (Interactive interactive in interactives) {
                    // Only draw if its visible
                    if (interactive.Coords.X / tileSize > startCol &&
                       interactive.Coords.X / tileSize < endCol &&
                       interactive.Coords.Y / tileSize > startRow &&
                       interactive.Coords.Y / tileSize < endRow)
                        // Only draw interactives with health bigger than 0 - it is harvested if health is <= 0
                        if (interactive.Type == Interactive.IntType.TREE && interactive.Health > 0)
                            spriteRender.Draw(interactive.SpriteFrame, interactive.Coords);
                }
            }
        }
    }
}
