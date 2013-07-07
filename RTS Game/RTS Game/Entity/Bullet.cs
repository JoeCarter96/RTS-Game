using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RTS_Game
{
    class Bullet : Entity
    {
        #region Variables
        private float speed;
        private float damage;
        #endregion

        #region Function Explanation
        //Set to 0,0 because we have to, instantly overwritten using PixelPosition Setter
        //in order to get both an accurate pixel position and tile position.
        #endregion
        public Bullet(Vector2 pixelPosition, Texture2D bulletTexture, float speed, float damage,
            float rotation) : base(new Vector2(0, 0), bulletTexture)
        {
            PixelPosition = pixelPosition;
            base.texture = texture;
            this.speed = speed;
            this.damage = damage;
            this.rotation = rotation;

        }

        #region Function Explanation
        //Moves Bullet, Updates Entity.
        #endregion
        public void update(GameTime gameTime)
        {
            pixelPosition += velocity;

            base.Update(gameTime);
        }
    }
}
