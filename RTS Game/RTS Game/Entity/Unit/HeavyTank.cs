using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RTS_Game
{
    class HeavyTank : Unit
    {
        #region Variables
        static int maxHealth = 100;
        static Texture2D texture = Resources.GetUnitTextures("HeavyTank");
        #endregion

        #region Function Explanation
        //Constructor.
        #endregion
        public HeavyTank(Vector2 tilePosition, Player owner, TileMap world) :
            base(world, owner, tilePosition, texture, maxHealth)
        {
            //Can't we just do this instead of constructor perameters, if the base variables are protected?
            MAX_SPEED = 3;
            ACCELLERATION = 0.2f;
            DAMAGE = 10;
            AOE = 4;
            ROF = 4;
        }

        #region Function Explanation
        //Firing code, updates Entity tree for this instance.
        #endregion
        public override void Update(GameTime gametime)
        {
            if (target != null && bulletTime >= ROF)
            {
                //Firing Code Here
                bulletTime = 0f;
            }

            base.Update(gametime);
        }
    }
}
