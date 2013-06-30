using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace RTS_Game
{
    #region Class Info
    /*Name: GameInstance.cs
     * Represents a play instance of the game. Like a skirmish session.
     * We can everntually pass game settings into the constructor to change
     * how the game instance will play out. 
     */
    #endregion
    class GameInstance
    {
        private Camera camera;
        private TileMap world;

        public GameInstance(Level level, Camera camera)
        {
            //Store the camera in a local variable so we can get its position later
            this.camera = camera;

            //Build the tilemap using the level
            world = new TileMap(level, 800, 600, 80);
        }

        public void Update(GameTime gameTime, Camera camera, KeyboardState keyboard, MouseState mouse)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            world.Draw(spriteBatch);
        }
    }
}
