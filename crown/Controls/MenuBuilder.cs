using Microsoft.Xna.Framework;
using TexturePackerLoader;
using static crown.Game1;

namespace crown {
    public class MenuBuilder {
        public static int menuYPosition = graphics.GraphicsDevice.Viewport.Height - 300;
        static int xOffset = 80;
        static int yOffset = 80;
        static int buttonSizeX = (int)menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Buttonfarmland).Size.X;
        static int buttonSizeY = (int)menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Buttonfarmland).Size.Y;

        public static void BuildGameMenu() {
            BuildUI();

            int xPos = 20;
            int yPos = menuYPosition;
            buttons.Add(CreateButton(xPos, yPos, Actions.Townhall, menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Buttontownhall)));
        }

        private static void BuildUI() {
            UIElement element = new UIElement(new Vector2(0, 0), menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Pop), UIElement.ElementType.Resources, UIElement.Resource.Population);
            uiElements.Add(element);

            float smallX = menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Pop).Size.X;
            float bigX = menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Workers).Size.X;
            float offsetX = smallX;
            /**
             * Resources on top of the screen
             */
            element = new UIElement(new Vector2(offsetX, 0), menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Workers), UIElement.ElementType.Resources, UIElement.Resource.Workers);
            uiElements.Add(element);

            offsetX += bigX;
            element = new UIElement(new Vector2(offsetX, 0), menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Gold), UIElement.ElementType.Resources, UIElement.Resource.Gold);
            uiElements.Add(element);

            offsetX += bigX;
            element = new UIElement(new Vector2(offsetX, 0), menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Wood), UIElement.ElementType.Resources, UIElement.Resource.Wood);
            uiElements.Add(element);

            offsetX += bigX;
            element = new UIElement(new Vector2(offsetX, 0), menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Stone), UIElement.ElementType.Resources, UIElement.Resource.Stone);
            uiElements.Add(element);

            offsetX += bigX;
            element = new UIElement(new Vector2(offsetX, 0), menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Wheat), UIElement.ElementType.Resources, UIElement.Resource.Wheat);
            uiElements.Add(element);
            offsetX += bigX;

            element = new UIElement(new Vector2(offsetX, 0), menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Beer), UIElement.ElementType.Resources, UIElement.Resource.Beer);
            uiElements.Add(element);
            offsetX += bigX;

            element = new UIElement(new Vector2(offsetX, 0), menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Food), UIElement.ElementType.Resources, UIElement.Resource.Food);
            uiElements.Add(element);

            /**
             * For Selected Buildings, etc.
             */
            offsetX = 0;
            int yPos = menuYPosition - 120;
            element = new UIElement(new Vector2(offsetX, yPos), menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Workers), UIElement.ElementType.MenuSelection, UIElement.Resource.Workers);
            uiElements.Add(element);
            offsetX += bigX;

            element = new UIElement(new Vector2(offsetX, yPos), menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Gold), UIElement.ElementType.MenuSelection, UIElement.Resource.Gold);
            uiElements.Add(element);
            offsetX += bigX;

            element = new UIElement(new Vector2(offsetX, yPos), menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Wood), UIElement.ElementType.MenuSelection, UIElement.Resource.Wood);
            uiElements.Add(element);
            offsetX += bigX;

            element = new UIElement(new Vector2(offsetX, yPos), menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Stone), UIElement.ElementType.MenuSelection, UIElement.Resource.Stone);
            uiElements.Add(element);
            offsetX += bigX;

            element = new UIElement(new Vector2(offsetX, yPos), menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Food), UIElement.ElementType.MenuSelection, UIElement.Resource.Food);
            uiElements.Add(element);
            offsetX += bigX;

            element = new UIElement(new Vector2(offsetX, yPos), menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Beer), UIElement.ElementType.MenuSelection, UIElement.Resource.Beer);
            uiElements.Add(element);
            offsetX += bigX;

            element = new UIElement(new Vector2(offsetX, yPos), menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Wheat), UIElement.ElementType.MenuSelection, UIElement.Resource.Wheat);
            uiElements.Add(element);
            offsetX += bigX;

            yPos = menuYPosition - 300;
            element = new UIElement(new Vector2(0, yPos), menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Menu3), UIElement.ElementType.GameSelection, UIElement.Resource.Details);
            uiElements.Add(element);
        }

        public static void BuildMainMenu() {
            Button startButton = new Button();
            startButton.Pos = new Vector2(300, 300);
            startButton.Rect = new Rectangle(300, 300, (int)menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.StartButton).Size.X, (int)menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.StartButton).Size.Y);
            startButton.Action = Actions.Start;
            startButton.SpriteFrame = menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.StartButton);
            mainButtons.Add(startButton);

            Button continueButton = new Button();
            continueButton.Pos = new Vector2(300, 450);
            continueButton.Rect = new Rectangle(300, 450, (int)menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.StartButton).Size.X, (int)menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.StartButton).Size.Y);
            continueButton.Action = Actions.Continue;
            continueButton.SpriteFrame = menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.ContinueButton);
            mainButtons.Add(continueButton);

            Button quitButton = new Button();
            quitButton.Pos = new Vector2(300, 600);
            quitButton.Rect = new Rectangle(300, 600, (int)menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.StartButton).Size.X, (int)menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.StartButton).Size.Y);
            quitButton.Action = Actions.Quit;
            quitButton.SpriteFrame = menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.QuitButton);
            mainButtons.Add(quitButton);
        }

        public static void MenuTierOne() {
            int xPos = 100;
            int yPos = menuYPosition;

            buttons.Add(CreateButton(xPos, yPos, Actions.House, menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Buttonhouse)));
            xPos += xOffset;

            buttons.Add(CreateButton(xPos, yPos, Actions.Road, menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Buttonroad)));
            xPos += xOffset;

            buttons.Add(CreateButton(xPos, yPos, Actions.Farm, menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Buttonfarm)));
            xPos += xOffset;

            buttons.Add(CreateButton(xPos, yPos, Actions.Woodcutter, menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Buttonwoodcutter)));
            xPos = 20;
            yPos += yOffset;

            buttons.Add(CreateButton(xPos, yPos, Actions.Tavern, menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Buttontavern)));
            xPos += xOffset;

            buttons.Add(CreateButton(xPos, yPos, Actions.Brewery, menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Buttonbrewery)));
            xPos += xOffset;

            buttons.Add(CreateButton(xPos, yPos, Actions.Storage, menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Buttonstorage)));
            xPos += xOffset;

            buttons.Add(CreateButton(xPos, yPos, Actions.Quarry, menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Buttonquarry)));
            xPos += xOffset;

            buttons.Add(CreateButton(xPos, yPos, Actions.Scientist, menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Buttonscientist)));
        }

        private static Button CreateButton(int xPos, int yPos, Actions action, SpriteFrame spriteFrame) {
            Button buttonHouse = new Button();
            buttonHouse.Pos = new Vector2(xPos, yPos);
            buttonHouse.Rect = new Rectangle(xPos, yPos, buttonSizeX, buttonSizeY);
            buttonHouse.Action = action;
            buttonHouse.SpriteFrame = spriteFrame;
            return buttonHouse;
        }
    }
}
