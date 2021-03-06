﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RTS_Game
{
    static class WaypointsGenerator
    {
        #region Class Description
        //This class is responsible for the generation of a Queue of waypoints which a Unit would use in order to move.
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

        
        public static Queue<Vector2> GenerateWaypoints(Vector2 unitPos, Vector2 target, bool ignoreObstacles)
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
                //If it's within bounds and not obstructed by a stationary thing,
                if ((((int)parent.X + 1) <= (tileArray.GetLength(0) - 1) && 
                    tileArray[(int)parent.X + 1, (int)parent.Y].Obstacle != true) || ignoreObstacles == true)
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
                    if (!(waypoints.Contains((new Vector2((int)parent.X - 1, (int)parent.Y))) &&
                    tileArray[(int)parent.X - 1, (int)parent.Y].Obstacle != true) || ignoreObstacles == true)
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

                # region Down
                if (((int) parent.Y + 1 >= 0))
                {
                    if ((!(waypoints.Contains(new Vector2((int) parent.X, (int) parent.Y + 1))) &&
                    tileArray[(int)parent.X, (int)parent.Y + 1].Obstacle != true) || ignoreObstacles == true)
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
                    if ((!(waypoints.Contains(new Vector2((int)parent.X, (int)parent.Y - 1))) &&
                    tileArray[(int)parent.X, (int)parent.Y - 1].Obstacle != true) || ignoreObstacles == true)
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
                    if ((!(waypoints.Contains(new Vector2((int)parent.X + 1, (int)parent.Y - 1))) &&
                    tileArray[(int)parent.X + 1, (int)parent.Y - 1].Obstacle != true) || ignoreObstacles == true)
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
                    if ((!(waypoints.Contains(new Vector2((int)parent.X - 1, (int)parent.Y - 1))) &&
                    tileArray[(int)parent.X - 1, (int)parent.Y - 1].Obstacle != true) || ignoreObstacles == true)
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
                    if ((!(waypoints.Contains(new Vector2((int)parent.X + 1, (int)parent.Y + 1))) &&
                    tileArray[(int)parent.X + 1, (int)parent.Y + 1].Obstacle != true) || ignoreObstacles == true)
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
                    if ((!(waypoints.Contains(new Vector2((int)parent.X - 1, (int)parent.Y + 1))) &&
                    tileArray[(int)parent.X - 1, (int)parent.Y + 1].Obstacle != true) || ignoreObstacles == true)
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

            //If a unit is moving whatsoever, it is no longer a semi-stationary object.
            //it's here to stop Harvesters making buildings not obstacles.
            tileArray[(int)unitPos.X, (int)unitPos.Y].Obstacle = false;

            //when we have made a path, return it.
            return waypoints;
        }

        //pathfinding while ignoring all the obstacles within a rectangle.
        public static Queue<Vector2> GenerateWaypoints(Vector2 unitPos, Vector2 target, Rectangle rectToIgnore)
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

                Rectangle modifiedRect = new Rectangle(ToTile(rectToIgnore.X) - 1, ToTile(rectToIgnore.Y) - 1, ToTile(rectToIgnore.Width) + 2,
                    ToTile(rectToIgnore.Height) + 2);

                //Normal Obstacle Avoidance.
                if (!modifiedRect.Contains(new Point((int)parent.X, (int)parent.Y)))
                {
                    # region Right
                    //If it's within bounds and not obstructed by a stationary thing,
                    if (((int)parent.X + 1) <= (tileArray.GetLength(0) - 1) &&
                        tileArray[(int)parent.X + 1, (int)parent.Y].Obstacle != true)
                    {
                        //..and is not a Vector2 in the queue.
                        if (!(waypoints.Contains(new Vector2((int)parent.X + 1, (int)parent.Y))))
                        {
                            gCosts[(int)parent.X + 1, (int)parent.Y] = gCosts[(int)parent.X, (int)parent.Y] + orthogonal;
                            h = (Math.Abs((int)parent.X + 1 - (int)(int)target.X) + Math.Abs((int)parent.Y - (int)target.Y)) * 10;
                            if (gCosts[(int)parent.X + 1, (int)parent.Y] + h < lowestF)
                            {
                                lowestF = gCosts[(int)parent.X + 1, (int)parent.Y] + h;
                                nextParent = new Vector2((int)parent.X + 1, (int)parent.Y);
                            }
                        }
                    }
                    #endregion

                    # region Left
                    if (((int)parent.X - 1 >= 0))
                    {
                        if (!(waypoints.Contains(new Vector2((int)parent.X - 1, (int)parent.Y))) &&
                        tileArray[(int)parent.X - 1, (int)parent.Y].Obstacle != true)
                        {
                            gCosts[(int)parent.X - 1, (int)parent.Y] = gCosts[(int)parent.X, (int)parent.Y] + orthogonal;
                            h = (Math.Abs((int)parent.X - 1 - (int)target.X) + Math.Abs((int)parent.Y - (int)target.Y)) * 10;
                            if (gCosts[(int)parent.X - 1, (int)parent.Y] + h < lowestF)
                            {
                                lowestF = gCosts[(int)parent.X - 1, (int)parent.Y] + h;
                                nextParent = new Vector2((int)parent.X - 1, (int)parent.Y);
                            }
                        }
                    }
                    #endregion

                    # region Down
                    if (((int)parent.Y + 1 >= 0))
                    {
                        if (!(waypoints.Contains(new Vector2((int)parent.X, (int)parent.Y + 1))) &&
                        tileArray[(int)parent.X, (int)parent.Y + 1].Obstacle != true)
                        {
                            gCosts[(int)parent.X, (int)parent.Y + 1] = gCosts[(int)parent.X, (int)parent.Y] + orthogonal;
                            h = (Math.Abs((int)parent.X - (int)target.X) + Math.Abs((int)parent.Y + 1 - (int)target.Y)) * 10;
                            if (gCosts[(int)parent.X, (int)parent.Y + 1] + h < lowestF)
                            {
                                lowestF = gCosts[(int)parent.X, (int)parent.Y + 1] + h;
                                nextParent = new Vector2((int)parent.X, (int)parent.Y + 1);
                            }
                        }
                    }
                    #endregion

                    #region Up
                    if (((int)parent.Y - 1 >= 0))
                    {
                        if (!(waypoints.Contains(new Vector2((int)parent.X, (int)parent.Y - 1))) &&
                        tileArray[(int)parent.X, (int)parent.Y - 1].Obstacle != true)
                        {
                            gCosts[(int)parent.X, (int)parent.Y - 1] = gCosts[(int)parent.X, (int)parent.Y] + orthogonal;
                            h = (Math.Abs((int)parent.X - (int)target.X) + Math.Abs((int)parent.Y - (int)target.Y - 1)) * 10;
                            if (gCosts[(int)parent.X, (int)parent.Y - 1] + h < lowestF)
                            {
                                lowestF = gCosts[(int)parent.X, (int)parent.Y - 1] + h;
                                nextParent = new Vector2((int)parent.X, (int)parent.Y - 1);
                            }
                        }
                    }
                    #endregion

                    #region Up and right
                    if (((int)parent.Y - 1 >= 0) && ((int)parent.X + 1) <= (tileArray.GetLength(0) - 1))
                    {
                        if (!(waypoints.Contains(new Vector2((int)parent.X + 1, (int)parent.Y - 1))) &&
                        tileArray[(int)parent.X + 1, (int)parent.Y - 1].Obstacle != true)
                        {
                            gCosts[(int)parent.X + 1, (int)parent.Y - 1] = gCosts[(int)parent.X, (int)parent.Y] + diagonal;
                            h = (Math.Abs((int)parent.X + 1 - (int)target.X) + Math.Abs((int)parent.Y - (int)target.Y - 1)) * 10;
                            if (gCosts[(int)parent.X + 1, (int)parent.Y - 1] + h < lowestF)
                            {
                                lowestF = gCosts[(int)parent.X + 1, (int)parent.Y - 1] + h;
                                nextParent = new Vector2((int)parent.X + 1, (int)parent.Y - 1);
                            }
                        }
                    }
                    #endregion

                    #region Up and left
                    if (((int)parent.Y - 1 >= 0) && ((int)parent.X - 1) >= 0)
                    {
                        if (!(waypoints.Contains(new Vector2((int)parent.X - 1, (int)parent.Y - 1))) &&
                        tileArray[(int)parent.X - 1, (int)parent.Y - 1].Obstacle != true)
                        {
                            gCosts[(int)parent.X - 1, (int)parent.Y - 1] = gCosts[(int)parent.X, (int)parent.Y] + diagonal;
                            h = (Math.Abs((int)parent.X - 1 - (int)target.X) + Math.Abs((int)parent.Y - (int)target.Y - 1)) * 10;
                            if (gCosts[(int)parent.X - 1, (int)parent.Y - 1] + h < lowestF)
                            {
                                lowestF = gCosts[(int)parent.X - 1, (int)parent.Y - 1] + h;
                                nextParent = new Vector2((int)parent.X - 1, (int)parent.Y - 1);
                            }
                        }
                    }
                    #endregion

                    #region Down and right
                    if (((int)parent.Y + 1 >= 0) && ((int)parent.X + 1) <= (tileArray.GetLength(0) - 1))
                    {
                        if (!(waypoints.Contains(new Vector2((int)parent.X + 1, (int)parent.Y + 1))) &&
                        tileArray[(int)parent.X + 1, (int)parent.Y + 1].Obstacle != true)
                        {
                            gCosts[(int)parent.X + 1, (int)parent.Y + 1] = gCosts[(int)parent.X, (int)parent.Y] + diagonal;
                            h = (Math.Abs((int)parent.X + 1 - (int)target.X) + Math.Abs((int)parent.Y + 1 - (int)target.Y)) * 10;
                            if (gCosts[(int)parent.X + 1, (int)parent.Y + 1] + h < lowestF)
                            {
                                lowestF = gCosts[(int)parent.X + 1, (int)parent.Y + 1] + h;
                                nextParent = new Vector2((int)parent.X + 1, (int)parent.Y + 1);
                            }
                        }
                    }
                    #endregion

                    #region Down and left
                    if (((int)parent.X - 1) >= 0 && ((int)parent.Y + 1 >= 0))
                    {
                        if (!(waypoints.Contains(new Vector2((int)parent.X - 1, (int)parent.Y + 1))) &&
                        tileArray[(int)parent.X - 1, (int)parent.Y + 1].Obstacle != true)
                        {
                            gCosts[(int)parent.X - 1, (int)parent.Y + 1] = gCosts[(int)parent.X, (int)parent.Y] + diagonal;
                            h = (Math.Abs((int)parent.X - 1 - (int)target.X) + Math.Abs((int)parent.Y - (int)target.Y + 1)) * 10;
                            if (gCosts[(int)parent.X - 1, (int)parent.Y + 1] + h < lowestF)
                            {
                                lowestF = gCosts[(int)parent.X - 1, (int)parent.Y + 1] + h;
                                nextParent = new Vector2((int)parent.X - 1, (int)parent.Y + 1);
                            }
                        }
                    }
                    #endregion

                    //Add lowest tile to waypoints.
                    waypoints.Enqueue(nextParent);
                }

                //Ignoring the obstacles of the building we're moving into.
                else
                {
                    #region Right
                    //If it's within bounds and not obstructed by a stationary thing,
                    if (((int)parent.X + 1) <= (tileArray.GetLength(0) - 1))
                    {
                        //..and is not a Vector2 in the queue.
                        if (!(waypoints.Contains(new Vector2((int)parent.X + 1, (int)parent.Y))))
                        {
                            gCosts[(int)parent.X + 1, (int)parent.Y] = gCosts[(int)parent.X, (int)parent.Y] + orthogonal;
                            h = (Math.Abs((int)parent.X + 1 - (int)(int)target.X) + Math.Abs((int)parent.Y - (int)target.Y)) * 10;
                            if (gCosts[(int)parent.X + 1, (int)parent.Y] + h < lowestF)
                            {
                                lowestF = gCosts[(int)parent.X + 1, (int)parent.Y] + h;
                                nextParent = new Vector2((int)parent.X + 1, (int)parent.Y);
                            }
                        }
                    }
                    #endregion

                    # region Left
                    if (((int)parent.X - 1 >= 0))
                    {
                        if (!(waypoints.Contains(new Vector2((int)parent.X - 1, (int)parent.Y))))
                        {
                            gCosts[(int)parent.X - 1, (int)parent.Y] = gCosts[(int)parent.X, (int)parent.Y] + orthogonal;
                            h = (Math.Abs((int)parent.X - 1 - (int)target.X) + Math.Abs((int)parent.Y - (int)target.Y)) * 10;
                            if (gCosts[(int)parent.X - 1, (int)parent.Y] + h < lowestF)
                            {
                                lowestF = gCosts[(int)parent.X - 1, (int)parent.Y] + h;
                                nextParent = new Vector2((int)parent.X - 1, (int)parent.Y);
                            }
                        }
                    }
                    #endregion

                    # region Down
                    if (((int)parent.Y + 1 >= 0))
                    {
                        if (!(waypoints.Contains(new Vector2((int)parent.X, (int)parent.Y + 1))))
                        {
                            gCosts[(int)parent.X, (int)parent.Y + 1] = gCosts[(int)parent.X, (int)parent.Y] + orthogonal;
                            h = (Math.Abs((int)parent.X - (int)target.X) + Math.Abs((int)parent.Y + 1 - (int)target.Y)) * 10;
                            if (gCosts[(int)parent.X, (int)parent.Y + 1] + h < lowestF)
                            {
                                lowestF = gCosts[(int)parent.X, (int)parent.Y + 1] + h;
                                nextParent = new Vector2((int)parent.X, (int)parent.Y + 1);
                            }
                        }
                    }
                    #endregion

                    #region Up
                    if (((int)parent.Y - 1 >= 0))
                    {
                        if (!(waypoints.Contains(new Vector2((int)parent.X, (int)parent.Y - 1))))
                        {
                            gCosts[(int)parent.X, (int)parent.Y - 1] = gCosts[(int)parent.X, (int)parent.Y] + orthogonal;
                            h = (Math.Abs((int)parent.X - (int)target.X) + Math.Abs((int)parent.Y - (int)target.Y - 1)) * 10;
                            if (gCosts[(int)parent.X, (int)parent.Y - 1] + h < lowestF)
                            {
                                lowestF = gCosts[(int)parent.X, (int)parent.Y - 1] + h;
                                nextParent = new Vector2((int)parent.X, (int)parent.Y - 1);
                            }
                        }
                    }
                    #endregion

                    #region Up and right
                    if (((int)parent.Y - 1 >= 0) && ((int)parent.X + 1) <= (tileArray.GetLength(0) - 1))
                    {
                        if (!(waypoints.Contains(new Vector2((int)parent.X + 1, (int)parent.Y - 1))))
                        {
                            gCosts[(int)parent.X + 1, (int)parent.Y - 1] = gCosts[(int)parent.X, (int)parent.Y] + diagonal;
                            h = (Math.Abs((int)parent.X + 1 - (int)target.X) + Math.Abs((int)parent.Y - (int)target.Y - 1)) * 10;
                            if (gCosts[(int)parent.X + 1, (int)parent.Y - 1] + h < lowestF)
                            {
                                lowestF = gCosts[(int)parent.X + 1, (int)parent.Y - 1] + h;
                                nextParent = new Vector2((int)parent.X + 1, (int)parent.Y - 1);
                            }
                        }
                    }
                    #endregion

                    #region Up and left
                    if (((int)parent.Y - 1 >= 0) && ((int)parent.X - 1) >= 0)
                    {
                        if (!(waypoints.Contains(new Vector2((int)parent.X - 1, (int)parent.Y - 1))))
                        {
                            gCosts[(int)parent.X - 1, (int)parent.Y - 1] = gCosts[(int)parent.X, (int)parent.Y] + diagonal;
                            h = (Math.Abs((int)parent.X - 1 - (int)target.X) + Math.Abs((int)parent.Y - (int)target.Y - 1)) * 10;
                            if (gCosts[(int)parent.X - 1, (int)parent.Y - 1] + h < lowestF)
                            {
                                lowestF = gCosts[(int)parent.X - 1, (int)parent.Y - 1] + h;
                                nextParent = new Vector2((int)parent.X - 1, (int)parent.Y - 1);
                            }
                        }
                    }
                    #endregion

                    #region Down and right
                    if (((int)parent.Y + 1 >= 0) && ((int)parent.X + 1) <= (tileArray.GetLength(0) - 1))
                    {
                        if (!(waypoints.Contains(new Vector2((int)parent.X + 1, (int)parent.Y + 1))))
                        {
                            gCosts[(int)parent.X + 1, (int)parent.Y + 1] = gCosts[(int)parent.X, (int)parent.Y] + diagonal;
                            h = (Math.Abs((int)parent.X + 1 - (int)target.X) + Math.Abs((int)parent.Y + 1 - (int)target.Y)) * 10;
                            if (gCosts[(int)parent.X + 1, (int)parent.Y + 1] + h < lowestF)
                            {
                                lowestF = gCosts[(int)parent.X + 1, (int)parent.Y + 1] + h;
                                nextParent = new Vector2((int)parent.X + 1, (int)parent.Y + 1);
                            }
                        }
                    }
                    #endregion

                    #region Down and left
                    if (((int)parent.X - 1) >= 0 && ((int)parent.Y + 1 >= 0))
                    {
                        if (!(waypoints.Contains(new Vector2((int)parent.X - 1, (int)parent.Y + 1))))
                        {
                            gCosts[(int)parent.X - 1, (int)parent.Y + 1] = gCosts[(int)parent.X, (int)parent.Y] + diagonal;
                            h = (Math.Abs((int)parent.X - 1 - (int)target.X) + Math.Abs((int)parent.Y - (int)target.Y + 1)) * 10;
                            if (gCosts[(int)parent.X - 1, (int)parent.Y + 1] + h < lowestF)
                            {
                                lowestF = gCosts[(int)parent.X - 1, (int)parent.Y + 1] + h;
                                nextParent = new Vector2((int)parent.X - 1, (int)parent.Y + 1);
                            }
                        }
                    }
                    #endregion

                    //Add lowest tile to waypoints.
                    waypoints.Enqueue(nextParent);
                }
            }
            //If a unit is moving whatsoever, it is no longer a semi-stationary object.
            //it's here to stop Harvesters making buildings not obstacles.
            tileArray[(int)unitPos.X, (int)unitPos.Y].Obstacle = false;

            //when we have made a path, return it.
            return waypoints;
        }


        public static int ToTile(int pixelPos)
        {
            return (int) Math.Round((decimal) pixelPos / GameClass.Tile_Width);
        }
    }
}