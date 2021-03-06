﻿using Microsoft.Xna.Framework;
using MonoGameLib.Core.Entities;
using System.Linq;
using TenSecondHero.Behaviors;
using TenSecondHero.Entities;

namespace TenSecondHero.Activities.GamePlay
{
    class ArrestThiefActivity : GamePlayActivity
    {
        public ArrestThiefActivity(MainGame game)
            : base(game, "Content/maps/arrest_thief.tmx", "images/background_nightsky.png")
        {
            foreach (var ent in _entities)
                ent.CollidesWithMap = false;

            foreach (var ent in _entities.OfType<BaseEntity>().Where(CanWalk))
                ent.Behaviors.Add(new WalkLeftRightBehavior(_levelMap, ent));
        }

        private bool CanWalk(BaseEntity entity)
        {
            if (entity is Enemy || (entity is Object && ((Object)entity).Name != "Cat") || entity is NPC)
                return true;

            return false;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (!_entities.OfType<Enemy>().Any())
                Exit(true);
        }

        //public override int GetScoreFor(BaseEntity entity)
        //{
        //    Object obj = entity as Object;
        //    if (obj != null)
        //    {
        //        if (obj.Name == "Thief01") return 3;
        //        else return -1;
        //    }
            
        //    return 0;
        //}
    }
}
