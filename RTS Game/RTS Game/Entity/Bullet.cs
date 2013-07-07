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
        private float speed;
        private float damage;

        //Set to 0, 0, instantly  overwritten using PixelPosition.
        public Bullet(Vector2 pixelPosition, Texture2D bulletTexture, float speed, float damage,
            float rotation) :
            base(new Vector2(0, 0), bulletTexture)
        {
            PixelPosition = pixelPosition;
            base.texture = texture;
            this.speed = speed;
            this.damage = damage;
            this.rotation = rotation;

        }

        public void update(GameTime gameTime)
        {
            pixelPosition += velocity;
        }
    }
}
