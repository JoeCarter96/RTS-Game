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

        static int oreAmount = 0;
        const int maxOreAmount = 500;
        List<Entity> entityListForRef;
        protected Ore targetOre;
        enum State { Stopped, Moving };
        State currentState = State.Stopped;
        #endregion

        Ore[,] oreArray = new Ore[30, 30];   //TEMP so find nearest ore works. This will be in gameclass and be passed to all harvesters as they are made.

        #region Function Explanation
        //Constructor.
        #endregion
        public Harvester(Vector2 tilePosition, Player owner, TileMap world, List<Entity> entityListForRef) :
            base(world, owner, tilePosition, texture, maxHealth, maxSpeed, acceleration, 0, 0, 0)
        {
            this.entityListForRef = entityListForRef;
        }


        #region Function Explanation
        //Finds the nearest docking station. If there isn't too many Harvesters assigned to it,
        // it assigns itself to it. Might not be needed depending on required complexity, just
        // a thought as docking stations tend to get rather busy and clogged. 
        #endregion
        public void MoveToRef()
        {
            Refinary bestChoice = null;
            Double lowestSolution = double.MaxValue;

            foreach (Refinary r in entityListForRef.ToList())
            {
                double diff = (r.TilePosition.X - tilePosition.X) *
                    (r.TilePosition.X - tilePosition.X);

                double solution = diff * (r.NumberOfHarvesters
                    / r.HarvesterLimit);

                if (solution < lowestSolution)
                {
                    lowestSolution = solution;
                    bestChoice = r;
                }           
            }

            if (bestChoice != null)
            {
                this.Waypoints = WaypointsGenerator.GenerateWaypoints(this.tilePosition, bestChoice.TilePosition);
            }

        }

        #region Function Explanation
        //Finds the nearest ore patch and moves to it.
        #endregion
        public void MoveToOre()
        {

            Vector2 orePos = Pathfinding.FindClosestOre.BeginSearch(this, world.TileArray);
            targetOre = oreArray[(int) orePos.X,(int) orePos.Y];
            Waypoints = WaypointsGenerator.GenerateWaypoints(tilePosition, orePos);
        }

        #region Function Explanation
        //Mines as much as it can from the ore tile it is on top of.
        #endregion
        public void Mine()
        {
            Ore tileToMine = oreArray[(int)tilePosition.X, (int)tilePosition.Y];

            if (maxOreAmount - oreAmount > tileToMine.CurrentAmount)
            {
                if (tileToMine.CurrentAmount > tileToMine.DEPLETIONAMOUNT)
                {
                    oreAmount += tileToMine.DEPLETIONAMOUNT;
                    tileToMine.CurrentAmount -= tileToMine.DEPLETIONAMOUNT;
                }
                else
                {
                    oreAmount += tileToMine.CurrentAmount;
                    tileToMine.CurrentAmount = 0;
                }
            }
            else
            {
                tileToMine.CurrentAmount -= (maxOreAmount - oreAmount);
                oreAmount = maxOreAmount;
            }
        }

        #region Function Explanation
        //Updates Entity tree for this instance.
        #endregion
        public override void Update(GameTime gametime)
        {
            // Needs timing code
            // needs changing state code when moving/stopping moving.
            if (currentState == State.Stopped)
            {
                if (oreAmount == maxOreAmount)
                {
                    MoveToRef();
                }
                else
                {
                    if (oreArray[(int)tilePosition.X, (int)tilePosition.Y].CurrentAmount != 0)
                    {
                        Mine();
                    }
                    else
                    {
                        MoveToOre();
                    }
                }
            }
            base.Update(gametime);
        }
    }
}
