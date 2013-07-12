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
        //The input opject
        private Input input;
        #endregion

        Unit test;  //TEMP.

        #region Function Explanation
        //Constructor.
        #endregion
        public GameInstance(Level level, Camera camera, Input input)
        {
            //Store the camera in a local variable so we can get its pixelPosition later
            this.camera = camera;

            //Build the tilemap using the level
            world = new TileMap(level, GameClass.Game_Width, GameClass.Game_Height, GameClass.Tile_Width);

            player = new Player(world);

            this.input = input;
            input.MouseClicked += MouseClicked;
            
            //we tell the camera the size of the tilemap so it can adjust its range
            camera.GiveTilemap(world);

            #region TEMP: Unit Testing.

            test = new HeavyTank(new Vector2(0, 0), player, world);
          //  test.Waypoints = WaypointsGenerator.GenerateWaypoints(test.TilePosition, new Vector2 (150, 30));
            player.PlayerMovingEntities.Add(test);

            Unit test2 = new HeavyTank(new Vector2(0, 1), player, world);
            test2.Waypoints = WaypointsGenerator.GenerateWaypoints(test2.TilePosition, new Vector2(150, 31));
            player.PlayerMovingEntities.Add(test2);

            Unit test3 = new HeavyTank(new Vector2(1, 0), player, world);
            test3.Waypoints = WaypointsGenerator.GenerateWaypoints(test3.TilePosition, new Vector2(151, 30));
            player.PlayerMovingEntities.Add(test3);

            Unit test4 = new HeavyTank(new Vector2(1, 1), player, world);
            test4.Waypoints = WaypointsGenerator.GenerateWaypoints(test4.TilePosition, new Vector2(151, 31));
            player.PlayerMovingEntities.Add(test4);

            //myBase = new Base(world, player, new Vector2(5, 5));  //TEMP
            #endregion
        }

        #region Function Explanation
        //Executes the MouseClicked() method of the first component which has contains set to true,
        //Which basically returns true if the mouse is contained within it.
        #endregion
        public virtual void MouseClicked(int x, int y, MouseButton button)
        {
            //Finds the position of the mouse within the world, not within viewport.
            Vector2 relativePosition = camera.Position + new Vector2(x, y);
            Vector2 mouseTile = new Vector2((float)Math.Round((double)relativePosition.X / GameClass.Tile_Width),
                (float)(Math.Round((double)relativePosition.Y / GameClass.Tile_Width)));

            test.Waypoints = WaypointsGenerator.GenerateWaypoints(test.TilePosition, mouseTile);
            if (!player.PlayerMovingEntities.Contains(test))
            {
                player.PlayerMovingEntities.Add(test);
            }
        }

        #region Function Explanation
        #endregion
        public virtual void MouseMoved(int x, int y)
        {
        }

        #region Function Explanation
        //Handles a cubic fucktonne of game logic.
        //Includes Moving and Updating Units, Updating Input.
        #endregion
        public void Update(GameTime gameTime, Camera camera, Input input)
        {
            //Update input.
            input.Update(gameTime);

            
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



/*        #region Function Explanation


*/