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
        private static Keys[] keys;
        private static bool keyDown = false;
        private static bool keyDownLastFrame = false;

        private MouseState mouseState;
        private static int x, y, dy, dx = 0;
        private static bool left, right, middle = false;
        private static bool leftLastFrame, rightLastFrame, middleLastFrame = false;
        //holds the x and y of the last frame
        private static int yLastFrame, xLastFrame = 0;

        //Mouse clicked event variables
        //Stores the mouse button that was clicked last on the MouseDown event
        private MouseButton LastMouseDown = MouseButton.None;

        private bool isMouseDown;

        public bool IsMouseDown
        {
            get { return isMouseDown; }
            set { isMouseDown = value; }
        }


        private Rectangle dragRect;

        public Rectangle DragRect
        {
            get { return dragRect; }   
            set { dragRect = value; }
        }
        

        private Vector2 dragOrigin = Vector2.Zero; 

        public Vector2 DragOrigin
        {
            get { return dragOrigin; }
            set { dragOrigin = value; }
        }
        

        //Properties
        public bool KeyDown
        {
            get { return keyDown; }
        }

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
        public delegate void KeyHandler(Keys[] keys);
        public event KeyHandler KeyPress;

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
            #region Storing various values
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

            if (keyboardState.GetPressedKeys().Length > 0)
            {
                keyDown = true;
                keys = keyboardState.GetPressedKeys();
            }
            else
            {
                keyDown = false;
            }
            #endregion

            #region MouseDown Triggering
            //If the mouse is pressed, we fire the mouse down event
            if (MouseDown != null)
            {
                if (mouseState.LeftButton == ButtonState.Pressed && !leftLastFrame)
                {
                    MouseDown(X, Y, MouseButton.Left);
                    LastMouseDown = MouseButton.Left;
                    isMouseDown = true;
                }
                else if (mouseState.RightButton == ButtonState.Pressed && !rightLastFrame)
                {
                    MouseDown(X, Y, MouseButton.Right);
                    LastMouseDown = MouseButton.Right;
                    isMouseDown = true;
                }
                else if (mouseState.MiddleButton == ButtonState.Pressed && !middleLastFrame)
                {
                    MouseDown(X, Y, MouseButton.Middle);
                    LastMouseDown = MouseButton.Middle;
                    isMouseDown = true;
                }
            }
            #endregion

            #region MouseUp Triggering
            //If the mouse is released, we fire the mouse up event
            if (MouseUp != null)
            {
                if (mouseState.LeftButton == ButtonState.Released && leftLastFrame)
                {
                    MouseUp(X, Y, MouseButton.Left);
                    isMouseDown = false;
                    DragOrigin = Vector2.Zero;
                }
                if (mouseState.RightButton == ButtonState.Released && rightLastFrame)
                {
                    MouseUp(X, Y, MouseButton.Right);
                    isMouseDown = false;
                    DragOrigin = Vector2.Zero;
                }
                if (mouseState.MiddleButton == ButtonState.Released && middleLastFrame)
                {
                    MouseUp(X, Y, MouseButton.Middle);
                    isMouseDown = false;
                    DragOrigin = Vector2.Zero;
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

            #region KeyPress Triggering
            if (KeyPress != null)
            {
                if (!keyDown && keyDownLastFrame)
                {
                    KeyPress(keys);
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

            keyDownLastFrame = keyDown;
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


    }
}
