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
        //GUI components to be drawn on the screen
        private List<Component> GuiComponents = new List<Component>();

        //Mouse pixelPosition and imformation
        protected Point MousePosition = new Point(0, 0);
        protected bool isLeftDown = false;
        protected bool isRightDown = false;

        //Delta x and y used for dragging and dropping
        protected int deltaX = 0;
        protected int deltaY = 0;

        //Last frame variables to work out changes between frames
        #region MousePositionLastFrame Explained
        //We store the last frames mouse pixelPosition so we can generate
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
            
        }

        //an overridable method that shows which mouse button was clicked
        protected virtual void Click(Point mousePos, MouseButton button)
        {

        }

        //The game update method
        public virtual void Update(GameTime gameTime, KeyboardState keyboard, MouseState mouse)
        {
            //Store the mouse pixelPosition
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

            //Store the last frame's mouse pixelPosition into a variable so we can do DY and DX calculations
            MousePositionLastFrame = MousePosition;
            //Storing the first frame's mouse state so we can check for clicks
            MouseStateLastFrame = mouse;
        }


        public void DrawCenterString(SpriteBatch spriteBatch, string text, Vector2 position, Color color, float scale)
        {
            SpriteFont font = Resources.TestFont;
            Vector2 length = font.MeasureString(text);

            spriteBatch.DrawString(font, text, position, color, 0, length / 2, scale, SpriteEffects.None, 0);

        }


        //The game draw method
        public virtual void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
