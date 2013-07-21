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

        public int DEPLETIONAMOUNT
        {
            get { return DEPLETION_AMOUNT; }
        }

        public int MAXAMOUNT
        {
            get { return MAX_AMOUNT; }
        }

        public int CurrentAmount
        {
            get { return currentAmount; }
            set { currentAmount = value; }
        }

        public Ore (Vector2 tilePosition) :
            base(tilePosition, Resources.GetBackgroundTextures("oreArray"))
        {
            currentAmount = 0;
        }
    } 
}
