using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RTS_Game
{
    public static class Resources
    {
        #region Variables
        private static Dictionary<String, Texture2D> BackgroundTextures = new Dictionary<string,Texture2D>();
        private static Dictionary<String, Texture2D> UnitTextures = new Dictionary<string, Texture2D>();
        private static Dictionary<String, Texture2D> BuildingTextures = new Dictionary<string, Texture2D>();
        private static Dictionary<String, Texture2D> GUITextures = new Dictionary<string, Texture2D>();
        private static Dictionary<String, Texture2D> BulletTextures = new Dictionary<String, Texture2D>();
        private static Dictionary<int, Level> LevelObjects = new Dictionary<int, Level>();
        
        //TEMP: Font used for testing
        public static SpriteFont TestFont;
        #endregion

        #region Background Textures
        public static void AddBackgroundTexture(Texture2D textureToAdd)
        {
            
            BackgroundTextures.Add(textureToAdd.Name, textureToAdd);
        }

        public static Texture2D GetBackgroundTextures(String requestedTextureName)
        {
            return BackgroundTextures[requestedTextureName];
        }
        #endregion

        #region Unit Textures
        public static void AddUnitTexture(Texture2D textureToAdd)
        {
            UnitTextures.Add(textureToAdd.Name, textureToAdd);
        }

        public static Dictionary<String, Texture2D> GetUnitTextures()
        {
            return UnitTextures;
        }
        #endregion

        #region Building Textures
        public static void AddBuildingTexture(Texture2D textureToAdd)
        {
            BuildingTextures.Add(textureToAdd.Name, textureToAdd);
        }

        public static Dictionary<String, Texture2D> GetBuildingTextures()
        {
            return BuildingTextures;
        }
        #endregion

        #region GUI Textures
        public static void AddGUITexture(Texture2D textureToAdd)
        {
            GUITextures.Add(textureToAdd.Name, textureToAdd);
        }

        public static Texture2D GetGUITextures(String requestedTextureName)
        {
            return GUITextures[requestedTextureName];
        }
        #endregion

        #region Bullet Textures
        public static void AddBulletTexture(Texture2D textureToAdd)
        {
            BulletTextures.Add(textureToAdd.Name, textureToAdd);
        }

        public static Texture2D GetBulletTextures(String requestedTextureName)
        {
            return BulletTextures[requestedTextureName];
        }
        #endregion
        
        #region Level Objects

        public static void AddLevelObject(Level LevedToAdd)
        {
            LevelObjects.Add(LevedToAdd.ID, LevedToAdd);
        }

        public static Level GetLevelObject(int requestedLevelID)
        {
            return LevelObjects[requestedLevelID];
        }
        #endregion

        public static Dictionary<String, Texture2D> getColouredTextures(Dictionary<String, Texture2D> textures, Color teamColour)
        {
            Dictionary<String, Texture2D> newTextures = new Dictionary<String, Texture2D>();

            //For each texture in texture list..
            foreach (KeyValuePair<String, Texture2D> k in textures)
            {
                newTextures.Add(k.Key, new Texture2D(k.Value.GraphicsDevice, k.Value.Width, k.Value.Height));

                //Array of Colours of each pixel. 
                //Goes from top left to bottom right.
                Color[] pixelRGBValues = new Color[k.Value.Width * k.Value.Height];
                //Puts colour of each Pixel into Array.
                k.Value.GetData(pixelRGBValues);

                for (int i = 0; i < k.Value.Width; i++)
                {
                    for (int j = 0; j < k.Value.Height; j++)
                    {
                        //Array2DTo1D converts from 2D to 1D by adding up all the full rows
                        // and then adding the remaining amount on the current row.
                        int Array2DTo1D = k.Value.Width * j + i;

                        //Shadows
                        if (pixelRGBValues[Array2DTo1D].G == 255 && pixelRGBValues[Array2DTo1D].B == 0 &&
                            pixelRGBValues[Array2DTo1D].R == 0)
                        {
                            pixelRGBValues[Array2DTo1D] = new Color(0, 0, 0, 100);
                        }
                        //Team Colours
                        #region Explanation
                        //If Red are Blue are equal, and Green is 0, it is a team colour pizel and so should
                        //be recoloured using the team colour. It multipies the team colour by the Blue value
                        //In order to shade the colour.
                        #endregion
                        else if (pixelRGBValues[Array2DTo1D].B == pixelRGBValues[Array2DTo1D].R && pixelRGBValues[Array2DTo1D].G == 0 &&
                            pixelRGBValues[Array2DTo1D].B != 0)
                        {
                            Color colour = new Color
                                (
                                teamColour.R / 8,
                                teamColour.G / 8,
                                teamColour.B / 8
                                );

                            pixelRGBValues[Array2DTo1D] = (colour * (pixelRGBValues[Array2DTo1D].R / 13));
                        }
                    }
                }
                //Sets k.Value to the array.
                newTextures[k.Key].SetData(pixelRGBValues);
            }
            return newTextures;
        }
    }
}
 