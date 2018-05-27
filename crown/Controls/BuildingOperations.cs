using crown.Buildings;
using crown.Terrain;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using TexturePackerLoader;
using static crown.Game1;

namespace crown {
    public class BuildingOperations {

        public static void BuildRoad(Tile tile, bool isFirstRoad) {
            bool isAllowed = true;

            if (tile.IsClear) {
                Rectangle rect = new Rectangle(tile.Rect.X, tile.Rect.Y, tileSize, tileSize);

                if (!isFirstRoad)
                    isAllowed = IsBesidesRoad(isAllowed, rect, true);

                if (isAllowed && mechanics.Gold - 5 >= 0) {
                    RemoveIntersectingTrees(rect);

                    Road road = new Road(new Vector2(tile.Rect.X, tile.Rect.Y), new Rectangle(rect.X, rect.Y, rect.Width, rect.Height));

                    // Use standard road texture
                    SpriteFrame spriteFrame = mapTileSheet.Sprite(TexturePackerMonoGameDefinitions.texturePackerSpriteAtlas.StreetHori);

                    // When everything is set and done we just add the new road
                    road.SpriteFrame = spriteFrame;
                    roads[tile.Rect.X / tileSize, tile.Rect.Y / tileSize] = road;
                    tile.IsClear = false;

                    ReplaceRoads(tile, spriteFrame);

                    // Subtract roadCost
                    mechanics.Gold -= 5;
                }
            }
        }

        public static void BuildTownHall(Tile tile, Costs costs) {
            bool isAllowed = true;
            Rectangle rectangle = new Rectangle(tile.Rect.X, tile.Rect.Y, tileSize * 2, tileSize * 2);
            int tilePosX = (tile.Rect.X) / tileSize + 1;
            int tilePosY = (tile.Rect.Y) / tileSize + 1;

            isAllowed = BuildingOperations.CheckIntersections(isAllowed, rectangle);

            if (tile.Type.Contains("grass")
                && tileMap[tilePosX, tile.Rect.Y / tileSize].IsClear
                && tileMap[tilePosX, tilePosY].IsClear
                && tileMap[tile.Rect.X / tileSize, tilePosY].IsClear
                && tileMap[tile.Rect.X / tileSize, tilePosY + 1].IsClear
                && tileMap[tilePosX, tilePosY + 1].IsClear
                && isAllowed
                && tile.IsClear) {
                mechanics.Buildings.Add(new Townhall(buildingTileSheet.Sprite(TexturePackerMonoGameDefinitions.buildingAtlas.LargeSelect)
                                      , new Vector2(tile.Rect.X, tile.Rect.Y)
                                      , rectangle
                                      , Building.BuildingTypes.Townhall
                                      , costs
                                      ));

                // Set tiles underneath to not clear
                tileMap[tilePosX, tile.Rect.Y / tileSize].IsClear = false;
                tileMap[tilePosX, tilePosY].IsClear = false;
                tileMap[tile.Rect.X / tileSize, tilePosY].IsClear = false;
                tile.IsClear = false;

                // Add the first road pieces
                BuildRoad(tileMap[tile.Rect.X / tileSize, tilePosY + 1], true);
                BuildRoad(tileMap[tilePosX, tilePosY + 1], true);

                // Unlocks the first menu tier
                MenuBuilder.MenuTierOne();
                RemoveIntersectingTrees(rectangle);
            }
        }

        internal static void BuildQuarry(Tile tile, Building.BuildingTypes type, Costs costs) {
            foreach (Interactive inter in interactives) {
                if (inter.Type == Interactive.IntType.STONE) {
                    if (new Rectangle(tile.Rect.X, tile.Rect.Y, tileSize * 2, tileSize * 2).Intersects(inter.Rect)) {
                        BuildLargeBuilding(tile, type, Costs.QuarryCosts());
                        break;
                    }
                }
            }
        }

        public static void BuildLargeBuilding(Tile tile, Building.BuildingTypes type, Costs costs) {
            bool isAllowed = true;
            Rectangle rectangle = new Rectangle(tile.Rect.X, tile.Rect.Y, tileSize * 2, tileSize * 2);
            int tilePosX = (tile.Rect.X) / tileSize + 1;
            int tilePosY = (tile.Rect.Y) / tileSize + 1;

            isAllowed = IsBesidesRoad(isAllowed, rectangle, false);
            isAllowed = CheckCosts(costs, isAllowed);

            if (type != Building.BuildingTypes.Quarry)
                isAllowed = CheckIntersections(isAllowed, rectangle);

            if (tile.Type.Contains("grass")
                && isAllowed
                && ((tile.IsClear
                && tileMap[tilePosX, tile.Rect.Y / tileSize].IsClear
                && tileMap[tilePosX, tilePosY].IsClear
                && tileMap[tile.Rect.X / tileSize, tilePosY].IsClear)
                || type == Building.BuildingTypes.Quarry)) {
                mechanics.Buildings.Add(GetLargeBuilding(buildingTileSheet.Sprite(TexturePackerMonoGameDefinitions.buildingAtlas.LargeSelect)
                                      , new Vector2(tile.Rect.X, tile.Rect.Y)
                                      , rectangle
                                      , type
                                      , costs
                                      ));

                // Set tiles underneath to not clear
                tileMap[tilePosX, tile.Rect.Y / tileSize].IsClear = false;
                tileMap[tilePosX, tilePosY].IsClear = false;
                tileMap[tile.Rect.X / tileSize, tilePosY].IsClear = false;
                tile.IsClear = false;

                RemoveIntersectingTrees(rectangle);
            }
        }

        public static void BuildSmallBuilding(Tile tile, Building.BuildingTypes type, Costs costs) {
            bool isAllowed = true;
            Rectangle rectangle = new Rectangle(tile.Rect.X, tile.Rect.Y, tileSize, tileSize);

            isAllowed = IsBesidesRoad(isAllowed, rectangle, true);
            isAllowed = CheckCosts(costs, isAllowed);

            if (tile.Type.Contains("grass")
              && isAllowed
              && tile.IsClear == true) {
                mechanics.Buildings.Add(GetSmallBuilding(buildingTileSheet.Sprite(TexturePackerMonoGameDefinitions.buildingAtlas.SmallSelect)
                                      , new Vector2(tile.Rect.X, tile.Rect.Y)
                                      , rectangle
                                      , type
                                      , costs));
                tile.IsClear = false;

                RemoveIntersectingTrees(rectangle);
            }
        }

        private static Building GetLargeBuilding(SpriteFrame spriteFrame, Vector2 pos, Rectangle rect, Building.BuildingTypes type, Costs costs) {
            if (type == Building.BuildingTypes.Farm)
                return new Farm(spriteFrame, pos, rect, type, costs);
            if (type == Building.BuildingTypes.Scientist)
                return new Scientist(spriteFrame, pos, rect, type, costs);
            if (type == Building.BuildingTypes.Quarry)
                return new Quarry(spriteFrame, pos, rect, type, costs);

            return null;
        }

        private static Building GetSmallBuilding(SpriteFrame spriteFrame, Vector2 pos, Rectangle rect, Building.BuildingTypes type, Costs costs) {
            if (type == Building.BuildingTypes.House)
                return new House(spriteFrame, pos, rect, type, costs);
            if (type == Building.BuildingTypes.Woodcutter)
                return new Woodcutter(spriteFrame, pos, rect, type, costs);

            return null;
        }
        public static bool CheckCosts(Costs costs, bool isAllowed) {
            // Check if the costs are okay
            if (mechanics.Gold + costs.Gold < 0 ||
                mechanics.Stone + costs.Stone < 0 ||
                mechanics.Wood + costs.Wood < 0 ||
                mechanics.Workers + costs.Workers < 0)
                isAllowed = false;
            return isAllowed;
        }

        public static void RemoveIntersectingTrees(Rectangle rectangle) {
            List<Interactive> interactivesToRemove = new List<Interactive>();
            foreach (Interactive inter in interactives) {
                if (inter.Rect.Intersects(rectangle) && inter.Type == Interactive.IntType.TREE) {
                    interactivesToRemove.Add(inter);
                }
            }
            foreach (Interactive inter in interactivesToRemove)
                interactives.Remove(interactives.Find(interactive => interactive.Equals(inter)));
        }

        public static bool CheckIntersections(bool isAllowed, Rectangle rectangle) {
            foreach (Building building in mechanics.Buildings) {
                if (building.Rect.Intersects(rectangle))
                    isAllowed = false;
            }
            foreach (Interactive inter in interactives) {
                // Trees may be intersected and will be removed
                if (inter.Rect.Intersects(rectangle) && inter.Type != Interactive.IntType.TREE)
                    isAllowed = false;
            }

            return isAllowed;
        }

        public static bool IsBesidesRoad(bool isAllowed, Rectangle rectangle, bool isSmall) {
            int xPos = rectangle.X / tileSize;
            int yPos = rectangle.Y / tileSize;
            if (isSmall) {
                if (roads[xPos + 1, yPos] == null
                 && roads[xPos - 1, yPos] == null
                 && roads[xPos, yPos + 1] == null
                 && roads[xPos, yPos - 1] == null)
                    isAllowed = false;
            } else {
                if (roads[xPos + 2, yPos] == null
                 && roads[xPos - 1, yPos] == null
                 && roads[xPos, yPos + 2] == null
                 && roads[xPos, yPos - 1] == null
                 && roads[xPos + 2, yPos + 1] == null
                 && roads[xPos - 1, yPos + 1] == null
                 && roads[xPos + 1, yPos + 2] == null
                 && roads[xPos + 1, yPos - 1] == null)
                    isAllowed = false;
            }
            isAllowed = RoadsBuilt(isAllowed);

            return isAllowed;
        }

        public static bool RoadsBuilt(bool isAllowed) {
            // Everything except the town hall has to be connected to roads
            // So check if there even are roads
            bool foundRoad = false;
            foreach (Road road in roads) {
                if (road != null)
                    foundRoad = true;
            }
            if (foundRoad == false)
                isAllowed = false;
            return isAllowed;
        }

        public static void ReplaceRoads(Tile tile, SpriteFrame spriteFrame) {
            if (roads != null) {
                foreach (Road road in roads) {
                    if (road != null) {
                        int x = (int)road.Coords.X / tileSize;
                        int y = (int)road.Coords.Y / tileSize;
                        // Do not build a new road when there already is one
                        if (roads[x, y] != null) {
                            // Left and right
                            if (roads[x + 1, y] != null || roads[x - 1, y] != null) {
                                spriteFrame = mapTileSheet.Sprite(TexturePackerMonoGameDefinitions.texturePackerSpriteAtlas.StreetHori);
                            }
                            // Up and Down
                            if (roads[x, y + 1] != null || roads[x, y - 1] != null) {
                                spriteFrame = mapTileSheet.Sprite(TexturePackerMonoGameDefinitions.texturePackerSpriteAtlas.StreetVerti);
                            }

                            // Left and Down
                            if (roads[x - 1, y] != null && roads[x, y + 1] != null) {
                                spriteFrame = mapTileSheet.Sprite(TexturePackerMonoGameDefinitions.texturePackerSpriteAtlas.StreetDownLeft);
                            }

                            // Left and Up
                            if (roads[x - 1, y] != null && roads[x, y - 1] != null) {
                                spriteFrame = mapTileSheet.Sprite(TexturePackerMonoGameDefinitions.texturePackerSpriteAtlas.StreetUpLeft);
                            }

                            // Right and Up
                            if (roads[x + 1, y] != null && roads[x, y - 1] != null) {
                                spriteFrame = mapTileSheet.Sprite(TexturePackerMonoGameDefinitions.texturePackerSpriteAtlas.StreetUpRight);
                            }

                            // Right and down
                            if (roads[x + 1, y] != null && roads[x, y + 1] != null) {
                                spriteFrame = mapTileSheet.Sprite(TexturePackerMonoGameDefinitions.texturePackerSpriteAtlas.StreetDownRight);
                            }

                            // Right down left
                            if (roads[x + 1, y] != null && roads[x, y + 1] != null && roads[x - 1, y] != null) {
                                spriteFrame = mapTileSheet.Sprite(TexturePackerMonoGameDefinitions.texturePackerSpriteAtlas.StreetLeftRightDown);
                            }

                            // Right up left
                            if (roads[x + 1, y] != null && roads[x, y - 1] != null && roads[x - 1, y] != null) {
                                spriteFrame = mapTileSheet.Sprite(TexturePackerMonoGameDefinitions.texturePackerSpriteAtlas.StreetUpRightLeft);
                            }

                            // Right up down
                            if (roads[x + 1, y] != null && roads[x, y - 1] != null && roads[x, y + 1] != null) {
                                spriteFrame = mapTileSheet.Sprite(TexturePackerMonoGameDefinitions.texturePackerSpriteAtlas.StreetUpRightDown);
                            }

                            // left up down
                            if (roads[x - 1, y] != null && roads[x, y - 1] != null && roads[x, y + 1] != null) {
                                spriteFrame = mapTileSheet.Sprite(TexturePackerMonoGameDefinitions.texturePackerSpriteAtlas.StreetLeftUpDown);
                            }

                            // cross
                            if (roads[x - 1, y] != null && roads[x, y + 1] != null && roads[x, y - 1] != null && roads[x + 1, y] != null) {
                                spriteFrame = mapTileSheet.Sprite(TexturePackerMonoGameDefinitions.texturePackerSpriteAtlas.StreetCross);
                            }

                            road.SpriteFrame = spriteFrame;
                            roads[x, y] = road;
                        }
                    }
                }
            }
        }

    }
}
