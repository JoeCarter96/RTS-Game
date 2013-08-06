using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace RTS_Game
{
    class PowerPlant : Building
    {

        #region Variable: SpriteDimensions
        static Rectangle spriteDimensions = new Rectangle(0, 0, 48, 48);
        #endregion

        public PowerPlant(TileMap world, Player owner, Vector2 TilePosition)
            : base(world, owner, TilePosition, owner.GetBuildingTextures("PowerPlant"), spriteDimensions)
        {
            MaxHealth = 500;

            Width = 2;
            Height = 3;
            ApplySizeChanges();
        }
    }
}
