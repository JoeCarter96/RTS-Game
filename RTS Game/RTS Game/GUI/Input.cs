using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace RTS_Game
{
    public class Input
    {
        //Variables
        private KeyboardState keyboardState;
        private MouseState mouseState;
        private static int x, y, dy, dx = 0;
        private static bool left, right, middle = false;
        private static bool leftLastFrame, rightLastFrame, middleLastFrame = false;

        //holds the x and y of the last frame
        private static int yLastFrame, xLastFrame = 0;

        //Mouse clicked event variables
        //Stores the mouse button that was clicked last on the MouseDown event
        private MouseButton LastMouseDown = MouseButton.None;

        //Properties
        public int X
        {
            get { return x; }
        }

        public int Y
        {
            get { return y; }
        }

        public Vector2 MousePos
        {
            get { return new Vector2(X, Y); }
        }

        public int DX
        {
            get { return dx; }
        }

        public int DY
        {
            get { return dy; }
        }

        public Vector2 DeltaMousePos
        {
            get { return new Vector2(DY, DX); }
        }

        public bool Left
        {
            get { return left; }
        }

        public bool Right
        {
            get { return right; }
        }

        public bool Middle
        {
            get { return middle; }
        }

        public int ScrollWheelValue
        {
            get { return mouseState.ScrollWheelValue; }
        }

        //Events
        public delegate void MouseHandler(int x, int y, MouseButton button);
        public event MouseHandler MouseUp;
        public event MouseHandler MouseDown;
        public event MouseHandler MouseClicked;

        public delegate void MouseMovedHandler(int x, int y);
        public event MouseMovedHandler MouseMoved;

        public Input()
        {
            //Stops first tick errors
            keyboardState = Keyboard.GetState();
            mouseState = Mouse.GetState();
        }

        public void Update(GameTime gameTime)
        {
            #region Storing various mouse values
            //updates the states of the mouse and keyboard
            keyboardState = Keyboard.GetState();
            mouseState = Mouse.GetState();

            //store the x and y values of the mouse
            x = mouseState.X;
            y = mouseState.Y;

            //mouse button down bools
            right= (mouseState.RightButton == ButtonState.Pressed);
            left = (mouseState.LeftButton == ButtonState.Pressed);
            middle = (mouseState.MiddleButton == ButtonState.Pressed);
            #endregion

            #region MouseDown Triggering
            //If the mouse is pressed, we fire the mouse down event
            if (MouseDown != null)
            {
                if (mouseState.LeftButton == ButtonState.Pressed && !leftLastFrame)
                {
                    MouseDown(X, Y, MouseButton.Left);
                    LastMouseDown = MouseButton.Left;
                }
                if (mouseState.RightButton == ButtonState.Pressed && !rightLastFrame)
                {
                    MouseDown(X, Y, MouseButton.Right);
                    LastMouseDown = MouseButton.Right;
                }
                if (mouseState.MiddleButton == ButtonState.Pressed && !middleLastFrame)
                {
                    MouseDown(X, Y, MouseButton.Middle);
                    LastMouseDown = MouseButton.Middle;
                }
            }
            #endregion

            #region MouseUp Triggering
            //If the mouse is released, we fire the mouse down event
            if (MouseUp != null)
            {
                if (mouseState.LeftButton == ButtonState.Released && leftLastFrame)
                {
                    MouseUp(X, Y, MouseButton.Left);
                }
                if (mouseState.RightButton == ButtonState.Released && rightLastFrame)
                {
                    MouseUp(X, Y, MouseButton.Right);
                }
                if (mouseState.MiddleButton == ButtonState.Released && middleLastFrame)
                {
                    MouseUp(X, Y, MouseButton.Middle);
                }
            }
            #endregion

            #region MouseClicked Triggering
            if (MouseClicked != null)
            {
                if (!left && leftLastFrame)
                {
                    MouseClicked(X, Y, MouseButton.Left);
                }

                if (!right && rightLastFrame)
                {
                    MouseClicked(X, Y, MouseButton.Right);
                }

                if (!middle && middleLastFrame)
                {
                    MouseClicked(X, Y, MouseButton.Middle);
                }
            }
            #endregion

            #region DY and DX calculation
            //Caluclating delta x and delta y
            dy = y - yLastFrame;
            dx = x - xLastFrame;
            #endregion

            #region MouseMoved Triggering
            //Fires the mouse moved event if there has been a change in position of the mouse
            if (MouseMoved != null)
            {
                if (dy > 0 || dy < 0 || dx > 0 || dx < 0)
                {
                    MouseMoved(X, Y);
                }
            }
            #endregion

            #region Storing last frame values
            //Stores the x and y of this frame so we cna calculate DY and DX later on
            yLastFrame = y;
            xLastFrame = x;

            leftLastFrame = left;
            rightLastFrame = right;
            middleLastFrame = middle;
            #endregion
        }

        public bool IsKeyUp(Keys key)
        {
            return keyboardState.IsKeyUp(key);
        }

        public bool IsKeyDown(Keys key)
        {
            return keyboardState.IsKeyDown(key);
        }

        #region Function Explanation
        //A method which finds the mouse position within the
        //entire game, not just within the viewport. I made it 
        //require the camera here and only have this called externally
        //so that this class can still be used with ease for menus,
        //which do not have a camera. This however means you must
        //remember to call this class on the XY variables before they
        //are used. We can easily do ingame menus now as we just don't use
        //this method, and use actual XY.
        #endregion
        public Vector2 relativeXY(Vector2 XY, Camera camera)
        {
            return (camera.Position + XY) / camera.Zoom;
        }
    }
}
