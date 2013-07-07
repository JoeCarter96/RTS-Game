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
          Represents a spawnable unit which can be moved about the battle field
          and shoot other enemys. Has a lot of stat veriables that will be changed 
          over time as the game progresses in development. 
        */
    #endregion

    class Unit : HealthEntity
    {
        #region variables
            #region Unit Attributes
            protected float MAX_SPEED;
            protected float ACCELLERATION;
            protected float DAMAGE;
            protected float AOE;
            protected float ROF;

            protected float CURRENT_SPEED;
            protected float bulletTime;
            protected HealthEntity target;
            #endregion

            #region Passed Variables
            
            #endregion

            #region Pathfinding
            //Next Tile unit is moving to.
            protected Vector2 NEXT_TARGET = new Vector2();
            //Final destination unit wants to reach.
            //It is a TILE, not pixel.
            protected Vector2 FINAL_TARGET = new Vector2();
            //Units PF Array for movement.
            protected int[,] PF_ARRAY;
             #endregion
        #endregion

       public float MaxSpeed
        {
            get { return MAX_SPEED; }
            set { MAX_SPEED = value; }
        }

       public int[,] PFArray
       {
           get { return PF_ARRAY; }
           set { PF_ARRAY = value; }
       }

        public Vector2 FinalTarget
        {
            get { return FINAL_TARGET; }
            set { FINAL_TARGET = value; }
        }

        public Unit(TileMap world, Player owner, Vector2 tilePosition, Texture2D texture, double maxHealth)
            : base(world, owner, tilePosition, texture, maxHealth)
        {
            owner.Units.Add(this);

            this.world = world;
            this.TilePosition = tilePosition; 

            //Setting PF Array Size to match world.
            PF_ARRAY = new int[world.TileArray.GetLength(0), world.TileArray.GetLength(1)];

            //Makes moving work first time.
            NEXT_TARGET = tilePosition;
        }

        //Gets the Distance to the next target Cell.
        public float DistanceToDestination
        {
            get { return Vector2.Distance(pixelPosition, new Vector2(NEXT_TARGET.X * world.TileWidth, NEXT_TARGET.Y * world.TileWidth)); }
        }

        #region Function Explanation
        //This is the code which moves the unit to the target fluidly.
        //The target is just the next cell/Tile. when it reaches it,
        //it uses FindNextCell to find the next tile to move to until
        //it reaches it's final Target.
        #endregion
        public void Move()
        {
            if (DistanceToDestination < MAX_SPEED)
            {
                FindNextCell();
            }
            else
            {
                //Accellerating.
                if (CURRENT_SPEED < MAX_SPEED)
                {
                    //If Max speed is smaller than current speed + accelleration, just make it max speed.
                    //Stops it going faster than it's max speed.
                    CURRENT_SPEED = Math.Min(MAX_SPEED, CURRENT_SPEED += ACCELLERATION);
                }
                Vector2 direction = new Vector2(NEXT_TARGET.X * world.TileWidth, NEXT_TARGET.Y * world.TileWidth) - pixelPosition;
                direction.Normalize();
                velocity = Vector2.Multiply(direction, CURRENT_SPEED);
                PixelPosition += velocity;
    }
}

        #region Function Explanation
        //Finding the Unit a new target by finding the next lowest potential field.

        //NEED TO CONVERT FROM PIXEL POSITION TO TILE POSITION IN SOME PLACES.
        #endregion
        public void FindNextCell()
        {
            //If we're not at the final target.
            if (new Vector2((float)Math.Round(base.TilePosition.X), 
                (float)Math.Round(base.TilePosition.Y)) != FINAL_TARGET)
            {
                int highest = -int.MaxValue;
                Vector2 nextTarget = new Vector2(base.TilePosition.X, base.TilePosition.Y);

                //Right
                try
                {
                    if (highest < PF_ARRAY[(int)base.TilePosition.X + 1, (int)base.TilePosition.Y] && PF_ARRAY[(int)base.TilePosition.X + 1, (int)base.TilePosition.Y] > 0 && world.TileArray[(int)base.TilePosition.X + 1, (int)base.TilePosition.Y].Occupied != true)
                    {
                        highest = PF_ARRAY[(int) base.tilePosition.X + 1, (int) base.tilePosition.Y];
                        nextTarget.X = base.tilePosition.X + 1;
                        nextTarget.Y = base.tilePosition.Y;
                    }
                }
                catch { Console.WriteLine("Right Limit"); }

                //Left
                try
                {
                    if (highest < PF_ARRAY[(int)base.TilePosition.X - 1, (int)base.TilePosition.Y] && PF_ARRAY[(int)base.TilePosition.X - 1, (int)base.TilePosition.Y] > 0 && world.TileArray[(int)base.TilePosition.X - 1, (int)base.TilePosition.Y].Occupied != true)
                    {
                        highest = PF_ARRAY[(int)base.TilePosition.X - 1, (int)base.TilePosition.Y];
                        nextTarget.X = base.TilePosition.X - 1;
                        nextTarget.Y = base.TilePosition.Y;
                    }
                }
                catch { Console.WriteLine("Left Limit"); }


                //Down
                try
                {

                    if (highest < PF_ARRAY[(int) base.tilePosition.X, (int) base.tilePosition.Y + 1] && PF_ARRAY[(int)base.tilePosition.X, (int)base.tilePosition.Y + 1] > 0 && world.TileArray[(int)base.tilePosition.X, (int) base.tilePosition.Y + 1].Occupied != true)
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

                    if (highest < PF_ARRAY[(int)base.tilePosition.X, (int)base.tilePosition.Y - 1] && PF_ARRAY[(int)base.tilePosition.X, (int)base.tilePosition.Y - 1] > 0 && world.TileArray[(int)base.tilePosition.X, (int) base.tilePosition.Y - 1].Occupied != true)
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

                    if (highest < PF_ARRAY[(int)base.tilePosition.X + 1, (int)base.tilePosition.Y - 1] && PF_ARRAY[(int)base.tilePosition.X + 1, (int)base.tilePosition.Y - 1] > 0 && world.TileArray[(int)base.tilePosition.X + 1, (int) base.tilePosition.Y - 1].Occupied != true)
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

                    if (highest < PF_ARRAY[(int)base.tilePosition.X - 1, (int)base.tilePosition.Y - 1] && PF_ARRAY[(int)base.tilePosition.X - 1, (int)base.tilePosition.Y - 1] > 0 && world.TileArray[(int)base.tilePosition.X - 1, (int) base.tilePosition.Y - 1].Occupied != true)
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

                    if (highest < PF_ARRAY[(int)base.tilePosition.X + 1, (int)base.tilePosition.Y + 1] && PF_ARRAY[(int)base.tilePosition.X + 1, (int)base.tilePosition.Y + 1] > 0 && world.TileArray[(int)base.tilePosition.X + 1, (int) base.tilePosition.Y + 1].Occupied != true)
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

                    if (highest < PF_ARRAY[(int)base.tilePosition.X - 1, (int)base.tilePosition.Y + 1] && PF_ARRAY[(int)base.tilePosition.X - 1, (int)base.tilePosition.Y + 1] > 0 && world.TileArray[(int)base.tilePosition.X - 1, (int) base.tilePosition.Y + 1].Occupied != true)
                    {
                        highest = PF_ARRAY[(int)base.tilePosition.X - 1, (int) base.tilePosition.Y + 1];
                        nextTarget.X = base.tilePosition.X - 1;
                        nextTarget.Y = base.tilePosition.Y + 1;
                    }
                }
                catch { }


                //Negative trail to push unit forward.
                PF_ARRAY[(int)base.tilePosition.X, (int) base.tilePosition.Y] -= 100;

                //Moving units occupation and target.
                world.TileArray[(int)base.tilePosition.X, (int) base.tilePosition.Y].Occupied = false;
                NEXT_TARGET = nextTarget;
                world.TileArray[(int)base.tilePosition.X, (int) base.tilePosition.Y].Occupied = true;

            }
            else    //When the unit is on the source tile.
            {
                //Stop this.Move being called in GameInstance.Update
                owner.MovingUnits.Remove(this);
                CURRENT_SPEED = 0;
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            bulletTime += (float) gameTime.ElapsedGameTime.TotalSeconds;
        }

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
                    if (highest < PF_ARRAY[(int)base.tilePosition.X - 1, (int)base.tilePosition.Y] && PF_ARRAY[(int)base.tilePosition.X - 1, (int)base.tilePosition.Y] > 0 && world.TileArray[(int)base.tilePosition.X - 1, (int) base.tilePosition.Y].Occupied != true)
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
                    if (highest < PF_ARRAY[(int) base.tilePosition.X + 1, (int) base.tilePosition.Y] && PF_ARRAY[(int)base.tilePosition.X + 1, (int)base.tilePosition.Y] > 0 && world.TileArray[(int)base.tilePosition.X + 1, (int) base.tilePosition.Y].Occupied != true)
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

                    if (highest < PF_ARRAY[(int) base.tilePosition.X, (int) base.tilePosition.Y + 1] && PF_ARRAY[(int)base.tilePosition.X, (int)base.tilePosition.Y + 1] > 0 && world.TileArray[(int)base.tilePosition.X, (int) base.tilePosition.Y + 1].Occupied != true)
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

                    if (highest < PF_ARRAY[(int)base.tilePosition.X, (int)base.tilePosition.Y - 1] && PF_ARRAY[(int)base.tilePosition.X, (int)base.tilePosition.Y - 1] > 0 && world.TileArray[(int)base.tilePosition.X, (int) base.tilePosition.Y - 1].Occupied != true)
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

                    if (highest < PF_ARRAY[(int)base.tilePosition.X + 1, (int)base.tilePosition.Y - 1] && PF_ARRAY[(int)base.tilePosition.X + 1, (int)base.tilePosition.Y - 1] > 0 && world.TileArray[(int)base.tilePosition.X + 1, (int) base.tilePosition.Y - 1].Occupied != true)
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

                    if (highest < PF_ARRAY[(int)base.tilePosition.X - 1, (int)base.tilePosition.Y - 1] && PF_ARRAY[(int)base.tilePosition.X - 1, (int)base.tilePosition.Y - 1] > 0 && world.TileArray[(int)base.tilePosition.X - 1, (int) base.tilePosition.Y - 1].Occupied != true)
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

                    if (highest < PF_ARRAY[(int)base.tilePosition.X + 1, (int)base.tilePosition.Y + 1] && PF_ARRAY[(int)base.tilePosition.X + 1, (int)base.tilePosition.Y + 1] > 0 && world.TileArray[(int)base.tilePosition.X + 1, (int) base.tilePosition.Y + 1].Occupied != true)
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

                    if (highest < PF_ARRAY[(int)base.tilePosition.X - 1, (int)base.tilePosition.Y + 1] && PF_ARRAY[(int)base.tilePosition.X - 1, (int)base.tilePosition.Y + 1] > 0 && world.TileArray[(int)base.tilePosition.X - 1, (int) base.tilePosition.Y + 1].Occupied != true)
                    {
                        highest = PF_ARRAY[(int)base.tilePosition.X - 1, (int) base.tilePosition.Y + 1];
                        nextTarget.X = base.tilePosition.X - 1;
                        nextTarget.Y = base.tilePosition.Y + 1;
                    }
                }
                catch { }


                //Negative trail to push unit forward.
                PF_ARRAY[(int)base.tilePosition.X, (int) base.tilePosition.Y] -= 100;

                //Moving units occupation and target.
                world.TileArray[(int)base.tilePosition.X, (int) base.tilePosition.Y].Occupied = false;
                NEXT_TARGET = nextTarget;
                world.TileArray[(int)base.tilePosition.X, (int) base.pixelPosition.Y].Occupied = true;

            }
            else    //When the unit is on the source tile.
            {
                //Stop this.Move being called in GameInstance.Update
                owner.MovingUnits.Remove(this); 
            }
        }

*/














/*
        #region Function Explanation
        //This is the code which moves the unit to the target fluidly.
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
                //Moving the unit.
                //Stand in code until i can be arsed moving stuff nicely.
                base.TilePosition = new Vector2(NEXT_TARGET.X, NEXT_TARGET.Y);
               
                  //TEMP.
            }
        }
*/