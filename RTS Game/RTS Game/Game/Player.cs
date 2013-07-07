using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RTS_Game
{
    public class Player
    {
        private List<Unit> units = new List<Unit>();
        private List<Unit> movingUnits = new List<Unit>();

        public List<Unit> MovingUnits
        {
            get { return movingUnits; }
            set { movingUnits = value; }
        }

        public List<Unit> Units
        {
            get { return units; }
            set { units = value; }
        }


        private TileMap world;

        public Player(TileMap world)
        {
            this.world = world;
        }
    }
}
