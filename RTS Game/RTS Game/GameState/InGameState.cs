using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RTS_Game
{
    class InGameState : BasicGameState
    {
        TileMap World;

        public InGameState(StateManager manager)
            : base(manager)
        {

        }

        public override void OnEnter()
        {
            Console.WriteLine("Entered Game!");
            Level.Level Level00 = new Level.Level("Test", Resources.GetLevelImage("Level_Test"), 0);    //This needs to be created in the Menu state and then passed here, but I got impatient :D
            World = new TileMap(Level00, 800, 600, 80);
        }

        public override void OnExit()
        {
            
        }

        public override void Update(GameTime gameTime, KeyboardState keyboard, MouseState mouse)
        {
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            World.Draw(spriteBatch);    //Hope I did this right.
        }
    }
}
