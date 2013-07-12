using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace RTS_Game
{
    //The base class for every game state, contains some methods
    //that will be called by the game state manager when switching states
    abstract class BasicGameState
    {
        //The name of the GameState this allows for debugging with OnEnter() and OnExit()
        private string name;

        //GUI components to be drawn on the screen
        private List<Component> GuiComponents = new List<Component>();

        //The input opject
        private Input input;

        //Used for FirstTick()
        private bool isFirstTick = true;

        public BasicGameState(string name)
        {
            this.name = name;
            input = new Input();
            //Adding input events
            input.MouseClicked += MouseClicked;
            input.MouseMoved += MouseMoved;

            InitGui();
            OnEnter();
        }

        //Executes the MouseClicked() method of the first component which has contains set to true,
        //Which basically returns true if the mouse is contained within it.
        public virtual void MouseClicked(int x, int y, MouseButton button)
        {
            for (int i = 0; i < GuiComponents.Count; i++)
            {
                if (GuiComponents[i].ContainsMouse)
                {
                    GuiComponents[i].MouseClicked(x, y, button);
                    break;
                }
            }
        }

        #region Function Explanation
        /* Here, we go throguh every component until a compoent return true
         * if returned true, the component ahs the mouse within its bounding box
         * we break here to stop multiple components haveing the mouse in their bounding
         * boxes
         */
        #endregion
        public virtual void MouseMoved(int x, int y)
        {
            for (int i = 0; i < GuiComponents.Count; i++)
            {
                if (GuiComponents[i].MouseMoved(x, y))
                {
                    break;
                } 
            }
        }

        //Pre-determined space for all Gui components to be initialised
        protected virtual void InitGui()
        {

        }

        protected virtual void OnEnter()
        {
            Console.WriteLine("Entering: {0}", name);
        }

        public virtual void OnExit()
        {
            Console.WriteLine("Exiting: {0}", name);
        }

        protected virtual void FirstTick(GameTime gameTime, Input input)
        {

        }

        protected void DrawCenterString(SpriteBatch spriteBatch, string text, Vector2 position, Color color, float scale)
        {
            SpriteFont font = Resources.TestFont;
            Vector2 length = font.MeasureString(text);

            spriteBatch.DrawString(font, text, position, color, 0, length / 2, scale, SpriteEffects.None, 0);
        }

        protected void RegisterComponent(Component component)
        {
            GuiComponents.Add(component);
        }

        protected Input GetInput()
        {
            return input;
        }

        //The game update method
        public virtual void Update(GameTime gameTime)
        {
            input.Update(gameTime);
            if (isFirstTick)
            {
                isFirstTick = false;
                FirstTick(gameTime, input);
            }
        }

        //The game draw method
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            foreach (Component component in GuiComponents)
            {
                component.Draw(spriteBatch);
            }
        }
    }
}
