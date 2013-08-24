using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using TenSecondHero.Entities;

namespace TenSecondHero.Activities.GamePlay
{
    class CatActivity : GamePlayActivity
    {
        public CatActivity(MainGame game)
            : base(game, "Content/maps/cat.tmx", "images/background_morningsky.png")
        {
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if( _entities.OfType<TenSecondHero.Entities.Object>().Count() <= 0 )
            {
                Exit(true);
            }
        }
    }
}
