using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RTS_Game
{
    public class AI
    {
        Player bot;
        Random rand = new Random();
        GameInstance gameInstance;

        int actions = 4;        //How many actions the AI has (4 at start)

        int newActionTime = 3000;     //how long to wait for a new action (Seconds)
        int timeSinceNewAction = 0; //How long since a new action was added.

        int timeBetweenActions = 4000;     //Time to wait before starting another action.
        int timeSinceAction = 0;        //Time since last action.


        public AI(GameInstance instance)
        {
            bot = new Player(instance, new Color(rand.Next(0, 255), rand.Next(0, 255), rand.Next(0, 255)));
            gameInstance = instance;
            instance.AIs.Add(this);

            new ConstructionYard(instance.World, bot, new Vector2(40, 40));
        }

        public void Update(GameTime gameTime)
        {
            //Adding an action if we can
            if (timeSinceNewAction + gameTime.ElapsedGameTime.Milliseconds >= newActionTime)
            {
                actions++;
                timeSinceNewAction = 0;
            }
            else
            {
                timeSinceNewAction += gameTime.ElapsedGameTime.Milliseconds;
            }

            //Preforming an action if we can
            if (timeSinceAction + gameTime.ElapsedGameTime.Milliseconds >= timeBetweenActions)
            {
                preformAction();
                timeSinceAction = 0;
            }
            else
            {
                timeSinceAction += gameTime.ElapsedGameTime.Milliseconds;
            }
        }

        public void preformAction()
        {
            //TODO: Completely replace, this is TEMP testing.
            if (bot.PlayerBuildings.OfType<Refinery>().Count() < 10)
            {
                new Refinery(gameInstance.World, bot, FindNearestTile.BeginSearch(new Vector2(40, 40), gameInstance.World.TileArray));
            }
            else if (bot.PlayerUnits.OfType<Harvester>().Count() < 4)
            {
                new Harvester(gameInstance.World, bot, new Vector2(36, 35), bot.PlayerBuildings, gameInstance.OreArray);
            }
        }
    }
}
