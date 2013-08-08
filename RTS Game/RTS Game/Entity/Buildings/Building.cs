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
        public Building(TileMap world, Player owner, Vector2 TilePosition, Texture2D texture, Rectangle spriteDimensions)
            : base(world, owner, TilePosition, texture, 100, spriteDimensions)
        {

        }

        #region Function Explanation
        //This method should be called when the width or height are called.
        #endregion
        protected virtual void ApplySizeChanges()
        {
            //clear out the old obstacle tiles
            if (OccupiedTiles != null && OccupiedTiles[0, 0] != null)
            {
                foreach (Tile t in OccupiedTiles)
                {
                    t.Obstacle = false;
                }
            }
            
            OccupiedTiles = new Tile[width, height];

            //Add new Obstacles.
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    //Finding tiles which building occupies. The (int)((SpriteDimensions.Width /2) /GameClass.Tile_Width) is because tile position is 
                    //central. We therefore do this code in order to reverse what we do in the entity draw method and measure from the top left of the sprite.
                    OccupiedTiles[x, y] = World.GetTile(x + ((int)TilePosition.X - (int)((SpriteDimensions.Width /2) /GameClass.Tile_Width)), 
                                                        y + ((int)TilePosition.Y - (int)((SpriteDimensions.Height /2) /GameClass.Tile_Width)));
                }
            }

            //Updates the bounding box of the building
            boundingBox = new Rectangle((int)TilePosition.X, (int)TilePosition.Y, GameClass.Tile_Width * width, GameClass.Tile_Width * height);

            //Recreate health bar
            //healthBar = new HealthBar(this, boundingBox);

            //update all the new Obstacle tiles 
            if (OccupiedTiles[0, 0] != null)
            {
                foreach (Tile t in OccupiedTiles)
                {
                    t.Obstacle = true;
                }
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
                t.Obstacle = false;
            }

            base.OnDeath(killer);
        }
    }
}
