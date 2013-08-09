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

        private List<Entity> playerBuildings = new List<Entity>();
        private List<Entity> playerUnits = new List<Entity>();
        private List<Entity> playerHarvesters = new List<Entity>();

        private List<Entity> playerMovingEntities = new List<Entity>();
        private List<Entity> playerSelectedEntities = new List<Entity>();
        
        //Holds the team coloured units and buildings.
        private Dictionary<String, Texture2D> unitTextures = Resources.getColouredTextures(Resources.GetUnitTextures(), teamColour);
        private Dictionary<String, Texture2D> buildingTextures = Resources.getColouredTextures(Resources.GetBuildingTextures(), teamColour);

        private int money = 10000;
        private static Color teamColour = new Color(42, 100, 52);
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

        public List<Entity> PlayerBuildings
        {
            get { return playerBuildings; }
            set { playerBuildings = value; }
        }

        public List<Entity> PlayerUnits
        {
            get { return playerUnits; }
            set { playerUnits = value; }
        }

        public List<Entity> PlayerHarvesters
        {
            get { return playerHarvesters; }
            set { playerHarvesters = value; }
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
