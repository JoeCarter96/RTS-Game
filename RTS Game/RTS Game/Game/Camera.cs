using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace RTS_Game
{
    public class Camera
    {
        #region Variables
        //Camera constants
        private const float CameraSpeed = 6F;
        private const float MinZoom = 0.5F;
        private const float MaxZoom = 2F;
        private const float ZoomSpeed = 0.05F;

        //The cameras matrix that will be used in spriteBatch.Begin()
        private Matrix matrix = new Matrix();

        //The pixelPosition of the top left corner of the screen relative to the camera
        private Vector2 position = new Vector2(0,0);

        //Zoom variable represents how far zoomed in the camera is
        private float zoom = 1f;

        //TODO: add support for full screen game
        private Viewport viewport = new Viewport(new Rectangle(0, 0, GameClass.Game_Width, GameClass.Game_Height));

        //The value of the mouses scroll from the last frame.
        //Used to find if the mouse has beens crolled since the last frame.
        private int ScrollValueLastFrame = 0;

        //We initially assume a 24 x 24 tilemap
        //We have methods that re-workout these values
        private int WorldWidth = 2400;
        private int WorldHeight = 2400;

        //allows us to disable the camera when the map is too small
        private bool enabled = true;
        #endregion

        public Matrix CameraMatrix
        {
            get { return matrix; }
        }

        public Vector2 Position
        {
            get { return position; }
            set { position = ClampCamera(value); }
        }

        public float Zoom
        {
            get { return zoom; }
            set { zoom = ClampZoom(value); }
        }

        public Viewport Viewport
        {
            get { return viewport; }
            set { viewport = value; }
        }

        public Camera()
        {
            Position = new Vector2(0, 0);

            if (WorldWidth < viewport.Width || WorldHeight < viewport.Height)
            {
                enabled = false;
            }
            else
            {
                enabled = true;
            }
        }

        #region Function Explanation
        //This allows the Camera to know the width and height of the map in pixels.
        #endregion
        public void GiveTilemap(TileMap tilemap)
        {
            WorldHeight = tilemap.Height * GameClass.Tile_Width;
            WorldWidth = tilemap.Width * GameClass.Tile_Width;
        }

        #region Function Explanation
        //Returns a vector2 that is within the Game field.
        #endregion
        private Vector2 ClampCamera(Vector2 value)
        {
            //Unfinished
            if (value.X < 0) { value.X = 0; }
            if (value.X > (WorldWidth * Zoom) - GameClass.Game_Width) { value.X = (WorldWidth * Zoom) - GameClass.Game_Width; }

            if (value.Y < 0) { value.Y = 0; }
            if (value.Y > (WorldHeight * zoom) - GameClass.Game_Height) { value.Y = (WorldHeight * zoom) - GameClass.Game_Height; }
            return value;
        }

        #region Function Explanation
        //Returns a float that is within our zoom limits.
        #endregion    
        public float ClampZoom(float value)
        {
            if (value < MinZoom) { value = MinZoom; }
            if (value > MaxZoom) { value = MaxZoom; }

            return value;
        }

        #region Function Explanation
        //Offsets the target pixelPosition by half of the screen so we can center on it.
        #endregion
        public void   CenterCameraOn(Vector2 targetPixel)
        {
            Position = targetPixel - new Vector2(GameClass.Game_Width / 2, GameClass.Game_Height / 2);
        }

        #region Function Explanation
        //Changing the camera depending on our inputs.
        #endregion   
        public void Update(Input input)
        {
            if (enabled)
            {
                #region Camera movement logic
                Vector2 movementVector = new Vector2(0, 0);
                //camera movement logic
                if (input.IsKeyDown(Keys.Left) || input.X < (GameClass.Game_Width / 100))
                {
                    movementVector.X--;
                }
                if (input.IsKeyDown(Keys.Right) || input.X > GameClass.Game_Width - (GameClass.Game_Width / 100))
                {
                    movementVector.X++;
                }
                if (input.IsKeyDown(Keys.Up) || input.Y < (GameClass.Game_Height / 100))
                {
                    movementVector.Y--;
                }
                if (input.IsKeyDown(Keys.Down) || input.Y > GameClass.Game_Height - (GameClass.Game_Height / 100))
                {
                    movementVector.Y++;
                }

                if (movementVector != Vector2.Zero)
                {
                    movementVector.Normalize();
                    Position += (movementVector * CameraSpeed);
                }
                #endregion

                #region Camera zoom logic - WIP
                int scrollValue = input.ScrollWheelValue;

                //the difference in the scroll values between this frame ans last frame
                int deltaScroll = scrollValue - ScrollValueLastFrame;

                //if deltaScroll == 0 there has been no change
                if (deltaScroll != 0)
                {
                    Vector2 PositionInitial = position;

                    if (deltaScroll > 0)
                    {
                        //Scroll up
                        Zoom += ZoomSpeed;
                    }
                    else if (deltaScroll < 0)
                    {
                        //Scroll Down
                        Zoom -= ZoomSpeed;
                    }

                    //position += (new Vector2(viewport.Width / 2, viewport.Height / 2));
                }

                ScrollValueLastFrame = scrollValue;
                #endregion
                  
                //Update the matrix
                matrix =
                    Matrix.CreateScale(new Vector3(Zoom, Zoom, 1)) *
                    Matrix.CreateTranslation(new Vector3(-Position.X, -Position.Y, 0));
            }
        }


        #region Function Explanation
        //A method which finds the mouse position within the
        //entire game, not just within the viewport. I made it called externally
        //so that this class can still be used with ease for menus,
        //which do not have a camera. This however means you must
        //remember to call this method on the XY variables before they
        //are used. We can easily do ingame menus now as we just don't use
        //this method, and use actual XY.
        #endregion
        public Vector2 relativeXY(Vector2 XY)
        {
            return (Position + XY) / Zoom;
        }
    }
}
