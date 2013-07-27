using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace RTS_Game
{
    class PowerPlant : Building
    {
        public PowerPlant(TileMap world, Player owner, Vector2 TilePosition)
            : base(world, owner, TilePosition, Resources.GetBuildingTextures("PowerPlant"))
        {
            MaxHealth = 500;

            Width = 2;
            Height = 2;
            ApplySizeChanges();
        }
    }
}
