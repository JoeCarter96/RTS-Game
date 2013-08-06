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
            }
        }

        #endregion

        #region Variable: Rotation
        private float rotation = 0f;
        public virtual float Rotation
        {
            get { return rotation; }
            set
            {
                origin = new Vector2(SpriteDimensions.Width / 2, SpriteDimensions.Height / 2);
                rotation = value;
            }
        }
        #endregion

        #region Variable: Origin
        private Vector2 origin = Vector2.Zero;
        public Vector2 Origin
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
            set
            {
                pixelPosition = value;

                double newX = (int)Math.Round((decimal)pixelPosition.X / GameClass.Tile_Width);
                double newY = (int)Math.Round((decimal)pixelPosition.Y / GameClass.Tile_Width);

                tilePosition = new Vector2((float)newX, (float)newY);

                Rotation = 0;
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
            get { return boundingBoxSize.CreateRectangle(new Vector2(pixelPosition.X - (spriteDimensions.Width / 2), pixelPosition.Y - (spriteDimensions.Height / 2))); }
        }
        #endregion

        #region Variable: BoundingBoxSize
        private Size boundingBoxSize;
        public Size BoundingBoxSize
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

        #region Variable: SpriteDimensions
        private Rectangle spriteDimensions = new Rectangle();
        public Rectangle SpriteDimensions
        {
            get { return spriteDimensions; }
            set { spriteDimensions = value; }
        }
        #endregion

        #region Variable: SourceRectangle
        private Rectangle sourceRectangle = new Rectangle();
        public Rectangle SourceRectangle
        {
            get { return sourceRectangle; }
            set
            {
                sourceRectangle = value;
                BoundingBoxSize = new Size(spriteDimensions);
            }
        }
        #endregion

        #region Function Explanation
        //Constructor.
        #endregion
        public Entity(Vector2 tilePosition, Texture2D texture, Rectangle spriteDimensions)
        {
            Texture = texture;
            //SpriteSheet Calculations (gets first image in spritesheet).
            SpriteDimensions = spriteDimensions;
            SourceRectangle = spriteDimensions;
            //We assign it to the property to also have it calculate the pixel position
            TilePosition = tilePosition;
            rotation = 0;
        }

        #region Function Explanation
        //Not currently used.
        #endregion
        public virtual void Update(GameTime gameTime)
        {

        }

        #region Function Explanation
        //Draws the Texture without Colour tint. The Rectangle created is offset by half the texture
        //so it is central to the tile/pixel position.
        #endregion
        public virtual void Draw(SpriteBatch spriteBatch)
        {
                spriteBatch.Draw(Texture, boundingBoxSize.CreateRectangle(new Vector2(pixelPosition.X - (spriteDimensions.Width / 2), pixelPosition.Y - (spriteDimensions.Height / 2))), SourceRectangle, Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, 0);
       }

        #region Function Explanation
        //Draws the Texture with Color tint.
        #endregion
        public virtual void Draw(SpriteBatch spriteBatch, Color color)
        {
            spriteBatch.Draw(Texture, boundingBoxSize.CreateRectangle(new Vector2(pixelPosition.X - (spriteDimensions.Width / 2))), SourceRectangle, color, 0f, origin, SpriteEffects.None, 0);
        }
    }
}