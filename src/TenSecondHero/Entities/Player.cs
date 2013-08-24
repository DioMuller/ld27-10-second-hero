using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using MonoGameLib.Core.Entities;
using MonoGameLib.Core.Sprites;

namespace TenSecondHero.Entities
{
    class Player : Entity
    {
        public Player() : base()
        {
            //TODO: Add behaviors;

            Sprite = new Sprite("sprites/10sechero.png", new Point(16, 48), 0);

            Sprite.Animations.Add(new Animation("default", 0, 0, 0));
            Sprite.ChangeAnimation(0);
        }
    }
}
