using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RTS_Game
{
    class SplashState : BasicGameState
    {
        #region Variables
        //Bool used to tell us if we are on the first tick of the update() for this state
        private bool FirstTick = true;

        //Bool to state if the user has pressed space to skip the splash screen
        private bool SkipScreen = false;

        //Bool to state if the splash screen has been up for the specificed amount of time
        private bool TimeFinished = false;

        //The time at which the state was entered
        private long StartTime;

        //The time the splash screen will stay for(in milli seconds)
        private long Duration = 1000;
        #endregion

        #region Function Explanation
        //Constructor.
        #endregion
        public SplashState()
        {

        }

        #region Function Explanation
        //Counts down time left showing Splash Screen.
        #endregion
        public override void Update(GameTime gameTime, KeyboardState keyboard, MouseState mouse)
        {
            //On the first call of the loop in this game state, we set the start time
            if (FirstTick)
            {
                StartTime = gameTime.TotalGameTime.Milliseconds;
                FirstTick = false;
            }

            TimeFinished = gameTime.TotalGameTime.TotalMilliseconds > (StartTime + Duration);
            SkipScreen = keyboard.IsKeyDown(Keys.Space);

            if (SkipScreen || TimeFinished)
            {
                StateManager.Instance.CurrentGameState = new MainMenuState();
            }
        }

        #region Function Explanation
        //Draw Splashscreen.
        #endregion
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(Resources.GetGUITextures("SplashScreen"), new Vector2(0, 0), Color.White);
            spriteBatch.End();
        }
    }
}
