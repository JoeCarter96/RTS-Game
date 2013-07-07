using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RTS_Game
{
    static class LevelLoad
    {
        #region Variables
        private static Texture2D[,] textureArray;
        private static Dictionary<Color, String> Colours = new Dictionary<Color, String>();
        #endregion

        #region Function Explanation
        //Constructor, Simply adds all pairs to the Dictionary.
        #endregion
        public LevelLoad()
        {
            Colours.Add(new Color(0, 0, 0), "DebugTile");
            Colours.Add(new Color(0, 178, 0), "Grass01");
            Colours.Add(new Color(191, 191, 191), "Road01");
            Colours.Add(new Color(63, 72, 204), "Water01");
        }

        #region Function Explanation
        //First gets a 1D Array (PixelRGBValues) of the colours of each pixel in a Texture2D Image (Level). 
        //It then loops through a Texture2D array/every pixel in the image. It tries to find the name of the 
        //texture represented by the current pixel in the image by comparing it's RGB value to a dictionary of them.
        //If it finds them it then tries to set the Texture2D array's current cell to the corresponding Texture in the
        //Array in Resources.cs.
        #endregion
        public static Texture2D[,] Load(Texture2D Level)
        {
            //Array of Colours of each pixel. 
            //Goes from top left to bottom right.
            Color[] pixelRGBValues = new Color[Level.Width * Level.Height];

            //Array of each Pixel Colours texture.
            textureArray = new Texture2D[Level.Width, Level.Height];

            //Puts colour of each Pixel into Array.
            Level.GetData(pixelRGBValues);

            for (int i = 0; i < Level.Width; i++)
            {
                for (int j = 0; j < Level.Height; j++)
                {
                    String textureName;
                    //Tries to find matching colour key, if it does sets it to textureName.
                    //Array2DTo1D converts from 2D to 1D by adding up all the full rows 
                    // (30 * I, would normally be 30*(i-1) but i starts at 0 so no need)
                    // and then adding the remaining amount on the current row.
                    int Array2DTo1D = Level.Width * j + i;
                    Colours.TryGetValue(pixelRGBValues[Array2DTo1D], out textureName);

                    //Tries to find texture in Resources dictionary, if it can't defaults to grass texture.
                    try
                    {
                        textureArray[i, j] = Resources.GetBackgroundTextures(textureName);
                    }
                    catch
                    {
                        textureArray[i, j] = Resources.GetBackgroundTextures("Grass01");
                    }
                } 
            }
            //Returns Texture Array.
            return textureArray;
        }

    }
}
