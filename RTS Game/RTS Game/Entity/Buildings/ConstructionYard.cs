using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace RTS_Game
{
    class ConstructionYard : Building
    {
        public ConstructionYard(TileMap world, Player owner, Vector2 TilePosition)
            : base(world, owner, TilePosition, Resources.GetBuildingTextures("ConstructionYard"))
        {
            MaxHealth = 500;

            Width = 3;
            Height = 3;
            ApplySizeChanges();
        }
    }
}
