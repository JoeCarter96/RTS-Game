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
          Represents a spawnable Unit which can be moved about the battle field
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
        #endregion

        #region Other Variables
        protected HealthEntity target;

        //Shooting and waiting to move (when another unit is in the way).
        protected int waitTimer = 0;
        protected int elapsedMills = 0;

        protected Turret turret;  
        #endregion

        #region Passed Variables
        protected List<Texture2D> textures;
        #endregion

        #region Pathfinding
        //Next Tile Unit is moving to.
        protected Vector2 nextTile = new Vector2();
        protected Vector2 currentTile = new Vector2();
        protected Queue<Vector2> WAYPOINTS = new Queue<Vector2>();
        protected bool ignoreObstacles = false;

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

        public Vector2 NextTile
        {
            get { return nextTile; }
            set { nextTile = value; }
        }

        public Vector2 CurrentTile
        {
            get { return currentTile; }
            set { currentTile = value; }
        }

        public List<Texture2D> Textures
        {
            get { return textures; }
            set { textures = value; }
        }

        public Turret Turret
        {
            get { return turret; }
            set { turret = value; }
        }

        #region Property Explanation
        //Override Enities rotation, just points to Entities rotation
        //but adds the unit only method SetCorrectRotation.
        #endregion
        public override float Rotation
        {
            get { return base.Rotation; }
            set
            {
                base.Rotation = value;
                SetCorrectTexture();
            }
        }

        public bool IgnoreObstacles
        {
            get { return ignoreObstacles; }
            set { ignoreObstacles = value; }
        }

        #region Function Explanation
        //Constructor, Adds Unit to entity list, passes a bunch of variables and then creates a PF array.
        //Sets Next Target to Tile Position so that when Move() is called it immediately looks for the next tile.
        #endregion
        public Unit(TileMap world, Player owner, Vector2 tilePosition, Texture2D texture, double maxHealth,
           float maxSpeed, float acceleration, float damage, float AOE, float ROF, Rectangle spriteDimensions)
            : base(world, owner, tilePosition, texture, maxHealth, spriteDimensions)
        {
            owner.PlayerUnits.Add(this);

            this.maxSpeed = maxSpeed;
            this.acceleration = acceleration;
            this.damage = damage;
            this.AOE = AOE;
            this.ROF = ROF;

            world.TileArray[(int)tilePosition.X, (int)tilePosition.Y].OccupiedByUnit = true;
            currentTile = tilePosition;
        }


        public float DistanceToDestination
        {
            get { return Vector2.Distance(PixelPosition, new Vector2(nextTile.X * World.TileWidth, nextTile.Y * World.TileWidth)); }
        }

        #region Function Explanation
        //This is the code which moves the Unit to the target fluidly.
        //The target is just the next cell/Tile. when it reaches it,
        //it uses waypoints.Dequeue to remove and use the next waypoint.
        //Also changes occupied tile, and handles collisions.
        #endregion
        public virtual void Move()
        {
            if (Waypoints.Count > 0)
            {
                //Moving as close as it can if the final target is occupied (group movement!)
                if (World.TileArray[(int)Waypoints.Last().X, (int)Waypoints.Last().Y].OccupiedByUnit ||
                    World.TileArray[(int)Waypoints.Last().X, (int)Waypoints.Last().Y].Obstacle)
                {
                    Waypoints = WaypointsGenerator.GenerateWaypoints(CurrentTile, FindNearestTile.BeginSearch(Waypoints.Last(), World.TileArray), true);
                    ignoreObstacles = true;
                }

                //If there is a unit in the way.
                if (World.TileArray[(int)nextTile.X, (int)nextTile.Y].OccupiedByUnit == true && ignoreObstacles == false)
                {
                    //If it's waited more then 3 seconds for the unit to move and it has not,
                    //Make the unit an obstacle (will be made false when the unit moves) and 
                    //move around it. Set wait to 0.
                    if (waitTimer + elapsedMills > 500)
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
                    //placed building), recalculate waypoints.
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
                    turret.PixelPosition = PixelPosition;
                    Rotation = toAngle(direction);
                    turret.Rotation = Rotation;
                }
            }

            else    //When the Unit has no more waypoints
            {  
                //Moving as close as it can if the target is occupied (group movement!)
                if (World.TileArray[(int)nextTile.X, (int)nextTile.Y].OccupiedByUnit ||
                    World.TileArray[(int)nextTile.X, (int)nextTile.Y].Obstacle)
                {
                    Waypoints = WaypointsGenerator.GenerateWaypoints(CurrentTile, FindNearestTile.BeginSearch(nextTile, World.TileArray), false);
                }


                if (DistanceToDestination < maxSpeed)   //If it's at it's target.
                {
                    //Stops this.Move being called in GameInstance.Update
                    Owner.PlayerMovingEntities.Remove(this);
                    CURRENT_SPEED = 0;
                    ignoreObstacles = false;
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
                    //Actually visibly moving, changing directione etc.
                    Vector2 direction = new Vector2(nextTile.X * World.TileWidth, nextTile.Y * World.TileWidth) - PixelPosition;
                    direction.Normalize();
                    Velocity = Vector2.Multiply(direction, CURRENT_SPEED);
                    PixelPosition += Velocity;
                    turret.PixelPosition = PixelPosition;
                    Rotation = toAngle(direction);
                    turret.Rotation = Rotation;
                }
            }
        }

        #region Function Explanation
        //Converts a Vector2 to an angle.
        #endregion
        public float toAngle(Vector2 vector)
        {
            //Find Angle (in rads):
            float rads = (float)Math.Atan2((float)vector.X, (float)-vector.Y);


            if (vector.X < 0)
            {
                return ((float)(2 * Math.PI) - Math.Abs(rads));
            }
            else
            {
                //Return Degrees in rads.
                return (rads);
            }
        }

        #region Function Explanation
        //Returns the correct image for the angle (Rotation, in radians) the Unit is at.
        #endregion
        public void SetCorrectTexture()
        {
            //Up
            if (Rotation > 5.890 || Rotation < 0.480)
            {
                SourceRectangle = new Rectangle(SpriteDimensions.Width * 0, SpriteDimensions.Height * 0,
                    SpriteDimensions.Width, SpriteDimensions.Height);
            }
            //Up Right
            else if (Rotation > 0.480 && Rotation < 1.178)
            {
                SourceRectangle = new Rectangle(SpriteDimensions.Width * 1, SpriteDimensions.Height * 0,
                    SpriteDimensions.Width, SpriteDimensions.Height);
            }
            //Right
            else if (Rotation > 1.178 && Rotation < 1.963)
            {
                SourceRectangle = new Rectangle(SpriteDimensions.Width * 2, SpriteDimensions.Height * 0,
                     SpriteDimensions.Width, SpriteDimensions.Height);
            }
            //Down Right
            else if (Rotation > 1.963 && Rotation < 2.749)
            {
                SourceRectangle = new Rectangle(SpriteDimensions.Width * 3, SpriteDimensions.Height * 0,
                    SpriteDimensions.Width, SpriteDimensions.Height);
            }
            //Down
            else if (Rotation > 2.749 && Rotation < 3.534)
            {
                SourceRectangle = new Rectangle(SpriteDimensions.Width * 4, SpriteDimensions.Height * 0,
                     SpriteDimensions.Width, SpriteDimensions.Height);
            }
            //Down Left
            else if (Rotation > 3.534 && Rotation < 4.320)
            {
                SourceRectangle = new Rectangle(SpriteDimensions.Width * 5, SpriteDimensions.Height * 0,
                    SpriteDimensions.Width, SpriteDimensions.Height);
            }
            //Left
            else if (Rotation > 4.320 && Rotation < 5.105)
            {
                SourceRectangle = new Rectangle(SpriteDimensions.Width * 6, SpriteDimensions.Height * 0,
                    SpriteDimensions.Width, SpriteDimensions.Height);
            }
            //Left Up
            else if (Rotation > 5.105 && Rotation < 5.890)
            {
                SourceRectangle = new Rectangle(SpriteDimensions.Width * 7, SpriteDimensions.Height * 0,
                    SpriteDimensions.Width, SpriteDimensions.Height);
            }
        }

        #region Function Explanation
        //Updates Entity tree, increases time since last shot.
        #endregion
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            bulletTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            elapsedMills = gameTime.ElapsedGameTime.Milliseconds;
        }

        #region Function Explanation
        //Draw Method, Draws Base texture.
        #endregion
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            if (turret != null)
            {
                turret.Draw(spriteBatch);
            }
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
                Vector2 nextTile = new Vector2(base.tilePosition.X, base.tilePosition.Y);

                //Left
                try
                {
                    if (highest < PF_ARRAY[(int)base.tilePosition.X - 1, (int)base.tilePosition.Y] && PF_ARRAY[(int)base.tilePosition.X - 1, (int)base.tilePosition.Y] > 0 && world.TileArray[(int)base.tilePosition.X - 1, (int) base.tilePosition.Y].OccupiedByUnit != true)
                    {
                        highest = PF_ARRAY[(int) base.tilePosition.X - 1, (int) base.tilePosition.Y];
                        nextTile.X = base.tilePosition.X - 1;
                        nextTile.Y = base.tilePosition.Y;
                    }
                }
                catch { Console.WriteLine("Left Limit"); }

                //Right
                try
                {
                    if (highest < PF_ARRAY[(int) base.tilePosition.X + 1, (int) base.tilePosition.Y] && PF_ARRAY[(int)base.tilePosition.X + 1, (int)base.tilePosition.Y] > 0 && world.TileArray[(int)base.tilePosition.X + 1, (int) base.tilePosition.Y].OccupiedByUnit != true)
                    {
                        highest = PF_ARRAY[(int) base.tilePosition.X + 1, (int) base.tilePosition.Y];
                        nextTile.X = base.tilePosition.X + 1;
                        nextTile.Y = base.tilePosition.Y;
                    }
                }
                catch { Console.WriteLine("Right Limit"); }

                //Down
                try
                {

                    if (highest < PF_ARRAY[(int) base.tilePosition.X, (int) base.tilePosition.Y + 1] && PF_ARRAY[(int)base.tilePosition.X, (int)base.tilePosition.Y + 1] > 0 && world.TileArray[(int)base.tilePosition.X, (int) base.tilePosition.Y + 1].OccupiedByUnit != true)
                    {
                        highest = PF_ARRAY[(int)base.tilePosition.X, (int) base.tilePosition.Y + 1];
                        nextTile.X = base.tilePosition.X;
                        nextTile.Y = base.tilePosition.Y + 1;
                    }
                }
                catch { Console.WriteLine("Lower Limit"); }


                //Up
                try
                {

                    if (highest < PF_ARRAY[(int)base.tilePosition.X, (int)base.tilePosition.Y - 1] && PF_ARRAY[(int)base.tilePosition.X, (int)base.tilePosition.Y - 1] > 0 && world.TileArray[(int)base.tilePosition.X, (int) base.tilePosition.Y - 1].OccupiedByUnit != true)
                    {
                        highest = PF_ARRAY[(int)base.tilePosition.X, (int) base.tilePosition.Y - 1];
                        nextTile.X = base.tilePosition.X;
                        nextTile.Y = base.tilePosition.Y - 1;
                    }
                }
                catch { Console.WriteLine("Upper Limit"); }


                //Up & Right
                try
                {

                    if (highest < PF_ARRAY[(int)base.tilePosition.X + 1, (int)base.tilePosition.Y - 1] && PF_ARRAY[(int)base.tilePosition.X + 1, (int)base.tilePosition.Y - 1] > 0 && world.TileArray[(int)base.tilePosition.X + 1, (int) base.tilePosition.Y - 1].OccupiedByUnit != true)
                    {
                        highest = PF_ARRAY[(int)base.tilePosition.X + 1, (int) base.tilePosition.Y - 1];
                        nextTile.X = base.tilePosition.X + 1;
                        nextTile.Y = base.tilePosition.Y - 1;
                    }
                }
                catch { }


                //Up & Left
                try
                {

                    if (highest < PF_ARRAY[(int)base.tilePosition.X - 1, (int)base.tilePosition.Y - 1] && PF_ARRAY[(int)base.tilePosition.X - 1, (int)base.tilePosition.Y - 1] > 0 && world.TileArray[(int)base.tilePosition.X - 1, (int) base.tilePosition.Y - 1].OccupiedByUnit != true)
                    {
                        highest = PF_ARRAY[(int)base.tilePosition.X - 1, (int) base.tilePosition.Y - 1];
                        nextTile.X = base.tilePosition.X - 1;
                        nextTile.Y = base.tilePosition.Y - 1;
                    }
                }
                catch { }

                //Down & Right
                try
                {

                    if (highest < PF_ARRAY[(int)base.tilePosition.X + 1, (int)base.tilePosition.Y + 1] && PF_ARRAY[(int)base.tilePosition.X + 1, (int)base.tilePosition.Y + 1] > 0 && world.TileArray[(int)base.tilePosition.X + 1, (int) base.tilePosition.Y + 1].OccupiedByUnit != true)
                    {
                        highest = PF_ARRAY[(int)base.tilePosition.X + 1, (int) base.tilePosition.Y + 1];
                        nextTile.X = base.tilePosition.X + 1;
                        nextTile.Y = base.tilePosition.Y + 1;
                    }
                }
                catch { }


                //Down & Left
                try
                {

                    if (highest < PF_ARRAY[(int)base.tilePosition.X - 1, (int)base.tilePosition.Y + 1] && PF_ARRAY[(int)base.tilePosition.X - 1, (int)base.tilePosition.Y + 1] > 0 && world.TileArray[(int)base.tilePosition.X - 1, (int) base.tilePosition.Y + 1].OccupiedByUnit != true)
                    {
                        highest = PF_ARRAY[(int)base.tilePosition.X - 1, (int) base.tilePosition.Y + 1];
                        nextTile.X = base.tilePosition.X - 1;
                        nextTile.Y = base.tilePosition.Y + 1;
                    }
                }
                catch { }


                //Negative trail to push harvester forward.
                PF_ARRAY[(int)base.tilePosition.X, (int) base.tilePosition.Y] -= 100;

                //Moving playerEntities occupation and target.
                world.TileArray[(int)base.tilePosition.X, (int) base.tilePosition.Y].OccupiedByUnit = false;
                nextTile = nextTile;
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
            if (base.TilePosition == nextTile)
            {
                //Find a new one.
                FindNextCell();
            }
            else
            {
                //Moving the harvester.
                //Stand in code until i can be arsed moving stuff nicely.
                base.TilePosition = new Vector2(nextTile.X, nextTile.Y);
               
                  //TEMP.
            }
        }
*/