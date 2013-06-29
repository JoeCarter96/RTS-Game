﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RTS_Game.Level
{
    static class LevelLoad
    {
        private static Texture2D[,] textureArray;

        private static Dictionary<Color, String> Colours = new Dictionary<Color, String>();

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
                    Colours.TryGetValue(pixelRGBValues[i * j], out textureName);

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

        #region Function Explanation
        //Simply adds all pairs to the Dictionary.
        #endregion
        public static void AddColours()
        {
            Colours.Add(new Color(0, 178, 0), "Grass01");
            Colours.Add(new Color(191, 191, 191), "Road01");
        }
    }
}
