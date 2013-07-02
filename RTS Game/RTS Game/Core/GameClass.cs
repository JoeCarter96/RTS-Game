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
            //Test
            Texture2D Level_Test = Content.Load<Texture2D>("Level_Test");
            Level Level00 = new Level("Test", Level_Test, 0);
            Resources.AddLevelObject(Level00);
            #endregion

            #region GUI Textures
            //Splash Screen
            Texture2D SplashScreen = Content.Load<Texture2D>("Splash");
            SplashScreen.Name = "SplashScreen";
            Resources.AddGUITexture(SplashScreen);
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
