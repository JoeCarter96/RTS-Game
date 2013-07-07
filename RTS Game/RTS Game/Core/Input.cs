﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RTS_Game.Core
{
    public class Input
    {
        #region Variables
        private KeyboardState currentKeyboard;
        private KeyboardState prevKeyboard;
        private MouseState currentMouse;
        private MouseState prevMouse;

        private List<Entity> entityList;
        private List<Entity> movingEntityList;
        #endregion

        #region Function Explanation
        //Ran when the actual game begins. Basically a constructor,
        //This is used in case we start Input earlier for use in options and stuff.
        #endregion
        public void gameStart(List<Entity> entityList, List<Entity> movingEntityList)
        {
            this.entityList = entityList;
            this.movingEntityList = movingEntityList;
        }

        #region Function Explanation
        //Checks if input has occured, and if so checks it against various conditions,
        //each one resulting in a different action.
        #endregion
        public void Update(GameTime gameTime)
        {
            //Update the state of the mouse and the keyboard
            currentKeyboard = Keyboard.GetState();
            currentMouse = Mouse.GetState();

            #region Left Click
            #region Explanation
            //Finds the tile which contains the mouse, and checks whether it contains a unit.
            //Pretty temporary, we need functionality for clicking on blank tiles. This can
            //And should be replaced with the bounding boxes for entities when implimented.
            #endregion
            
            if (currentMouse.LeftButton == ButtonState.Released && prevMouse.LeftButton == ButtonState.Pressed)
            {
                if (entityList.Count > 0)
                {
                    foreach (Unit u in entityList)
                    {
                        //Unit area.
                        Vector2 tilePos = new Vector2(currentMouse.X / GameClass.Tile_Width,
                            currentMouse.Y / GameClass.Tile_Width);

                        FieldModifer.calculateField(u.PFArray,(int)tilePos.X,(int)tilePos.Y, 100);
                        u.FinalTarget = new Vector2((int)tilePos.X, (int)tilePos.Y);
                        if (u.MaxSpeed == 5)    //TEMP, stops unit buildings moving.
                            movingEntityList.Add(u);

                    }
                }
            }
            #endregion

            #region Left Click and Hold
            //Unused at this moment.
            
            if (currentMouse.LeftButton == ButtonState.Pressed && prevMouse.LeftButton == ButtonState.Pressed)
            {

            }
            #endregion

            //Updating previous input states.
            prevKeyboard = currentKeyboard;
            prevMouse = currentMouse;
        }
    }
}
