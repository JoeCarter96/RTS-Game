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
        public MainMenuState()
        {

        }

        protected override void Click(Point mousePos, MouseButton button)
        {
            if (button == MouseButton.Left)
            {
                StateManager.Instance.CurrentGameState = new InGameState(Resources.GetLevelObject(5), null);
            }
            base.Click(mousePos, button);
        }

        public override void Update(GameTime gameTime, KeyboardState keyboard, MouseState mouse)
        {
            base.Update(gameTime, keyboard, mouse);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            DrawCenterString(spriteBatch, "Main menu", new Vector2(GameClass.Game_Width / 2, 70), Color.Black, 2);

            DrawCenterString(spriteBatch, "Click anywhere to go to the game!", new Vector2(GameClass.Game_Width / 2, 200), Color.Black, 1); 
            spriteBatch.End();
        }
    }
}
