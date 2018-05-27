using Microsoft.Xna.Framework;
using static crown.Game1;

namespace crown {
    public class MenuBuilder {
        public static int menuYPosition = graphics.GraphicsDevice.Viewport.Height - 300;
        static int xOffset = 80;
        static int yOffset = 80;

        public static void BuildGameMenu() {
            UIElement element = new UIElement(new Vector2(0, 0), menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Menu1), UIElement.ElementType.Resources);
            uiElements.Add(element);
            int selectionPosition = menuYPosition - 300;
            element = new UIElement(new Vector2(0, selectionPosition), menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Menu2), UIElement.ElementType.MenuSelection);
            uiElements.Add(element);
            element = new UIElement(new Vector2(0, selectionPosition), menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Menu3), UIElement.ElementType.GameSelection);
            uiElements.Add(element);

            int xPos = 0;
            int yPos = menuYPosition;

            Button buttonTownhall = new Button();
            buttonTownhall.Pos = new Vector2(xPos + 20, yPos + 20);
            buttonTownhall.Rect = new Rectangle(xPos + 20, yPos + 20, (int)menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Buttonfarmland).Size.X, (int)menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Buttonfarmland).Size.Y);
            buttonTownhall.Type = Button.ButtonType.Townhall;
            buttonTownhall.SpriteFrame = menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Buttontownhall);
            buttons.Add(buttonTownhall);
        }

        public static void BuildMainMenu() {
            Button startButton = new Button();
            startButton.Pos = new Vector2(300, 300);
            startButton.Rect = new Rectangle(300, 300, (int)menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.StartButton).Size.X, (int)menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.StartButton).Size.Y);
            startButton.Type = Button.ButtonType.Start;
            startButton.SpriteFrame = menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.StartButton);
            mainButtons.Add(startButton);

            Button continueButton = new Button();
            continueButton.Pos = new Vector2(300, 450);
            continueButton.Rect = new Rectangle(300, 450, (int)menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.StartButton).Size.X, (int)menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.StartButton).Size.Y);
            continueButton.Type = Button.ButtonType.Continue;
            continueButton.SpriteFrame = menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.ContinueButton);
            mainButtons.Add(continueButton);

            Button quitButton = new Button();
            quitButton.Pos = new Vector2(300, 600);
            quitButton.Rect = new Rectangle(300, 600, (int)menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.StartButton).Size.X, (int)menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.StartButton).Size.Y);
            quitButton.Type = Button.ButtonType.Quit;
            quitButton.SpriteFrame = menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.QuitButton);
            mainButtons.Add(quitButton);
        }

        public static void MenuTierOne() {
            int xPos = 100;
            int yPos = menuYPosition + 20;

            // Town hall has been built
            Button buttonHouse = new Button();
            buttonHouse.Pos = new Vector2(xPos, yPos);
            buttonHouse.Rect = new Rectangle(xPos, yPos, (int)menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Buttonfarmland).Size.X, (int)menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Buttonfarmland).Size.Y);
            buttonHouse.Type = Button.ButtonType.House;
            buttonHouse.SpriteFrame = menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Buttonhouse);
            buttons.Add(buttonHouse);

            xPos += xOffset;

            Button buttonRoads = new Button();
            buttonRoads.Pos = new Vector2(xPos, yPos);
            buttonRoads.Rect = new Rectangle(xPos, yPos, (int)menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Buttonfarmland).Size.X, (int)menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Buttonfarmland).Size.Y);
            buttonRoads.Type = Button.ButtonType.Road;
            buttonRoads.SpriteFrame = menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Buttonroad);
            buttons.Add(buttonRoads);

            xPos += xOffset;

            Button buttonFarm = new Button();
            buttonFarm.Pos = new Vector2(xPos, yPos);
            buttonFarm.Rect = new Rectangle(xPos, yPos, (int)menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Buttonfarmland).Size.X, (int)menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Buttonfarmland).Size.Y);
            buttonFarm.Type = Button.ButtonType.Farm;
            buttonFarm.SpriteFrame = menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Buttonfarm);
            buttons.Add(buttonFarm);

            xPos += xOffset;

            Button buttonWoodcutter = new Button();
            buttonWoodcutter.Pos = new Vector2(xPos, yPos);
            buttonWoodcutter.Rect = new Rectangle(xPos, yPos, (int)menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Buttonfarmland).Size.X, (int)menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Buttonfarmland).Size.Y);
            buttonWoodcutter.Type = Button.ButtonType.Woodcutter;
            buttonWoodcutter.SpriteFrame = menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Buttonwoodcutter);
            buttons.Add(buttonWoodcutter);

            xPos = 20;
            yPos += yOffset;

            Button buttonStorage = new Button();
            buttonStorage.Pos = new Vector2(xPos, yPos);
            buttonStorage.Rect = new Rectangle(xPos, yPos, (int)menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Buttonfarmland).Size.X, (int)menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Buttonfarmland).Size.Y);
            buttonStorage.Type = Button.ButtonType.Storage;
            buttonStorage.SpriteFrame = menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Buttonstorage);
            buttons.Add(buttonStorage);

            xPos += xOffset;

            Button buttonQuarry = new Button();
            buttonQuarry.Pos = new Vector2(xPos, yPos);
            buttonQuarry.Rect = new Rectangle(xPos, yPos, (int)menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Buttonfarmland).Size.X, (int)menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Buttonfarmland).Size.Y);
            buttonQuarry.Type = Button.ButtonType.Quarry;
            buttonQuarry.SpriteFrame = menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Buttonquarry);
            buttons.Add(buttonQuarry);

            xPos += xOffset;

            Button buttonScientist = new Button();
            buttonScientist.Pos = new Vector2(xPos, yPos);
            buttonScientist.Rect = new Rectangle(xPos, yPos, (int)menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Buttonfarmland).Size.X, (int)menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Buttonfarmland).Size.Y);
            buttonScientist.Type = Button.ButtonType.Scientist;
            buttonScientist.SpriteFrame = menuTileSheet.Sprite(TexturePackerMonoGameDefinitions.menuAtlas.Buttonscientist);
            buttons.Add(buttonScientist);
        }

    }
}
