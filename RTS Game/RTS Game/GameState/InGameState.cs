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
        #region Variables
        private GameInstance game;
        private Camera camera;
        #endregion

        #region Function Explanation
        //Constructor.
        #endregion
        public InGameState(Level level, GameOptions options)
            : base("InGameState")
        {
            Console.WriteLine("Entered Game State");
            
            //Create a new camera
            camera = new Camera();

            //Level ID will be passed to this state somehow
            game = new GameInstance(level, camera);

        }

        #region Function Explanation
        //Updates Camera and Game.
        #endregion
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            camera.Update(GetInput());
            game.Update(gameTime, camera, GetInput());
        }

        #region Function Explanation
        //Draw with and without consideration towards the Camera.
        #endregion
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
