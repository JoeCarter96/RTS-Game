using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RTS_Game
{
    class Harvester : Unit
    {
        #region Variables
        static float maxHealth = 100;
        static Texture2D texture = Resources.GetUnitTextures("HeavyTank");
        static float maxSpeed = 1.5f;
        static float acceleration = 0.2f;
        static float damage = 0;
        static float AOE = 0;
        static float ROF = 0;

        protected Ore targetOre;
        #endregion

        Ore[,] Ore = new Ore[30, 30];   //TEMP so find nearest ore works. This will be in gameclass and be passed to all harvesters as they are made.


        #region Function Explanation
        //Constructor.
        #endregion
        public Harvester(Vector2 tilePosition, Player owner, TileMap world) :
            base(world, owner, tilePosition, texture, maxHealth, maxSpeed, acceleration, damage,
            AOE, ROF)
        {
            
        }


        #region Function Explanation
        //Finds the nearest docking station. If there isn't too many Harvesters assigned to it,
        // it assigns itself to it. Might not be needed depending on required complexity, just
        // a thought as docking stations tend to get rather busy and clogged. 
        #endregion
        public void AssignDock()
        {

        }

        #region Function Explanation
        //Finds the nearest ore patch and moves to it.
        #endregion
        public void findNearestOre()
        {

            Vector2 orePos = Pathfinding.FindClosestOre.BeginSearch(this, world.TileArray);
            targetOre = Ore[(int) orePos.X,(int) orePos.Y];
            Waypoints = WaypointsGenerator.GenerateWaypoints(tilePosition, orePos);
        }

        #region Function Explanation
        //Firing code, updates Entity tree for this instance.
        #endregion
        public override void Update(GameTime gametime)
        {
            if (target != null && bulletTime >= ROF)
            {
                //Firing Code Here
                bulletTime = 0f;
            }

            base.Update(gametime);
        }
    }
}
