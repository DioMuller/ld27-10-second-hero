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
    class Bomb : Enemy
    {
        GamePlayActivity _level;

        public Bomb(GamePlayActivity level)
            : base("Bomb", new Vector2(16, 16))
        {
            _level = level;
            Sprite.Animations.Add(new Animation("active", 0, 0, 1));
            Sprite.ChangeAnimation("active");
        }

        public Task Explode()
        {
            return Explode(_level, this);
        }

        public async static Task Explode(GamePlayActivity level, BaseEntity obj, bool removeEntity = true, uint duration = 1000)
        {
            var explosion = new Explosion { CollidesWithMap = false };
            explosion.Position = new Vector2(obj.Position.X - (explosion.Size.X - obj.Size.X) / 2, obj.Position.Y - (explosion.Size.Y - obj.Size.Y));

            level.AddEntity(explosion);
            if(removeEntity)
                level.RemoveEntity(obj);
            await Task.Delay((int)duration);
            level.RemoveEntity(explosion);
        }
    }
}
