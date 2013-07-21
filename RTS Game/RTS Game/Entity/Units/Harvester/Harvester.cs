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
        enum State { Stopped, Moving, Unloading };
        State currentState = State.Stopped;
        Refinary targetRef = null;
        int timeSinceUnload = 0;
        const int timeBetweenUnloads = 1;
        const int amountToUnload = 250;
        Ore[,] oreArray;
        #endregion

        #region Function Explanation
        //Constructor.
        #endregion
        public Harvester(Vector2 tilePosition, Player owner, TileMap world, List<Entity> entityListForRef, Ore[,] oreArray) :
            base(world, owner, tilePosition, texture, maxHealth, maxSpeed, acceleration, 0, 0, 0)
        {
            this.entityListForRef = entityListForRef;
            this.oreArray = oreArray;
        }

        #region Function Explanation
        //Unloads as much ore as it can to the refinary until it is empty.
        //When it is, it changes state to stopped and makes the target refinary
        //null so it will find some more ore in the next update loop*. (*this is
        //handled within update).
        #endregion
        public void UnloadOre()
        {
            currentState = State.Unloading;

            //Actually unloading. Play any animation here.
            if (oreAmount > amountToUnload)
            {
                oreAmount -= amountToUnload;
                owner.Money += amountToUnload;
            }
            else
            {
                owner.Money += maxOreAmount - oreAmount;
                oreAmount = 0;
            }

            if (oreAmount == 0)
            {
                currentState = State.Stopped;
            }

            //Resets ref so update code works correctly.
            targetRef = null;
        }

        #region Function Explanation
        //Finds the nearest unloading station. If there isn't too many Harvesters assigned to it,
        // it assigns itself to it. Might not be needed depending on required complexity, just
        // a thought as unloading stations tend to get rather busy and clogged. 
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
                targetRef = bestChoice;
            }

        } 

        #region Function Explanation
        //Finds the nearest ore patch and moves to it.
        #endregion
        public void MoveToOre()
        {
            Vector2 orePos = Pathfinding.FindClosestOre.BeginSearch(this, world.TileArray, oreArray);
            targetOre = oreArray[(int) orePos.X,(int) orePos.Y];
            Waypoints = WaypointsGenerator.GenerateWaypoints(tilePosition, orePos);
            owner.PlayerMovingEntities.Add(this);
            NextTarget = Waypoints.Dequeue();
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
        //This is the code which moves the harvester to the target fluidly.
        //The target is just the next cell/Tile. when it reaches it,
        //it uses waypoints.Dequeue to remove and use the next waypoint.
        //Also changes occupied tile.
        #endregion
        public override void Move()
        {
            if (Waypoints.Count > 0)
            {
                if (DistanceToDestination < maxSpeed)
                {
                    //If there is a newly placed building in the way, recalculate waypoints.
                    if (world.TileArray[(int)NEXT_TARGET.X, (int)NEXT_TARGET.Y].Obstacle == true)
                    {
                        WaypointsGenerator.GenerateWaypoints(TilePosition, Waypoints.Last());
                    }
                    world.TileArray[(int)NEXT_TARGET.X, (int)NEXT_TARGET.Y].OccupiedByUnit = false;
                    NEXT_TARGET = Waypoints.Dequeue();
                    world.TileArray[(int)NEXT_TARGET.X, (int)NEXT_TARGET.Y].OccupiedByUnit = true;
                }
                else
                {
                    //Accellerating.
                    if (CURRENT_SPEED < maxSpeed)
                    {
                        //If Max speed is smaller than current speed + acceleration, just make it max speed.
                        //Stops it going faster than it's max speed.
                        CURRENT_SPEED = Math.Min(maxSpeed, CURRENT_SPEED += acceleration);
                    }
                    Vector2 direction = new Vector2(NEXT_TARGET.X * world.TileWidth, NEXT_TARGET.Y * world.TileWidth) - pixelPosition;
                    direction.Normalize();
                    velocity = Vector2.Multiply(direction, CURRENT_SPEED);
                    PixelPosition += velocity;
                }
            }
            else    //When the harvester is on the source tile.
            {
                //Stop this.Move being called in GameInstance.Update
                owner.PlayerMovingEntities.Remove(this);
                CURRENT_SPEED = 0;
                currentState = State.Stopped;
            }
        }


        #region Function Explanation
        //Handles the move/mine/unload cycle, by use of states
        //within this class.
        #endregion
        public override void Update(GameTime gametime)
        {
            // needs changing state code when moving/stopping moving.

            if (currentState == State.Stopped)
            {
                //if we're full of ore..
                if (oreAmount == maxOreAmount)
                {
                    //If we're stopped and at refinary, begin unloading.
                    if (targetRef != null && tilePosition == targetRef.TilePosition)
                    {
                        currentState = State.Unloading;                       
                    }

                    //otherwise, move to the refinary.
                    else
                    {
                        MoveToRef();
                    }
                }

                //If we're not stopped and not full, mine or find ore to mine.
                else
                {
                    if (oreArray[(int)tilePosition.X, (int)tilePosition.Y].CurrentAmount != 0)
                    {
                        Mine();
                    }
                    else
                    {
                        currentState = State.Moving;
                        MoveToOre();
                    }
                }
            }

            //Unloading to Refinary code.
            else if (currentState == State.Unloading)
            {
                if (timeSinceUnload == 0)
                {
                    UnloadOre();
                }

                //If it's close enough to the time betwen drops, unload some ore and reset timer.
                if (timeSinceUnload + gametime.ElapsedGameTime.Seconds > timeBetweenUnloads)
                {
                    timeSinceUnload = 0;
                }

                //otherwise, just update the timer.
                else
                {
                    timeSinceUnload += gametime.ElapsedGameTime.Seconds;
                }
            }
            base.Update(gametime);
        }


    }
}
