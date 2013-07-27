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
        private bool occupiedByUnit;
        private bool obstacle;

        public bool OccupiedByUnit
        {
            get { return obstacle; }
            set { obstacle = value; }
        }

        public bool Obstacle
        {
            get { return occupiedByUnit; }
            set { occupiedByUnit = value; }
        }

        public Vector2 Index
        {
            get { return index; }
        }

        public Rectangle Rectangle
        {
            get { return new Rectangle((int)PixelPosition.X, (int)PixelPosition.Y, GameClass.Tile_Width, GameClass.Tile_Width); }
        }

        public Tile(Vector2 tilePosition, Vector2 index, Texture2D texture)
            : base(tilePosition, texture)
        {
            this.index = index;
            this.TilePosition = tilePosition;
            this.PixelPosition = tilePosition * GameClass.Tile_Width;
        }
    }
}
