using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RTS_Game
{
    public class TileMap
    {
        private Tile[,] TileArray;

        private int width;
        private int height;
        private int tilewidth;

        #region Function Explanation
        //Creates TileArray, complete with Textured Background.
        #endregion
        public TileMap(Level level, int Width, int Height, int TileWidth)
        {
            //Creates Array of textures.
            Texture2D[,] TextureArray = LevelLoad.Load(level.LevelImage);

            //Sets TileArray to level size.
            TileArray = new Tile[TextureArray.GetLength(0), TextureArray.GetLength(1)];

            //Used for loop
            int X = 0;
            int Y = 0;

            //Initialisation loop.
            for (int i = 0; i < TileArray.GetLength(1); i++)
            {
                for (int j = 0; j < TileArray.GetLength(0); j++)
                {
                    //Creates new tile.
                    TileArray[j, i] = new Tile(new Vector2(i * TileWidth, j * TileWidth), new Vector2(i, j), TextureArray[i, j]);
                    //Increases X for next tile.
                    X += TileWidth;
                }
                //Start a new row.
                Y += TileWidth;
                X = 0;
            }
        }

        //DEBUG: function to print all the positions of every tile
        public void PrintPositions()
        {
            Console.WriteLine("Begining Position Print:");
            Console.WriteLine("------------------------");

            foreach (Tile t in TileArray)
            {
                Console.WriteLine(t.Position);
            }
        }

        #region Function Explanation
        //Draws Each Tile.
        #endregion
        public void Draw(SpriteBatch spriteBatch)
        {
            //Is this the best way to do it?
            foreach (Tile t in TileArray)
                t.Draw(spriteBatch);

        }
    }
}
