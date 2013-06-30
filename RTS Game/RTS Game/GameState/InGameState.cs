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

        public InGameState(StateManager manager)
            : base(manager)
        {

        }

        public override void OnEnter()
        {
            Console.WriteLine("Entered Game State");

            //Create a new camera
            camera = new Camera();

            //Level ID will be passed to this state somehow
            game = new GameInstance(Resources.GetLevelObject(00), camera);

        }

        public override void OnExit()
        {
            
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
        }
    }
}
