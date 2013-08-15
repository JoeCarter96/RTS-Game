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
        String type;
        String building;
        String unit;

        public Button(Texture2D texture, Vector2 position, Player player)
            : base(texture, position, new Size(100, 100))
        {
            
        }

        public override void MouseClicked(int x, int y, MouseButton button)
        {
            if (button == MouseButton.Left)
            {
                /*
                if (type == "CreateUnit")
                {
                    
                        localplayer.Warfproduction(unit)

                        
                         * find a warf
                         * if there is one, call createunit(unitname)
                         * 
                         * unitname finds unit type and makes itr
                         * */
                    }
                }


        public override void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
