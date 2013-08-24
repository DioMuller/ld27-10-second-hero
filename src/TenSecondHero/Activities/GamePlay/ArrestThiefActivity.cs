﻿using Microsoft.Xna.Framework;
using System.Linq;
using TenSecondHero.Behaviors;

namespace TenSecondHero.Activities.GamePlay
{
    class ArrestThiefActivity : GamePlayActivity
    {
        public ArrestThiefActivity(MainGame game)
            : base(game, "Content/maps/arrest_thief.tmx", "images/background_nightsky.png")
        {
            foreach (var ent in _entities)
                ent.CollidesWithMap = false;

            foreach (var ent in _entities.OfType<Entities.Object>())
                ent.Behaviors.Add(new WalkLeftRightBehavior(_levelMap, ent));
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (_entities.OfType<Entities.Object>().Count(e => e.Name.Contains("Thief")) <= 0)
                Exit(true);
        }
    }
}