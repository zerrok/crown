using Microsoft.Xna.Framework;
using System;
using TexturePackerLoader;
using static crown.Game1;
using System.Collections.Generic;
using crown.Terrain;
using Microsoft.Xna.Framework.Graphics;

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

        public static void DrawMouseSelection(SpriteRender spriteRender, Vector2 mousePosition, Actions mouseAction) {
            SpriteFrame spriteframe = null;

            if (mouseAction == Actions.Townhall || mouseAction == Actions.Farm || mouseAction == Actions.Scientist || mouseAction == Actions.Quarry || mouseAction == Actions.Brewery || mouseAction == Actions.Tavern || mouseAction == Actions.Storage)
                spriteframe = buildingTileSheet.Sprite(TexturePackerMonoGameDefinitions.buildingAtlas.LargeSelect);
            if (mouseAction == Actions.House || mouseAction == Actions.Road || mouseAction == Actions.Woodcutter)
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

        public static void DrawMainMenu(SpriteRender spriteRender, List<Button> menu) {
            foreach (Button button in menu) {
                if (button.SpriteFrame != null)
                    spriteRender.Draw(button.SpriteFrame, button.Pos);
            }
        }

        public static void DrawMenu(SpriteRender spriteRender, List<Button> menu, SpriteBatch spriteBatch, Actions mouseAction) {
            foreach (UIElement ui in uiElements) {
                if (ui != null) {
                    if (ui.Type == UIElement.ElementType.Resources) {
                        spriteRender.Draw(ui.SpriteFrame, ui.Pos);
                        spriteBatch.DrawString(font, ui.TextTop, new Vector2(ui.Pos.X + (ui.Resource1 != UIElement.Resource.Population && ui.Resource1 != UIElement.Resource.Workers ? 33 : 0), ui.Pos.Y + 4), Color.White);
                        spriteBatch.DrawString(font, ui.TextBottom, new Vector2(ui.Pos.X + (ui.Resource1 != UIElement.Resource.Population && ui.Resource1 != UIElement.Resource.Workers ? 33 : 0), ui.Pos.Y + 29), Color.White);
                    }
                    if (ui.Type == UIElement.ElementType.MenuSelection) {
                        // Draw infos for menu selection
                        if (mouseAction != Actions.Nothing) {
                            spriteRender.Draw(ui.SpriteFrame, ui.Pos);
                            spriteBatch.DrawString(font, ui.TextTop, new Vector2(ui.Pos.X + (ui.Resource1 != UIElement.Resource.Population && ui.Resource1 != UIElement.Resource.Workers ? 33 : 0), ui.Pos.Y + 4), Color.White);
                            spriteBatch.DrawString(font, ui.TextBottom, new Vector2(ui.Pos.X + (ui.Resource1 != UIElement.Resource.Population && ui.Resource1 != UIElement.Resource.Workers ? 33 : 0), ui.Pos.Y + 29), Color.White);

                        }
                    }
                    if (ui.Type == UIElement.ElementType.GameSelection) {
                        // Draw infos for selection
                        int menuYPosition = graphics.GraphicsDevice.Viewport.Height - 300;
                        if (selectedBuilding != null) {
                            spriteRender.Draw(ui.SpriteFrame, ui.Pos);
                            spriteBatch.DrawString(font, selectedBuilding.Name, new Vector2(ui.Pos.X + 10, ui.Pos.Y + 8), Color.White);
                            spriteBatch.DrawString(font, selectedBuilding.Description, new Vector2(ui.Pos.X + 10, ui.Pos.Y + 24), Color.White);
                        }
                    }
                }
            }
            foreach (Button button in menu) {
                if (button.SpriteFrame != null)
                    spriteRender.Draw(button.SpriteFrame, button.Pos);
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
                        if (interactive.Health > 0)
                            spriteRender.Draw(interactive.SpriteFrame, interactive.Coords);
                }
            }
        }

        internal static void DrawPeople(SpriteRender spriteRender, List<Citizen> citizens) {
            GetRenderableTilesAndCenterTile(out int startCol, out int endCol, out int startRow, out int endRow);
            if (citizens != null)
                foreach (Citizen citizen in citizens) {
                    if (citizen.Pos.X / tileSize > startCol &&
                        citizen.Pos.X / tileSize < endCol &&
                        citizen.Pos.Y / tileSize > startRow &&
                        citizen.Pos.Y / tileSize < endRow) {
                        spriteRender.Draw(citizen.SpriteFrame, citizen.Pos);
                    }

                }
        }

    }
}
