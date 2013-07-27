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

        Ore[,] oreArray;   //TEMP so find nearest ore works. This will be passed to all harvesters as they are made.
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
            oreArray = new Ore[world.TileArray.GetLength(0), world.TileArray.GetLength(1)];

            for (int i = 0; i < oreArray.GetLength(0); i++)
            {
                for (int j = 0; j < oreArray.GetLength(1); j++)
                {
                    oreArray[i, j] = new Ore(new Vector2(i, j));
                } 
            }

            for (int i = 10; i < 30; i++)
            {
                for (int j = 10; j < 110; j++)
                {
                    oreArray[i, j].CurrentAmount = 100;
                }
            }

            Unit HT1 = new HeavyTank(new Vector2(2, 6), player, world);
            Unit HT2 = new HeavyTank(new Vector2(2, 7), player, world);
            Unit HT3 = new HeavyTank(new Vector2(3, 6), player, world);
            Unit HT4 = new HeavyTank(new Vector2(3, 7), player, world); ;

            ConstructionYard CY = new ConstructionYard(world, player, new Vector2(2, 2));
            PowerPlant PP1 = new PowerPlant(world, player, new Vector2(6, 3));
            PowerPlant PP2 = new PowerPlant(world, player, new Vector2(8, 3));
            Refinery REF = new Refinery(world, player, new Vector2(6, 6));
            Harvester H1 = new Harvester(world, player, new Vector2(12, 12), player.Entities, oreArray);



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
                }
                #endregion

                #region Updating Units
                    //Update every Entity.
                    foreach (Entity e in player.Entities)
                    {
                        e.Update(gameTime);
                    }
                    #endregion
            }   
        }

        #region Function Explanation
        //The Draw method which will be offset and scaled by the Camera.
        #endregion
        public void Draw(SpriteBatch spriteBatch)
        {
            world.Draw(spriteBatch);

            foreach (Ore o in oreArray)
            {
                if (o.CurrentAmount > 0)
                {
                    o.Draw(spriteBatch);
                }
            }

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
            spriteBatch.DrawString(Resources.TestFont, "$" + player.Money.ToString(), new Vector2(0, 30), Color.Black);
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