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
        private Input input;
        #endregion

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
            Unit test = new HeavyTank(new Vector2(5, 6), player, world);
            Unit test2 = new HeavyTank(new Vector2(5, 7), player, world);
            Unit test3 = new HeavyTank(new Vector2(6, 6), player, world);
            Unit test4 = new HeavyTank(new Vector2(6, 7), player, world);

            Base myBase = new Base(world, player, new Vector2(24, 5));
            #endregion
        }

        #region Function Explanation
        /*Executes the MouseClicked() method.
         * Left Click:
                First of all it sees if there are any entities within the selected region.
                if there are, they are selected. If there are not, it moves any movable 
                Entities (Units) to that location. 
         * Right Click:
                Deselects all units.
         */
        #endregion
        public virtual void MouseClicked(int x, int y, MouseButton button)
        {
            #region Left Mouse Click
            if (button == MouseButton.Left)
            {
                //Finds the position of the mouse within the world, not within viewport.
                Vector2 relativePosition = input.relativeXY(new Vector2(x, y), camera);
                Vector2 mouseTile = new Vector2((float)Math.Floor((double)relativePosition.X / GameClass.Tile_Width),
                    (float)(Math.Floor((double)relativePosition.Y / GameClass.Tile_Width)));


                //FindClosestOre for Entity to selected units.
                player.PlayerSelectedEntities.Add(player.Entities.Find(delegate(Entity entity)
                {
                    //Returns whatever harvester whos bounding box contains
                    //mouse, or null if there is not one which does.
                    return entity.BoundingBox.Contains(new Point((int)relativePosition.X,
                        (int)relativePosition.Y));

                    // Old Method, assuming one above is better?
                    //Returns whatever harvester is at mouse, or null if there is not one.
                    // return entity.TilePosition == mouseTile; 

                }));

                //If null was returned it's an empty location to move any selected units to
                //Therefore, remove it from selected list and move any selected units to the clicked location.
                if (player.PlayerSelectedEntities.Last() == null)
                {
                    player.PlayerSelectedEntities.Remove(null);

                    #region Deselecting Buildings as they cannot move.
                    foreach (Building b in player.PlayerSelectedEntities.OfType<Building>().ToList<Building>())
                    {
                        player.PlayerSelectedEntities.Remove(b);
                    }
                    #endregion

                    #region Moving Entities which can.
                    foreach (Unit u in player.PlayerSelectedEntities.OfType<Unit>().ToList<Unit>())
                    {
                        //Generate Waypoints and move!.
                        u.Waypoints = WaypointsGenerator.GenerateWaypoints(u.TilePosition, mouseTile);
                        if (!player.PlayerMovingEntities.Contains(u))
                        {
                            player.PlayerMovingEntities.Add(u);
                            u.NextTarget = u.Waypoints.Dequeue();
                        }
                    }
                    #endregion
                }
            }
            #endregion

            #region Right Mouse Click
            else if (button == MouseButton.Right)
            {
                player.PlayerSelectedEntities.Clear();

            }
            #endregion
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
                //Moving every moving harvester which is meant to.
                if (player.PlayerMovingEntities.Count > 0)
                {
                    foreach (Unit u in player.PlayerMovingEntities.ToList())
                    {
                        u.Move();
                    }

                #endregion

                #region Updating Units
                    //Update every harvester.
                    foreach (Entity e in player.Entities)
                    {
                        e.Update(gameTime);
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

            foreach (Entity e in player.Entities)
            {
                e.Draw(spriteBatch);
            }

            foreach (Entity e in player.PlayerSelectedEntities)
            {
                spriteBatch.Draw(Resources.GetGUITextures("SelectedRectangle"), e.BoundingBox, Color.White);
            }
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


/*
 * //parsing units in the entity list.
            foreach (Unit u in player.Entities.OfType<Unit>())
            {
                u.Draw(spriteBatch);
            }

*/