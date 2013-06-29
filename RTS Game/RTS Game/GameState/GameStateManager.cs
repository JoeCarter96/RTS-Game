using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RTS_Game
{
    class GameStateManager
    {
        private States CurrentState = States.NullState;
        private Dictionary<States, BasicGameState> GameStateDictionary = new Dictionary<States, BasicGameState>();

        private BasicGameState CurrentState = null;
        public GameStateManager()
        {

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
            if (state == States.NullState)
                return;

            CurrentState = GameStateDictionary[state];
        }
    }
}
