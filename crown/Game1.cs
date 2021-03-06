﻿using crown.Terrain;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using TexturePackerLoader;
using System.Collections.Generic;
using System.Linq;

namespace crown
{

    public enum Actions
    {
        // Buttons only
        Main, Start, Quit, Continue,
        // Buttons, buildings and mouseactions
        Farm, House, Townhall, Nothing, Road, Woodcutter, Quarry, Storage, Scientist, Tavern, Brewery
    }

    public class Game1 : Game
    {

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
        public static List<Button> buttons;
        public static List<Button> mainButtons;
        public static List<UIElement> uiElements;
        public static List<Citizen> citizens;

        public enum GameState { Menu, Game };
        public GameState gameState = GameState.Menu;

        // Seed for Random Generation
        public static Random random = new Random();

        public static Building selectedBuilding;

        SpriteBatch spriteBatch;

        public static Actions mouseAction = Actions.Nothing;
        Vector2 mousePositionInWorld;

        // Saves the last mouse state
        MouseState oldState;
        // Saves the last keyboard state
        KeyboardState oldKeyState;

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
            buttons = new List<Button>();
            mainButtons = new List<Button>();
            uiElements = new List<UIElement>();
            roads = new Road[1, 1];
            tileMap = new Tile[1, 1];
            mechanics = new Mechanics();
            MenuBuilder.BuildMainMenu();

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
            KeyboardState keyboardState = Keyboard.GetState();
            if (oldKeyState.IsKeyUp(Keys.Escape) && keyboardState.IsKeyDown(Keys.Escape)) {
                if (gameState == GameState.Game)
                    gameState = GameState.Menu;
                else if (gameState == GameState.Menu)
                    gameState = GameState.Game;
            }

            // Keyboard Controls
            Controls.CameraControls(cam, camSpeed);

            // Mouse Controls
            MouseState mouseState = Mouse.GetState();
            mousePositionInWorld = GetMouseWorldPosition(mouseState);
            if (mouseState.LeftButton == ButtonState.Pressed)
                MouseControls(mouseState);

            // Cancel current action
            if (mouseState.RightButton == ButtonState.Pressed) {
                mouseAction = Actions.Nothing;
                selectedBuilding = null;
            }

            oldState = mouseState;
            oldKeyState = keyboardState;

            if (gameState == GameState.Game)
                mechanics.UpdateMechanics();

            base.Update(gameTime);
        }

        private void MouseControls(MouseState mouseState) {
            Point mousePoint = new Point(mouseState.X, mouseState.Y);
            Rectangle mainRect = new Rectangle(0, MenuBuilder.menuYPosition, 400, 300);

            if (mainRect != null)
                if (!mainRect.Contains(mousePoint)
                    && (oldState.LeftButton == ButtonState.Released || mouseAction == Actions.Road) && gameState == GameState.Game) {
                    // Mouse interaction with the game world
                    if (mouseAction != Actions.Nothing) {
                        mouseAction = Controls.BuildStuff(mouseAction, mousePositionInWorld);
                    } else {
                        Controls.InteractWithStuff(mousePositionInWorld);
                    }

                    // Mouse interaction with the menu
                } else if (mainRect.Contains(mousePoint) && gameState == GameState.Game) {
                    MenuControls(mousePoint);
                    selectedBuilding = null;
                } else if (gameState == GameState.Menu) {
                    foreach (Button button in mainButtons)
                        if (button.Rect.Contains(mousePoint)) {
                            if (button.Action == Actions.Start) {
                                InitializeGame();

                                gameState = GameState.Game;
                            }
                            if (button.Action == Actions.Quit) {
                                UnloadContent();
                                Exit();
                            }
                            if (button.Action == Actions.Continue) {
                                gameState = GameState.Game;
                            }
                        }
                }

        }

        private static void InitializeGame() {
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

            buttons = new List<Button>();
            uiElements = new List<UIElement>();
            MenuBuilder.BuildGameMenu();
        }

        private void MenuControls(Point mousePoint) {
            foreach (Button button in buttons)
                if (button.Rect.Contains(mousePoint)) {
                    mouseAction = button.Action;
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

            if (gameState == GameState.Game) {
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, null);
                Drawing.DrawMenu(spriteRender, buttons, spriteBatch, mouseAction);
                spriteBatch.End();
            } else if (gameState == GameState.Menu) {
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, null);
                Drawing.DrawMainMenu(spriteRender, mainButtons);
                spriteBatch.End();
            }

            base.Draw(gameTime);
        }

    }
}
