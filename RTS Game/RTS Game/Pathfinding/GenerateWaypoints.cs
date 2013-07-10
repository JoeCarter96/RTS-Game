using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RTS_Game
{
    static class GenerateWaypoints
    {
        #region Class Description
        //This class is responsible for the generation of the Potential Fields. Think of it as a Unility class, it is simply
        //a plug numbers in, get numbers out concept. When a unit is selected and a new location is clicked, the calculatedField
        //method needs to be called. This uses the addToField and Equation methods to construct a Potential field, by first generating
        //a large positive field at mouse Vector2, and then negative ones at enemy locations. Every time something moves (Units), we need to
        //loop through each moving unit and update their PF. Not too brilliant, however if we can make use of convoys/groups when moving
        //many playerEntities at once, it should actually be quite efficient. I may also be able to add a method which just updates the potential
        //field at the changed locations, however I need to first find a method to remove the old potential field which the unit was creating
        // before I can add the new one. We can also make use of pre-generated arrays of fields, so for instance a unit could have a 2D array
        //which holds a negative field, which can just be added to the array generated here. All in all, there are a lot of possibilities.
        #endregion

        #region Variables
        static Tile[,] tileArray;
        #endregion

        #region Function Description
        //Passes a reference of the tileArray.
        #endregion
        public static void setup(Tile[,] passedTileArray)
        {
            tileArray = passedTileArray;
        }

        #region Function Description
        //Generates a complete Potential Field, using addToField to calculate the individual component fields
        //and then this adds them all together.
        #endregion
        //Constants for calculating the heuristic.
        private const int orthogonal = 10;
        private const int diagonal = 14;

        public static Queue<Vector2> GenerateWaypoints(Vector2 unitPos, Vector2 target)
        {
            //creating a Queue of waypoints and an integer array of G costs to refer to.
            Queue<Vector2> waypoints = new Queue<Vector2>();
            int[,] gCosts = new int[tileArray.GetLength(0), tileArray.GetLength(1)];

            //The next Vector2 to be added to waypoints, calculated below.
            Vector2 nextParent = unitPos;
            //F of the nextParent, calculated below.
            int lowestF = int.MaxValue;

            int h;  //H is not needed to be stored so use one var.

            //While the algorithm has not reached source..
            while (nextParent != target)
            {
                //Set the tile to search around to the old next (int) parent/tile.
                Vector2 parent = nextParent;
                //Set F to highest possible value so at least one direction will be chosen.
                lowestF = int.MaxValue;

                # region Right
                //If it's within bounds..
                if (((int) parent.X + 1) <= (tileArray.GetLength(0) - 1))
                {
                    //..and is not a Vector2 in the queue.
                    if (!(waypoints.Contains(new Vector2((int) parent.X + 1, (int) parent.Y))))
                    {
                        gCosts[(int) parent.X + 1, (int) parent.Y] = gCosts[(int) parent.X, (int) parent.Y] + orthogonal;
                        h = (Math.Abs((int) parent.X + 1 - (int)(int) target.X) + Math.Abs((int) parent.Y - (int) target.Y)) * 10;
                        if (gCosts[(int) parent.X + 1, (int) parent.Y] + h < lowestF)
                        {
                            lowestF = gCosts[(int) parent.X + 1, (int) parent.Y] + h;
                            nextParent = new Vector2((int) parent.X + 1, (int) parent.Y);
                        }
                    }
                }
                #endregion

                # region Left
                if (((int) parent.X - 1 >= 0))
                {
                    if (!(waypoints.Contains(new Vector2((int) parent.X - 1, (int) parent.Y))))
                    {
                        gCosts[(int) parent.X - 1, (int) parent.Y] = gCosts[(int) parent.X, (int) parent.Y] + orthogonal;
                        h = (Math.Abs((int) parent.X - 1 - (int) target.X) + Math.Abs((int) parent.Y - (int) target.Y)) * 10;
                        if (gCosts[(int) parent.X - 1, (int) parent.Y] + h < lowestF)
                        {
                            lowestF = gCosts[(int) parent.X - 1, (int) parent.Y] + h;
                            nextParent = new Vector2((int) parent.X - 1, (int) parent.Y);
                        }
                    }
                }
                #endregion

f                # region Down
                if (((int) parent.Y + 1 >= 0))
                {
                    if (!(waypoints.Contains(new Vector2((int) parent.X, (int) parent.Y + 1))))
                    {
                        gCosts[(int) parent.X, (int) parent.Y + 1] = gCosts[(int) parent.X, (int) parent.Y] + orthogonal;
                        h = (Math.Abs((int) parent.X - (int) target.X) + Math.Abs((int) parent.Y + 1 - (int) target.Y)) * 10;
                        if (gCosts[(int) parent.X, (int) parent.Y + 1] + h < lowestF)
                        {
                            lowestF = gCosts[(int) parent.X, (int) parent.Y + 1] + h;
                            nextParent = new Vector2((int) parent.X, (int) parent.Y + 1);
                        }
                    }
                }
                #endregion

                #region Up
                if (((int) parent.Y - 1 >= 0))
                {
                    if (!(waypoints.Contains(new Vector2((int) parent.X, (int) parent.Y - 1))))
                    {
                        gCosts[(int) parent.X, (int) parent.Y - 1] = gCosts[(int) parent.X, (int) parent.Y] + orthogonal;
                        h = (Math.Abs((int) parent.X - (int) target.X) + Math.Abs((int) parent.Y - (int) target.Y - 1)) * 10;
                        if (gCosts[(int) parent.X, (int) parent.Y - 1] + h < lowestF)
                        {
                            lowestF = gCosts[(int) parent.X, (int) parent.Y - 1] + h;
                            nextParent = new Vector2((int) parent.X, (int) parent.Y - 1);
                        }
                    }
                }
                #endregion

                #region Up and right
                if (((int) parent.Y - 1 >= 0) && ((int) parent.X + 1) <= (tileArray.GetLength(0) - 1))
                {
                    if (!(waypoints.Contains(new Vector2((int) parent.X + 1, (int) parent.Y - 1))))
                    {
                        gCosts[(int) parent.X + 1, (int) parent.Y - 1] = gCosts[(int) parent.X, (int) parent.Y] + diagonal;
                        h = (Math.Abs((int) parent.X + 1 - (int) target.X) + Math.Abs((int) parent.Y - (int) target.Y - 1)) * 10;
                        if (gCosts[(int) parent.X + 1, (int) parent.Y - 1] + h < lowestF)
                        {
                            lowestF = gCosts[(int) parent.X + 1, (int) parent.Y - 1] + h;
                            nextParent = new Vector2((int) parent.X + 1, (int) parent.Y - 1);
                        }
                    }
                }
                #endregion

                #region Up and left
                if (((int) parent.Y - 1 >= 0) && ((int) parent.X - 1) >= 0)
                {
                    if (!(waypoints.Contains(new Vector2((int) parent.X - 1, (int) parent.Y - 1))))
                    {
                        gCosts[(int) parent.X - 1, (int) parent.Y - 1] = gCosts[(int) parent.X, (int) parent.Y] + diagonal;
                        h = (Math.Abs((int) parent.X - 1 - (int) target.X) + Math.Abs((int) parent.Y - (int) target.Y - 1)) * 10;
                        if (gCosts[(int) parent.X - 1, (int) parent.Y - 1] + h < lowestF)
                        {
                            lowestF = gCosts[(int) parent.X - 1, (int) parent.Y - 1] + h;
                            nextParent = new Vector2((int) parent.X - 1, (int) parent.Y - 1);
                        }
                    }
                }
                #endregion

                #region Down and right
                if (((int) parent.Y + 1 >= 0) && ((int) parent.X + 1) <= (tileArray.GetLength(0) - 1))
                {
                    if (!(waypoints.Contains(new Vector2((int) parent.X + 1, (int) parent.Y + 1))))
                    {
                        gCosts[(int) parent.X + 1, (int) parent.Y + 1] = gCosts[(int) parent.X, (int) parent.Y] + diagonal;
                        h = (Math.Abs((int) parent.X + 1 - (int) target.X) + Math.Abs((int) parent.Y + 1 - (int) target.Y)) * 10;
                        if (gCosts[(int) parent.X + 1, (int) parent.Y + 1] + h < lowestF)
                        {
                            lowestF = gCosts[(int) parent.X + 1, (int) parent.Y + 1] + h;
                            nextParent = new Vector2((int) parent.X + 1, (int) parent.Y + 1);
                        }
                    }
                }
                #endregion

                #region Down and left
                if (((int) parent.X - 1) >= 0 && ((int) parent.Y + 1 >= 0))
                {
                    if (!(waypoints.Contains(new Vector2((int) parent.X - 1, (int) parent.Y + 1))))
                    {
                        gCosts[(int) parent.X - 1, (int) parent.Y + 1] = gCosts[(int) parent.X, (int) parent.Y] + diagonal;
                        h = (Math.Abs((int) parent.X - 1 - (int) target.X) + Math.Abs((int) parent.Y - (int) target.Y + 1)) * 10;
                        if (gCosts[(int) parent.X - 1, (int) parent.Y + 1] + h < lowestF)
                        {
                            lowestF = gCosts[(int) parent.X - 1, (int) parent.Y + 1] + h;
                            nextParent = new Vector2((int) parent.X - 1, (int) parent.Y + 1);
                        }
                    }
                }
                #endregion

                //Add lowest tile to waypoints.
                waypoints.Enqueue(nextParent);
            }

            //when we have made a path, return it.
            return waypoints;
        }
    }
}