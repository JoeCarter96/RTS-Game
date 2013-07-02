using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RTS_Game
{
    class InGameState : BasicGameState
    {
        private GameInstance game;
        private Camera camera;

        public InGameState(Level level, GameOptions options)
        {
            Console.WriteLine("Entered Game State");

            //Create a new camera
            camera = new Camera();

            //Level ID will be passed to this state somehow
            game = new GameInstance(level, camera);

        }

        public override void Update(GameTime gameTime, KeyboardState keyboard, MouseState mouse)
        {
            camera.Update(keyboard, mouse);
            game.Update(gameTime, camera, keyboard, mouse);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, 
                              null, null, null, null, camera.CameraMatrix);
            game.Draw(spriteBatch);
            spriteBatch.End();

            //Drawing without the camera
            spriteBatch.Begin();
            game.StaticDraw(spriteBatch);
            spriteBatch.End();
        }
    }
}
