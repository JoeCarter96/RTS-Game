using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace RTS_Game
{
    //The base class for every game state, contains some methods
    //that will be called by the game state manager when switching states
    abstract class BasicGameState
    {
        private StateManager manager;
        public BasicGameState(StateManager manager)
        {
            this.manager = manager;
        }

        //by pulling out jsut this method from the manager we can limit the access of the manager
        public void EnterState(States state)
        {
            manager.EnterState(state);
        }

        //called when the manager changes to this state
        public abstract void OnEnter();

        //called when the manager exits from this state
        public abstract void OnExit();

        //The game update method
        public abstract void Update(GameTime gameTime, KeyboardState keyboard, MouseState mouse);

        //The game draw method
        public abstract void Draw(SpriteBatch spriteBatch);
    }
}
