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

        public static Texture2D GetUnitTextures(String requestedTextureName)
        {
            return UnitTextures[requestedTextureName];
        }
        #endregion

        #region Building Textures
        public static void AddBuildingTexture(Texture2D textureToAdd)
        {
            BuildingTextures.Add(textureToAdd.Name, textureToAdd);
        }

        public static Texture2D GetBuildingTextures(String requestedTextureName)
        {
            return BuildingTextures[requestedTextureName];
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


        public static List<Texture2D> setTeamColours(List<Texture2D> textures, Color teamColour)
        {
            //For each initialTexture in initialTexture list..
            foreach (Texture2D t in textures)
            {
                //Array of Colours of each pixel. 
                //Goes from top left to bottom right.
                Color[] pixelRGBValues = new Color[t.Width * t.Height];
                //Puts colour of each Pixel into Array.
                t.GetData(pixelRGBValues);

                for (int i = 0; i < t.Width; i++)
                {
                    for (int j = 0; j < t.Height; j++)
                    {
                        //Array2DTo1D converts from 2D to 1D by adding up all the full rows
                        // and then adding the remaining amount on the current row.
                        int Array2DTo1D = t.Width * i + j;

                        //If Red are Blue are equal, and Green is 0, it is a team colour pizel and so should
                        //be recoloured using the team colour. It multipies the team colour by the Blue value
                        //In order to shade the colour.
                        if (pixelRGBValues[Array2DTo1D].B == pixelRGBValues[Array2DTo1D].R && pixelRGBValues[Array2DTo1D].G == 0 &&
                            pixelRGBValues[Array2DTo1D].B != 0)
                        {

                            Color colour = new Color
                                (
                                teamColour.R / 2,
                                teamColour.G / 2,
                                teamColour.B / 2
                                );


                            pixelRGBValues[Array2DTo1D] = (colour * (pixelRGBValues[Array2DTo1D].R / 50));
                        }
                    }
                }
                //Sets t to the array.
                t.SetData(pixelRGBValues);
            }
            return textures;
        }


    }
}
 