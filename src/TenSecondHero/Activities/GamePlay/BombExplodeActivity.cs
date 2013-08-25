using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using TenSecondHero.Entities;
using TenSecondHero.Behaviors;
using TenSecondHero.Core;
using MonoGameLib.Core;

namespace TenSecondHero.Activities.GamePlay
{
    class BombExplodeActivity : GamePlayActivity
    {
        bool _exploding;

        public BombExplodeActivity(MainGame game)
            : base(game, "Content/maps/bomb_explode.tmx", "images/background_morningsky.png")
        {
            foreach (var ent in _entities.OfType<NPC>())
            {
                ent.Behaviors.Add(new WalkLeftRightBehavior(_levelMap, ent));
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (!_exploding && _entities.OfType<Bomb>().Count() <= 0)
            {
                Exit(true);
            }
        }

        public async override void OnTimeout()
        {
            _exploding = true;
            SoundManager.PlaySound("explosion");
            await _entities.OfType<Bomb>().First().Explode();
            base.OnTimeout();
        }

        public override int GetScoreFor(BaseEntity entity)
        {
            if( entity is Bomb ) return 3;
            else return -1;
        }
    }
}
