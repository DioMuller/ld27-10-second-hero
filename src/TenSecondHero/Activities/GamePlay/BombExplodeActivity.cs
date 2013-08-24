using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using TenSecondHero.Entities;
using TenSecondHero.Behaviors;
using TenSecondHero.Core;
using TenSecondHero.Activities.Base;

namespace TenSecondHero.Activities.GamePlay
{
    class BombExplodeActivity : GamePlayActivity
    {
        bool _exploding;

        public BombExplodeActivity(Game game)
            : base(game, "Content/maps/bomb_explode.tmx", "images/background_morningsky.png")
        {
            foreach (var ent in _entities.OfType<Entities.Object>().Where(e => e.Name.Contains("Citizen")))
            {
                ent.Behaviors.Add(new WalkLeftRightBehavior(_levelMap, ent));
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (!_exploding && _entities.OfType<Bomb>().Count() <= 0)
            {
                Exit(LevelResult.Succeded);
            }
        }

        public async override void OnTimeout()
        {
            _exploding = true;
            await _entities.OfType<Bomb>().First().Explode();
            base.OnTimeout();
        }
    }
}
