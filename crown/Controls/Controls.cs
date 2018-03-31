using crown.Terrain;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using static crown.Game1;

namespace crown {
  class Controls {

    public static void BuildTownHall(Tile tile) {
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
          && isAllowed) {
        buildings.Add(new Building(buildingTileSheet.Sprite(TexturePackerMonoGameDefinitions.buildingAtlas.Townhall)
                    , new Vector2(tile.Rect.X, tile.Rect.Y)
                    , rectangle));
      }
    }

    public static void BuildHouse(Tile tile) {
      bool isAllowed = true;
      Rectangle rectSmall = new Rectangle(tile.Rect.X, tile.Rect.Y, 64, 64);
      foreach (Building building in buildings) {
        if (building.Rect.Intersects(rectSmall))
          isAllowed = false;
      }

      if (tile.Type.Contains("grass") && isAllowed) {
        buildings.Add(new Building(buildingTileSheet.Sprite(TexturePackerMonoGameDefinitions.buildingAtlas.House)
                    , new Vector2(tile.Rect.X, tile.Rect.Y)
                    , rectSmall));
      }
    }

    public static void MakeFarmableLand(Tile[,] tileMap, Tile tile) {
      int tileX = tile.Rect.X / tileSize;
      int tileY = tile.Rect.Y / tileSize;

      // Make farmable land
      // Only allowed on grass and when grass or dirt is adjacent on all sides
      int upperX = tileX + 1 >= tileMap.GetUpperBound(0) - 1 ? tileMap.GetUpperBound(0) - 1 : tileX + 1;
      int lowerX = tileX - 1 < 1 ? 1 : tileX - 1;
      int upperY = tileY + 1 >= tileMap.GetUpperBound(1) - 1 ? tileMap.GetUpperBound(1) - 1 : tileY + 1;
      int lowerY = tileY - 1 < 1 ? 1 : tileY - 1;
      if (tile.Type.Contains("grass")
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

    public static void CameraControls(Camera2d cam, float camSpeed) {
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
