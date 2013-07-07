using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RTS_Game
{
    //The main manager for the game state
    class StateManager
    {
        #region Variables
        #region Singleton class variables
        private static StateManager manager = null;
        public static StateManager Instance
        {
            get
            {
                if (manager == null)
                {
                    manager = new StateManager();
                }

                return manager;
            }
        }
        #endregion

        private BasicGameState currentGameState;
        private Point mousePosition = new Point(0, 0);
        #endregion

        public BasicGameState CurrentGameState
        {
            get { return currentGameState; }
            set { currentGameState = value; }
        }

        public Point MousePosition
        {
            get { return mousePosition; }
        }

        public StateManager()
        {
            //we start the game is a splash screen state
            currentGameState = new SplashState();
        }

        #region Function Explanation
        //Converts the X and Y variables of the Mouse state to a point and returns it.
        #endregion
        public static Point GetMousePosition(MouseState mouse)
        {
            return new Point(mouse.X, mouse.Y);
        }

        #region Function Explanation
        //Updates Mouse and current Game State.
        #endregion
        public void Update(GameTime gameTime, KeyboardState keyboard, MouseState mouse)
        {
            mousePosition = GetMousePosition(mouse);
            currentGameState.Update(gameTime, keyboard, mouse);
        }

        #region Function Explanation
        //Draws Current Game State.
        #endregion
        public void Draw(SpriteBatch spriteBatch)
        {
            currentGameState.Draw(spriteBatch);
        }

    }
}
