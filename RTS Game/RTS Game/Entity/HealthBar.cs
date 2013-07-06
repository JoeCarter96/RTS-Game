using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RTS_Game
{
    //TODO: make this an entity when the entity tree is cleaned up
    //TOFO: Use a single texture for health bars with colour tinting
    class HealthBar : Entity
    {
        private const int HeightOffset = 70;

        private HealthEntity target;
        private double percentage = 1;
        private Rectangle destRectangle = Rectangle.Empty;

        public HealthBar(HealthEntity target)
            :base(target.TilePosition, Resources.GetGUITextures("HealthFore"))
        {
            this.target = target;
            Update(null);
        }

        public override void Update(GameTime gameTime)
        {
            Vector2 position = target.PixelPosition + new Vector2(0, HeightOffset);

            percentage = target.GetHealthPercentage();
            int width = (int)Math.Floor(texture.Width * percentage);
            destRectangle = new Rectangle((int)position.X, (int)position.Y, width, texture.Height);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //draw the background
            spriteBatch.Draw(Resources.GetGUITextures("HealthBack"), target.PixelPosition + new Vector2(0, HeightOffset), Color.White);

            //draw the forground
            spriteBatch.Draw(texture, destRectangle, Color.White);
        }
    }
}
