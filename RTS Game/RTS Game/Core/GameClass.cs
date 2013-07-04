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

namespace RTS_Game
{
    public class GameClass : Microsoft.Xna.Framework.Game
    {
        //Game constants
        public const int Game_Width = 800;
        public const int Game_Height = 600;
        public const int Tile_Width = 80;

        //Game objects
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private KeyboardState keyboard;
        private MouseState mouse;

        //fps stuff
        private double LastFrameTime = 0;
        private double FPS = 0;

        public GameClass()
        {
            //Content setup
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            //make the mouse visible, we want to see the mouse
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            //Set the size of the game window
            graphics.PreferredBackBufferHeight = Game_Height;
            graphics.PreferredBackBufferWidth = Game_Width;
            graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            #region Background Textures.

            //Debug Tile
            Texture2D DebugTile = Content.Load<Texture2D>("DebugTile");
            DebugTile.Name = "DebugTile";
            Resources.AddBackgroundTexture(DebugTile);

            //Grass01
            Texture2D Grass01 = Content.Load<Texture2D>("Grass01");
            Grass01.Name = "Grass01";
            Resources.AddBackgroundTexture(Grass01);

            //Road01
            Texture2D Road01 = Content.Load<Texture2D>("Road01");
            Road01.Name = "Road01";
            Resources.AddBackgroundTexture(Road01);

            //Water01
            Texture2D Water01 = Content.Load<Texture2D>("Water01");
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

            //Snow Test
            Texture2D Snow_Test = Content.Load<Texture2D>("Levels/Snow_Test");
            Level Level02 = new Level("Snow Test", Snow_Test, 2);
            Resources.AddLevelObject(Level02);

            //Desert Test
            Texture2D Desert_Test = Content.Load<Texture2D>("Levels/Desert_Test");
            Level Level03 = new Level("Desert Test", Desert_Test, 3);
            Resources.AddLevelObject(Level03);

            //Ocean Combat
            Texture2D Ocean_Combat = Content.Load<Texture2D>("Levels/Ocean_Combat");
            Level Level04 = new Level("Ocean Combat", Ocean_Combat, 4);
            Resources.AddLevelObject(Level04);

            //MegaMap
            Texture2D MegaMap = Content.Load<Texture2D>("Levels/MegaMap");
            Level Level05 = new Level("MegaMap", MegaMap, 5);
            Resources.AddLevelObject(Level05);
            #endregion

            #region GUI Textures
            //Splash Screen
            Texture2D SplashScreen = Content.Load<Texture2D>("Splash");
            SplashScreen.Name = "SplashScreen";
            Resources.AddGUITexture(SplashScreen);
            #endregion

            #region Unit Textures
            //Splash Screen
            Texture2D Tank01 = Content.Load<Texture2D>("Tank01");
            Tank01.Name = "Tank01";
            Resources.AddUnitTexture(Tank01);
            #endregion
            
            Resources.TestFont = Content.Load<SpriteFont>("TestFont");
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            //Update the state of the mouse and the keyboard
            keyboard = Keyboard.GetState();
            mouse = Mouse.GetState();

            //Update the StateManager
            StateManager.Instance.Update(gameTime, keyboard, mouse);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            StateManager.Instance.Draw(spriteBatch);
            base.Draw(gameTime);
        }
    }
}
