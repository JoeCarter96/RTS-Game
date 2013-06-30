using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Graphics;

namespace RTS_Game
{
    public class Level
    {

        private String NAME;
        private Texture2D LEVELIMAGE;
        private int iD;

        public Level(String Name, Texture2D LevelImage, int ID)
        {
            this.NAME = Name;
            this.LEVELIMAGE = LevelImage;
            this.iD = ID;
        }

        public String LevelName
        {
            get { return NAME; }
            set { NAME = value; }
        }

        public Texture2D LevelImage
        {
            get { return LEVELIMAGE; }
            set { LEVELIMAGE = value; }
        }

        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }
    }
}
