using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace RTS_Game
{
    #region Class Info
    /*Name: Component.cs
     * Represents a GUI Component that will be drawn to the screen. 
     * This will be the base class for all gui objects
     */
    #endregion
    abstract class Component
    {
        //The game state that the component is being draw on.
        //we store it to access the mouse parameters
        private BasicGameState gameState;
        

        public Component(BasicGameState gameState)
        {
            this.gameState = gameState;
        }

        public virtual void Update(GameTime gameTime, MouseState mouse)
        {
            
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
