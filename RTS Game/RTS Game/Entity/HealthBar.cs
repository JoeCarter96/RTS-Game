using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RTS_Game
{
    //TODO: make this an entity when the entity tree is cleaned up
    class HealthBar : Entity
    {
        #region Variables
        private HealthEntity target;
        //The bounding box of the entity we are drawing around
        private Rectangle boundingBox;
        //The destination rectangle of the beackground and foreground of the bar
        private Rectangle DestinationBack = Rectangle.Empty;
        private Rectangle DestinationFront = Rectangle.Empty;
        #endregion

        #region Function Explanation
        //Constructor.
        #endregion
        public HealthBar(HealthEntity target, Rectangle boundingBox)
            :base(target.TilePosition, Resources.GetGUITextures("HealthBar"))
        {
            this.target = target;
            this.boundingBox = boundingBox;
        }

        #region Function Explanation
        //Recalculates Position and Rectangles.
        #endregion
        public override void Update(GameTime gameTime)
        {
            //The targets position + an offset that needs tweeking
            Vector2 position = target.PixelPosition + new Vector2(0, boundingBox.Height + boundingBox.Height / 8);

            //DestinationBack logic
            int width = boundingBox.Width;
            DestinationBack = new Rectangle((int)position.X, (int)position.Y, width, texture.Height);
            
            //DestinationFront logic
            width = (int)(boundingBox.Width * target.GetHealthPercentage());
            DestinationFront = new Rectangle((int)position.X, (int)position.Y, width, texture.Height);
        }

        #region Function Explanation
        //Just draws Healthbar.
        #endregion
        public override void Draw(SpriteBatch spriteBatch)
        {
            //draw the background
            spriteBatch.Draw(texture, DestinationBack, Color.Red);

            //draw the foreground
            spriteBatch.Draw(texture, DestinationFront, Color.Green);
        }
    }
}
