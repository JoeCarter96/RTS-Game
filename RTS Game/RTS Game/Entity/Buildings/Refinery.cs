using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RTS_Game
{
    class Refinery : Building
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

        #region Variable: SpriteDimensions
        static Rectangle spriteDimensions = new Rectangle(0, 0, 72, 72);
        #endregion

        public Refinery(TileMap world, Player owner, Vector2 TilePosition)
            : base(world, owner, TilePosition, owner.GetBuildingTextures("Refinery"), spriteDimensions)
        {
            MaxHealth = 500;
            Width = 3;
            Height = 3;
            ApplySizeChanges();
        }

    }
}
