using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RTS_Game
{
    class Player
    {
        #region Variables
        private TileMap world;

        private List<Entity> playerEntities = new List<Entity>();
        private List<Entity> playerMovingEntities = new List<Entity>();
        private List<Entity> playerSelectedEntities = new List<Entity>();
        
        //Holds the team coloured units and buildings.
        private Dictionary<String, Texture2D> unitTextures = Resources.getColouredTextures(Resources.GetUnitTextures(), teamColour);
        private Dictionary<String, Texture2D> buildingTextures = Resources.getColouredTextures(Resources.GetBuildingTextures(), teamColour);

        private int money = 0;
        private static Color teamColour = new Color(0, 200, 0);
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

        public Texture2D GetUnitTextures(String requestedName)
        {
             return unitTextures[requestedName]; 
        }

        public Texture2D GetBuildingTextures(String requestedName)
        {
            return buildingTextures[requestedName];
        }

        public int Money
        {
            get { return money; }
            set { money = value; }
        }

        public Color TeamColour
        {
            get { return teamColour; }
            set { teamColour = value; }
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
