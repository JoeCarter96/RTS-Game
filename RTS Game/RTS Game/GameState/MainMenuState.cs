using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RTS_Game
{
    class MainMenuState : BasicGameState
    {
        Rectangle play = new Rectangle(20, 162, 147, 54);
        Rectangle options = new Rectangle(20, 240, 147, 54);
        Rectangle credits = new Rectangle(20, 317, 147, 54);

        #region Function Explanation
        //Constructor.
        #endregion
        public MainMenuState()
            : base("MainMenuState")
        {
        }


        #region Function Explanation
        //Click code, loading Level. Will eventually be called by Input.
        #endregion
        public override void MouseClicked(int x, int y, MouseButton button)
        {
            if (button == MouseButton.Left)
            {
                if (play.Contains(new Point(x, y)))
                {
                    StateManager.Instance.CurrentGameState = new InGameState(Resources.GetLevelObject(01), null);
                }
            }
        }

        #region Function Explanation
        //Update loop, Updates Basic Game State base.
        #endregion
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        #region Function Explanation
        //Draws Main Menu GUI.
        #endregion
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            spriteBatch.GraphicsDevice.Clear(Color.Black);

            spriteBatch.Draw(Resources.GetGUITextures("LeftBack"), new Rectangle(0, 114, 195, 320), Color.White);

            spriteBatch.Draw(Resources.GetGUITextures("TopRightBack"), new Rectangle(533, 0, 267, 60), Color.White);
            DrawCenterString(spriteBatch, " Heavy Assault \n'Mammoth' Tank", new Vector2(667, 30), Color.Cyan, 0.8f);

            spriteBatch.Draw(Resources.GetGUITextures("BottomRightBack"), new Rectangle(361, 517, 439, 133), Color.White);
            DrawCenterString(spriteBatch, "120mm cannon (x2)\n6-Rack Missile Launcher (x2)\nRH armor, depleted uranium strike plates", new Vector2(581, 560), Color.Cyan, 0.8f);

             

            spriteBatch.Draw(Resources.GetGUITextures("BackgroundUnit"), new Rectangle(252, 127, 484, 363), Color.White);
        
            spriteBatch.Draw(Resources.GetGUITextures("ButtonBack"), play, Color.White);
            spriteBatch.Draw(Resources.GetGUITextures("ButtonBack"), options, Color.White);
            spriteBatch.Draw(Resources.GetGUITextures("ButtonBack"), credits, Color.White);
  
            DrawCenterString(spriteBatch, "Play", new Vector2(play.X + (play.Width / 2), play.Y + (play.Height/2)), Color.Cyan, 1);
            DrawCenterString(spriteBatch, "Options", new Vector2(options.X + (options.Width / 2), options.Y + (options.Height / 2)), Color.Cyan, 1);
            DrawCenterString(spriteBatch, "Credits", new Vector2(credits.X + (credits.Width / 2), credits.Y + (credits.Height / 2)), Color.Cyan, 1);



           // DrawCenterString(spriteBatch, "Main menu", new Vector2(GameClass.Game_Width / 2, 70), Color.White, 2);
         //   DrawCenterString(spriteBatch, "Click anywhere to go to the game!", new Vector2(GameClass.Game_Width / 2, 200), Color.Black, 1); 
            spriteBatch.End();
        }
    }
}
