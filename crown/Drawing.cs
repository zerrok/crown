using Microsoft.Xna.Framework;
using System;
using TexturePackerLoader;
using static crown.Game1;
using System.Collections.Generic;
using crown.Terrain;
using System.Linq;
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

        public static void DrawMouseSelection(SpriteRender spriteRender, Vector2 mousePosition, MouseAction mouseAction) {
            SpriteFrame spriteframe = null;

            if (mouseAction == MouseAction.Townhall || mouseAction == MouseAction.Farm || mouseAction == MouseAction.Scientist)
                spriteframe = buildingTileSheet.Sprite(TexturePackerMonoGameDefinitions.buildingAtlas.LargeSelect);
            if (mouseAction == MouseAction.House || mouseAction == MouseAction.Road || mouseAction == MouseAction.Woodcutter || mouseAction == MouseAction.Quarry)
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

        public static void DrawMenu(SpriteRender spriteRender, List<Button> menu, SpriteBatch spriteBatch, MouseAction mouseAction) {
            foreach (UIElement element in uiElements) {
                if (element != null) {
                    if (element.Type == UIElement.ElementType.Resources) {
                        spriteRender.Draw(element.SpriteFrame, element.Pos);
                        // DRAW RESOURCES UI
                        // Population and workers
                        spriteBatch.DrawString(font, mechanics.Population + " / " + mechanics.MaxPop, new Vector2(10, 36), Color.White);
                        spriteBatch.DrawString(font, mechanics.Workers.ToString(), new Vector2(10, 84), Color.White);
                        // Gold
                        spriteBatch.DrawString(font, mechanics.Gold.ToString(), new Vector2(146, 11), Color.White);
                        spriteBatch.DrawString(font, mechanics.GoldDelta.ToString(), new Vector2(146, 36), Color.White);
                        // Wood
                        spriteBatch.DrawString(font, mechanics.Wood + " / " + mechanics.WoodStorage, new Vector2(313, 11), Color.White);
                        spriteBatch.DrawString(font, mechanics.WoodDelta.ToString(), new Vector2(313, 36), Color.White);
                        // Food
                        spriteBatch.DrawString(font, mechanics.Food + " / " + mechanics.FoodStorage, new Vector2(146, 60), Color.White);
                        spriteBatch.DrawString(font, mechanics.FoodDelta.ToString(), new Vector2(146, 84), Color.White);
                        // Stone
                        spriteBatch.DrawString(font, mechanics.Stone + " / " + mechanics.StoneStorage, new Vector2(313, 60), Color.White);
                        spriteBatch.DrawString(font, mechanics.StoneDelta.ToString(), new Vector2(313, 84), Color.White);
                    }
                    if (element.Type == UIElement.ElementType.MenuSelection) {
                        // Draw infos for menu selection
                        if (mouseAction != MouseAction.Nothing) {
                            spriteRender.Draw(element.SpriteFrame, element.Pos);

                            Costs costs = null;
                            if (mouseAction == MouseAction.House)
                                costs = Costs.HouseCosts();
                            if (mouseAction == MouseAction.Farm)
                                costs = Costs.FarmCosts();
                            if (mouseAction == MouseAction.Woodcutter)
                                costs = Costs.WoodcutterCosts();
                            if (mouseAction == MouseAction.Quarry)
                                costs = Costs.QuarryCosts();
                            if (mouseAction == MouseAction.Scientist)
                                costs = Costs.ScientistCosts();

                            if (costs != null) {
                                spriteBatch.DrawString(font, mouseAction.ToString(), new Vector2(element.Pos.X + 10, element.Pos.Y + 8), Color.White);
                                spriteBatch.DrawString(font, costs.Workers.ToString(), new Vector2(element.Pos.X + 10, element.Pos.Y + 190), Color.White);
                                spriteBatch.DrawString(font, costs.Gold.ToString(), new Vector2(element.Pos.X + 40, element.Pos.Y + 210), Color.White);
                                spriteBatch.DrawString(font, costs.GoldUpkeep.ToString(), new Vector2(element.Pos.X + 40, element.Pos.Y + 234), Color.White);
                                spriteBatch.DrawString(font, costs.Wood.ToString(), new Vector2(element.Pos.X + 146, element.Pos.Y + 210), Color.White);
                                spriteBatch.DrawString(font, costs.WoodUpkeep.ToString(), new Vector2(element.Pos.X + 146, element.Pos.Y + 234), Color.White);
                                spriteBatch.DrawString(font, costs.Stone.ToString(), new Vector2(element.Pos.X + 40, element.Pos.Y + 260), Color.White);
                                spriteBatch.DrawString(font, costs.StoneUpkeep.ToString(), new Vector2(element.Pos.X + 40, element.Pos.Y + 283), Color.White);
                                spriteBatch.DrawString(font, costs.Food.ToString(), new Vector2(element.Pos.X + 146, element.Pos.Y + 260), Color.White);
                                spriteBatch.DrawString(font, costs.FoodUpkeep.ToString(), new Vector2(element.Pos.X + 146, element.Pos.Y + 283), Color.White);
                            }
                        }
                    }
                    if (element.Type == UIElement.ElementType.GameSelection) {
                        // Draw infos for selection
                        int menuYPosition = graphics.GraphicsDevice.Viewport.Height - 300;
                        if (selectedBuilding != null) {
                            spriteRender.Draw(element.SpriteFrame, element.Pos);
                            spriteBatch.DrawString(font, selectedBuilding.Name, new Vector2(element.Pos.X + 10,element.Pos.Y + 8), Color.White);
                            spriteBatch.DrawString(font, selectedBuilding.Description, new Vector2(element.Pos.X + 10, element.Pos.Y + 24), Color.White);
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
