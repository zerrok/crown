using crown.Terrain;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using TexturePackerLoader;
using System.Collections.Generic;
using System.Linq;

namespace crown {
    public class Game1 : Game {

        public static GraphicsDeviceManager graphics;

        // player camera
        public static Camera2d cam = new Camera2d();
        public float camSpeed = 8f;

        // Textures
        public static SpriteRender spriteRender;
        public static SpriteSheet mapTileSheet;
        public static SpriteSheet buildingTileSheet;
        public static SpriteSheet menuTileSheet;
        public static SpriteSheet interactiveTileSheet;
        public static int tileSize = 32;
        public static Tile[,] tileMap;

        // Game relevant objects
        public static Mechanics mechanics;
        public static List<Interactive> interactives;
        public static Road[,] roads;
        public static List<Menu> menu;

        // Seed for Random Generation
        public static Random random = new Random();

        SpriteBatch spriteBatch;

        // Which action is the mouse currently doing
        public enum MouseAction {
            FARMLAND, HOUSE, TOWNHALL, NOTHING, ROAD
        }

        public MouseAction mouseAction = MouseAction.NOTHING;
        Vector2 mousePositionInWorld;

        // Saves the last mouse state
        MouseState oldState;

        // Menu font
        public static SpriteFont font;
        Texture2D pixel;

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

            cam.Pos = new Vector2(3000, 3000);
            cam.Zoom = 1;

            interactives = new List<Interactive>();
            menu = new List<Menu>();
            roads = new Road[1, 1];
            tileMap = new Tile[1, 1];
            mechanics = new Mechanics();

            tileSize = (int)mapTileSheet.Sprite(TexturePackerMonoGameDefinitions.texturePackerSpriteAtlas.Dirt1).Size.X;
        }


        protected override void LoadContent() {
            // Load Spritebatch and SpriteRender for spritesheets
            spriteBatch = new SpriteBatch(GraphicsDevice);
            spriteRender = new SpriteRender(spriteBatch);

            var spriteSheetLoader = new SpriteSheetLoader(this.Content);
            mapTileSheet = spriteSheetLoader.Load("tiles/tileAtlas");
            menuTileSheet = spriteSheetLoader.Load("menu/menuAtlas");
            buildingTileSheet = spriteSheetLoader.Load("buildings/buildingAtlas");
            interactiveTileSheet = spriteSheetLoader.Load("interactives/interactiveAtlas");

            font = Content.Load<SpriteFont>("maelFont");

            // Make a 1x1 texture named pixel.  
            pixel = new Texture2D(GraphicsDevice, 1, 1);

            // Create a 1D array of color data to fill the pixel texture with.  
            Color[] colorData = {
                        Color.White
                    };

            // Set the texture data with our color information.  
            pixel.SetData<Color>(colorData);

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
                // Reset everything
                GC.Collect();
                mechanics.Buildings = new List<Building>();
                interactives = new List<Interactive>();
                tileMap = new Tile[1, 1];
                mechanics = new Mechanics();

                // Regenerate everything
                tileMap = new MapGenerator().GetMap(250, 250);
                roads = new Road[250, 250];
                InteractiveGenerator.PlaceInteractives(tileMap);

                // Sort trees so they are rendered correctly
                IOrderedEnumerable<Interactive> sortedInteractives = interactives.OrderBy(interactive => interactive.Coords.Y);
                interactives = new List<Interactive>();
                foreach (Interactive inter in sortedInteractives) {
                    interactives.Add(inter);
                }

                menu = new List<Menu>();
                MenuBuilder.BuildGameMenu();
            }

            // Mouse Controls
            MouseState mouseState = Mouse.GetState();
            mousePositionInWorld = GetMouseWorldPosition(mouseState);
            if (mouseState.LeftButton == ButtonState.Pressed)
                MouseControls(mouseState);

            // Cancel current action
            if (mouseState.RightButton == ButtonState.Pressed)
                mouseAction = MouseAction.NOTHING;

            oldState = mouseState;

            mechanics.UpdateMechanics();

            base.Update(gameTime);
        }

        private void MouseControls(MouseState mouseState) {
            Point mousePoint = new Point(mouseState.X, mouseState.Y);
            Menu main = new Menu();
            foreach (Menu menuItem in menu)
                if (menuItem != null && menuItem.Type == Menu.MenuType.MAIN)
                    main = menuItem;

            if (main != null)
                if (!main.MainRect.Contains(mousePoint)
                    && (oldState.LeftButton == ButtonState.Released || mouseAction == MouseAction.ROAD)) {
                    // Mouse interaction with the game world
                    if (mouseAction != MouseAction.NOTHING) {
                        mouseAction = Controls.BuildStuff(mouseAction, mousePositionInWorld);
                    } else {
                        Controls.InteractWithStuff(mousePositionInWorld);
                    }

                    // Mouse interaction with the menu
                } else if (main.MainRect.Contains(mousePoint)) {
                    MenuControls(mousePoint);
                }

        }

        private void MenuControls(Point mousePoint) {
            foreach (Menu item in menu)
                if (item.MainRect.Contains(mousePoint)) {
                    if (item.Type == Menu.MenuType.BUTTON_TOWNHALL)
                        mouseAction = MouseAction.TOWNHALL;
                    else if (item.Type == Menu.MenuType.BUTTON_ROAD)
                        mouseAction = MouseAction.ROAD;
                    else if (item.Type == Menu.MenuType.BUTTON_HOUSE)
                        mouseAction = MouseAction.HOUSE;
                    else if (item.Type == Menu.MenuType.BUTTON_FARMLAND)
                        mouseAction = MouseAction.FARMLAND;
                }
        }

        private static Vector2 GetMouseWorldPosition(MouseState mouseState) {
            return Vector2.Transform(new Point(mouseState.X, mouseState.Y).ToVector2(), Matrix.Invert(cam.GetTransformation(graphics.GraphicsDevice)));
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, cam.GetTransformation(graphics.GraphicsDevice));
            Drawing.DrawTerrain(spriteRender);
            Drawing.DrawMouseSelection(spriteRender, mousePositionInWorld, mouseAction);
            Drawing.DrawBuildings(spriteRender, mechanics.Buildings);
            Drawing.DrawInteractives(spriteRender, interactives);
            Drawing.DrawRoads(spriteRender, roads);
            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, null);
            Drawing.DrawMenu(spriteRender, menu);

            // Draw text on top of everything
            // Draw a fancy rectangle.  
            spriteBatch.Draw(pixel, new Rectangle(0, 0, 450, 150), Color.SlateGray);
            spriteBatch.DrawString(font, "Population: " + mechanics.Population + " / " + mechanics.MaxPop, new Vector2(16, 16), Color.White);
            spriteBatch.DrawString(font, "Available Workers: " + mechanics.Workers, new Vector2(16, 32), Color.White);
            spriteBatch.DrawString(font, "Gold: " + mechanics.Gold, new Vector2(16, 48), Color.White);
            spriteBatch.DrawString(font, "Wood: " + mechanics.Wood + " / " + mechanics.WoodStorage, new Vector2(16, 64), Color.White);
            spriteBatch.DrawString(font, "Stone: " + mechanics.Stone + " / " + mechanics.StoneStorage, new Vector2(16, 80), Color.White);
            spriteBatch.DrawString(font, "Food: " + mechanics.Food + " / " + mechanics.FoodStorage, new Vector2(16, 96), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }

    }
}
