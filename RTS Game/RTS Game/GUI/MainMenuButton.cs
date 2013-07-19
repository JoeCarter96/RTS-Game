using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RTS_Game.GUI
{
    class MainMenuButton : Component
    {
        private Color color = Color.Black;
        public MainMenuButton(Vector2 position)
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

