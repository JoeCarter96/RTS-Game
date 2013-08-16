using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RTS_Game
{
    static class FindNearestTile
    {
        static Tile[,] tileArray;
        static List<Tile> toBeSearched = new List<Tile>();
        static List<Tile> alreadySearched = new List<Tile>();
        static Tile target = null;
        static int cycler = 0;

        #region Function Explanation
        /*Adds the units position to the to be searched list and calls the search method
         * in order to begin the search. When the (closest) target/ore has been found,
         * it breaks out of the loop of sending tiles to the search algorithm and 
         * returns the tile where ore exists.
         * */
        #endregion
        static public Vector2 BeginSearch(Vector2 startPos, Tile[,] worldArray)
        {
            tileArray = worldArray;
            target = null;
            alreadySearched.Clear();
            toBeSearched.Clear();

            toBeSearched.Add(tileArray[(int)startPos.X, (int)startPos.Y]);

            while (target == null)
            {
                foreach (Tile t in toBeSearched.ToList())
                {
                    if (!alreadySearched.Contains(t))
                    {
                        if (target == null)
                        {
                            Search(t);
                        }

                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        toBeSearched.Remove(t);
                    }
                }
            }
            return target.TilePosition;
        }

        #region Function Explanation
        /*First checks if the passed tile is ore. If it is, it has found ore and this method ends.
         * If not, it checks to make sure each of the tiles around have not been searched, and if they
         * have not, they are added to a list to be passed to this method. The weird code using rectangles
         * basically just constructs a rectangle which is the size of the array and then checks that the 
         * tile is contained by it. If it is not it is out of the array and if it were called we would get errors.
         */
        #endregion
        static public void Search(Tile tileToSearch)
        {
            //If it's a free tile, we want to move to it.
            if (!tileToSearch.Obstacle && !tileToSearch.OccupiedByUnit)
            {
                target = tileToSearch;
            }
            
            //otherwise, search the surrounding tiles.
            else
            {
                alreadySearched.Add(tileToSearch);
                toBeSearched.Remove(tileToSearch);

                if (cycler > 3)
                    cycler = 0;

                switch (cycler)
                {
                    case (0):
                        {
                            //if the tile is within the game world.
                            if (new Rectangle(0, 0, tileArray.GetLength(0), tileArray.GetLength(1)).Contains
                                (new Point((int)(int)tileToSearch.TilePosition.X + 1, (int)(int)tileToSearch.TilePosition.Y)))
                            {
                                //If it's not already been searched, add Tile in question to search list.
                                if (!alreadySearched.Contains(tileArray[(int)tileToSearch.TilePosition.X + 1, (int)tileToSearch.TilePosition.Y]))
                                {
                                    toBeSearched.Add(tileArray[(int)tileToSearch.TilePosition.X + 1, (int)tileToSearch.TilePosition.Y]);
                                }
                            }

                            cycler++;
                            break;
                        }

                    case (1):
                        {
                            if (new Rectangle(0, 0, tileArray.GetLength(0), tileArray.GetLength(1)).Contains
                                (new Point((int)tileToSearch.TilePosition.X - 1, (int)tileToSearch.TilePosition.Y)))
                            {
                                if (!alreadySearched.Contains(tileArray[(int)tileToSearch.TilePosition.X - 1, (int)tileToSearch.TilePosition.Y]))
                                {
                                    toBeSearched.Add(tileArray[(int)tileToSearch.TilePosition.X - 1, (int)tileToSearch.TilePosition.Y]);
                                }
                            }

                            cycler++;
                            break;
                        }

                    case (2):
                        {
                            if (new Rectangle(0, 0, tileArray.GetLength(0), tileArray.GetLength(1)).Contains
                                (new Point((int)tileToSearch.TilePosition.X, (int)tileToSearch.TilePosition.Y + 1)))
                            {
                                if (!alreadySearched.Contains(tileArray[(int)tileToSearch.TilePosition.X, (int)tileToSearch.TilePosition.Y + 1]))
                                {
                                    toBeSearched.Add(tileArray[(int)tileToSearch.TilePosition.X, (int)tileToSearch.TilePosition.Y + 1]);
                                }
                            }

                            cycler++;
                            break;
                        }

                    case (3):
                        {
                            if (new Rectangle(0, 0, tileArray.GetLength(0), tileArray.GetLength(1)).Contains
                                (new Point((int)tileToSearch.TilePosition.X + 1, (int)tileToSearch.TilePosition.Y)))
                            {
                                if (!alreadySearched.Contains(tileArray[(int)tileToSearch.TilePosition.X + 1, (int)tileToSearch.TilePosition.Y]))
                                {
                                    toBeSearched.Add(tileArray[(int)tileToSearch.TilePosition.X + 1, (int)tileToSearch.TilePosition.Y]);
                                }
                            }
                            cycler++;
                            break;
                        }
                }
            }
        }
    }
}
