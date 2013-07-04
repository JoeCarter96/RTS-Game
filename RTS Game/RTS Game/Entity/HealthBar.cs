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
    class HealthBar
    {
        private HealthEntity target;
        private const int HeightOffset = 70;

        public HealthBar(HealthEntity target)
        {
            this.target = target;
        }

        private void DrawForeground(SpriteBatch spriteBatch)
        {
            Texture2D texture = Resources.GetGUITextures("HealthFore");
            
            double percentage = target.GetHealthPercentage();
            int width = (int)Math.Floor(texture.Width * percentage);
            
            Rectangle destRectangle = new Rectangle((int)target.PixelPosition.X, (int)target.PixelPosition.Y + HeightOffset, width, texture.Height);
            spriteBatch.Draw(texture, destRectangle, Color.White);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //draw the background
            spriteBatch.Draw(Resources.GetGUITextures("HealthBack"), target.PixelPosition + new Vector2(0, HeightOffset), Color.White);

            //draw trhe forground
            DrawForeground(spriteBatch);
        }
    }
}
