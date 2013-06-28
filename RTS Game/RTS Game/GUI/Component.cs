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
    /*Name: Componant.cs
     * Represents a gui componant that will be drawn to the screen. 
     * This will be the base class for all gui objects
     */
    #endregion
    abstract class Componant
    {
        protected Rectangle rectangle;
        protected Point MousePosition = new Point(0,0);

        protected int deltaX = 0;
        protected int deltaY = 0;

        #region MousePositionLastFrame Explained
        //We store the last frames mouse position so we can generate
        //deltaX and deltaY which are very usfull for dragging.
        //We will updates this ad the end of the run update execution
        #endregion
        private Point MousePositionLastFrame = new Point(0, 0);

        #region MouseStateLastFrame Explained
        //We store the mouse state of the last frame to check if there
        //has been a change in mouse button states. It there has then it means
        //that there has been a click between this frame and last frame
        #endregion
        private MouseState MouseStateLastFrame = new MouseState();

        public Componant(Rectangle rectangle)
        {
            this.rectangle = rectangle;
        }

        //Packages the MouseStates X and Y proterties into a vector object and returns it
        protected Point GetMousePosition(MouseState mouse)
        {
            return new Point(mouse.X, mouse.Y);
        }

        public abstract void Update(GameTime gameTime, MouseState mouse)
        {
            //Store the mouse position
            MousePosition = GetMousePosition(mouse);

            deltaX = MousePosition.X - MousePositionLastFrame.X;
            deltaY = MousePosition.Y - MousePositionLastFrame.Y;

            //Checking for right clicks in the componats rectangle
            if (mouse.RightButton != MouseStateLastFrame.RightButton)
            {
                //There has been a right click.
                if (rectangle.Contains(MousePosition))
                {
                    //Call right click event
                }
            }

            //Checking for left clicks in the componats rectangle
            if (mouse.LeftButton != MouseStateLastFrame.LeftButton)
            {
                //There has been a left click.
                if (rectangle.Contains(MousePosition))
                {
                    //Call left click event
                }
            }

            //Store the last frame's mouse position into a variable so we can do DY and DX calculations
            MousePositionLastFrame = MousePosition;
            
            //Storing the lst frame's mouse state so we cna check for clicks
            MouseStateLastFrame = mouse;
        }

        public abstract void Draw(SpriteBatch spriteBatch);
    }
}
