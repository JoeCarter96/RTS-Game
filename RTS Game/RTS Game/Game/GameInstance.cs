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
        private Game.Player player;

        public GameInstance(Level level, Camera camera)
        {
            //Store the camera in a local variable so we can get its pixelPosition later
            this.camera = camera;

            //Build the tilemap using the level
            world = new TileMap(level, 800, 600, 80);

            player = new Game.Player(world);

            //we tell the camera the size of the tilemap so it can adjust its range
            camera.GiveTilemap(world);

            Unit test = new Unit(world, player, new Vector2(10, 10), Resources.GetBackgroundTextures("DebugTile"), 100);
            test.FinalTarget = new Vector2(20, 30);
            player.MovingUnits.Add(test);
        }

        public void Update(GameTime gameTime, Camera camera, KeyboardState keyboard, MouseState mouse)
        {
            //Moving every moving unit.
            foreach (Unit u in player.MovingUnits)
            {
                u.Move();
            }
        }

        //The drawmethod that will be offset and scaled by the camera
        public void Draw(SpriteBatch spriteBatch)
        {
            world.Draw(spriteBatch);
            foreach (Unit u in player.Units)
            {
                u.Draw(spriteBatch);
            }
        }

        //Draw method that is not effected by the camera, used for UIs
        //These will bw draw on top of the tile map and be unaffected by movement of the camera

        public void StaticDraw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Resources.TestFont, camera.Position.ToString(), new Vector2(0 ,0), Color.Black);
        }
    }
}
