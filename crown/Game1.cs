using crown.Terrain;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using TexturePackerLoader;

namespace crown {
  public class Game1 : Game {

    public static GraphicsDeviceManager graphics;
    SpriteBatch spriteBatch;

    // player camera
    public static Camera2d cam = new Camera2d();
    public float camSpeed = 10f;

    // Textures
    public static SpriteRender spriteRender;
    public static SpriteSheet mapTileSheet;
    public static int tileSize = 32;
    public static Tile[,] tileMap;

    // Seed for Random Generation
    public static Random random = new Random();

    public Game1() {
      graphics = new GraphicsDeviceManager(this) {
        IsFullScreen = false,
        PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width,
        PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height
      };
      Window.IsBorderless = true;
      Content.RootDirectory = "Content";
    }

    protected override void Initialize() {
      base.Initialize();

      IsMouseVisible = true;

      cam.Pos = new Vector2(500, 500);
      cam.Zoom = 1f;

      tileMap = new Tile[1, 1];
    }

    protected override void LoadContent() {
      // Load Spritebatch and SpriteRender for spritesheets
      spriteBatch = new SpriteBatch(GraphicsDevice);
      spriteRender = new SpriteRender(spriteBatch);

      var spriteSheetLoader = new SpriteSheetLoader(this.Content);
      mapTileSheet = spriteSheetLoader.Load("tiles/tileAtlas");
    }

    protected override void UnloadContent() {
      Content.Unload();
    }

    protected override void Update(GameTime gameTime) {
      // Exit the game
      if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape)) {
        UnloadContent();
        Exit();
      }

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
      if (Keyboard.GetState().IsKeyDown(Keys.Q)) {
        // TODO: Auslagern in Menü 
        tileMap = new MapGenerator().GetMap(250, 250);
      }

      // Mouse Controls
      MouseState mouseState;
      Vector2 mousePositionInWorld;
      GetMouseState(out mouseState, out mousePositionInWorld);

      // Tile interaction
      if (mouseState.LeftButton == ButtonState.Pressed) {
        foreach (Tile tile in tileMap)
          if (tile != null && tile.Rect.Contains(mousePositionInWorld)) {
            int tileX = tile.Rect.X / 32;
            int tileY = tile.Rect.Y / 32;

            // Make farmable land
            int upperX = tileX + 1 >= tileMap.GetUpperBound(0) - 1 ? tileMap.GetUpperBound(0) - 1 : tileX + 1;
            int lowerX = tileX - 1 < 1 ? 1 : tileX - 1;
            int upperY = tileY + 1 >= tileMap.GetUpperBound(1) - 1 ? tileMap.GetUpperBound(1) - 1 : tileY + 1;
            int lowerY = tileY - 1 < 1 ? 1 : tileY - 1;
            if (tile.Type.Contains("grass")
              && tileX != 0
              && tileY != 0
              && (tileMap[upperX, tileY].Type.Contains("grass") || tileMap[upperX, tileY].Type.Contains("dirt"))&& !tileMap[upperX, tileY].Type.Contains("tone")
              && (tileMap[lowerX, tileY].Type.Contains("grass") || tileMap[lowerX, tileY].Type.Contains("dirt"))&& !tileMap[lowerX, tileY].Type.Contains("tone")
              && (tileMap[tileX, upperY].Type.Contains("grass") || tileMap[tileX, upperY].Type.Contains("dirt"))&& !tileMap[tileX, upperY].Type.Contains("tone")
              && (tileMap[tileX, lowerY].Type.Contains("grass") || tileMap[tileX, lowerY].Type.Contains("dirt"))&& !tileMap[tileX, lowerY].Type.Contains("tone")) {
              tile.Type = TexturePackerMonoGameDefinitions.texturePackerSpriteAtlas.Dirt1;
              MapGenerator.AddDirtBorders(tileMap);
            }

          }
      }

      base.Update(gameTime);
    }

    private static void GetMouseState(out MouseState mouseState, out Vector2 mousePositionInWorld) {
      mouseState = Mouse.GetState();
      var mousePoint = new Point(mouseState.X, mouseState.Y);
      mousePositionInWorld = Vector2.Transform(mousePoint.ToVector2(), Matrix.Invert(cam.GetTransformation(graphics.GraphicsDevice)));
    }

    protected override void Draw(GameTime gameTime) {
      GraphicsDevice.Clear(Color.Black);

      spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, cam.GetTransformation(graphics.GraphicsDevice));
      Drawing.drawTerrain(spriteRender);
      spriteBatch.End();

      base.Draw(gameTime);
    }

  }
}
