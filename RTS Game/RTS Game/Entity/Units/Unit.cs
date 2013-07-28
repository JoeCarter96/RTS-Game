using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RTS_Game
{
    #region Class Info
        /*Name: Unit.cs
          Represents a spawnable harvester which can be moved about the battle field
          and shoot other enemys. Has a lot of stat veriables that will be changed 
          over time as the game progresses in development. 
        */
    #endregion

    class Unit : HealthEntity
    {
        #region Variables
            #region Unit Attributes
            protected float maxSpeed;
            protected float acceleration;
            protected float damage;
            protected float AOE;
            protected float ROF;
            protected float CURRENT_SPEED;
            protected float bulletTime;

            protected HealthEntity target;
            #endregion

            #region Passed Variables
            
            #endregion

            #region Pathfinding
            //Next Tile harvester is moving to.
            protected Vector2 NEXT_TARGET = new Vector2();
            protected Queue<Vector2> WAYPOINTS = new Queue<Vector2>();
             #endregion
        #endregion

        public float MaxSpeed
        {
            get { return maxSpeed; }
            set { maxSpeed = value; }
        }

        public Queue<Vector2> Waypoints
       {
           get { return WAYPOINTS; }
           set { WAYPOINTS = value; }
       }

        public Vector2 NextTarget
        {
            get { return NEXT_TARGET; }
            set { NEXT_TARGET = value; }
        }


        #region Function Explanation
        //Constructor, Adds Unit to entity list, passes a bunch of variables and then creates a PF array.
        //Sets Next Target to Tile Position so that when Move() is called it immediately looks for the next tile.
        #endregion
        public Unit(TileMap world, Player owner, Vector2 tilePosition, Texture2D texture, double maxHealth,
           float maxSpeed, float acceleration, float damage, float AOE, float ROF)
            : base(world, owner, tilePosition, texture, maxHealth)
        {
            this.maxSpeed = maxSpeed;
            this.acceleration = acceleration;
            this.damage = damage;
            this.AOE = AOE;
            this.ROF = ROF;
            world.TileArray[(int) tilePosition.X, (int) tilePosition.Y].OccupiedByUnit = true;
        }

        #region Function Explanation
        //Gets the Distance to the next target Cell.
        #endregion
        public float DistanceToDestination
        {
            get { return Vector2.Distance(PixelPosition, new Vector2(NEXT_TARGET.X * World.TileWidth, NEXT_TARGET.Y * World.TileWidth)); }
        }

        #region Function Explanation
        //This is the code which moves the harvester to the target fluidly.
        //The target is just the next cell/Tile. when it reaches it,
        //it uses waypoints.Dequeue to remove and use the next waypoint.
        //Also changes occupied tile.
        #endregion
        public virtual void Move()
        {
            if (Waypoints.Count > 0)
            {
                if (DistanceToDestination < maxSpeed)
                {
                    //If there is a newly placed building in the way, recalculate waypoints.
                    if (World.TileArray[(int)NEXT_TARGET.X, (int)NEXT_TARGET.Y].Obstacle == true)
                    {
                        WaypointsGenerator.GenerateWaypoints(TilePosition, Waypoints.Last());
                    }
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
            else    //When the Unit has no more waypoints
            {
                if (DistanceToDestination < maxSpeed)
                {
                    //Stops this.Move being called in GameInstance.Update
                    Owner.PlayerMovingEntities.Remove(this);
                    CURRENT_SPEED = 0;
                }
                else    //if it's not on the final tile, continue to move
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
        //Converts a Vector2 to an angle.
        #endregion
        public float toAngle(Vector2 vector)
        {
            //Find Angle using Trig (in rads):
            float degrees = (float) Math.Atan2((float)vector.Y, (float)vector.X);
            //Return Degrees in rads.
            return (float)(degrees);
        }

        #region Function Explanation
        //Updates Entity tree, increases time since last shot.
        #endregion
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            bulletTime += (float) gameTime.ElapsedGameTime.TotalSeconds;
        }

        #region Function Explanation
        //Draw Method, Draws Base texture.
        #endregion
        public override void Draw(SpriteBatch spriteBatch)
        {   
            base.Draw(spriteBatch);
        }
    }
}



/*
        public void FindNextCell()
        {
            //If we're not at the final target.
            if (base.tilePosition != FINAL_TARGET)
            {
                int highest = int.MaxValue;
                Vector2 nextTarget = new Vector2(base.tilePosition.X, base.tilePosition.Y);

                //Left
                try
                {
                    if (highest < PF_ARRAY[(int)base.tilePosition.X - 1, (int)base.tilePosition.Y] && PF_ARRAY[(int)base.tilePosition.X - 1, (int)base.tilePosition.Y] > 0 && world.TileArray[(int)base.tilePosition.X - 1, (int) base.tilePosition.Y].OccupiedByUnit != true)
                    {
                        highest = PF_ARRAY[(int) base.tilePosition.X - 1, (int) base.tilePosition.Y];
                        nextTarget.X = base.tilePosition.X - 1;
                        nextTarget.Y = base.tilePosition.Y;
                    }
                }
                catch { Console.WriteLine("Left Limit"); }

                //Right
                try
                {
                    if (highest < PF_ARRAY[(int) base.tilePosition.X + 1, (int) base.tilePosition.Y] && PF_ARRAY[(int)base.tilePosition.X + 1, (int)base.tilePosition.Y] > 0 && world.TileArray[(int)base.tilePosition.X + 1, (int) base.tilePosition.Y].OccupiedByUnit != true)
                    {
                        highest = PF_ARRAY[(int) base.tilePosition.X + 1, (int) base.tilePosition.Y];
                        nextTarget.X = base.tilePosition.X + 1;
                        nextTarget.Y = base.tilePosition.Y;
                    }
                }
                catch { Console.WriteLine("Right Limit"); }

                //Down
                try
                {

                    if (highest < PF_ARRAY[(int) base.tilePosition.X, (int) base.tilePosition.Y + 1] && PF_ARRAY[(int)base.tilePosition.X, (int)base.tilePosition.Y + 1] > 0 && world.TileArray[(int)base.tilePosition.X, (int) base.tilePosition.Y + 1].OccupiedByUnit != true)
                    {
                        highest = PF_ARRAY[(int)base.tilePosition.X, (int) base.tilePosition.Y + 1];
                        nextTarget.X = base.tilePosition.X;
                        nextTarget.Y = base.tilePosition.Y + 1;
                    }
                }
                catch { Console.WriteLine("Lower Limit"); }


                //Up
                try
                {

                    if (highest < PF_ARRAY[(int)base.tilePosition.X, (int)base.tilePosition.Y - 1] && PF_ARRAY[(int)base.tilePosition.X, (int)base.tilePosition.Y - 1] > 0 && world.TileArray[(int)base.tilePosition.X, (int) base.tilePosition.Y - 1].OccupiedByUnit != true)
                    {
                        highest = PF_ARRAY[(int)base.tilePosition.X, (int) base.tilePosition.Y - 1];
                        nextTarget.X = base.tilePosition.X;
                        nextTarget.Y = base.tilePosition.Y - 1;
                    }
                }
                catch { Console.WriteLine("Upper Limit"); }


                //Up & Right
                try
                {

                    if (highest < PF_ARRAY[(int)base.tilePosition.X + 1, (int)base.tilePosition.Y - 1] && PF_ARRAY[(int)base.tilePosition.X + 1, (int)base.tilePosition.Y - 1] > 0 && world.TileArray[(int)base.tilePosition.X + 1, (int) base.tilePosition.Y - 1].OccupiedByUnit != true)
                    {
                        highest = PF_ARRAY[(int)base.tilePosition.X + 1, (int) base.tilePosition.Y - 1];
                        nextTarget.X = base.tilePosition.X + 1;
                        nextTarget.Y = base.tilePosition.Y - 1;
                    }
                }
                catch { }


                //Up & Left
                try
                {

                    if (highest < PF_ARRAY[(int)base.tilePosition.X - 1, (int)base.tilePosition.Y - 1] && PF_ARRAY[(int)base.tilePosition.X - 1, (int)base.tilePosition.Y - 1] > 0 && world.TileArray[(int)base.tilePosition.X - 1, (int) base.tilePosition.Y - 1].OccupiedByUnit != true)
                    {
                        highest = PF_ARRAY[(int)base.tilePosition.X - 1, (int) base.tilePosition.Y - 1];
                        nextTarget.X = base.tilePosition.X - 1;
                        nextTarget.Y = base.tilePosition.Y - 1;
                    }
                }
                catch { }

                //Down & Right
                try
                {

                    if (highest < PF_ARRAY[(int)base.tilePosition.X + 1, (int)base.tilePosition.Y + 1] && PF_ARRAY[(int)base.tilePosition.X + 1, (int)base.tilePosition.Y + 1] > 0 && world.TileArray[(int)base.tilePosition.X + 1, (int) base.tilePosition.Y + 1].OccupiedByUnit != true)
                    {
                        highest = PF_ARRAY[(int)base.tilePosition.X + 1, (int) base.tilePosition.Y + 1];
                        nextTarget.X = base.tilePosition.X + 1;
                        nextTarget.Y = base.tilePosition.Y + 1;
                    }
                }
                catch { }


                //Down & Left
                try
                {

                    if (highest < PF_ARRAY[(int)base.tilePosition.X - 1, (int)base.tilePosition.Y + 1] && PF_ARRAY[(int)base.tilePosition.X - 1, (int)base.tilePosition.Y + 1] > 0 && world.TileArray[(int)base.tilePosition.X - 1, (int) base.tilePosition.Y + 1].OccupiedByUnit != true)
                    {
                        highest = PF_ARRAY[(int)base.tilePosition.X - 1, (int) base.tilePosition.Y + 1];
                        nextTarget.X = base.tilePosition.X - 1;
                        nextTarget.Y = base.tilePosition.Y + 1;
                    }
                }
                catch { }


                //Negative trail to push harvester forward.
                PF_ARRAY[(int)base.tilePosition.X, (int) base.tilePosition.Y] -= 100;

                //Moving playerEntities occupation and target.
                world.TileArray[(int)base.tilePosition.X, (int) base.tilePosition.Y].OccupiedByUnit = false;
                NEXT_TARGET = nextTarget;
                world.TileArray[(int)base.tilePosition.X, (int) base.pixelPosition.Y].OccupiedByUnit = true;

            }
            else    //When the harvester is on the source tile.
            {
                //Stop this.Move being called in GameInstance.Update
                owner.MovingUnits.Remove(this); 
            }
        }

*/

/*
        #region Function Explanation
        //This is the code which moves the harvester to the target fluidly.
        //The target is just the next cell/Tile. when it reaches it,
        //it uses FindNextCell to find the next tile to move to until
        //it reaches it's final Target.
        #endregion
        public void Move()
        {
            //If it is at the Tile, find a new one.
            if (base.TilePosition == NEXT_TARGET)
            {
                //Find a new one.
                FindNextCell();
            }
            else
            {
                //Moving the harvester.
                //Stand in code until i can be arsed moving stuff nicely.
                base.TilePosition = new Vector2(NEXT_TARGET.X, NEXT_TARGET.Y);
               
                  //TEMP.
            }
        }
*/