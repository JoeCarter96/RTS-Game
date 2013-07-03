using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RTS_Game
{
    public class Tile : Entity
    {
        private Vector2 index;
        private bool occupied;

        public bool Occupied
        {
            get { return occupied; }
            set { occupied = value; }
        }

        public Vector2 Index
        {
            get { return index; }
        }

        public Rectangle Rectangle
        {
            get { return new Rectangle((int)position.X, (int)position.Y, GameClass.Tile_Width, GameClass.Tile_Width); }
        }

        public Tile(TileMap world, Vector2 position, Vector2 index, Texture2D texture)
            : base(world, position, texture)
        {
            this.index = index;
            this.position = position;
        }
    }
}
