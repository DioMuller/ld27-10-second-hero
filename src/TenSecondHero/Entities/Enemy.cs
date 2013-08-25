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
    class Enemy : Object
    {
        public Enemy(string name, Vector2 size)
            : base(name, size)
        {
        }
    }
}
