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
        #region Variables
        private Camera camera;
        private TileMap world;
        private Player player;
        #endregion

        private Unit test;  //TEMP.
        

        #region Function Explanation
        //Constructor.
        #endregion
        public GameInstance(Level level, Camera camera)
        {
            //Store the camera in a local variable so we can get its pixelPosition later
            this.camera = camera;

            //Build the tilemap using the level
            world = new TileMap(level, GameClass.Game_Width, GameClass.Game_Height, GameClass.Tile_Width);

            player = new Player(world);

            //we tell the camera the size of the tilemap so it can adjust its range
            camera.GiveTilemap(world);

            #region TEMP: Unit Testing.

            test = new HeavyTank(new Vector2(0, 0), player, world);
            test.Waypoints = WaypointsGenerator.GenerateWaypoints(test.TilePosition, new Vector2 (12, 22));
            player.PlayerMovingEntities.Add(test);

            Unit test2 = new HeavyTank(new Vector2(24, 0), player, world);
            test2.Waypoints = WaypointsGenerator.GenerateWaypoints(test2.TilePosition, new Vector2(150, 22));
            player.PlayerMovingEntities.Add(test2);

            //myBase = new Base(world, player, new Vector2(5, 5));  //TEMP
            #endregion
        }

        #region Function Explanation
        //Handles a cubic fucktonne of game logic.
        //Includes Moving and Updating Units, Updating Input.
        #endregion
        public void Update(GameTime gameTime, Camera camera, Input input)
        {

            if (player.Entities.Count > 0)
            {
                #region Moving Units
                //Moving every moving unit which is meant to.
                if (player.PlayerMovingEntities.Count > 0)
                {
                    foreach (Unit u in player.PlayerMovingEntities.ToList())
                    {
                        u.Move();
                    }

                #endregion

                #region Updating Units
                    //Update every unit.
                    foreach (Unit u in player.Entities)
                    {
                        u.Update(gameTime);
                    }
                    #endregion
                }
            }

            //Update input.
            input.Update(gameTime);
        }

        #region Function Explanation
        //The Draw method which will be offset and scaled by the Camera.
        #endregion
        public void Draw(SpriteBatch spriteBatch)
        {
            world.Draw(spriteBatch);
            foreach (Unit u in player.Entities)
            {
                u.Draw(spriteBatch);
            }

            //myBase.Draw(spriteBatch);
        }

        #region Function Explanation
        //Draw method that is not effected by the camera, used for UIs.
        //Intended to be drawn on top of the tile map and be unaffected by movement of the camera
        #endregion
        public void StaticDraw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Resources.TestFont, camera.Position.ToString(), new Vector2(0 ,0), Color.Black);
        }
    }
}
