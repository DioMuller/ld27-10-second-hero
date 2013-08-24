using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using MonoGameLib.Core.Entities;
using MonoGameLib.Core.Input;
using MonoGameLib.Core.Sprites;
using TenSecondHero.Behaviors;
using TenSecondHero.Activities.GamePlay;

namespace TenSecondHero.Entities
{
    class Bomb : Object
    {
        GamePlayActivity _level;

        public Bomb(GamePlayActivity level)
            : base("Bomb", new Vector2(16, 16))
        {
            _level = level;
            Sprite.Animations.Add(new Animation("active", 0, 0, 1));
            Sprite.ChangeAnimation("active");
        }

        public async Task Explode()
        {
            var explosion = new Explosion();
            explosion.Position = new Vector2(Position.X - (explosion.Size.X - Size.X) / 2, Position.Y - (explosion.Size.Y - Size.Y));

            _level.AddEntity(explosion);
            _level.RemoveEntity(this);
            await Task.Delay(1000);
        }
    }
}
