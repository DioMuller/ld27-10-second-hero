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

namespace TenSecondHero.Entities
{
    class Explosion : Object
    {
        public Explosion()
            : base("Explosion", new Vector2(32, 32))
        {
            Sprite.Animations.Add(new Animation("exploding", 0, 0, 1));
            Sprite.ChangeAnimation("exploding");
            var bh = Behaviors.OfType<PickableBehavior>().FirstOrDefault();
            Behaviors.Remove(bh);
        }
    }
}
