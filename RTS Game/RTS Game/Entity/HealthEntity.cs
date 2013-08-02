using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RTS_Game
{
    #region Info
    /* A health entity is any entity that is considered to be alive.
     * We will use health entitys for playerEntities and building that have a health bar
     * and can die. We want the health entitys to be aware of the tile map so that 
     * is passed in.
     */
    #endregion
    class HealthEntity : Entity
    {
        #region Variable: Health
        private double health;
        public double Health
        {
            get { return health; }
            protected set 
            { 
                health = value;
                if (health > MaxHealth)
                {
                    health = MaxHealth;
                }
            }
        }
        #endregion
        #region Variable: MaxHealth
        private double maxHealth;
        public double MaxHealth
        {
            get { return maxHealth; }
            protected set { maxHealth = value; }
        }
        #endregion

        #region Variable: Alive
        //A variable specifying if the entity should still be drawn and updated
        private bool alive = true;
        public bool Alive
        {
            get { return alive; }
            protected set { alive = value; }
        }
        #endregion
        #region Variable: DrawHealthBar
        private bool drawHealthBar = true;
        public bool DrawHealthBar
        {
            get { return drawHealthBar; }
            private set { drawHealthBar = value; }
        }
        #endregion

       /*#region Variable: HealthBar
        private HealthBar healthBar = null;
        public HealthBar HealthBar
        {
            get { return healthBar; }
            protected set { healthBar = value; }
        }
        #endregion*/

        #region Variable: World
        private TileMap world;
        protected TileMap World
        {
            get { return world; }
            private set { world = value; }
        }
        #endregion
        #region Variable: Owner
        private Player owner;
        protected Player Owner
        {
            get { return owner; }
            private set { owner = value; }
        }
        #endregion

        //Assuming the harvester is spawned with full health
        public HealthEntity(TileMap world, Player owner, Vector2 tilePosition, Texture2D texture, double maxHealth,
            Rectangle spriteDimensions)
            : base(tilePosition, texture, spriteDimensions)
        {
            Owner = owner;
            World = world;
            MaxHealth = maxHealth;
            Health = maxHealth;
            //healthBar = new HealthBar(this, new Rectangle((int)PixelPosition.X, (int)PixelPosition.Y, GameClass.Tile_Width, GameClass.Tile_Width));
            
            owner.Entities.Add(this);
        }

        //Allows for a different start health
        /*public HealthEntity(TileMap world, Player owner, Vector2 tilePosition, Texture2D texture, double maxHealth, double startHealth)
            : base(tilePosition, texture)
        {
            /*this.health = startHealth;
            
            this.owner = owner;
            this.world = world;
            this.maxHealth = maxHealth;
            

            owner.Entities.Add(this);

            
            healthBar = new HealthBar(this, new Rectangle((int)PixelPosition.X, (int)PixelPosition.Y, GameClass.Tile_Width, GameClass.Tile_Width));
            
        }*/

        public void Kill()
        {
            Damage(null, maxHealth);
        }

        //TODO: think of better name than "damager"
        //Note: we pass the entity which did the damage so that we can log it for end game statistics
        public void Damage(HealthEntity damager, double damage)
        {
            Health -= damage;

            if (Health <= 0)
            {
                Alive = false;
                OnDeath(damager);
            }
        }

        //Called when the harvester is killed, can be overridden
        public virtual void OnDeath(HealthEntity killer)
        {

        }

        //For use by the health bar class
        public double GetHealthPercentage()
        {
            return Health / MaxHealth;
        }


        //NOTE: we only want to update and draw when the health entity is alive
        //This means we dont need to 
        public override void Update(GameTime gameTime)
        {
            if (DrawHealthBar)
            {
                //healthBar.Update(gameTime);
            }

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            if (drawHealthBar)
            {
                //healthBar.Draw(spriteBatch);
            }
        }
    }
}
