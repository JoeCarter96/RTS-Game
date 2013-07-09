using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace RTS_Game
{
    class Input
    {
        //Variables
        private KeyboardState keyboardState;
        private MouseState mouseState;
        private static int x, y, dy, dx = 0;
        private static bool left, right = false;

        //holds the x and y of the last frame
        private static int oldy, oldx = 0;

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

        public int ScrollWheelValue
        {
            get { return mouseState.ScrollWheelValue; }
        }

        //Events
        public delegate void MouseClickedHandler(int x, int y, MouseButton button);
        public event MouseClickedHandler MouseClicked;

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
            //updates the states of the mouse and keyboard
            keyboardState = Keyboard.GetState();
            mouseState = Mouse.GetState();

            x = mouseState.X;
            y = mouseState.Y;

            //Checking if there has been any clicks
            if (MouseClicked != null)
            {
                if (mouseState.LeftButton == ButtonState.Pressed && !left)
                {
                    MouseClicked(X, Y, MouseButton.Left);
                }
                if (mouseState.RightButton == ButtonState.Pressed && !right)
                {
                    MouseClicked(X, Y, MouseButton.Right);
                }
            }

            //Checking id left and right are down or not
            right = (mouseState.RightButton == ButtonState.Pressed);
            left = (mouseState.LeftButton == ButtonState.Pressed);

            //Caluclating delta x and delta y
            dy = y - oldy;
            dx = x - oldx;

            //checking for mouse movements
            if (MouseMoved != null)
            {
                if (dy > 0 || dy < 0 || dx > 0 || dx < 0)
                {
                    MouseMoved(X, Y);
                }
            }

            oldy = y;
            oldx = x;
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
