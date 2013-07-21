using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RTS_Game
{
    class Player
    {
        #region Variables
        private List<Entity> playerEntities = new List<Entity>();
        private List<Entity> playerMovingEntities = new List<Entity>();
        private List<Entity> playerSelectedEntities = new List<Entity>();
        private TileMap world;

        private int money = 0;

        #endregion

        public List<Entity> PlayerMovingEntities
        {
            get { return playerMovingEntities; }
            set { playerMovingEntities = value; }
        }

        public List<Entity> PlayerSelectedEntities
        {
            get { return playerSelectedEntities; }
            set { playerSelectedEntities = value; }
        }

        public List<Entity> Entities
        {
            get { return playerEntities; }
            set { playerEntities = value; }
        }

        public int Money
        {
            get { return money; }
            set { money = value; }
        }

        #region Function Explanation
        //Constructor.
        #endregion
        public Player(TileMap world)
        {
            this.world = world;
        }



        
    }
}
