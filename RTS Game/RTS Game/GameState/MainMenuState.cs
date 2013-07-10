using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RTS_Game
{
    class MainMenuState : BasicGameState
    {
        #region Function Explanation
        //Constructor.
        #endregion
        public MainMenuState()
            : base("MainMenuState")
        {

        }

        #region Function Explanation
        //Click code, loading Level. Will eventually be called by Input.
        #endregion
        public override void MouseClicked(int x, int y, MouseButton button)
        {
            base.MouseClicked(x, y, button);

            StateManager.Instance.CurrentGameState = new InGameState(Resources.GetLevelObject(5), null);
        }

        #region Function Explanation
        //Update loop, Updates Basic Game State base.
        #endregion
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        #region Function Explanation
        //Draws Main Menu GUI.
        #endregion
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            DrawCenterString(spriteBatch, "Main menu", new Vector2(GameClass.Game_Width / 2, 70), Color.Black, 2);

            DrawCenterString(spriteBatch, "Click anywhere to go to the game!", new Vector2(GameClass.Game_Width / 2, 200), Color.Black, 1); 
            spriteBatch.End();
        }
    }
}
