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
        static int maxHealth = 100;
        static Texture2D texture = Resources.GetUnitTextures("HeavyTank");

        public HeavyTank(Vector2 tilePosition, Player owner, TileMap world) :
            base(world, owner, tilePosition, texture, maxHealth)
        {
            //Can't we just do this instead of constructor perameters, if the base variables are protected?
            MAX_SPEED = 5;
            ACCELLERATION = 0.5f;
            DAMAGE = 10;
            AOE = 4;
            ROF = 4;
        }

        public void Update(GameTime gametime)
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
