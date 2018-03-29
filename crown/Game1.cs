using crown.Terrain;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using TexturePackerLoader;
using System.Collections.Generic;

namespace crown {
  public class Game1 : Game {

    public static GraphicsDeviceManager graphics;
    SpriteBatch spriteBatch;

    // player camera
    public static Camera2d cam = new Camera2d();
    public float camSpeed = 15f;

    // Textures
    public static SpriteRender spriteRender;
    public static SpriteSheet mapTileSheet;
    public static SpriteSheet buildingTileSheet;
    public static SpriteSheet menuTileSheet;
    public static int tileSize = 32;
    public static Tile[,] tileMap;
    public static Menu menu;

    // Which action is the mouse currently doing
    public enum MouseAction {
      FARMLAND, HOUSE, TOWNHALL, NOTHING
    }

    public MouseAction mouseAction = MouseAction.NOTHING;
    Vector2 mousePositionInWorld;

    // Game relevant objects
    public static List<Building> buildings;

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

      buildings = new List<Building>();

      // TODO initialize after map is loaded
      menu = new Menu(menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Maincontrols)
        , menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Buttonhouse)
        , menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Buttontownhall)
        , menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Buttonfarmland));
    }

    protected override void LoadContent() {
      // Load Spritebatch and SpriteRender for spritesheets
      spriteBatch = new SpriteBatch(GraphicsDevice);
      spriteRender = new SpriteRender(spriteBatch);

      var spriteSheetLoader = new SpriteSheetLoader(this.Content);
      mapTileSheet = spriteSheetLoader.Load("tiles/tileAtlas");
      menuTileSheet = spriteSheetLoader.Load("menu/menuAtlas");
      buildingTileSheet = spriteSheetLoader.Load("buildings/buildingAtlas");
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

      // Keyboard Controls
      Controls.CameraControls(cam, camSpeed);

      if (Keyboard.GetState().IsKeyDown(Keys.Q)) {
        // TODO: Auslagern in Menü 
        tileMap = new MapGenerator().GetMap(250, 250);
      }

      // Mouse Controls
      MouseControls();

      base.Update(gameTime);
    }

    private void MouseControls() {
      MouseState mouseState = Mouse.GetState();
      mousePositionInWorld = GetMouseWorldPosition(mouseState);
      Point mousePoint = new Point(mouseState.X, mouseState.Y);

      // Mouse interaction with the world
      if (mouseState.LeftButton == ButtonState.Pressed && !menu.MainRect.Contains(mousePoint)) {
        MapInteraction();

        // Mouse interaction with the menu
      } else if (mouseState.LeftButton == ButtonState.Pressed && menu.MainRect.Contains(mousePoint)) {
        MenuControls(mousePoint);
      }

      // Cancel current action
      if (mouseState.RightButton == ButtonState.Pressed)
        mouseAction = MouseAction.NOTHING;
    }

    private void MapInteraction() {
      foreach (Tile tile in tileMap)
        if (tile != null && tile.Rect.Contains(mousePositionInWorld)) {
          if (mouseAction == MouseAction.FARMLAND) {
            Controls.MakeFarmableLand(tileMap, tile);
          }

          if (mouseAction == MouseAction.TOWNHALL) {
            Controls.BuildTownHall(tile);
          }

          if (mouseAction == MouseAction.HOUSE) {
            Controls.BuildHouse(tile);
          }
          // Cancel building when left shift is not pressed
          if (!Keyboard.GetState().IsKeyDown(Keys.LeftShift)) {
            mouseAction = MouseAction.NOTHING;
          }
        }
    }

    private void MenuControls(Point mousePoint) {
      if (menu.HallRect.Contains(mousePoint)) {
        mouseAction = MouseAction.TOWNHALL;
      } else if (menu.HouseRect.Contains(mousePoint)) {
        mouseAction = MouseAction.HOUSE;
      } else if (menu.FarmlandRect.Contains(mousePoint)) {
        mouseAction = MouseAction.FARMLAND;
      } else {
        mouseAction = MouseAction.NOTHING;
      }
    }

    private static Vector2 GetMouseWorldPosition(MouseState mouseState) {
      return Vector2.Transform(new Point(mouseState.X, mouseState.Y).ToVector2(), Matrix.Invert(cam.GetTransformation(graphics.GraphicsDevice)));
    }

    protected override void Draw(GameTime gameTime) {
      GraphicsDevice.Clear(Color.Black);

      spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, cam.GetTransformation(graphics.GraphicsDevice));
      Drawing.drawTerrain(spriteRender);
      Drawing.drawMouseSelection(spriteRender, mousePositionInWorld, mouseAction);
      Drawing.drawBuildings(spriteRender, buildings);
      spriteBatch.End();

      spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, null);
      Drawing.drawMenu(spriteRender, menu);
      spriteBatch.End();

      base.Draw(gameTime);
    }

  }
}
