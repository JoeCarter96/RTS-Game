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
        #region Variables
        protected Texture2D texture;
        protected float rotation = 0f;
        protected Vector2 origin = Vector2.Zero;
        protected Vector2 velocity = Vector2.Zero;

        //Positioning variables
        protected Vector2 pixelPosition;
        protected Vector2 tilePosition;

        //Bounding box variables
        private Size boundingBoxSize;

        #endregion

        public Vector2 PixelPosition
        {
            get { return pixelPosition; }
            set { 
                pixelPosition = value;
                tilePosition = new Vector2((int)Math.Round((decimal)value.X / GameClass.Tile_Width),
                    (int)Math.Round((decimal)value.Y / GameClass.Tile_Width));

            }
        }

        public Vector2 TilePosition
        {
            get { return tilePosition; }
            set { 
                tilePosition = value;
                pixelPosition = value * GameClass.Tile_Width;
            }
        }

        public float Rotation
        {
            get { return rotation; }
            set
            {
                origin = new Vector2(texture.Width / 2, texture.Height / 2);
                rotation = value;
                origin = Vector2.Zero;
            }
        }

        public Texture2D Texture
        {
            get { return texture; }
            set { texture = value; }
        }

        public Rectangle BoundingBox
        {
            //Converts the size into a boundingbox, based on the position of the entity
            //See Size.cs for more info
            get { return boundingBoxSize.CreateRectangle(pixelPosition); }
        }

        protected Size BoundingBoxSize
        {
            set { boundingBoxSize = value; }
            get { return boundingBoxSize; }
        }

        #region Function Explanation
        //Constructor.
        #endregion
        public Entity(Vector2 tilePosition, Texture2D texture)
        {
            //we assign it to the property to also have it calculate the pixel position
            TilePosition = tilePosition;
            this.texture = texture;

            //initially we assume that the size of the entity is its texture size.
            boundingBoxSize = new Size(texture);
        }

        #region Function Explanation
        //Returns the center of the texture.
        #endregion
        public Vector2 GetCenter()
        {
            float x = pixelPosition.X + (texture.Width / 2);
            float y = pixelPosition.Y + (texture.Height / 2);

            return new Vector2(x, y);
        }

        public Vector2 GetCenterTile()
        {
            //TEMP. Uses ToTile.
            return new Vector2(ToTile(BoundingBox.Center.X), ToTile(BoundingBox.Center.Y));
        }

        #region Function Explanation
        //Not currently used.
        #endregion
        public virtual void Update(GameTime gameTime)
        {

        }

        #region Function Explanation
        //Draws the Texture without Colour tint.
        #endregion
        //New draw method uses bounding box to stretch the sprite to fit it
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, BoundingBox, null, Color.White, rotation, origin, SpriteEffects.None, 0);
        }
        /*public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, pixelPosition, null, Color.White, rotation, origin, 1.0f, SpriteEffects.None, 0);
        }*/

        #region Function Explanation
        //Draws the Texture with Color tint.
        #endregion
        //New draw method uses bounding box to stretch the sprite to fit it, with a specified color tint
        public virtual void Draw(SpriteBatch spriteBatch, Color color)
        {
            spriteBatch.Draw(texture, BoundingBox, null, color, rotation, origin, SpriteEffects.None, 0);
        }
        /*public virtual void Draw(SpriteBatch spriteBatch, Color color)
        {
            spriteBatch.Draw(texture, pixelPosition, null, color, rotation, origin, 1.0f, SpriteEffects.None, 0);
        }*/

        public static int ToTile(int pixelPos)
        {   //TEMP.
            return (int)Math.Floor((decimal)pixelPos / GameClass.Tile_Width);
        }
    }
}