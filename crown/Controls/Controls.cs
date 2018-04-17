﻿using crown.Terrain;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using TexturePackerLoader;
using static crown.Game1;

namespace crown
{
    class Controls
    {

        public static void BuildRoad(Tile tile)
        {
            if (tile.IsClear) {
                Road road = new Road(new Vector2(tile.Rect.X, tile.Rect.Y), new Rectangle(tile.Rect.X, tile.Rect.Y, tileSize, tileSize));

                // Use standard road texture
                SpriteFrame spriteFrame = mapTileSheet.Sprite(TexturePackerMonoGameDefinitions.texturePackerSpriteAtlas.StreetHori);

                // When everyrhing is set and done we just add the new road
                road.SpriteFrame = spriteFrame;
                roads[tile.Rect.X / tileSize, tile.Rect.Y / tileSize] = road;
                tile.IsClear = false;

                ReplaceRoads(tile, spriteFrame);
            }

        }

        private static void ReplaceRoads(Tile tile, SpriteFrame spriteFrame)
        {
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

        public static void BuildTownHall(Tile tile)
        {
            bool isAllowed = true;
            Rectangle rectangle = new Rectangle(tile.Rect.X, tile.Rect.Y, 128, 128);
            int tilePosX = (tile.Rect.X) / tileSize + 1;
            int tilePosY = (tile.Rect.Y) / tileSize + 1;

            foreach (Building building in buildings) {
                if (building.Rect.Intersects(rectangle))
                    isAllowed = false;
            }

            if (tile.Type.Contains("grass")
                && tileMap[tilePosX, tile.Rect.Y / tileSize].Type.Contains("grass")
                && tileMap[tilePosX, tilePosY].Type.Contains("grass")
                && tileMap[tile.Rect.X / tileSize, tilePosY].Type.Contains("grass")
                && isAllowed
                && tile.IsClear == true) {
                buildings.Add(new Building(buildingTileSheet.Sprite(TexturePackerMonoGameDefinitions.buildingAtlas.Townhall)
                            , new Vector2(tile.Rect.X, tile.Rect.Y)
                            , rectangle));
            }
        }

        public static void BuildHouse(Tile tile)
        {
            bool isAllowed = true;
            Rectangle rectSmall = new Rectangle(tile.Rect.X, tile.Rect.Y, 64, 64);
            foreach (Building building in buildings) {
                if (building.Rect.Intersects(rectSmall))
                    isAllowed = false;
            }

            if (tile.Type.Contains("grass")
              && isAllowed
              && tile.IsClear == true) {
                buildings.Add(new Building(buildingTileSheet.Sprite(TexturePackerMonoGameDefinitions.buildingAtlas.House)
                            , new Vector2(tile.Rect.X, tile.Rect.Y)
                            , rectSmall));
            }
        }

        public static void MakeFarmableLand(Tile[,] tileMap, Tile tile)
        {
            int tileX = tile.Rect.X / tileSize;
            int tileY = tile.Rect.Y / tileSize;

            // Make farmable land
            // Only allowed on grass and when grass or dirt is adjacent on all sides
            int upperX = tileX + 1 >= tileMap.GetUpperBound(0) - 1 ? tileMap.GetUpperBound(0) - 1 : tileX + 1;
            int lowerX = tileX - 1 < 1 ? 1 : tileX - 1;
            int upperY = tileY + 1 >= tileMap.GetUpperBound(1) - 1 ? tileMap.GetUpperBound(1) - 1 : tileY + 1;
            int lowerY = tileY - 1 < 1 ? 1 : tileY - 1;
            if (tile.Type.Contains("grass")
              && tile.IsClear == true
              && tileX != 0
              && tileY != 0
              && (tileMap[upperX, tileY].Type.Contains("grass") || tileMap[upperX, tileY].Type.Contains("dirt")) && !tileMap[upperX, tileY].Type.Contains("tone")
              && (tileMap[lowerX, tileY].Type.Contains("grass") || tileMap[lowerX, tileY].Type.Contains("dirt")) && !tileMap[lowerX, tileY].Type.Contains("tone")
              && (tileMap[tileX, upperY].Type.Contains("grass") || tileMap[tileX, upperY].Type.Contains("dirt")) && !tileMap[tileX, upperY].Type.Contains("tone")
              && (tileMap[tileX, lowerY].Type.Contains("grass") || tileMap[tileX, lowerY].Type.Contains("dirt")) && !tileMap[tileX, lowerY].Type.Contains("tone")) {
                tile.Type = TexturePackerMonoGameDefinitions.texturePackerSpriteAtlas.Dirt1;
                MapGenerator.AddDirtBorders(tileMap);
            }

        }

        public static void CameraControls(Camera2d cam, float camSpeed)
        {
            // Camera Movement
            if (Keyboard.GetState().IsKeyDown(Keys.S)) {
                cam.Move(new Vector2(0, camSpeed));
            }
            if (Keyboard.GetState().IsKeyDown(Keys.W)) {
                cam.Move(new Vector2(0, -camSpeed));
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A)) {
                cam.Move(new Vector2(-camSpeed, 0));
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D)) {
                cam.Move(new Vector2(camSpeed, 0));
            }
            if (Keyboard.GetState().IsKeyDown(Keys.N)) {
                if (cam.Zoom >= 0.5f)
                    cam.Zoom -= 0.05f;
                if (cam.Zoom < 0.5f)
                    cam.Zoom = 0.5f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.M)) {
                if (cam.Zoom <= 3f)
                    cam.Zoom += 0.05f;
            }
        }

    }
}
