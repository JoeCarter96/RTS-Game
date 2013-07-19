using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RTS_Game
{
    class Ore : Entity
    {
        protected int MAX_AMOUNT = 100;
        protected int currentAmount;
        protected int DEPLETION_AMOUNT = 50;

        public Ore (Vector2 tilePosition) :
            base(tilePosition, Resources.GetBackgroundTextures("Ore"))
        {
        
        }
    } 
}
