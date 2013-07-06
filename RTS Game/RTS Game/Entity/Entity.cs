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
        
        protected Vector2 origin = Vector2.Zero;
        protected Vector2 velocity = Vector2.Zero;

        //positioning variables.
        protected Vector2 pixelPosition;
        protected Vector2 tilePosition;

        public Vector2 PixelPosition
        {
            get { return pixelPosition; }
            set { pixelPosition = value;
                tilePosition = value / GameClass.Tile_Width;
            }
        }

        public Vector2 TilePosition
        {
            get { return tilePosition; }
            set { tilePosition = value;
            pixelPosition = value * GameClass.Tile_Width;
            }
        }

        public float Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }

        public Entity(Vector2 tilePosition, Texture2D texture)
        {
            //we assign it to the property to also have it calculate the pixel position
            TilePosition = tilePosition;
            this.texture = texture;
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
