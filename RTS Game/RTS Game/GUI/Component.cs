using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace RTS_Game
{
    #region Class Info
    /*Name: Component.cs
     * Represents a GUI Component that will be drawn to the screen. 
     * This will be the base class for all gui objects
     */
    #endregion
    abstract class Component
    {
        private Vector2 position;
        private Size size;
        private Texture2D texture;
        private bool focused = false;
        private bool containsMouse = false;

        protected Texture2D Drawtexture
        {
            get { return texture; }
            set { texture = value; }
        }

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public Size Dimensions
        {
            get { return size; }
            set { size = value; }
        }

        public bool Focused
        {
            get { return focused; }
            set { focused = value; }
        }

        public bool ContainsMouse
        {
            get { return containsMouse; }
        }

        protected Rectangle DestinationRectangle
        {
            get { return size.CreateRectangle(position); }
        }

        public Component(Texture2D startTexture, Vector2 position, Size size)
        {
            this.position = position;
            this.size = size;
            this.texture = startTexture;
        }

        //returns true if the mouse is contained in the hitbox
        //used to deturmine if the mouse is in the component
        public virtual bool MouseMoved(int x, int y)
        {
            containsMouse = DestinationRectangle.Contains(new Point(x, y));
            return containsMouse;
        }

        //called when the mouse is clicked inside the component
        public virtual void MouseClicked(int x, int y, MouseButton button)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, DestinationRectangle, Color.White);
        }

        public virtual void Draw(SpriteBatch spriteBatch, Color color)
        {
            spriteBatch.Draw(texture, DestinationRectangle, color);
        }
    }
}
