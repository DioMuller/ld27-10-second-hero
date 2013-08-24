using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using MonoGameLib.Core.Sprites;

namespace TenSecondHero.Entities
{
    class Checkpoint : BaseEntity
    {
        public Checkpoint(Vector2 size, string name) : base()
        {
            Sprite = new Sprite("sprites/" + name.ToLower() + ".png", new Point((int)size.X, (int)size.Y), 0);

            BoundingSize = size;

            Sprite.Animations.Add(new Animation("default", 0, 0, 0));
            Sprite.ChangeAnimation(0);
        }
    }
}
