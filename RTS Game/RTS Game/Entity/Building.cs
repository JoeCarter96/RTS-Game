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
        private int width = 1;
        private int height = 1;

        Tile[,] OccupiedTiles;

        public Building(TileMap world, Player owner, Texture2D texture, Vector2 TilePosition)
            :base(world, owner, TilePosition, texture, 100)
        {
            
        }

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
