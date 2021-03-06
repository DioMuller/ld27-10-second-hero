﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using TenSecondHero.Entities;

namespace TenSecondHero.Activities.GamePlay
{
    class SaveActivity01 : GamePlayActivity
    {
        public SaveActivity01(MainGame game) : base(game, "Content/maps/save01.tmx", "images/background_morningsky.png")
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
