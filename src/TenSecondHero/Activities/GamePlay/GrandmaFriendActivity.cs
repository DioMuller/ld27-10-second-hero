using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace TenSecondHero.Activities.GamePlay
{
    class GrandmaFriendActivity : GamePlayActivity
    {
        public GrandmaFriendActivity(MainGame game)
            : base(game, "Content/maps/grandma_friend.tmx", "images/background_nightsky.png")
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
