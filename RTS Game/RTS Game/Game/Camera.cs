using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace RTS_Game
{
    class Camera
    {
        //The cameras matrix
        private Matrix matrix = new Matrix();
        //The position the camera is focused on(center of screen)
        private Vector2 position = new Vector2(0,0);
        //Viewport which represents the bounds of the screen
        private Viewport viewport = new Viewport(0,0,GameClass.Game_Width, GameClass.Game_Height);
        //Zoom variable represents how far zoomed in the camera is
        private float zoom = 1f;


        #region Properties
        public Matrix CameraMatrix
        {
            get { return matrix; }
        }
        public Vector2 Position
        {
            get { return position; }
            set 
            { 
                position = value;
                //Limiter logic to stop the camera going outside the bounds of the tilemap

                if (position.X < viewport.Width / 2)
                {
                    position.X = viewport.Width / 2;
                }
            }
        }
        public float Zoom
        {
            get { return zoom; }
            set 
            { 
                zoom = value;
                if (zoom < 0.5f)
                {
                    //A limiter to stop zoom going too low
                    zoom = 0.5f;
                }
            }
        }
        #endregion


        public Camera()
        {
            Position = new Vector2(0,0);
        }

        public Camera(Vector2 startPosition)
        {
            Position = startPosition;
        }

        //This is where we change the camera depending on our inputs
        public void Update(KeyboardState keyboard, MouseState mouse)
        {
            //camera movement logic
            if (keyboard.IsKeyDown(Keys.Left))
            {
                position.X--;
            }
            if (keyboard.IsKeyDown(Keys.Right))
            {
                position.X++;
            }
            if (keyboard.IsKeyDown(Keys.Up))
            {
                position.Y--;
            }
            if (keyboard.IsKeyDown(Keys.Down))
            {
                position.Y++;
            }

            //Debug code
            if (keyboard.IsKeyDown(Keys.Space))
            {
                Console.WriteLine(Position.ToString());
            }

            //Set the matrix
            matrix = Matrix.CreateScale(new Vector3(Zoom, Zoom, 0)) * 
                     Matrix.CreateTranslation(new Vector3(-Position.X, -Position.Y, 0)) * 
                     Matrix.CreateTranslation(new Vector3(viewport.Width / 2, viewport.Height / 2, 0));
            
            
            //Giving Credit
            //I understand some of this, but most came from this tutorial:
            //http://www.youtube.com/watch?v=c_SPRT7DAeM
        }
    }
}
