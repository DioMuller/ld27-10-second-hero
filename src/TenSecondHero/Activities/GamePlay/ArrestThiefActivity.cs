using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using TenSecondHero.Entities;
using TenSecondHero.Behaviors;
using TenSecondHero.Core;

namespace TenSecondHero.Activities.GamePlay
{
    class ArrestThiefActivity : GamePlayActivity
    {
        public ArrestThiefActivity(MainGame game)
            : base(game, "Content/maps/arrest_thief.tmx", "images/background_nightsky.png")
        {
            foreach (var ent in _entities)
                ent.CollidesWithMap = false;

            foreach (var ent in _entities.OfType<Entities.Object>().Where(e => e.Name.Contains("Citizen")))
                ent.Behaviors.Add(new WalkLeftRightBehavior(_levelMap, ent));
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            /*if (!_exploding && _entities.OfType<>().Count() <= 0)
            {
                Exit(true);
            }*/
        }

        public async override void OnTimeout()
        {
            //_exploding = true;
            //await _entities.OfType<Bomb>().First().Explode();
            base.OnTimeout();
        }
    }
}
