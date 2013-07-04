using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RTS_Game
{
    class HealthEntity : Entity
    {
        //Health variables to deturmine if the unit is dead and also for the health bar
        protected double maxHealth;
        protected double health;

        //Used to stop the unit from being drawn and updated once it is dead
        protected bool alive = true;

        private HealthBar healthBar;

        //Assuming the unit is spawned with full health
        public HealthEntity(TileMap world, Vector2 tilePosition, Texture2D texture, double maxHealth)
            : base(world, tilePosition, texture)
        {
            this.maxHealth = maxHealth;
            health = maxHealth;

            healthBar = new HealthBar(this);
        }

        //Allows for a different start health
        public HealthEntity(TileMap world, Vector2 tilePosition, Texture2D texture, double maxHealth, double startHealth)
            : base(world, tilePosition, texture)
        {
            this.maxHealth = maxHealth;
            this.health = startHealth;

            //Stops the health going over its maximum
            if (startHealth > maxHealth)
                health = maxHealth;
        }

        //TODO: think of better name than "damager"
        //Note: we pass the entity which did the damage so that we can log it for end game statistics
        public void Damage(HealthEntity damager, double damage)
        {
            health -= damage;

            //DEBUG: testing out health bars
            Console.WriteLine("{0} damage done, health now at {1}. The health percentage is{2}", damage, health, GetHealthPercentage());

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
                if (health < 0)
                {
                    OnDeath(null); //wasnt killed by an entity.
                    alive = false;
                    #region NOTE: OnDeath() call
                    /*OnDeath() would only be called once as once alive 
                    is false this will no longer be checked*/
                    #endregion
                }

                base.Update(gameTime);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (alive)
            {
                base.Draw(spriteBatch);
                healthBar.Draw(spriteBatch);
            }
        }
    }
}
