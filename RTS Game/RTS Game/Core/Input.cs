using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RTS_Game.Core
{
    public class Input
    {
        private KeyboardState currentKeyboard;
        private KeyboardState prevKeyboard;
        private MouseState currentMouse;
        private MouseState prevMouse;

        private Vector2 mousePos;

        private List<Entity> entityList;


        public void gameStart(List<Entity> entityList)
        {
            this.entityList = entityList;
        }

        public void Update(GameTime gameTime)
        {
            //Update the state of the mouse and the keyboard
            currentKeyboard = Keyboard.GetState();
            currentMouse = Mouse.GetState();

            //Update the StateManager
            StateManager.Instance.Update(gameTime, currentKeyboard, currentMouse);

            //Left Click
            if (currentMouse.LeftButton == ButtonState.Released && prevMouse.LeftButton == ButtonState.Pressed)
            {
                foreach (Unit u in entityList)
                {
                    //IF USER CLICKS ON UNIT, U.MOVE.
                    if (u.PixelPosition
                }
            }

            //Left Click and Hold
            if (currentMouse.LeftButton == ButtonState.Pressed && prevMouse.LeftButton == ButtonState.Pressed)
            {

            }

            prevKeyboard = currentKeyboard;
            prevMouse = currentMouse;
        }
    }
}
