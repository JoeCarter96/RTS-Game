using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RTS_Game
{
    public class Size
    {
        private int width;
        private int height;

        public int Width
        {
            get { return width; }
            set { width = value; }
        }

        public int Height
        {
            get { return height; }
            set { height = value; }
        }

        public Size(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        public Size(Rectangle rect)
        {
            this.width = rect.Width;
            this.height = rect.Height;
        }

        public Rectangle CreateRectangle(Vector2 position)
        {
            return new Rectangle((int)position.X, (int)position.Y, width, height);
        }
    }
}
