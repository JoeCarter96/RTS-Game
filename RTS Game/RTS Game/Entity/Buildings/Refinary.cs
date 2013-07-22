using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RTS_Game
{
    class Refinary : Building
    {
        private int numberOfHarvesters = 0;
        //This limit can be broken if there are no refinarys anywhere near but this one.
        private int harvesterLimit = 5;

        public int NumberOfHarvesters
        {
            get { return numberOfHarvesters; }
            set { numberOfHarvesters = value; }
        }

        public int HarvesterLimit
        {
            get { return harvesterLimit; }
            set { harvesterLimit = value; }
        }

        public Refinary(TileMap world, Player owner, Vector2 TilePosition)
            : base(world, owner, TilePosition, Resources.GetBuildingTextures("ConstructionYard"))
        {
            MaxHealth = 500;

            Width = 4;
            Height = 4;
            ApplySizeChanges();
        }

    }
}
