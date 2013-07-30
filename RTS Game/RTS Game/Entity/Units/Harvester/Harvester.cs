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

        #region Textures
        private static List<Texture2D> textures = new List<Texture2D> 
        {
            Resources.GetUnitTextures("Harvester"),
            Resources.GetUnitTextures("Harvester"),
            Resources.GetUnitTextures("Harvester"),
            Resources.GetUnitTextures("Harvester"),
            Resources.GetUnitTextures("Harvester"),
            Resources.GetUnitTextures("Harvester"),
            Resources.GetUnitTextures("Harvester"),
            Resources.GetUnitTextures("Harvester"),
        };
        #endregion

        private static float maxSpeed = 1.5f;
        private static float acceleration = 0.2f;

        private int oreAmount = 0;
        const int maxOreAmount = 500;
        List<Entity> entityListForRef;
        protected Ore targetOre;
        enum State { Stopped, Moving, Unloading };
        State currentState = State.Stopped;
        Refinery targetRef = null;
        int timeSinceLast = 0;
        const int timeBetweenUnloads = 250;
        const int timeBetweenMines = 750;
        const int amountToUnload = 25;
        Ore[,] oreArray;
        #endregion

        #region Function Explanation
        //Constructor.
        #endregion
        public Harvester(TileMap world, Player owner, Vector2 tilePosition, List<Entity> entityListForRef, Ore[,] oreArray) :
            base(world, owner, tilePosition, textures, maxHealth, maxSpeed, acceleration, 0, 0, 0)
        {
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
                double diff = (r.TilePosition.X - TilePosition.X) *
                    (r.TilePosition.X - TilePosition.X);

                double solution = diff * (r.NumberOfHarvesters
                    / r.HarvesterLimit);

                if (solution < lowestSolution)
                {
                    lowestSolution = solution;
                    bestChoice = r;
                }
            }

            //If there is a refinery chosen, move to it.
            if (bestChoice != null)
            {
                this.Waypoints = WaypointsGenerator.GenerateWaypoints(this.TilePosition, bestChoice.GetCenterTile(), bestChoice.BoundingBox);
                Owner.PlayerMovingEntities.Add(this);
                NextTarget = Waypoints.Dequeue();
                targetRef = bestChoice;
            }

        } 

        #region Function Explanation
        //Finds the nearest ore patch and moves to it.
        #endregion
        public void MoveToOre()
        {
            Vector2 orePos = Pathfinding.FindClosestOre.BeginSearch(this, World.TileArray, oreArray);
            targetOre = oreArray[(int) orePos.X,(int) orePos.Y];
            targetOre.BeingMined = true;    //No other harvesters can touch this ore.

            if (targetRef == null)
            {
                Waypoints = WaypointsGenerator.GenerateWaypoints(TilePosition, orePos);
            }
            //Ignore the obstacles of the refinery, for when we move from it after unloading.
            else
            {
                Waypoints = WaypointsGenerator.GenerateWaypoints(TilePosition, orePos, targetRef.BoundingBox);
            }

            Owner.PlayerMovingEntities.Add(this);
            NextTarget = Waypoints.Dequeue();
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
            if (Waypoints.Count > 0)
            {
                if (DistanceToDestination < maxSpeed)
                {
                    World.TileArray[(int)NEXT_TARGET.X, (int)NEXT_TARGET.Y].OccupiedByUnit = false;
                    NEXT_TARGET = Waypoints.Dequeue();
                    World.TileArray[(int)NEXT_TARGET.X, (int)NEXT_TARGET.Y].OccupiedByUnit = true;
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
                    Vector2 direction = new Vector2(NEXT_TARGET.X * World.TileWidth, NEXT_TARGET.Y * World.TileWidth) - PixelPosition;
                    direction.Normalize();
                    Velocity = Vector2.Multiply(direction, CURRENT_SPEED);
                    PixelPosition += Velocity;
                    Rotation = toAngle(direction);
                }
            }

            else    //When the harvester has no more waypoints
            {
                if (DistanceToDestination < maxSpeed)
                {
                    //Stops this.Move being called in GameInstance.Update
                    Owner.PlayerMovingEntities.Remove(this);
                    CURRENT_SPEED = 0;
                    currentState = State.Stopped;
                }
                else    //if it's not on the tile, continue to move
                {
                    //Accellerating.
                    if (CURRENT_SPEED < maxSpeed)
                    {
                        //If Max speed is smaller than current speed + acceleration, just make it max speed.
                        //Stops it going faster than it's max speed.
                        CURRENT_SPEED = Math.Min(maxSpeed, CURRENT_SPEED += acceleration);
                    }
                    Vector2 direction = new Vector2(NEXT_TARGET.X * World.TileWidth, NEXT_TARGET.Y * World.TileWidth) - PixelPosition;
                    direction.Normalize();
                    Velocity = Vector2.Multiply(direction, CURRENT_SPEED);
                    PixelPosition += Velocity;
                    Rotation = toAngle(direction);
                }
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
                    if (targetRef != null && TilePosition == targetRef.GetCenterTile())
                    {
                        currentState = State.Unloading;
                    }

                    //otherwise, move to the refinery.
                    else
                    {
                        currentState = State.Moving;
                        MoveToRef();
                    }
                }

                //If we're not stopped and not full, mine or find ore to mine.
                else
                {
                    if (oreArray[(int)TilePosition.X, (int)TilePosition.Y].CurrentAmount != 0)
                    {
                        if (timeSinceLast == 0)
                        {
                            Mine();
                            targetOre = oreArray[(int)TilePosition.X, (int)TilePosition.Y];
                        }

                        //If it's close enough to the time betwen drops, unload some ore and reset timer.
                        if (timeSinceLast + gametime.ElapsedGameTime.Milliseconds > timeBetweenMines)
                        {
                            timeSinceLast = 0;
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
