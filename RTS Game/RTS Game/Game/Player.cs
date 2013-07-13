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

        #region Function Explanation
        //Constructor.
        #endregion
        public Player(TileMap world)
        {
            this.world = world;
        }
    }
}
