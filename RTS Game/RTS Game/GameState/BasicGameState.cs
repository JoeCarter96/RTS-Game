using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace RTS_Game
{
    //The base class for every game state, contains some methods
    //that will be called by the game state manager when switching states
    abstract class BasicGameState
    {
        protected Point MousePosition = new Point(0, 0);

        //Mouse click variables to keep track of which buttons are down on the mouse
        protected bool isLeftDown = false;
        protected bool isRightDown = false;

        //Delta x and y used for dragging and dropping
        protected int deltaX = 0;
        protected int deltaY = 0;

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

        public BasicGameState()
        {
            //Stops any chance of mouse de-sync
            //MousePosition = StateManager.Instance.MousePosition;
        }

        //an overridable method that shows which mouse button was clicked
        protected virtual void Click(Point mousePos, MouseButton button)
        {

        }

        //The game update method
        public virtual void Update(GameTime gameTime, KeyboardState keyboard, MouseState mouse)
        {
            //Store the mouse position
            MousePosition = StateManager.GetMousePosition(mouse);

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

        //The game draw method
        public virtual void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
