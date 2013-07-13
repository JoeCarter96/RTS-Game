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
        #region Variables
        private Tile[,] tileArray;
        private int TILE_WIDTH;
        #endregion

        public Tile[,] TileArray
        {
            get { return tileArray; }
        }

        public int TileWidth
        {
            get { return TILE_WIDTH; }
            set { TILE_WIDTH = value; }
        }
        
        //NOTE: width and height may be the wrong way around.
        public int Width
        {
            get { return tileArray.GetLength(0); }
        }

        public int Height
        {
            get { return tileArray.GetLength(1); }
        }
         
        #region Function Explanation
        //Creates tileArray, complete with Textured Background.
        #endregion
        public TileMap(Level level, int Width, int Height, int TileWidth)
        {
            //Creates Array of textures.
            Texture2D[,] TextureArray = LevelLoad.Load(level.LevelImage);

            TILE_WIDTH = TileWidth;

            //Sets tileArray to level size.
            tileArray = new Tile[TextureArray.GetLength(0), TextureArray.GetLength(1)];

            //Used for loop
            int X = 0;
            int Y = 0;

            //Initialisation loop.
            for (int i = 0; i < tileArray.GetLength(0); i++)
            {
                for (int j = 0; j < tileArray.GetLength(1); j++)
                {
                    //Creates new tile.
                    tileArray[i, j] = new Tile(new Vector2(i, j), new Vector2(i, j), TextureArray[i, j]);
                    //Increases X for next tile.
                    X += TileWidth;
                }
                //Start a new row.
                Y += TileWidth;
                X = 0;
            }

            //Passes reference of tileArray.
            WaypointsGenerator.setup(tileArray);
        }

        #region Function Explanation
        //Returns a Tile when fed Coordinates.
        #endregion
        public Tile GetTile(int x, int y)
        {
            if (x > Width || y > Height)
            {
                return null;
            }
            else
            {
                return tileArray[x, y];
            }
        }

        #region Function Explanation
        //Draws Each Tile.
        #endregion
        public void Draw(SpriteBatch spriteBatch)
        {
            //Is this the best way to do it?
            foreach (Tile t in tileArray)
                t.Draw(spriteBatch);
        }
    }
}
