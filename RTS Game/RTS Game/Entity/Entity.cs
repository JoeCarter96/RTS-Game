using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RTS_Game
{
    class Entity
    {
        protected Texture2D texture;
        protected float rotation = 0f;

        protected Vector2 position;
        protected Vector2 velocity = new Vector2(0, 0);
        protected Vector2 origin;

        public Entity(Vector2 position, Texture2D texture)
        {
            this.position = position;
            this.texture = texture;
            origin = new Vector2(texture.Width / 2, texture.Height / 2);
        }

        //Returns the center of the texture
        public Vector2 GetCenter()
        {
            float x = position.X + texture.Width / 2;
            float y = position.Y + texture.Height / 2;

            return new Vector2(x, y);
        }

        public virtual void Update(GameTime gameTime)
        {

        }

        //Draws the texture without color tint
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, Color.White, rotation, origin, 1.0f, SpriteEffects.None, 0);
        }

        //Draws the texture with color tint
        public virtual void Draw(SpriteBatch spriteBatch, Color color)
        {
            spriteBatch.Draw(texture, position, null, color, rotation, origin, 1.0f, SpriteEffects.None, 0);
        }
    }
}
