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

        public TileMap()
        {

        }

        //Initialises Array, after Content load as textures are needed.
        public void AfterContentLoad(int Width, int Height, int TileWidth)
        {
            //Amount of tiles.
            TileArray = new Tile[(Width / TileWidth), (Height / TileWidth)];

            //Used for loop
            int X = 0;
            int Y = 0;

            //Initialisation loop.
            for (int i = 0; i < TileArray.GetLength(1); i++)
            {
                for (int j = 0; j < TileArray.GetLength(0); j++)
                {
                    //Creates new tile.
                    TileArray[j, i] = new Tile(new Vector2(i * TileWidth, j * TileWidth), new Vector2(i, j), null); 
                    //Increases X for next tile.
                    X += TileWidth;
                }
                //Start a new row.
                Y += TileWidth;
                X = 0;
            }

            this.width = Width;
            this.height = Height;
            this.tilewidth = TileWidth;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //Is this the best way to do it?
            foreach (Tile t in TileArray)
                t.Draw(spriteBatch);

        }
    }
}
