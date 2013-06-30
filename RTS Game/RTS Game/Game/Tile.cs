using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RTS_Game
{
    class Tile : Entity
    {
        private Vector2 index;

        public Vector2 Index
        {
            get { return index; }
        }

        public Tile(Vector2 position, Vector2 index, Texture2D texture)
            :base(position, texture)
        {
            this.index = index;
        }
    }
}
