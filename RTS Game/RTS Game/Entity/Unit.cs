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
        //Next Tile unit is moving to.
        Vector2 NEXT_TARGET = new Vector2();
        //Final destination unit wants to reach.
        Vector2 FINAL_TARGET = new Vector2();
        //Units PF Array for movement.
        public int[,] PF_ARRAY;
        //Reference to the TileMap class.
        TileMap world;
        //Reference to Player.
        Game.Player owner;
        #endregion

        public Vector2 FinalTarget
        {
            get { return FINAL_TARGET; }
            set { FINAL_TARGET = value; }
        }

        public Unit(TileMap world, Game.Player owner, Vector2 position, Texture2D texture, double maxHealth)
            : base(world, position, texture, maxHealth)
        {
            this.owner = owner;
            this.world = world;

            //Setting PF Array Size to match world.
            PF_ARRAY = new int[world.TileArray.GetLength(0), world.TileArray.GetLength(1)];

            //Makes moving work first time.
            NEXT_TARGET = position;
        }


        #region Function Explanation
        //This is the code which moves the unit to the target fluidly.
        //The target is just the next cell/Tile. when it reaches it,
        //it uses FindNextCell to find the next tile to move to until
        //it reaches it's final Target.
        #endregion
        public void Move()
        {
            //If it is at the next position.
            if (base.position.X == NEXT_TARGET.X && base.position.Y == NEXT_TARGET.Y)
            {
                //Find a new one.
                FindNextCell();
            }
            else
            {
                //Moving the unit.
                //Stand in code until i can be arsed moving stuff nicely.
                base.position.X = NEXT_TARGET.X;
                base.position.Y = NEXT_TARGET.Y;
            }
        }

        #region Function Explanation
        //Finding the Unit a new target by finding the next lowest potential field.
        #endregion
        public void FindNextCell()
        {
            //If we're not at the final target.
            if (base.position != FINAL_TARGET)
            {
                int highest = int.MaxValue;
                Vector2 nextTarget = new Vector2(base.position.X, base.position.Y);

                //Left
                try
                {
                    if (highest < PF_ARRAY[(int)base.position.X - 1, (int)base.position.Y] && PF_ARRAY[(int)base.position.X - 1, (int)base.position.Y] > 0 && world.TileArray[(int)base.position.X - 1, (int) base.position.Y].Occupied != true)
                    {
                        highest = PF_ARRAY[(int) base.position.X - 1, (int) base.position.Y];
                        nextTarget.X = base.position.X - 1;
                        nextTarget.Y = base.position.Y;
                    }
                }
                catch { Console.WriteLine("Left Limit"); }

                //Right
                try
                {
                    if (highest < PF_ARRAY[(int) base.position.X + 1, (int) base.position.Y] && PF_ARRAY[(int)base.position.X + 1, (int)base.position.Y] > 0 && world.TileArray[(int)base.position.X + 1, (int) base.position.Y].Occupied != true)
                    {
                        highest = PF_ARRAY[(int) base.position.X + 1, (int) base.position.Y];
                        nextTarget.X = base.position.X + 1;
                        nextTarget.Y = base.position.Y;
                    }
                }
                catch { Console.WriteLine("Right Limit"); }

                //Down
                try
                {

                    if (highest < PF_ARRAY[(int) base.position.X, (int) base.position.Y + 1] && PF_ARRAY[(int)base.position.X, (int)base.position.Y + 1] > 0 && world.TileArray[(int)base.position.X, (int) base.position.Y + 1].Occupied != true)
                    {
                        highest = PF_ARRAY[(int)base.position.X, (int) base.position.Y + 1];
                        nextTarget.X = base.position.X;
                        nextTarget.Y = base.position.Y + 1;
                    }
                }
                catch { Console.WriteLine("Lower Limit"); }


                //Up
                try
                {

                    if (highest < PF_ARRAY[(int)base.position.X, (int)base.position.Y - 1] && PF_ARRAY[(int)base.position.X, (int)base.position.Y - 1] > 0 && world.TileArray[(int)base.position.X, (int) base.position.Y - 1].Occupied != true)
                    {
                        highest = PF_ARRAY[(int)base.position.X, (int) base.position.Y - 1];
                        nextTarget.X = base.position.X;
                        nextTarget.Y = base.position.Y - 1;
                    }
                }
                catch { Console.WriteLine("Upper Limit"); }


                //Up & Right
                try
                {

                    if (highest < PF_ARRAY[(int)base.position.X + 1, (int)base.position.Y - 1] && PF_ARRAY[(int)base.position.X + 1, (int)base.position.Y - 1] > 0 && world.TileArray[(int)base.position.X + 1, (int) base.position.Y - 1].Occupied != true)
                    {
                        highest = PF_ARRAY[(int)base.position.X + 1, (int) base.position.Y - 1];
                        nextTarget.X = base.position.X + 1;
                        nextTarget.Y = base.position.Y - 1;
                    }
                }
                catch { }


                //Up & Left
                try
                {

                    if (highest < PF_ARRAY[(int)base.position.X - 1, (int)base.position.Y - 1] && PF_ARRAY[(int)base.position.X - 1, (int)base.position.Y - 1] > 0 && world.TileArray[(int)base.position.X - 1, (int) base.position.Y - 1].Occupied != true)
                    {
                        highest = PF_ARRAY[(int)base.position.X - 1, (int) base.position.Y - 1];
                        nextTarget.X = base.position.X - 1;
                        nextTarget.Y = base.position.Y - 1;
                    }
                }
                catch { }

                //Down & Right
                try
                {

                    if (highest < PF_ARRAY[(int)base.position.X + 1, (int)base.position.Y + 1] && PF_ARRAY[(int)base.position.X + 1, (int)base.position.Y + 1] > 0 && world.TileArray[(int)base.position.X + 1, (int) base.position.Y + 1].Occupied != true)
                    {
                        highest = PF_ARRAY[(int)base.position.X + 1, (int) base.position.Y + 1];
                        nextTarget.X = base.position.X + 1;
                        nextTarget.Y = base.position.Y + 1;
                    }
                }
                catch { }


                //Down & Left
                try
                {

                    if (highest < PF_ARRAY[(int)base.position.X - 1, (int)base.position.Y + 1] && PF_ARRAY[(int)base.position.X - 1, (int)base.position.Y + 1] > 0 && world.TileArray[(int)base.position.X - 1, (int) base.position.Y + 1].Occupied != true)
                    {
                        highest = PF_ARRAY[(int)base.position.X - 1, (int) base.position.Y + 1];
                        nextTarget.X = base.position.X - 1;
                        nextTarget.Y = base.position.Y + 1;
                    }
                }
                catch { }


                //Negative trail to push unit forward.
                PF_ARRAY[(int)base.position.X, (int) base.position.Y] -= 100;

                //Moving units occupation and target.
                world.TileArray[(int)base.position.X, (int) base.position.Y].Occupied = false;
                NEXT_TARGET = nextTarget;
                world.TileArray[(int)base.position.X, (int) base.position.Y].Occupied = true;

            }
            else    //When the unit is on the source tile.
            {
                //Stop this.Move being called in GameInstance.Update
                owner.MovingUnits.Remove(this); 
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {   
            base.Draw(spriteBatch);
        }
    }
}
