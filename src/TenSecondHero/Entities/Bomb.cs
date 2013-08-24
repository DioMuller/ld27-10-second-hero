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
    class Bomb : Object
    {
        public Bomb()
            : base("Bomb", new Vector2(16, 16))
        {
            Sprite.Animations.Add(new Animation("active", 0, 0, 1));
            Sprite.ChangeAnimation("active");
        }

        public async Task Explode()
        {
            throw new NotImplementedException();
        }
    }
}
