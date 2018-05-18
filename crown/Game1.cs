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
        public static SpriteRender spriteRender;

        // player camera
        public static Camera2d cam = new Camera2d();
        public float camSpeed = 32f;

        // Textures
        public static SpriteSheet mapTileSheet;
        public static SpriteSheet buildingTileSheet;
        public static SpriteSheet menuTileSheet;
        public static SpriteSheet interactiveTileSheet;
        public static SpriteSheet peopleTileSheet;

        public static int tileSize;
        public static Tile[,] tileMap;

        // Game relevant objects
        public static Mechanics mechanics;
        public static List<Interactive> interactives;
        public static Road[,] roads;
        public static List<Button> menuButtons;
        public static List<Citizen> citizens;

        // Seed for Random Generation
        public static Random random = new Random();

        public static Building selectedBuilding;

        SpriteBatch spriteBatch;

        // Which action is the mouse currently doing
        public enum MouseAction {
            FARM, HOUSE, TOWNHALL, NOTHING, ROAD,
            WOODCUTTER
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

            cam.Pos = new Vector2(10000, 10000);
            cam.Zoom = 0.5f;

            interactives = new List<Interactive>();
            menuButtons = new List<Button>();
            roads = new Road[1, 1];
            tileMap = new Tile[1, 1];
            mechanics = new Mechanics();

            tileSize = (int)mapTileSheet.Sprite(TexturePackerMonoGameDefinitions.texturePackerSpriteAtlas.Grass1).Size.X;
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
            peopleTileSheet = spriteSheetLoader.Load("people/peopleAtlas");

            font = Content.Load<SpriteFont>("maelFont");

            // Make a 1x1 texture named pixel.  
            pixel = new Texture2D(GraphicsDevice, 1, 1);

            // Create a 1D array of color data to fill the pixel texture with.  
            Color[] colorData = {
                        new Color(Color.White, 0.5f)
                    };

            // Set the texture data with our color information.  
            pixel.SetData(colorData);

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
                citizens = new List<Citizen>();

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

                menuButtons = new List<Button>();
                MenuBuilder.BuildGameMenu();
            }

            // Mouse Controls
            MouseState mouseState = Mouse.GetState();
            mousePositionInWorld = GetMouseWorldPosition(mouseState);
            if (mouseState.LeftButton == ButtonState.Pressed)
                MouseControls(mouseState);

            // Cancel current action
            if (mouseState.RightButton == ButtonState.Pressed) {
                mouseAction = MouseAction.NOTHING;
                selectedBuilding = null;
            }

            oldState = mouseState;

            mechanics.UpdateMechanics();

            base.Update(gameTime);
        }

        private void MouseControls(MouseState mouseState) {
            Point mousePoint = new Point(mouseState.X, mouseState.Y);
            Rectangle mainRect = new Rectangle(0, MenuBuilder.menuYPosition, 400, 300);

            if (mainRect != null)
                if (!mainRect.Contains(mousePoint)
                    && (oldState.LeftButton == ButtonState.Released || mouseAction == MouseAction.ROAD)) {
                    // Mouse interaction with the game world
                    if (mouseAction != MouseAction.NOTHING) {
                        mouseAction = Controls.BuildStuff(mouseAction, mousePositionInWorld);
                    } else {
                        Controls.InteractWithStuff(mousePositionInWorld);
                    }

                    // Mouse interaction with the menu
                } else if (mainRect.Contains(mousePoint)) {
                    MenuControls(mousePoint);
                }

        }

        private void MenuControls(Point mousePoint) {
            foreach (Button button in menuButtons)
                if (button.Rect.Contains(mousePoint)) {
                    if (button.Type == Button.ButtonType.BUTTON_TOWNHALL)
                        mouseAction = MouseAction.TOWNHALL;
                    else if (button.Type == Button.ButtonType.BUTTON_ROAD)
                        mouseAction = MouseAction.ROAD;
                    else if (button.Type == Button.ButtonType.BUTTON_HOUSE)
                        mouseAction = MouseAction.HOUSE;
                    else if (button.Type == Button.ButtonType.BUTTON_FARM)
                        mouseAction = MouseAction.FARM;
                    else if (button.Type == Button.ButtonType.BUTTON_WOODCUTTER)
                        mouseAction = MouseAction.WOODCUTTER;
                }
        }

        private static Vector2 GetMouseWorldPosition(MouseState mouseState) {
            return Vector2.Transform(new Point(mouseState.X, mouseState.Y).ToVector2(), Matrix.Invert(cam.GetTransformation(graphics.GraphicsDevice)));
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, cam.GetTransformation(graphics.GraphicsDevice));
            Drawing.DrawTerrain(spriteRender);
            Drawing.DrawInteractives(spriteRender, interactives);
            Drawing.DrawRoads(spriteRender, roads);
            Drawing.DrawBuildings(spriteRender, mechanics.Buildings);
            Drawing.DrawPeople(spriteRender, citizens);
            Drawing.DrawMouseSelection(spriteRender, mousePositionInWorld, mouseAction);
            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, null);
            Drawing.DrawMenu(spriteRender, menuButtons);

            // Draw text on top of everything
            // Draw a fancy rectangle.  
            spriteBatch.Draw(pixel, new Rectangle(0, 0, 450, 150), Color.Black);
            spriteBatch.DrawString(font, "Population: " + mechanics.Population + " / " + mechanics.MaxPop, new Vector2(16, 16), Color.White);
            spriteBatch.DrawString(font, "Available Workers: " + mechanics.Workers, new Vector2(16, 32), Color.White);
            spriteBatch.DrawString(font, "Gold: " + mechanics.Gold + "   " + mechanics.GoldDelta + " / tick", new Vector2(16, 48), Color.White);
            spriteBatch.DrawString(font, "Wood: " + mechanics.Wood + " / " + mechanics.WoodStorage + "   " + mechanics.WoodDelta + " / tick", new Vector2(16, 64), Color.White);
            spriteBatch.DrawString(font, "Stone: " + mechanics.Stone + " / " + mechanics.StoneStorage + "   " + mechanics.StoneDelta + " / tick", new Vector2(16, 80), Color.White);
            spriteBatch.DrawString(font, "Food: " + mechanics.Food + " / " + mechanics.FoodStorage + "   " + mechanics.FoodDelta + " / tick", new Vector2(16, 96), Color.White);
            spriteBatch.End();

            // Draw infos for selection
            if (selectedBuilding != null) {
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, null);
                spriteBatch.Draw(pixel, new Rectangle(0, 175, 450, 150), Color.DarkSlateBlue);
                spriteBatch.DrawString(font, selectedBuilding.Type.ToString(), new Vector2(16, 191), Color.White);
                if (selectedBuilding.Type == Building.BuildingTypes.HOUSE)
                    spriteBatch.DrawString(font, "Inhabitants: " + selectedBuilding.Inhabitants, new Vector2(16, 206), Color.White);
                if (selectedBuilding.Type == Building.BuildingTypes.FARM || selectedBuilding.Type == Building.BuildingTypes.WOODCUTTER)
                    spriteBatch.DrawString(font, "Workers: " + selectedBuilding.Inhabitants, new Vector2(16, 206), Color.White);
                spriteBatch.End();

            }

            // Draw infos for menu selection
            if (mouseAction != MouseAction.NOTHING) {
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, null);
                spriteBatch.Draw(pixel, new Rectangle(0, 350, 450, 150), Color.OrangeRed);

                Costs costs = null;
                if (mouseAction == MouseAction.HOUSE)
                    costs = Costs.HouseCosts();
                if (mouseAction == MouseAction.FARM)
                    costs = Costs.FarmCosts();
                if (mouseAction == MouseAction.WOODCUTTER)
                    costs = Costs.WoodcutterCosts();

                if (costs != null) {
                    spriteBatch.DrawString(font, mouseAction.ToString(), new Vector2(16, 351), Color.White);
                    spriteBatch.DrawString(font, "Gold: " + costs.Gold, new Vector2(16, 366), Color.White);
                    spriteBatch.DrawString(font, "Wood: " + costs.Wood, new Vector2(16, 381), Color.White);
                    spriteBatch.DrawString(font, "Stone: " + costs.Stone, new Vector2(16, 396), Color.White);
                    spriteBatch.DrawString(font, "Workers: " + costs.Workers, new Vector2(16, 411), Color.White);
                    spriteBatch.DrawString(font, "Gold Upkeep: " + costs.GoldUpkeep, new Vector2(16, 426), Color.White);
                    spriteBatch.DrawString(font, "Wood Upkeep: " + costs.WoodUpkeep, new Vector2(16, 441), Color.White);
                    spriteBatch.DrawString(font, "Stone Upkeep: " + costs.StoneUpkeep, new Vector2(16, 456), Color.White);
                }
                spriteBatch.End();
            }

            base.Draw(gameTime);
        }

    }
}
