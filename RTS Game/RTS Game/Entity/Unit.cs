using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RTS_Game
{
    #region Class Info
        /*Name: Unit.cs
          Represents a spawnable unit which can eb moved about the battle field
          and shoot other enemys. Has a lot of stat veriables that will be changed 
          over time as the game progresses in development. 
        */
    #endregion
    class Unit : HealthEntity
    {
        public Unit(Vector2 position, Texture2D texture, double maxHealth)
            :base(position, texture, maxHealth)
        {

        }

        public override void Update(GameTime gameTime)
        {
            
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            
            base.Draw(spriteBatch);
        }
    }
}
