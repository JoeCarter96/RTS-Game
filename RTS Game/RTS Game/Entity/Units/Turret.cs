using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RTS_Game
{
    class Turret : Entity
    {
        #region Variables
        private Unit unit;
        #endregion

        #region Function Explanation
        //Constructor.
        #endregion
        public Turret(Unit unit, Texture2D TurretTexture) :
            base(unit.TilePosition, TurretTexture, unit.SpriteDimensions )
        {
            this.unit = unit;
            SetCorrectTexture();
        }

        #region Function Explanation
        //Returns the correct image for the angle (Rotation, in radians) the Unit is at.
        #endregion
        public void SetCorrectTexture()
        {
            //Up
            if (Rotation > 5.890 || Rotation < 0.480)
            {
                SourceRectangle = new Rectangle(SpriteDimensions.Width * 0, SpriteDimensions.Height * 1,
                    SpriteDimensions.Width, SpriteDimensions.Height);
            }
            //Up Right
            else if (Rotation > 0.480 && Rotation < 1.178)
            {
                SourceRectangle = new Rectangle(SpriteDimensions.Width * 1, SpriteDimensions.Height * 1,
                    SpriteDimensions.Width, SpriteDimensions.Height);
            }
            //Right
            else if (Rotation > 1.178 && Rotation < 1.963)
            {
                SourceRectangle = new Rectangle(SpriteDimensions.Width * 2, SpriteDimensions.Height * 1,
                     SpriteDimensions.Width, SpriteDimensions.Height);
            }
            //Down Right
            else if (Rotation > 1.963 && Rotation < 2.749)
            {
                SourceRectangle = new Rectangle(SpriteDimensions.Width * 3, SpriteDimensions.Height * 1,
                    SpriteDimensions.Width, SpriteDimensions.Height);
            }
            //Down
            else if (Rotation > 2.749 && Rotation < 3.534)
            {
                SourceRectangle = new Rectangle(SpriteDimensions.Width * 4, SpriteDimensions.Height * 1,
                     SpriteDimensions.Width, SpriteDimensions.Height);
            }
            //Down Left
            else if (Rotation > 3.534 && Rotation < 4.320)
            {
                SourceRectangle = new Rectangle(SpriteDimensions.Width * 5, SpriteDimensions.Height * 1,
                    SpriteDimensions.Width, SpriteDimensions.Height);
            }
            //Left
            else if (Rotation > 4.320 && Rotation < 5.105)
            {
                SourceRectangle = new Rectangle(SpriteDimensions.Width * 6, SpriteDimensions.Height * 1,
                    SpriteDimensions.Width, SpriteDimensions.Height);
            }
            //Left Up
            else if (Rotation > 5.105 && Rotation < 5.890)
            {
                SourceRectangle = new Rectangle(SpriteDimensions.Width * 7, SpriteDimensions.Height * 1,
                    SpriteDimensions.Width, SpriteDimensions.Height);
            }
        }

        #region Function Explanation
        //Firing code, updates Entity tree for this instance.
        #endregion
        public override void Update(GameTime gametime)
        {
            base.Update(gametime);

        }

        #region Function Explanation
        //Draws the Texture without Colour tint.
        #endregion
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
