using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RTS_Game
{
    static class FieldModifer
    {
        #region Class Description
        //This class is responsible for the generation of the Potential Fields. Think of it as a Unility class, it is simply
        //a plug numbers in, get numbers out concept. When a unit is selected and a new location is clicked, the calculatedField
        //method needs to be called. This uses the addToField and Equation methods to construct a Potential field, by first generating
        //a large positive field at mouse point, and then negative ones at enemy locations. Every time something moves (Units), we need to
        //loop through each moving unit and update their PF. Not too brilliant, however if we can make use of convoys/groups when moving
        //many units at once, it should actually be quite efficient. I may also be able to add a method which just updates the potential
        //field at the changed locations, however I need to first find a method to remove the old potential field which the unit was creating
        // before I can add the new one. We can also make use of pre-generated arrays of fields, so for instance a unit could have a 2D array
        //which holds a negative field, which can just be added to the array generated here. All in all, there are a lot of possibilities.
        #endregion

        //Reference of the tileArray.
        static Tile[,] tileArray;

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
        public static void calculateField(int[,] fieldToModify, int sourceX, int sourceY, int maxPositiveRange)
        {
            Console.WriteLine("Calculating Field");

            //Clears the array.
            for (int i = 0; i < fieldToModify.GetLength(0); i++)
            {
                for (int j = 0; j < fieldToModify.GetLength(1); j++)
                {
                    fieldToModify[i, j] = 0;
                }
            }


            //Creates the positive field.
            addToField(fieldToModify, sourceX, sourceY, true, 100);


            //Add code for generating negative fields here.


        }

        #region Function Description
        //Adds to the current potential field, called by CalculateField();
        #endregion
        public static void addToField(int[,] fieldToModify, int sourceX, int sourceY, bool positive, int maxRange)
        {
            //Calculating effective range of field for more efficient calculations.
            //Returns highest value between the two, makes sure it doesn't go below zero.
            int minX = Math.Max(0, sourceX - maxRange);
            int minY = Math.Max(0, sourceY - maxRange);
            //Returns lowest value between the two, makes sure it doesn't go below above array size.
            int maxX = Math.Min(sourceX + maxRange, tileArray.GetLength(0));
            int maxY = Math.Min(sourceY + maxRange, tileArray.GetLength(1));
                
            //Loop through every cell.
            for (int i = minX; i < maxX; i++)
            {
                for (int j = minY; j < maxY; j++)
                {
                    //Calculating difference from the source to the current cell in squares.
                    int dX = Math.Abs(sourceX - i);
                    int dY = Math.Abs(sourceY - j);
                    int distanceFromSource = (dX + dY);

                    //Intensity of the current cell.
                    int newFieldStrength;

                    //Calculating the strength of this cell.
                    if (distanceFromSource < maxRange)
                    {
                        //If the source is a positive emitter.
                        if (positive == true)
                        {
                            newFieldStrength = PositiveEquation(fieldToModify, i, j, distanceFromSource);
                        }

                        //Or if it's a Negative Emitter.
                        else
                        {
                            newFieldStrength = NegativeEquationSmallShort(fieldToModify, i, j, distanceFromSource);
                        }
                        //Actually changing the field strength for this cell.
                        fieldToModify[i, j] = newFieldStrength;
                    }
                }
            }
        }

        #region Function Description
        //The only positive equation needed: Generated at mouse point.
        #endregion
        public static int PositiveEquation(int[,] fieldToModify, int currentPosX, int currentPosY, int distanceFromSource)
        {
            if (distanceFromSource > 0)
            {
                //Actual Calculation.
                int newFieldStrength = 255 - distanceFromSource * 3;

                //Making sure it stays positive.
                if (newFieldStrength > 0)
                    return newFieldStrength += fieldToModify[currentPosX, currentPosY];
                else
                    return fieldToModify[currentPosX, currentPosY];
            }
            // If distanceFromSource !> 0, it's at the source.
            else
            {
                return int.MaxValue;
            }
        }

        #region Function Description
        //A small negative field with a short decay, for cliffs ,for example.
        #endregion
        public static int NegativeEquationSmallShort(int[,] fieldToModify, int currentPosX, int currentPosY, int distanceFromSource)
        {
            if (distanceFromSource > 0)
            {
                //Actual Calculation.
                int newFieldStrength = -(150 / (int)Math.Ceiling((Double)distanceFromSource));

                //Making sure it stays positive.
                if (newFieldStrength < 0)
                    return newFieldStrength += fieldToModify[currentPosX, currentPosY];
                else
                    return fieldToModify[currentPosX, currentPosY];
            }
            // If distanceFromSource !> 0, it's at the source.
            else
            {
                return -(int.MaxValue);
            }
        }
    }
}