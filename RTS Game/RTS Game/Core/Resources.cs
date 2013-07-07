using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RTS_Game
{
    public static class Resources
    {   
        //DEBUG: Font used for testing
        public static SpriteFont TestFont;

        private static Dictionary<String, Texture2D> BackgroundTextures = new Dictionary<string,Texture2D>();
        private static Dictionary<String, Texture2D> UnitTextures = new Dictionary<string, Texture2D>();
        private static Dictionary<String, Texture2D> BuildingTextures = new Dictionary<string, Texture2D>();
        private static Dictionary<String, Texture2D> GUITextures = new Dictionary<string, Texture2D>();
        private static Dictionary<String, Texture2D> BulletTextures = new Dictionary<String, Texture2D>();
        private static Dictionary<int, Level> LevelObjects = new Dictionary<int, Level>();
        

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
    }
}
