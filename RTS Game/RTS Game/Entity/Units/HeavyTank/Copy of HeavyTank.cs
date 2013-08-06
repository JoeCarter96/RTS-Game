using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RTS_Game
{
    class Turret : Unit
    {
        #region Variables
        static float maxHealth = 100;

        static float maxSpeed = 3f;
        static float acceleration = 0.2f;
        static float damage = 10;
        static float AOE = 4;
        static float ROF = 7;

        static Rectangle spriteDimensions = new Rectangle(0, 0, 24, 24);
        #endregion

        #region Function Explanation
        //Constructor.
        #endregion
        public Turret(Vector2 tilePosition, Player owner, TileMap world) :
            base(world, owner, tilePosition, owner.GetUnitTextures("Turret"), maxHealth, maxSpeed, acceleration, damage,
            AOE, ROF, spriteDimensions)
        {

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
