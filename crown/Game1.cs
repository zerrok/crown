using crown.Terrain;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TexturePackerLoader;

namespace crown {
    public class Game1 : Game {
        public static GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        // player camera
        public static Camera2d cam = new Camera2d();
        float camSpeed = 10f;

        // Textures
        public static SpriteRender spriteRender;
        public static SpriteSheet mapTileSheet;

        public static Tile[,] tileMap;

        public Game1() {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;
            Content.RootDirectory = "Content";
        }

        protected override void Initialize() {
            base.Initialize();

            IsMouseVisible = true;

            cam.Pos = new Vector2(0, 0);
            cam.Zoom = 1f;

            // TODO: Auslagern in Update wenn Menü eingebaut
            tileMap = new MapGenerator().GetMap(50, 50);
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
                System.Environment.Exit(0);
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

            base.Update(gameTime);
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
