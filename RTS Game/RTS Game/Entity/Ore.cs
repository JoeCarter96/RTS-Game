using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RTS_Game
{
    class Ore : Entity
    {
        public const int MaxOreAmount = 1000;

        #region Variable: BeingMined
        private bool beingMined = false;
        public bool BeingMined
        {
            get { return beingMined; }
            set { beingMined = value; }
        }
        #endregion
        #region Variable: CurrentAmount
        protected int currentAmount = 0;
        public int CurrentAmount
        {
            get { return currentAmount; }
            set { currentAmount = value; }
        }
        #endregion
        #region Variable: DepletionAmount
        protected int depletionAmount = 50;

        public int DepletionAmount
        {
            get { return depletionAmount; }
        }
        #endregion
        #region Variable: SpriteDimensions
        static Rectangle spriteDimensions = new Rectangle(0, 0, 24, 24);
        #endregion

        public Ore (Vector2 tilePosition) :
            base(tilePosition, Resources.GetBackgroundTextures("Ore"), spriteDimensions)
        {

        }
    } 
}
