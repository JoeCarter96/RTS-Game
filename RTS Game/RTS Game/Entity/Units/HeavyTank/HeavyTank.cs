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

        #region Textures
        private static List<Texture2D> textures = new List<Texture2D> 
        { 
            Resources.GetUnitTextures("HeavyTankUp"),
            Resources.GetUnitTextures("HeavyTankUpRight"),
            Resources.GetUnitTextures("HeavyTankRight"),
            Resources.GetUnitTextures("HeavyTankDownRight"),
            Resources.GetUnitTextures("HeavyTankDown"),
            Resources.GetUnitTextures("HeavyTankDownLeft"),
            Resources.GetUnitTextures("HeavyTankLeft"),
            Resources.GetUnitTextures("HeavyTankUpLeft")
        };
        #endregion

        static float maxSpeed = 3f;
        static float acceleration = 0.2f;
        static float damage = 10;
        static float AOE = 4;
        static float ROF = 7;
        #endregion

        #region Function Explanation
        //Constructor.
        #endregion
        public HeavyTank(Vector2 tilePosition, Player owner, TileMap world) :
            base(world, owner, tilePosition, textures, maxHealth, maxSpeed, acceleration, damage,
            AOE, ROF)
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
