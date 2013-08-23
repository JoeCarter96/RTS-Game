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
        private static float maxHealth = 100;

        private static float maxSpeed = 1.5f;
        private static float acceleration = 0.2f;

        static Rectangle spriteDimensions = new Rectangle(0, 0, 48, 48);

        private int oreAmount = 0;
        const int maxOreAmount = 500;
        private List<Entity> entityListForRef;
        protected Ore targetOre;
        private enum State { Stopped, Moving, Unloading, Waiting };
        private State currentState = State.Stopped;
        private Refinery targetRef = null;
        private int timeSinceLast = 0;
        const int timeBetweenUnloads = 1000;
        const int timeBetweenMines = 750;
        const int amountToUnload = 250;
        private Ore[,] oreArray;
        #endregion

        #region Function Explanation
        //Constructor.
        #endregion
        public Harvester(TileMap world, Player owner, Vector2 tilePosition, List<Entity> entityListForRef, Ore[,] oreArray) :
            base(world, owner, tilePosition, owner.GetUnitTextures("Harvester"), maxHealth, maxSpeed, acceleration, 0, 0, 0,
            spriteDimensions)
        {
            owner.PlayerHarvesters.Add(this);
            this.entityListForRef = entityListForRef;
            this.oreArray = oreArray;
        }

        #region Function Explanation
        //Unloads as much ore as it can to the refinery until it is empty.
        //When it is, it changes state to stopped and makes the target refinery
        //null so it will find some more ore in the next update loop*. (*this is
        //handled within update).
        #endregion
        public void UnloadOre()
        {
            //Actually unloading. Play any animation here.
            if (oreAmount > amountToUnload)
            {
                oreAmount -= amountToUnload;
                Owner.Money += amountToUnload;
            }
            else
            {
                Owner.Money += oreAmount;
                oreAmount = 0;
            }

            if (oreAmount == 0)
            {
                currentState = State.Stopped;
            }
        }

        #region Function Explanation
        //Finds the nearest unloading station. If there isn't too many Harvesters assigned to it,
        // it assigns itself to it. Might not be needed depending on required complexity, just
        // a thought as unloading stations tend to get rather busy and clogged. 
        #endregion
        public void MoveToRef()
        {
            Refinery bestChoice = null;
            Double lowestSolution = double.MaxValue;

            //parsing units in the entity list.
            foreach (Refinery r in entityListForRef.OfType<Refinery>())
            {
                double diff = Math.Abs(r.TilePosition.X - TilePosition.X) *
                    Math.Abs(r.TilePosition.Y - TilePosition.Y);

                double solution = diff * ((double) (r.NumberOfHarvesters + 1)
                    / (double) r.HarvesterLimit);

                if (solution < lowestSolution)
                {
                    lowestSolution = solution;
                    bestChoice = r;
                }
            }

            //If there is a refinery chosen, move to it.
            if (bestChoice != null)
            {
                Waypoints = WaypointsGenerator.GenerateWaypoints(this.TilePosition, bestChoice.MineSpot, false);
                Owner.PlayerMovingEntities.Add(this);
                NextTile = Waypoints.Dequeue();
                targetRef = bestChoice;
                targetRef.NumberOfHarvesters++;
            }

        }

        #region Function Explanation
        //Finds the nearest ore patch and moves to it.
        #endregion
        public void MoveToOre()
        {
            Vector2 orePos = Pathfinding.FindClosestOre.BeginSearch(this, World.TileArray, oreArray);
            targetOre = oreArray[(int)orePos.X, (int)orePos.Y];
            targetOre.BeingMined = true;    //No other harvesters can touch this ore.

            if (targetRef == null)
            {
                Waypoints = WaypointsGenerator.GenerateWaypoints(TilePosition, orePos, false);
            }
            //Ignore the obstacles of the refinery, for when we move from it after unloading.
            else
            {
                Waypoints = WaypointsGenerator.GenerateWaypoints(TilePosition, orePos, targetRef.BoundingBox);
            }

            Owner.PlayerMovingEntities.Add(this);
            NextTile = Waypoints.Dequeue();
        }

        #region Function Explanation
        //Mines as much as it can from the ore tile it is on top of.
        #endregion
        public void Mine()
        {
            Ore tileToMine = oreArray[(int)TilePosition.X, (int)TilePosition.Y];

            if (maxOreAmount - oreAmount > tileToMine.CurrentAmount)
            {
                if (tileToMine.CurrentAmount > tileToMine.DepletionAmount)
                {
                    oreAmount += tileToMine.DepletionAmount;
                    tileToMine.CurrentAmount -= tileToMine.DepletionAmount;
                }
                else
                {
                    oreAmount += tileToMine.CurrentAmount;
                    tileToMine.CurrentAmount = 0;
                    targetOre.BeingMined = false;   //Allows other harvesters to mine the ore we were on.
                }
            }
            else
            {
                tileToMine.CurrentAmount -= (maxOreAmount - oreAmount);
                oreAmount = maxOreAmount;
                tileToMine.BeingMined = false;
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
            //If we're not moving to refinary OR we are and it's in use, we can go there. We can still move if
            // we're waiting, which means we're gonna move to another tile nearby and wait.
            if (targetRef == null || targetRef.InUse == false || currentState == State.Waiting)
            {
                currentState = State.Moving;

                //if we're moving anywhere else, just move.
                if (Waypoints.Count > 0)
                {
                    //If there is a unit in the way.
                    if (World.TileArray[(int)nextTile.X, (int)nextTile.Y].OccupiedByUnit == true)
                    {
                        //If it's waited more then 3 seconds for the unit to move and it has not,
                        //Make the unit an obstacle (will be made false when the unit moves) and 
                        //move around it. Set wait to 0.
                        if (waitTimer + elapsedMills > 1000)
                        {
                            World.TileArray[(int)nextTile.X, (int)nextTile.Y].Obstacle = true;
                            Waypoints = WaypointsGenerator.GenerateWaypoints(CurrentTile, Waypoints.Last(), false);
                            nextTile = Waypoints.Dequeue();
                            waitTimer = 0;
                        }
                        else   //If it's still waiting, increment wait timer.
                        {
                            waitTimer += elapsedMills;
                        }
                    }

                    else if (DistanceToDestination < maxSpeed)   //Changing tile target, if we can.
                    {
                        //If there is a stationary object in the way (stopped tank or newly
                        //placed building) which is not with in rectangle to ignore,
                        //recalculate waypoints.

                        if (World.TileArray[(int)nextTile.X, (int)nextTile.Y].Obstacle == true)
                        {
                            Waypoints = WaypointsGenerator.GenerateWaypoints(CurrentTile, Waypoints.Last(), false);
                            nextTile = Waypoints.Dequeue();
                        }

                        else    //If nothing is in the way, change the target to the next waypoint.
                        {
                            World.TileArray[(int)currentTile.X, (int)currentTile.Y].OccupiedByUnit = false;

                            currentTile = nextTile;
                            nextTile = Waypoints.Dequeue();

                            World.TileArray[(int)currentTile.X, (int)currentTile.Y].OccupiedByUnit = true;
                        }
                    }

                    else    //If there is still space to move to the next target.
                    {
                        //Accellerating.
                        if (CURRENT_SPEED < maxSpeed)
                        {
                            //If Max speed is smaller than current speed + acceleration, just make it max speed.
                            //Stops it going faster than it's max speed.
                            CURRENT_SPEED = Math.Min(maxSpeed, CURRENT_SPEED += acceleration);
                        }
                        //Actually visibly moving, changing directione etc.
                        Vector2 direction = new Vector2(nextTile.X * World.TileWidth, nextTile.Y * World.TileWidth) - PixelPosition;
                        direction.Normalize();
                        Velocity = Vector2.Multiply(direction, CURRENT_SPEED);
                        PixelPosition += Velocity;
                        Rotation = toAngle(direction);
                    }
                }

                else    //When the Unit has no more waypoints
                {
                    if (DistanceToDestination < maxSpeed)   //If it's at it's target.
                    {
                        //Stops this.Move being called in GameInstance.Update
                        Owner.PlayerMovingEntities.Remove(this);
                        CURRENT_SPEED = 0;
                        currentState = State.Stopped;
                        World.TileArray[(int)currentTile.X, (int)currentTile.Y].OccupiedByUnit = false;
                        currentTile = TilePosition;
                        World.TileArray[(int)currentTile.X, (int)currentTile.Y].OccupiedByUnit = true;
                    }
                    else    // OR if it's not on the final tile, continue to move
                    {
                        //Accellerating.
                        if (CURRENT_SPEED < maxSpeed)
                        {
                            //If Max speed is smaller than current speed + acceleration, just make it max speed.
                            //Stops it going faster than it's max speed.
                            CURRENT_SPEED = Math.Min(maxSpeed, CURRENT_SPEED += acceleration);
                        }
                        //Actually visibly moving, changing direction etc.
                        Vector2 direction = new Vector2(nextTile.X * World.TileWidth, nextTile.Y * World.TileWidth) - PixelPosition;
                        direction.Normalize();
                        Velocity = Vector2.Multiply(direction, CURRENT_SPEED);
                        PixelPosition += Velocity;
                        Rotation = toAngle(direction);
                    }
                }
            }
            //If we are trying to move to ref while it's in use, find a nearby tile and wait.
            else
            {
                // TODO: Add code to wait by ref.
                currentState = State.Stopped;
            }
        }

        #region Function Explanation
        //Handles the move/mine/unload cycle, by use of states
        //within this class.
        #endregion
        public override void Update(GameTime gametime) 
        {
            if (currentState == State.Stopped)
            {
                //if we're full of ore..
                if (oreAmount == maxOreAmount)
                {
                    //If we're stopped and at refinery, begin unloading.
                    if (targetRef != null && TilePosition == targetRef.MineSpot)
                    {
                        currentState = State.Unloading;
                        targetRef.InUse = true;
                        Rotation = 5f;
                    }

                    //otherwise, move to the refinery, if we can.
                    else
                    {
                        if (targetRef == null)
                        {
                            currentState = State.Moving;
                            MoveToRef();
                        }
                    }
                }

                //If we're not full, mine or find ore to mine.
                else
                {
                    if (oreArray[(int)TilePosition.X, (int)TilePosition.Y].CurrentAmount != 0)
                    {
                        //If it's close enough to the time betwen drops, mine some ore and reset timer.
                        if (timeSinceLast + gametime.ElapsedGameTime.Milliseconds > timeBetweenMines)
                        {
                            timeSinceLast = 0;
                            Mine();
                            targetOre = oreArray[(int)TilePosition.X, (int)TilePosition.Y];
                        }

                        //otherwise, just update the timer.
                        else
                        {
                            timeSinceLast += gametime.ElapsedGameTime.Milliseconds;
                        }
                    }

                    else
                    {
                        currentState = State.Moving;
                        MoveToOre();
                    }


                    if (targetRef != null)
                    {
                        targetRef.NumberOfHarvesters--;
                        targetRef.InUse = false;
                        targetRef = null;
                    }
                }
            }

            //Unloading to Refinery code.
            else if (currentState == State.Unloading)
            {
                if (timeSinceLast == 0)
                {
                    UnloadOre();
                }

                //If it's close enough to the time betwen drops, unload some ore and reset timer.
                if (timeSinceLast + gametime.ElapsedGameTime.Milliseconds > timeBetweenUnloads)
                {
                    timeSinceLast = 0;
                }

                //otherwise, just update the timer.
                else
                {
                    timeSinceLast += gametime.ElapsedGameTime.Milliseconds;
                }
            }

            base.Update(gametime);
        }
    }
}
