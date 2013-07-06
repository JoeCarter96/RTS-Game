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
     * We will use health entitys for units and building that have a health bar
     * and can die. We want the health entitys to be aware of the tile map so that 
     * is passed in.
     */
    #endregion
    class HealthEntity : Entity
    {
        //Health variables to deturmine if the unit is dead and also for the health bar
        protected double maxHealth;
        protected double health;

        //Used to stop the unit from being drawn and updated once it is dead
        protected bool alive = true;

        private bool drawHealthBar = true;
        private HealthBar healthBar;

        protected TileMap world;
        protected Player owner;

        public bool Alive
        {
            get { return alive; }
        }

        public bool DrawHealthBar
        {
            get { return drawHealthBar; }
            set { drawHealthBar = value; }
        }

        //Assuming the unit is spawned with full health
        public HealthEntity(TileMap world, Player owner, Vector2 tilePosition, Texture2D texture, double maxHealth)
            : base(tilePosition, texture)
        {
            this.owner = owner;
            this.world = world;
            this.maxHealth = maxHealth;
            health = maxHealth;

            healthBar = new HealthBar(this);
        }

        //Allows for a different start health
        public HealthEntity(TileMap world, Player owner, Vector2 tilePosition, Texture2D texture, double maxHealth, double startHealth)
            : base(tilePosition, texture)
        {
            this.owner = owner;
            this.world = world;
            this.maxHealth = maxHealth;
            this.health = startHealth;

            healthBar = new HealthBar(this);

            //Stops the health going over its maximum
            if (startHealth > maxHealth)
                health = maxHealth;
        }

        public void Kill()
        {
            Damage(null, maxHealth);
        }

        //TODO: think of better name than "damager"
        //Note: we pass the entity which did the damage so that we can log it for end game statistics
        public void Damage(HealthEntity damager, double damage)
        {
            health -= damage;

            if (health <= 0)
            {
                alive = false;
                OnDeath(damager);
            }
        }

        //Called when the unit is killed, can be overridden
        public virtual void OnDeath(HealthEntity killer)
        {

        }

        //For use by the health bar class
        public double GetHealthPercentage()
        {
            return health / maxHealth;
        }

        public override void Update(GameTime gameTime)
        {
            if (alive)
            {
                //DEBUG: testing healthbar damage
                //remove this to stop units dying :D
                Damage(null, 0.1);

                if (drawHealthBar)
                {
                    healthBar.Update(gameTime);
                }
                base.Update(gameTime);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (alive)
            {
                base.Draw(spriteBatch);
                if (drawHealthBar)
                {
                    healthBar.Draw(spriteBatch);
                }
            }
        }
    }
}
