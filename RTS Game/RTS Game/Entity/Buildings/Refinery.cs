using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RTS_Game
{
    class Refinery : Building
    {
        private int numberOfHarvesters = 0;
        //This limit can be broken if there are no Refinerys anywhere near but this one.
        private int harvesterLimit = 5;

        private Vector2 mineSpot;

        private bool inUse = false;

        public bool InUse
        {
            get { return inUse; }
            set { inUse = value; }
        }
        
        public Vector2 MineSpot
        {
            get { return mineSpot; }
            set { mineSpot = value; }
        }
        
        public int NumberOfHarvesters
        {
            get { return numberOfHarvesters; }
            set { numberOfHarvesters = value; }
        }

        public int HarvesterLimit
        {
            get { return harvesterLimit; }
            set { harvesterLimit = value; }
        }

        #region Variable: SpriteDimensions
        static Rectangle spriteDimensions = new Rectangle(0, 0, 72, 72);
        #endregion

        public Refinery(TileMap world, Player owner, Vector2 TilePosition)
            : base(world, owner, TilePosition, owner.GetBuildingTextures("Refinery"), spriteDimensions)
        {
            MaxHealth = 500;
            Width = 3;
            Height = 3;
            ApplySizeChanges();
            MineSpot = new Vector2(TilePosition.X, TilePosition.Y + 1);
        }

        #region Function Explanation
        //This method should be called when the width or height are called.
        #endregion
        protected override void ApplySizeChanges()
        {
            //clear out the old obstacle tiles
            if (OccupiedTiles != null && OccupiedTiles[0, 0] != null)
            {
                foreach (Tile t in OccupiedTiles)
                {
                    t.Obstacle = false;
                }
            }

            OccupiedTiles = new Tile[Width, Height];

            //Add new Obstacles.
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    //Finding tiles which building occupies. The (int)((SpriteDimensions.Width /2) /GameClass.Tile_Width) is because tile position is 
                    //central. We therefore do this code in order to reverse what we do in the entity draw method and measure from the top left of the sprite.
                    OccupiedTiles[x, y] = World.GetTile(x + ((int)TilePosition.X - (int)((SpriteDimensions.Width / 2) / GameClass.Tile_Width)),
                                                        y + ((int)TilePosition.Y - (int)((SpriteDimensions.Height / 2) / GameClass.Tile_Width)));
                }
            }

            //Updates the bounding box of the building
            boundingBox = new Rectangle((int)TilePosition.X, (int)TilePosition.Y, GameClass.Tile_Width * Width, GameClass.Tile_Width * Height);

            //Recreate health bar
            //healthBar = new HealthBar(this, boundingBox);

            //Remove any tiles we don't want to be occupied (mostly for Refinery)
            OccupiedTiles[0, 0] = null;
            OccupiedTiles[2, 0] = null;
            OccupiedTiles[1, 2] = null;
            OccupiedTiles[2, 2] = null;

            //update all the new Obstacle tiles 
                foreach (Tile t in OccupiedTiles)
                {
                    if (t != null)
                    {
                        t.Obstacle = true;
                    }
                }
            }
    }
}
