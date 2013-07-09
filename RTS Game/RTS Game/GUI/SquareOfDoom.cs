using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RTS_Game
{
    class SquareOfDoom : Component
    {
        private Color color = Color.Black;
        public SquareOfDoom(Vector2 position)
            : base(Resources.GetGUITextures("HealthBar"), position, new Size(100, 100))
        {

        }

        public override void MouseClicked(int x, int y, MouseButton button)
        {
            if (button == MouseButton.Left)
            {
                Random rand = new Random();
                color = new Color(rand.Next(255), rand.Next(255), rand.Next(255), 255);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch, color);
        }
    }
}
