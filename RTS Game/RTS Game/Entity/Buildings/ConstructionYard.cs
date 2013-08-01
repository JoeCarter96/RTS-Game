using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace RTS_Game
{
    class ConstructionYard : Building
    {
        #region Variable: SpriteDimensions
        static Rectangle spriteDimensions = new Rectangle(0, 0, 72, 72);
        #endregion

        public ConstructionYard(TileMap world, Player owner, Vector2 TilePosition)
            : base(world, owner, TilePosition, owner.GetBuildingTextures("ConstructionYard"), spriteDimensions)
        {
            MaxHealth = 500;

            Width = 3;
            Height = 3;
            ApplySizeChanges();
        }
    }
}
