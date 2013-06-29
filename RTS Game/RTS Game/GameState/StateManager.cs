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
        #region Singleton class variables
        private static StateManager manager;
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

        private Dictionary<States, BasicGameState> GameStateDictionary = new Dictionary<States, BasicGameState>();
        private BasicGameState CurrentGameState = null;
        private States CurrentState = States.NullState;
        
        public StateManager()
        {
            AddState(States.SplashScreen, new SplashState(this));
            AddState(States.MainMenu, new MainMenuState(this));
            AddState(States.InGame, new InGameState(this));

            EnterState(States.SplashScreen);
        }

        private void AddState(States state, BasicGameState GameState)
        {
            //If there is no GameState for the given state enum entry 
            //then add the new state
            if (!GameStateDictionary.ContainsKey(state))
            {
                GameStateDictionary.Add(state, GameState);
            }
        }

        public void EnterState(States state)
        {
            //we dont want the game returning to the null state, that is just 
            //to lower the complexities of initialisation
            if (state == States.NullState || state == CurrentState)
            {
                Console.WriteLine("ERROR: attempted to enter an invalid state: {0}", state.ToString());
                return;
            }

            //tell the state we are currently in that we are exiting from it
            if (CurrentGameState != null)
            {
                CurrentGameState.OnExit();
            }

            //Stops us from entering a state that has been been added yet
            if (GameStateDictionary[state] == null)
            {
                Console.WriteLine("Attempted to change to a a state that has not been implemented full yet.");
                return;
            }

            //Change the state
            CurrentGameState = GameStateDictionary[state];

            //tell the new state that we are now in that we are entering it
            CurrentGameState.OnEnter();
        }

        public void Update(GameTime gameTime, KeyboardState keyboard, MouseState mouse)
        {
            CurrentGameState.Update(gameTime, keyboard, mouse);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            CurrentGameState.Draw(spriteBatch);
        }
    }
}
