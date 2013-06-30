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
        public MainMenuState(StateManager manager)
            : base(manager)
        {
        }

        public override void OnEnter()
        {
            Console.WriteLine("Entered the main menu state");
            EnterState(States.InGame);  //TEMP, jumps to game straight away.
        }

        public override void OnExit()
        {
            manager.LevelID = 0;    //TEMP, gives it a level without messing with GUI.
        }

        public override void Update(GameTime gameTime, KeyboardState keyboard, MouseState mouse)
        {
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            spriteBatch.End();
        }
    }
}
