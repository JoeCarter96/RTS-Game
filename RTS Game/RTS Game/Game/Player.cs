using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RTS_Game.Game
{
    class Player
    {
        private List<Unit> Units;
        private List<Unit> movingUnits;
        private TileMap world;

        public Player(TileMap world)
        {
            this.world = world;
        }
    }
}
