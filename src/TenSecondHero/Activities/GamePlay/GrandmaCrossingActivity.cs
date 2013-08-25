using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using TenSecondHero.Behaviors;

namespace TenSecondHero.Activities.GamePlay
{
    class GrandmaCrossingActivity : GamePlayActivity
    {
        public GrandmaCrossingActivity(MainGame game)
            : base(game, "Content/maps/help_grandma.tmx", "images/background_morningsky.png")
        {
            foreach (var ent in _entities)
                ent.CollidesWithMap = false;

            foreach (var ent in _entities.OfType<Entities.NPC>())
            {
                ent.Behaviors.Add(new WalkLeftRightBehavior(_levelMap, ent));
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if( _entities.OfType<TenSecondHero.Entities.Object>().Where( (o) => o.Name == "GrandmaCitizen01" ).Count() <= 0 )
            {
                Exit(true);
            }
        }

        //public override int GetScoreFor(Entities.BaseEntity entity)
        //{
        //    TenSecondHero.Entities.Object obj = entity as TenSecondHero.Entities.Object;

        //    if (obj != null)
        //    {
        //        if (obj.Name == "GrandmaCitizen") return 3;
        //        else return -1;
        //    }

        //    return 0;
        //}
    }
}
