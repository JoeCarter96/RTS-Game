using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace RTS_Game
{
    #region Class Info
    /*Name: Component.cs
     * Represents a GUI Component that will be drawn to the screen. 
     * This will be the base class for all gui objects
     */
    #endregion
    abstract class Component
    {
        //'Hitbox' for the GUI object.
        protected Rectangle hitbox;
        protected Point MousePosition = new Point(0,0);

        //Delta x and y used for dragging and dropping
        protected int deltaX = 0;
        protected int deltaY = 0;

        //Mouse click variables to keep track of which buttons are down on the mouse
        protected bool isLeftDown = false;
        protected bool isRightDown = false;

        //GUI component click events
        public event EventHandler RightClick;
        public event EventHandler LeftClick;

        //Last frame variables to work out changes between frames
        #region MousePositionLastFrame Explained
        //We store the last frames mouse position so we can generate
        //deltaX and deltaY which are very useful for dragging.
        //We will update this at the end of the run update execution.
        #endregion
        private Point MousePositionLastFrame = new Point(0, 0);
        #region MouseStateLastFrame Explained
        //We store the mouse state of the last frame to check if there
        //has been a change in mouse button states. It there has, it means
        //that there has been a click between this frame and last frame.
        #endregion
        private MouseState MouseStateLastFrame = new MouseState();

        public Component(Rectangle hitbox)
        {
            this.hitbox = hitbox;
        }

        //Packages the MouseStates X and Y proterties into a vector object and returns it
        protected Point GetMousePosition(MouseState mouse)
        {
            return new Point(mouse.X, mouse.Y);
        }

        public virtual void Update(GameTime gameTime, MouseState mouse)
        {
            //Store the mouse position
            MousePosition = GetMousePosition(mouse);

            deltaX = MousePosition.X - MousePositionLastFrame.X;
            deltaY = MousePosition.Y - MousePositionLastFrame.Y;

            //Checking if the right button has been pressed
            if (mouse.RightButton != MouseStateLastFrame.RightButton && isRightDown == false)
            {
                isRightDown = true;
                Click(MousePosition, MouseButton.Right);
            }

            //Checking if the left button has been pressed
            if (mouse.LeftButton != MouseStateLastFrame.LeftButton && isLeftDown == false)
            {
                isLeftDown = true;
                Click(MousePosition, MouseButton.Left);
            }

            //Checking if mouse buttons have been released
            isRightDown = !(mouse.RightButton == ButtonState.Released);
            isLeftDown = !(mouse.LeftButton == ButtonState.Released);

            //Store the last frame's mouse position into a variable so we can do DY and DX calculations
            MousePositionLastFrame = MousePosition;
            
            //Storing the first frame's mouse state so we can check for clicks
            MouseStateLastFrame = mouse;
        }

        //Called wehn there is a click of any kind on the screen.
        //This click might not be in the rectangle
        //We have this overidable so we can use it for full screen GUI's
        protected virtual void Click(Point mousePos, MouseButton button)
        {
            //If the mouse is in the rectangle, fire the corresponding event
            if (hitbox.Contains(mousePos))
            {
                switch (button)
                {
                    case MouseButton.Right:
                        if (RightClick != null)
                        {
                            RightClick(this, EventArgs.Empty);
                        }
                        break;
                    case MouseButton.Left:
                        if (LeftClick != null)
                        {
                            LeftClick(this, EventArgs.Empty);
                        }
                        break;
                }
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
