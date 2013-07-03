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
        public int[,] PFARRAY;
        int POSX;
        int POSY;
        Point TARGET = new Point();
        Tile[,] tileArray;

        public int PosX
        {
            get { return POSX; }
        }

        public int PosY
        {
            get { return POSY; }
        }

        public Point Target
        {
            get { return TARGET; }
            set { TARGET = value; }
        }

        public Unit(Tile[,] tileArray, Vector2 position, Texture2D texture, double maxHealth)
            :base(position, texture, maxHealth)
        {
            this.tileArray = tileArray;
            PFARRAY = new int[tileArray.GetLength(0), tileArray.GetLength(1)];
        }

        public void move(int targetX, int targetY)
        {
            if (POSX != targetX || POSY != targetY)
            {
                int highest = -1000;
                int newPosX = POSX;
                int newPosY = POSY;

                //Left
                try
                {
                    if (highest < PFARRAY[POSX - 1, POSY] && PFARRAY[POSX - 1, POSY] > 0 && tileArray[POSX - 1, POSY].occupied != true)
                    {
                        highest = PFARRAY[POSX - 1, POSY];
                        newPosX = POSX - 1;
                        newPosY = POSY;
                    }
                }
                catch { Console.WriteLine("Left Limit"); }

                //Right
                try
                {
                    if (highest < PFARRAY[POSX + 1, POSY] && PFARRAY[POSX + 1, POSY] > 0 && tileArray[POSX + 1, POSY].occupied != true)
                    {
                        highest = PFARRAY[POSX + 1, POSY];
                        newPosX = POSX + 1;
                        newPosY = POSY;
                    }
                }
                catch { Console.WriteLine("Right Limit"); }

                //Down
                try
                {

                    if (highest < PFARRAY[POSX, POSY + 1] && PFARRAY[POSX, POSY + 1] > 0 && tileArray[POSX, POSY + 1].occupied != true)
                    {
                        highest = PFARRAY[POSX, POSY + 1];
                        newPosX = POSX;
                        newPosY = POSY + 1;
                    }
                }
                catch { Console.WriteLine("Lower Limit"); }


                //Up
                try
                {

                    if (highest < PFARRAY[POSX, POSY - 1] && PFARRAY[POSX, POSY - 1] > 0 && tileArray[POSX, POSY - 1].occupied != true)
                    {
                        highest = PFARRAY[POSX, POSY - 1];
                        newPosX = POSX;
                        newPosY = POSY - 1;
                    }
                }
                catch { Console.WriteLine("Upper Limit"); }


                //Up & Right
                try
                {

                    if (highest < PFARRAY[POSX + 1, POSY - 1] && PFARRAY[POSX + 1, POSY - 1] > 0 && tileArray[POSX + 1, POSY - 1].occupied != true)
                    {
                        highest = PFARRAY[POSX + 1, POSY - 1];
                        newPosX = POSX + 1;
                        newPosY = POSY - 1;
                    }
                }
                catch { }


                //Up & Left
                try
                {

                    if (highest < PFARRAY[POSX - 1, POSY - 1] && PFARRAY[POSX - 1, POSY - 1] > 0 && tileArray[POSX - 1, POSY - 1].occupied != true)
                    {
                        highest = PFARRAY[POSX - 1, POSY - 1];
                        newPosX = POSX - 1;
                        newPosY = POSY - 1;
                    }
                }
                catch { }

                //Down & Right
                try
                {

                    if (highest < PFARRAY[POSX + 1, POSY + 1] && PFARRAY[POSX + 1, POSY + 1] > 0 && tileArray[POSX + 1, POSY + 1].occupied != true)
                    {
                        highest = PFARRAY[POSX + 1, POSY + 1];
                        newPosX = POSX + 1;
                        newPosY = POSY + 1;
                    }
                }
                catch { }


                //Down & Left
                try
                {

                    if (highest < PFARRAY[POSX - 1, POSY + 1] && PFARRAY[POSX - 1, POSY + 1] > 0 && tileArray[POSX - 1, POSY + 1].occupied != true)
                    {
                        highest = PFARRAY[POSX - 1, POSY + 1];
                        newPosX = POSX - 1;
                        newPosY = POSY + 1;
                    }
                }
                catch { }


                //Negative trail to push unit forward.
                PFARRAY[POSX, POSY] -= 100;

                //Moving unit
                POSX = newPosX;
                POSY = newPosY;

            }
            else    //When the unit is on the source tile.
            {
                MOVE = false;   //Stopping the move loop in Tilemap.update();
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
