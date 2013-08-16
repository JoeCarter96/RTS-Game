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
    public class GameInstance
    {
        #region Variables
        private Camera camera;
        private TileMap world;
        private Player localPlayer;
        private Input input;

        int cycler = 0;     //Used to cycle through anything via keypress, ie CY'S, units etc.

        private List<Player> players = new List<Player>();
        private List<AI> Ais = new List<AI>();       

        private Ore[,] oreArray;   //TEMP so find nearest ore works. This will be passed to all Unit as they are made.

        public Ore[,] OreArray
        {
            get { return oreArray; }
        }

        #endregion

        public List<Player> Players
        {
            get { return players; }
            set { players = value; }
        }

        public List<AI> AIs
        {
            get { return Ais; }
            set { Ais = value; }
        }

        public TileMap World
        {
            get { return world; }
        }

        #region Function Explanation
        //Constructor.
        #endregion
        public GameInstance(Level level, Camera camera, Input input)
        {
            //Store the camera in a local variable so we can get its pixelPosition later
            this.camera = camera;

            //Build the tilemap using the level
            world = new TileMap(level, GameClass.Game_Width, GameClass.Game_Height, GameClass.Tile_Width);

            //TEMP Player Testing.
            Player player1 = new Player(this, new Color(42, 100, 52));
            localPlayer = player1;

            //TEMP AI Test.
            AI AITEST = new AI(this);
            

            this.input = input;
            input.MouseClicked += MouseClicked;
            input.KeyPress += KeyPress;
            
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

            new ConstructionYard(world, localPlayer, new Vector2(2, 2));
            new ConstructionYard(world, localPlayer, new Vector2(49, 2));
            new PowerPlant(world, localPlayer, new Vector2(6, 3));
            new PowerPlant(world, localPlayer, new Vector2(8, 3));
            new Refinery(world, localPlayer, new Vector2(6, 7));
            new Refinery(world, localPlayer, new Vector2(6, 30));
            new Harvester(world, localPlayer, new Vector2(9, 9), localPlayer.PlayerBuildings, oreArray);

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
                Vector2 relativePosition = camera.relativeXY(new Vector2(x, y));
                Vector2 mouseTile = new Vector2((float)Math.Round((double)relativePosition.X / GameClass.Tile_Width),
                    (float)Math.Round(((double)relativePosition.Y / GameClass.Tile_Width)));


                //Adds any entities whose bounding boxes contain the mouse to selected entities.
                localPlayer.PlayerSelectedEntities.Add(localPlayer.PlayerUnits.Find(delegate(Entity entity)
                {
                    //Returns whatever unit which their bounding box contains
                    //mouse, or null if there is not one which does.
                    return entity.BoundingBox.Contains(new Point((int)relativePosition.X,
                        (int)relativePosition.Y));
                }));

                //If null was returned it's an empty location to move any selected units to
                //Therefore, remove it from selected list and move any selected units to the clicked location.
                if (localPlayer.PlayerSelectedEntities.Last() == null)
                {
                    localPlayer.PlayerSelectedEntities.Remove(null);

                    #region Deselecting Buildings as they cannot move.
                    foreach (Building b in localPlayer.PlayerSelectedEntities.OfType<Building>().ToList<Building>())
                    {
                        localPlayer.PlayerSelectedEntities.Remove(b);
                    }
                    #endregion

                    #region Moving Entities which can.
                    foreach (Unit u in localPlayer.PlayerSelectedEntities.OfType<Unit>().ToList<Unit>())
                    {
                        //Generate Waypoints and move!.
                        u.Waypoints = WaypointsGenerator.GenerateWaypoints(u.TilePosition, mouseTile);
                        if (!localPlayer.PlayerMovingEntities.Contains(u))
                        {
                            localPlayer.PlayerMovingEntities.Add(u);
                            u.NextTile = u.Waypoints.Dequeue();
                        }
                    }
                    #endregion
                }
            }
            #endregion

            #region Right Mouse Click
            else if (button == MouseButton.Right)
            {
                localPlayer.PlayerSelectedEntities.Clear();

            }
            #endregion

            #region Middle Mouse Click
            else if (button == MouseButton.Middle)
            {
            }
            #endregion
        }
        
        #region Function Explanation
        #endregion
        public virtual void MouseMoved(int x, int y)
        {
        }

        #region Function Explanation
        #endregion
        public virtual void KeyPress(Keys[] keys)
        {
            if (keys.Length == 1)
            {
                #region Heavytank
                if (keys[0] == Keys.D1)
                {
                    if (localPlayer.Money >= 2500)
                    {
                        #region Calculating Mouse tile.
                        //Finds the position of the mouse within the world, not within viewport.
                        Vector2 relativePosition = camera.relativeXY(new Vector2(input.X, input.Y));
                        Vector2 mouseTile = new Vector2((float)Math.Round((double)relativePosition.X / GameClass.Tile_Width),
                            (float)Math.Round(((double)relativePosition.Y / GameClass.Tile_Width)));
                        #endregion

                        if (!world.TileArray[(int)mouseTile.X, (int)mouseTile.Y].Obstacle &&
                            !world.TileArray[(int)mouseTile.X, (int)mouseTile.Y].OccupiedByUnit)
                        {
                            new HeavyTank(world, localPlayer, mouseTile);
                            localPlayer.Money -= 2500;
                        }
                    }
                }
                #endregion

                #region Harvester
                if (keys[0] == Keys.D2)
                {
                    if (localPlayer.Money >= 2000)
                    {
                        #region Calculating Mouse tile.
                        //Finds the position of the mouse within the world, not within viewport.
                        Vector2 relativePosition = camera.relativeXY(new Vector2(input.X, input.Y));
                        Vector2 mouseTile = new Vector2((float)Math.Round((double)relativePosition.X / GameClass.Tile_Width),
                            (float)Math.Round(((double)relativePosition.Y / GameClass.Tile_Width)));
                        #endregion

                        if (!world.TileArray[(int)mouseTile.X, (int)mouseTile.Y].Obstacle &&
                            !world.TileArray[(int)mouseTile.X, (int)mouseTile.Y].OccupiedByUnit)
                        {
                            new Harvester(world, localPlayer, mouseTile, localPlayer.PlayerBuildings, oreArray);
                            localPlayer.Money -= 2000;
                        }
                    }
                }
                #endregion

                #region Ref
                if (keys[0] == Keys.D3)
                {
                    if (localPlayer.Money >= 3000)
                    {
                        #region Calculating Mouse tile.
                        //Finds the position of the mouse within the world, not within viewport.
                        Vector2 relativePosition = camera.relativeXY(new Vector2(input.X, input.Y));
                        Vector2 mouseTile = new Vector2((float)Math.Round((double)relativePosition.X / GameClass.Tile_Width),
                            (float)Math.Round(((double)relativePosition.Y / GameClass.Tile_Width)));
                        #endregion

                        if (!world.TileArray[(int)mouseTile.X, (int)mouseTile.Y].Obstacle &&
                            !world.TileArray[(int)mouseTile.X, (int)mouseTile.Y].OccupiedByUnit)
                        {
                            new Refinery(world, localPlayer, mouseTile);
                            localPlayer.Money -= 3000;
                        }
                    }
                }
                #endregion

                #region TEMP: Debug for players.
                if (keys[0] == Keys.M)
                {
                    localPlayer.Money += 10000;
                }
                #endregion

                #region Cycling through CY's
                // Cycles through all CY's until it finds the next one in the cycle.
                if (keys[0] == Keys.Back)
                {
                    int currentCY = 0;
                    int CYNumber = localPlayer.PlayerBuildings.OfType<ConstructionYard>().Count() - 1;

                    foreach (Building B in localPlayer.PlayerBuildings.OfType<ConstructionYard>())
                    {
                        //Go back to start if we go over the number of CY's.
                        if (cycler > CYNumber)
                        {
                            cycler = 0;
                        }

                        if (currentCY == cycler)
                        {
                            camera.CenterCameraOn(B.PixelPosition);
                            cycler++;
                            break;
                        }

                        currentCY++;      
                    }
                }
                #endregion

                #region Cycling through Units
                // Cycles through all CY's until it finds the next one in the cycle.
                if (keys[0] == Keys.U)
                {
                    int currentUnit = 0;
                    int UnitNumber = localPlayer.PlayerUnits.Count() - 1;

                    foreach (Unit u in localPlayer.PlayerUnits)
                    {
                        //Go back to start if we go over the number of CY's.
                        if (cycler > UnitNumber)
                        {
                            cycler = 0;
                        }

                        if (currentUnit == cycler)
                        {
                            camera.CenterCameraOn(u.PixelPosition);
                            cycler++;
                            break;
                        }

                        currentUnit++;
                    }
                }
                #endregion
                
            }
        }

        #region Function Explanation
        //Handles a cubic fucktonne of game logic.
        //Includes Moving and Updating Units, Updating Input.
        #endregion
        public void Update(GameTime gameTime, Camera camera, Input input)
        {
            //Update input.
            input.Update(gameTime);

            foreach (AI a in AIs)
            {
                a.Update(gameTime);
            }

            foreach (Player p in players)
            {
                if (p.PlayerUnits.Count > 0)
                {
                    #region Moving Units
                    //Moving every moving Unit which is meant to.
                    if (p.PlayerMovingEntities.Count > 0)
                    {
                        foreach (Unit u in p.PlayerMovingEntities.ToList())
                        {
                            u.Move();
                        }
                    }
                    #endregion

                    #region Updating Units
                    //Update every Entity.
                    foreach (Entity e in p.PlayerUnits)
                    {
                        e.Update(gameTime);
                    }

                    #endregion

                    #region TEMP for shits and giggles
                    foreach (Unit u in p.PlayerUnits)
                    {
                        if (u.Turret != null)
                        {
                            Vector2 target = camera.relativeXY(input.MousePos);

                            Vector2 angle = new Vector2(target.X - u.Turret.PixelPosition.X,
                                target.Y - u.Turret.PixelPosition.Y);

                            u.Turret.Rotation = u.toAngle(angle);
                        }
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
            world.Draw(spriteBatch, camera);

            foreach (Ore o in oreArray)
            {
                if (o.CurrentAmount > 0)
                {
                    o.Draw(spriteBatch);
                }
            }

            foreach (Player p in players)
            {
                foreach (Entity e in p.PlayerBuildings)
                {
                    e.Draw(spriteBatch);
                }

                foreach (Entity e in p.PlayerUnits)
                {
                    e.Draw(spriteBatch);
                }

                foreach (Entity e in p.PlayerSelectedEntities)
                {
                    spriteBatch.Draw(Resources.GetGUITextures("SelectedRectangle"), e.BoundingBox, null, Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, 0);
                }
            }

            //DEBUG.
            foreach (Tile t in world.TileArray)
            {
                if (t.Obstacle)
                {
                 //   spriteBatch.Draw(Resources.GetGUITextures("TileOverlay"), t.BoundingBox, null, new Color(255, 0, 0, 10), 0f, new Vector2(0, 0), SpriteEffects.None, 0);
                }
            }

        }

        #region Function Explanation
        //Draw method that is not effected by the camera, used for UIs.
        //Intended to be drawn on top of the tile map and be unaffected by movement of the camera
        #endregion
        public void StaticDraw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Resources.TestFont, camera.Position.ToString(), new Vector2(0 ,0), Color.Black);
            spriteBatch.DrawString(Resources.TestFont, "$" + localPlayer.Money.ToString(), new Vector2(0, 30), Color.Black);
        }
    }
}

/*        #region Function Explanation


*/


/*
 * //parsing units in the entity list.
            foreach (Unit u in localPlayer.Entities.OfType<Unit>())
            {
                u.Draw(spriteBatch);
            }

*/