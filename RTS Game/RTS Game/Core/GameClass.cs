using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using RTS_Game;

namespace RTS_Game
{
    public class GameClass : Microsoft.Xna.Framework.Game
    {
        #region Variables
        //Game constants
        public const int Game_Width = 1440;
        public const int Game_Height = 900;
        public const int Tile_Width = 24;

        //Game objects
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private KeyboardState keyboard;
        private MouseState mouse;
        private Vector2 mousePos;

        private Dictionary<String, Texture2D> mouseTextures = new Dictionary<String, Texture2D>();
        private Texture2D currentMouseTexture;
        #endregion

        #region Function Explanation
        //Constructor.
        #endregion
        public GameClass()
        {
            //Content setup
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            //make the mouse visible, we want to see the mouse
            //IsMouseVisible = true;
        }

        #region Function Explanation
        //Sets up screen size.
        #endregion
        protected override void Initialize()
        {
            //Set the size of the game window
            graphics.PreferredBackBufferHeight = Game_Height;
            graphics.PreferredBackBufferWidth = Game_Width;
            this.graphics.IsFullScreen = true;
            graphics.ApplyChanges();

            base.Initialize();
        }

        #region Function Explanation
        //Loads all content used in game.
        #endregion
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            #region Background Textures.

            //Debug Tile
            Texture2D DebugTile = Content.Load<Texture2D>("BackgroundTiles/DebugTile");
            DebugTile.Name = "DebugTile";
            Resources.AddBackgroundTexture(DebugTile);

            //Ore
            Texture2D Ore = Content.Load<Texture2D>("BackgroundTiles/Ore");
            Ore.Name = "Ore";
            Resources.AddBackgroundTexture(Ore);

            //Grass01
            Texture2D Grass01 = Content.Load<Texture2D>("BackgroundTiles/Grass/Grass01");
            Grass01.Name = "Grass01";
            Resources.AddBackgroundTexture(Grass01);

            //Grass02
            Texture2D Grass02 = Content.Load<Texture2D>("BackgroundTiles/Grass/Grass02");
            Grass02.Name = "Grass02";
            Resources.AddBackgroundTexture(Grass02);

            //Grass03
            Texture2D Grass03 = Content.Load<Texture2D>("BackgroundTiles/Grass/Grass03");
            Grass03.Name = "Grass03";
            Resources.AddBackgroundTexture(Grass03);

            //Grass04
            Texture2D Grass04 = Content.Load<Texture2D>("BackgroundTiles/Grass/Grass04");
            Grass04.Name = "Grass04";
            Resources.AddBackgroundTexture(Grass04);

            //Road01
            Texture2D Road01 = Content.Load<Texture2D>("BackgroundTiles/Road01");
            Road01.Name = "Road01";
            Resources.AddBackgroundTexture(Road01);

            //Water01
            Texture2D Water01 = Content.Load<Texture2D>("BackgroundTiles/Water01");
            Water01.Name = "Water01";
            Resources.AddBackgroundTexture(Water01);
            #endregion

            #region Level Objects.
            //Debug
            Texture2D Debug = Content.Load<Texture2D>("Levels/Debug");
            Level Level00 = new Level("Level_Test", Debug, 0);
            Resources.AddLevelObject(Level00);

            //Island
            Texture2D Island = Content.Load<Texture2D>("Levels/Island");
            Level Level01 = new Level("Island", Island, 1);
            Resources.AddLevelObject(Level01);

            //Bridge
            Texture2D Bridge = Content.Load<Texture2D>("Levels/Bridge");
            Level Level02 = new Level("Bridge", Bridge, 2);
            Resources.AddLevelObject(Level02);
            #endregion

            #region GUI Textures
            //Splash Screen
            Texture2D SplashScreen = Content.Load<Texture2D>("GUI/SplashScreen");
            SplashScreen.Name = "SplashScreen";
            Resources.AddGUITexture(SplashScreen);

            #region Main Menu
            //ButtonBack
            Texture2D ButtonBack = Content.Load<Texture2D>("GUI/MainMenu/ButtonBack");
            ButtonBack.Name = "ButtonBack";
            Resources.AddGUITexture(ButtonBack);

            //LeftBack
            Texture2D LeftBack = Content.Load<Texture2D>("GUI/MainMenu/LeftBack");
            LeftBack.Name = "LeftBack";
            Resources.AddGUITexture(LeftBack);

            //TopRightBack
            Texture2D TopRightBack = Content.Load<Texture2D>("GUI/MainMenu/TopRightBack");
            TopRightBack.Name = "TopRightBack";
            Resources.AddGUITexture(TopRightBack);

            //BackgroundUnit
            Texture2D BackgroundUnit = Content.Load<Texture2D>("GUI/MainMenu/BackgroundUnit");
            BackgroundUnit.Name = "BackgroundUnit";
            Resources.AddGUITexture(BackgroundUnit);

            //BottomRightBack
            Texture2D BottomRightBack = Content.Load<Texture2D>("GUI/MainMenu/BottomRightBack");
            BottomRightBack.Name = "BottomRightBack";
            Resources.AddGUITexture(BottomRightBack);
            #endregion

            #region Ingame
            //Healthbar
            Texture2D HealthBar = Content.Load<Texture2D>("GUI/Ingame/Healthbar");
            HealthBar.Name = "HealthBar";
            Resources.AddGUITexture(HealthBar);

            //SelectedRectangle
            Texture2D SelectedRectangle = Content.Load<Texture2D>("GUI/Ingame/SelectedRectangle");
            SelectedRectangle.Name = "SelectedRectangle";
            Resources.AddGUITexture(SelectedRectangle);

            //TileOverlay
            Texture2D TileOverlay = Content.Load<Texture2D>("GUI/Ingame/TileOverlay");
            TileOverlay.Name = "TileOverlay";
            Resources.AddGUITexture(TileOverlay);

            //Cursors.
            mouseTextures.Add("Normal", Content.Load<Texture2D>("GUI/Ingame/Cursors/Normal"));

            currentMouseTexture = mouseTextures["Normal"];
            #endregion
            #endregion

            #region Unit Textures

            //Heavy Tank
            Texture2D HeavyTank = Content.Load<Texture2D>("Units/HeavyTank/HeavyTank");
            HeavyTank.Name = "HeavyTank";
            Resources.AddUnitTexture(HeavyTank);


            //Harvester
            Texture2D Harvester = Content.Load<Texture2D>("Units/Harvester");
            Harvester.Name = "Harvester";
            Resources.AddUnitTexture(Harvester);
            #endregion

            #region Building Textures
            //Contruction Yard
            Texture2D ConstructionYard = Content.Load<Texture2D>("Buildings/ConstructionYard");
            ConstructionYard.Name = "ConstructionYard";
            Resources.AddBuildingTexture(ConstructionYard);

            //Power Plant
            Texture2D PowerPlant = Content.Load<Texture2D>("Buildings/PowerPlant");
            PowerPlant.Name = "PowerPlant";
            Resources.AddBuildingTexture(PowerPlant);

            //Refinery
            Texture2D Refinery = Content.Load<Texture2D>("Buildings/Refinery");
            Refinery.Name = "Refinery";
            Resources.AddBuildingTexture(Refinery);
            #endregion

            Resources.TestFont = Content.Load<SpriteFont>("GUI/TestFont");
        }

        #region Function Explanation
        //Dynamically loads a texture.
        #endregion
        /*public Texture2D LoadTexture(string texturePath)
        {
            string name;
            if (texturePath.Contains("/"))
            {
                string[] array = texturePath.Split('/');
                name = array[array.Length - 1];
            }
            else
            {
                name = texturePath;
            }

            Texture2D texture = Content.Load<Texture2D>(texturePath);
            texture.Name = name;

            return texture;
        }*/

        #region Function Explanation
        //Currently Unused.
        #endregion
        protected override void UnloadContent()
        {

        }

        #region Function Explanation
        //Updates Statemanager, rest is temporary.
        #endregion
        protected override void Update(GameTime gameTime)
        {
            //Update the state of the mouse and the keyboard
            keyboard = Keyboard.GetState();
            mouse = Mouse.GetState();

            //Update the StateManager
            StateManager.Instance.Update(gameTime);

            //Cursor
            mousePos = new Vector2(mouse.X, mouse.Y);

            //TODO: Add code for changing conditions while in diferent states.
            if (StateManager.Instance.CurrentGameState.GetType() == new MainMenuState().GetType())
            {

            }

            base.Update(gameTime);
        }

        #region Function Explanation
        //Draws State and therefore all of the game world.
        #endregion
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            StateManager.Instance.Draw(spriteBatch);
            spriteBatch.Begin();
            spriteBatch.Draw(currentMouseTexture, mousePos, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
