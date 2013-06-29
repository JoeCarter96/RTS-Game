using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RTS_Game
{
    public static class Resources
    {   
        private static Dictionary<String, Texture2D> BackgroundTextures = new Dictionary<string,Texture2D>();
        private static Dictionary<String, Texture2D> LevelImages = new Dictionary<string, Texture2D>();
        private static Dictionary<String, Texture2D> UnitTextures = new Dictionary<string, Texture2D>();
        private static Dictionary<String, Texture2D> BuildingTextures = new Dictionary<string, Texture2D>();
        private static Dictionary<String, Texture2D> GUITextures = new Dictionary<string, Texture2D>();

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

        #region Level Images
        public static void AddLevelImage(Texture2D imageToAdd)
        {
            LevelImages.Add(imageToAdd.Name, imageToAdd);
        }

        public static Texture2D GetLevelImage(String requestedImageName)
        {
            return BackgroundTextures[requestedImageName];
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

        #region Unit Textures
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
    }
}
