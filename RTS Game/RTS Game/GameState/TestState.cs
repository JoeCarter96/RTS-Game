using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RTS_Game
{
    class TestState : BasicGameState
    {
        private SquareOfDoom test;
        public TestState()
            : base("TestState")
        {

        }

        protected override void InitGui()
        {
            test = new SquareOfDoom(new Vector2(100, 100));
            RegisterComponent(test);
        }

        public override void MouseDown(int x, int y, MouseButton button)
        {
            //base.MouseDown(x, y, button);

            //Console.WriteLine("{0} has been pressed", button.ToString());
        }

        public override void MouseClicked(int x, int y, MouseButton button)
        {
            base.MouseClicked(x, y, button);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            base.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}
