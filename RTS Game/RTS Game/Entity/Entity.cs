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
        #region Variable: Texture
        private Texture2D texture;
        public Texture2D Texture
        {
            get { return texture; }
            protected set
            {
                texture = value;
                BoundingBoxSize = new Size(texture);

            }
        }
        #endregion
        #region Variable: Rotation
        private float rotation = 0f;
        public float Rotation
        {
            get { return rotation; }
            protected set
            {
                //NOTE: this might not work, it is untested
                origin = new Vector2(Texture.Width / 2, Texture.Height / 2);
                rotation = value;
                origin = Vector2.Zero;
            }
        }
        #endregion

        #region Variable: Oragin
        private Vector2 origin = Vector2.Zero;
        private Vector2 Origin
        {
            get { return origin; }
            set { origin = value; }
        }
        #endregion
        #region Variable: Velocity
        protected Vector2 velocity = Vector2.Zero;
        public Vector2 Velocity
        {
            get { return velocity; }
            protected set { velocity = value; }
        }
        #endregion

        #region Variable: PixelPosition
        private Vector2 pixelPosition;
        public Vector2 PixelPosition
        {
            get { return pixelPosition; }
            protected set
            {
                pixelPosition = value;

                double newX = (int)Math.Round((decimal)pixelPosition.X / GameClass.Tile_Width);
                double newY = (int)Math.Round((decimal)pixelPosition.Y / GameClass.Tile_Width);

                tilePosition = new Vector2((float)newX, (float)newY);
            }
        }
        #endregion
        #region Variable: TilePosition
        private Vector2 tilePosition;
        public Vector2 TilePosition
        {
            get { return tilePosition; }
            protected set
            {
                tilePosition = value;
                PixelPosition = value * GameClass.Tile_Width;
            }
        }
        #endregion

        #region Variable: BoundingBox
        public Rectangle BoundingBox
        {
            get { return boundingBoxSize.CreateRectangle(PixelPosition); }
        }
        #endregion
        #region Variable: BoundingBoxSize
        private Size boundingBoxSize;
        private Size BoundingBoxSize
        {
            get { return boundingBoxSize; }
            set { boundingBoxSize = value; }
        }
        #endregion
        #region Variable: BoundingBoxWidth
        public int BoundingBoxWidth
        {
            get { return BoundingBoxSize.Width; }
            set { BoundingBoxSize.Width = value; }
        }
        #endregion
        #region Variable: BoundingBoxHeight
        public int BoundingBoxHeight
        {
            get { return boundingBoxSize.Height; }
            protected set { BoundingBoxSize.Height = value; }
        }
        #endregion


        #region Function Explanation
        //Constructor.
        #endregion
        public Entity(Vector2 tilePosition, Texture2D texture)
        {
            //we assign it to the property to also have it calculate the pixel position
            TilePosition = tilePosition;
            Texture = texture;
        }

        #region Function Explanation
        //Returns the center of the texture.
        #endregion
        public Vector2 GetCenter()
        {
            float x = PixelPosition.X + (Texture.Width / 2);
            float y = PixelPosition.Y + (Texture.Height / 2);

            return new Vector2(x, y);
        }

        public Vector2 GetCenterTile()
        {
            //TEMP. Uses ToTile.
            return new Vector2(ToTile(BoundingBox.Center.X), ToTile(BoundingBox.Center.Y));
        }

        public static int ToTile(int pixelPos)
        {
            return (int)Math.Floor((decimal)pixelPos / GameClass.Tile_Width);
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
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, BoundingBox, null, Color.White, rotation, origin, SpriteEffects.None, 0);
        }

        #region Function Explanation
        //Draws the Texture with Color tint.
        #endregion
        public virtual void Draw(SpriteBatch spriteBatch, Color color)
        {
            spriteBatch.Draw(Texture, BoundingBox, null, color, rotation, origin, SpriteEffects.None, 0);
        }
    }
}