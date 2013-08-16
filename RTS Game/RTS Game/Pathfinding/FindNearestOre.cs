using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RTS_Game.Pathfinding
{

    //NOTE: This class essentially uses pseudo code as we're not sure how the
    //ore will be stored yet: as a tile, in a list, 2D array, etc. Expect it to
    //change. May also need a maximum distance to search (RA1 did) and needs some
    //code to stop the search if no ore exists, though this may be handled within the
    //harvester.
    static class FindClosestOre
    {
        static Harvester harvester;
        static Tile[,] world;
        static Ore[,] oreArray;
        static List<Ore> toBeSearched = new List<Ore>();
        static List<Ore> alreadySearched = new List<Ore>();
        static Ore target = null;

        #region Function Explanation
        /*Adds the units position to the to be searched list and calls the search method
         * in order to begin the search. When the (closest) target/ore has been found,
         * it breaks out of the loop of sending tiles to the search algorithm and 
         * returns the tile where ore exists.
         * */
        #endregion
        static public Vector2 BeginSearch(Harvester harvesterToMove, Tile[,] worldArray, Ore[,] OreArray)
        {
            //Setting/resetting values as it is a static class.
            harvester = harvesterToMove;
            world = worldArray;
            oreArray = OreArray;
            target = null;
            toBeSearched = new List<Ore>();
            alreadySearched = new List<Ore>();

            toBeSearched.Add(oreArray[(int)harvester.TilePosition.X, (int)harvester.TilePosition.Y]);

            while (target == null)
            {
                foreach (Ore t in toBeSearched.ToList<Ore>())
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
        static public void Search(Ore tileToSearch)
        {
            if (tileToSearch.CurrentAmount > 0 && tileToSearch.BeingMined == false)
            {
                target = tileToSearch;
            }
            else
            {
                alreadySearched.Add(tileToSearch);
                toBeSearched.Remove(tileToSearch);

                if (new Rectangle(0, 0, oreArray.GetLength(0), oreArray.GetLength(1)).Contains
                    (new Point((int) (int) tileToSearch.TilePosition.X + 1, (int) (int) tileToSearch.TilePosition.Y)))
                {
                    if (!alreadySearched.Contains(oreArray[(int) tileToSearch.TilePosition.X + 1, (int) tileToSearch.TilePosition.Y]))
                    {
                        toBeSearched.Add(oreArray[(int) tileToSearch.TilePosition.X + 1, (int) tileToSearch.TilePosition.Y]);
                    }
                }

                if (new Rectangle(0, 0, oreArray.GetLength(0), oreArray.GetLength(1)).Contains
                    (new Point((int)tileToSearch.TilePosition.X - 1, (int)tileToSearch.TilePosition.Y)))
                {
                    if (!alreadySearched.Contains(oreArray[(int) tileToSearch.TilePosition.X - 1, (int) tileToSearch.TilePosition.Y]))
                    {
                        toBeSearched.Add(oreArray[(int) tileToSearch.TilePosition.X - 1, (int) tileToSearch.TilePosition.Y]);
                    }
                }

                if (new Rectangle(0, 0, oreArray.GetLength(0), oreArray.GetLength(1)).Contains
                    (new Point((int)tileToSearch.TilePosition.X, (int)tileToSearch.TilePosition.Y + 1)))
                {
                    if (!alreadySearched.Contains(oreArray[(int) tileToSearch.TilePosition.X, (int) tileToSearch.TilePosition.Y + 1]))
                    {
                        toBeSearched.Add(oreArray[(int) tileToSearch.TilePosition.X, (int) tileToSearch.TilePosition.Y + 1]);
                    }
                }

                if (new Rectangle(0, 0, oreArray.GetLength(0), oreArray.GetLength(1)).Contains
                    (new Point((int)tileToSearch.TilePosition.X + 1, (int)tileToSearch.TilePosition.Y)))
                {
                    if (!alreadySearched.Contains(oreArray[(int) tileToSearch.TilePosition.X + 1, (int) tileToSearch.TilePosition.Y]))
                    {
                        toBeSearched.Add(oreArray[(int) tileToSearch.TilePosition.X + 1, (int) tileToSearch.TilePosition.Y]);
                    }
                }

            }
        }

    }
}
