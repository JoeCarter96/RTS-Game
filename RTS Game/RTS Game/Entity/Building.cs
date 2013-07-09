using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RTS_Game
{
    class Building : HealthEntity
    {
        #region Variables
        private int width = 1;
        private int height = 1;

        protected Tile[,] OccupiedTiles = null;
        protected Rectangle boundingBox = Rectangle.Empty;
        #endregion

        public int Width
        {
            get { return width; }
            set 
            {
                width = value;
                if (width < 1) { width = 1; }
                if (width > 5) { width = 5; }
            }
        }

        public int Height
        {
            get { return height; }
            set 
            {
                height = value;
                if (height < 1) { height = 1; }
                if (height > 5) { height = 5; }
            }
        }

        #region Function Explanation
        //Constructor.
        #endregion
        public Building(TileMap world, Player owner, Vector2 TilePosition, Texture2D texture)
            :base(world, owner, TilePosition, texture, 100)
        {
            //Assuming the building is a 1x1. We will have to re-call this
            ApplySizeChanges();
        }

        #region Function Explanation
        //This method should be called when the width or height are called.
        #endregion
        protected void ApplySizeChanges()
        {
            //clear out the old occupied tiles
            if (OccupiedTiles != null)
            {
                foreach (Tile t in OccupiedTiles)
                {
                    t.Occupied = false;
                }
            }
            
            OccupiedTiles = new Tile[width, height];
            int xCounter = 0;
            int yCounter = 0;
            for (int x = (int)TilePosition.X; x <= width; x++)
            {
                for (int y = (int)TilePosition.Y; y <= height; y++)
                {
                    OccupiedTiles[xCounter, yCounter] = world.GetTile(x, y);
                    yCounter++;
                }
                xCounter++;
            }

            //Updates the bounding box of the building
            boundingBox = new Rectangle((int)TilePosition.X, (int)TilePosition.Y, GameClass.Tile_Width * width, GameClass.Tile_Width * height);

            //Recreate health bar
            healthBar = new HealthBar(this, boundingBox);

            //update all the new occupied tiles 
            foreach (Tile t in OccupiedTiles)
            {
                t.Occupied = true;
            }
        }

        #region Function Explanation
        //Code called when a building is Destroyed. 
        //Unoccupies tiles used by the building and removes Entity(?)
        #endregion
        public override void OnDeath(HealthEntity killer)
        {
            foreach(Tile t in OccupiedTiles)
            {
                t.Occupied = false;
            }

            base.OnDeath(killer);
        }
    }
}
