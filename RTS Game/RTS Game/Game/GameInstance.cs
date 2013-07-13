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

            #endregion
        }

        #region Function Explanation
        //Executes the MouseClicked() method. This handles a LOT of code.
        //First of all it sees if there are any entities within the selected region.
        //if there are, they are selected. If there are not, it moves any movable 
        //Entities (Units) to that location. Expect a lot more to follow.
        #endregion
        public virtual void MouseClicked(int x, int y, MouseButton button)
        {
            //Finds the position of the mouse within the world, not within viewport.
            Vector2 relativePosition = input.relativeXY(new Vector2(x, y), camera);
            Vector2 mouseTile = new Vector2((float)Math.Floor((double)relativePosition.X / GameClass.Tile_Width),
                (float)(Math.Floor((double)relativePosition.Y / GameClass.Tile_Width)));


            //Searching for Entity to selected units.
            player.PlayerSelectedEntities.Add(player.Entities.Find(delegate(Entity entity)
            {
                //Returns whatever unit whos bounding box contains
                //mouse, or null if there is not one which does.
                return entity.BoundingBox.Contains(new Point((int)relativePosition.X,
                    (int)relativePosition.Y));

                // Old Method, assuming one above is better?
                //Returns whatever unit is at mouse, or null if there is not one.
                // return entity.TilePosition == mouseTile; 
                  
            }));

            //If null was returned, remove it and move any selected units to the clicked location.
            if (player.PlayerSelectedEntities.Last() == null)
            {
                player.PlayerSelectedEntities.Remove(null);
                foreach (Unit u in player.PlayerSelectedEntities.ToList<Entity>())
                {
                    //Deselect unit. TEMP?
                    player.PlayerSelectedEntities.Remove(u);

                    //Generate Waypoints and move!.
                    u.Waypoints = WaypointsGenerator.GenerateWaypoints(u.TilePosition, mouseTile);
                    if (!player.PlayerMovingEntities.Contains(u))
                    {
                        player.PlayerMovingEntities.Add(u);
                    }
                    
                }
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

            foreach (Entity e in player.PlayerSelectedEntities)
            {
                spriteBatch.Draw(Resources.GetGUITextures("SelectedRectangle"), e.BoundingBox, Color.White);
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