using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace RTS_Game
{
    #region Class Info
    /*Name: GameInstance.cs
     * Represents a play instance of the game. Like a skirmish session.
     * We can everntually pass game settings into the constructor to change
     * how the game instance will play out. 
     */
    #endregion
    class GameInstance
    {
        public GameInstance()
        {

        }

        //Loads all the content of the game instance
        public void LoadContent(ContentManager content)
        {

        }

        protected override void Update(GameTime gameTime, KeyboardState keyboard, MouseState mouse)
        {

        }

        protected override void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
