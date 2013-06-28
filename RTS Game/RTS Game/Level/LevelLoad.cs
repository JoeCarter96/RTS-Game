using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RTS_Game.Level
{
    class LevelLoad
    {
        String[,] pixelColours;

        Dictionary<Color, String> Colours = new Dictionary<Color, String>();

        #region Function Explanation
        //First gets a 1d array of the colours of each pixel in a Texture2D. 
        //It then loops through a String array, setting the values to the bitmaps
        //colour/texture value at that position.
        #endregion
        public LevelLoad(Texture2D Level)
        {
            //Array of Colours of each pixel.
            Color[] pixelColourValues = new Color[Level.Width * Level.Height];
            //String Array of each Pixel Colours texture value.
            pixelColours = new String[Level.Width, Level.Height];

            //Puts colour of each Pixel into Array.
            Level.GetData(pixelColourValues);

            for (int i = 0; i < Level.Width; i++)
            {
                for (int j = 0; j < Level.Height; j++)
                {
                    //Tries to find matching colour key, if it does sets cell to the name of texture.
                    Colours.TryGetValue(pixelColourValues[i * j], out pixelColours[i, j]);
                }
            }
        }

        #region Function Explanation
        //Simply adds all Pairs to the Dictionary.
        #endregion
        public void AddColours()
        {
            Colours.Add(new Color(0, 178, 0), "Grass");
        }
    }
}
