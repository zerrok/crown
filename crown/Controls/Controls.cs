using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using crown.Terrain;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace crown {
  class Controls {

    public static void MakeFarmableLand(Tile[,] tileMap, Tile tile) {
      int tileX = tile.Rect.X / 32;
      int tileY = tile.Rect.Y / 32;

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
        if (cam.Zoom >= 1f)
          cam.Zoom -= 0.05f;
        if (cam.Zoom < 1f)
          cam.Zoom = 1f;
      }
      if (Keyboard.GetState().IsKeyDown(Keys.M)) {
        if (cam.Zoom <= 3f)
          cam.Zoom += 0.05f;
      }
    }

  }
}
