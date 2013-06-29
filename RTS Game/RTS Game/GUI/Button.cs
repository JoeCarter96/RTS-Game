using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RTS_Game
{
    class Button : Component
    {
        private Vector2 position;
        private Texture2D texture;

        public Vector2 Position
        {
            get { return position; }
            set 
            {
                position = value;
                //move the hitbox's position
                hitbox = new Rectangle((int)value.X, (int)value.Y, hitbox.Width, hitbox.Height);
            }
        }

        //We have 3 different textures for the button depending if the
        //mouse is not over it, over it or clicked
        private Texture2D textureNormal;
        private Texture2D textureOver;
        private Texture2D textureDown;

        public Button(Vector2 position, Texture2D texture)
            //make a rectangle out of the position and size of the texture
            :base(new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height))
        {
            this.position = position;

            this.textureNormal = texture;
            this.textureOver = texture;
            this.textureDown = texture;

            texture = textureNormal;
        }

        public Button(Vector2 position, Texture2D textureNormal, Texture2D textureOver, Texture2D textureDown)
            //make a rectangle out of the position and size of the texture
            : base(new Rectangle((int)position.X, (int)position.Y, textureNormal.Width, textureNormal.Height))
        {
            this.position = position;

            this.textureNormal = textureNormal;
            this.textureOver = textureOver;
            this.textureDown = textureDown;

            texture = textureNormal;
        }

        public override void Update(GameTime gameTime, Microsoft.Xna.Framework.Input.MouseState mouse)
        {
            base.Update(gameTime, mouse);

            if (hitbox.Contains(MousePosition))
            {
                if (isLeftDown)
                {
                    texture = textureDown;
                }
                else
                {
                    texture = textureOver;
                }
            }
            else
            {
                texture = textureNormal;
            }

            //TEMP CODE for debug purposes
            if (isRightDown && hitbox.Contains(MousePosition))
            {
                Position += new Vector2(deltaX, deltaY);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
    }
}
