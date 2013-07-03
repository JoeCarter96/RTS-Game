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

        //Assuming the unit is spawned with full health
        public HealthEntity(TileMap world, Vector2 position, Texture2D texture, double maxHealth)
            : base(world, position, texture)
        {
            this.maxHealth = maxHealth;
            health = maxHealth;
        }

        //Allows for a different start health
        public HealthEntity(TileMap world, Vector2 position, Texture2D texture, double maxHealth, double startHealth)
            : base(world, position, texture)
        {
            this.maxHealth = maxHealth;
            this.health = startHealth;

            //Stops the health going over its maximum
            if (startHealth > maxHealth)
                health = maxHealth;
        }

        //Called when the unit is killed, can be overridden
        public virtual void OnDeath()
        {

        }

        //For use by the health bar class
        public double GetHealthPercentage()
        {
            return maxHealth / health;
        }

        public override void Update(GameTime gameTime)
        {
            if (alive)
            {
                if (health < 0)
                {
                    OnDeath();
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
                //Also will be drawing the ehalth bar here.
                base.Draw(spriteBatch);
            }
        }
    }
}
