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
        static float maxHealth = 100;

        static float maxSpeed = 3f;
        static float acceleration = 0.2f;
        static float damage = 10;
        static float AOE = 4;
        static float ROF = 7;

        static Rectangle spriteDimensions = new Rectangle(0, 0, 30, 30);
        #endregion

        #region Property Explanation
        //Override Enities rotation, just points to Entities rotation
        //but adds the unit only method SetCorrectRotation.
        #endregion
        public override float Rotation
        {
            get { return base.Rotation; }
            set
            {
                base.Rotation = value;
                SetCorrectTexture();
            }
        }

        #region Function Explanation
        //Constructor.
        #endregion
        public HeavyTank(Vector2 tilePosition, Player owner, TileMap world) :
            base(world, owner, tilePosition, owner.GetUnitTextures("HeavyTank"), maxHealth, maxSpeed, acceleration, damage,
            AOE, ROF, spriteDimensions)
        {
            turret = new HeavyTankTurret(this, owner.GetUnitTextures("HeavyTank"));
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
