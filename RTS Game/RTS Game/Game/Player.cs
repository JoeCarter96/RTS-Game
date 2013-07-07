using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RTS_Game
{
    class Player
    {
        private List<Unit> playerEntities = new List<Unit>();
        private List<Unit> playerMovingEntities = new List<Unit>();

        public List<Unit> MovingUnits
        {
            get { return playerMovingEntities; }
            set { playerMovingEntities = value; }
        }

        public List<Unit> Units
        {
            get { return playerEntities; }
            set { playerEntities = value; }
        }


        private TileMap world;

        public Player(TileMap world)
        {
            this.world = world;
        }
    }
}
