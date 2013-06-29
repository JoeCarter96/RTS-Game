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
        //Bool used to tell us if we are on the first tick of the update() for this state
        private bool FirstTick = true;

        //Bool to state if the user has pressed space to skip the splash screen
        private bool SkipScreen = false;

        //Bool to state if the splash screen has been up for the specificed amount of time
        private bool TimeFinished = false;

        //
        private long StartTime;
        private long Duration = 5000;

        public SplashState(StateManager manager)
            : base(manager)
        {

        }

        public override void OnEnter()
        {
            Console.WriteLine("Splash State has been entered");
        }

        public override void OnExit()
        {
            
        }

        public override void Update(GameTime gameTime, KeyboardState keyboard, MouseState mouse)
        {
            if (FirstTick)
            {
                StartTime = gameTime.TotalGameTime.Milliseconds;
                FirstTick = false;
            }

            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            
        }
    }
}
