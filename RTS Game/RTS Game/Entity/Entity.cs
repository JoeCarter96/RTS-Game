using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RTS_Game
{
    public class Entity
    {
        protected Texture2D texture;
        protected float rotation = 0f;

        protected Vector2 pixelPosition;
        protected Vector2 tilePosition;
        protected Vector2 velocity = new Vector2(0, 0);
        protected Vector2 origin;
        protected TileMap world;

        public Vector2 PixelPosition
        {
            get { return pixelPosition; }
            set { pixelPosition = value;
                tilePosition = value / world.TileWidth;
            }
        }

        public Vector2 TilePosition
        {
            get { return tilePosition; }
            set { tilePosition = value;
                pixelPosition = value * world.TileWidth;
            }
        }

        public Entity(TileMap world, Vector2 tilePosition, Texture2D texture)
        {
            this.world = world;
            this.tilePosition = tilePosition;
            this.texture = texture;

            origin = new Vector2(0, 0);
        }

        //Returns the center of the texture
        public Vector2 GetCenter()
        {
            float x = pixelPosition.X + texture.Width / 2;
            float y = pixelPosition.Y + texture.Height / 2;

            return new Vector2(x, y);
        }

        public virtual void Update(GameTime gameTime)
        {

        }

        //Draws the texture without color tint
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, pixelPosition, null, Color.White, rotation, origin, 1.0f, SpriteEffects.None, 0);
        }

        //Draws the texture with color tint
        public virtual void Draw(SpriteBatch spriteBatch, Color color)
        {
            spriteBatch.Draw(texture, pixelPosition, null, color, rotation, origin, 1.0f, SpriteEffects.None, 0);
        }
    }
}
