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
using MonoGameLib.Tiled;

namespace TenSecondHero.Entities
{
    class Player : BaseEntity
    {
        public Player(Map map) : base()
        {
            //TODO: Add behaviors;
            Behaviors.Add(new ControllableBehavior(map, this, new KeyboardInput()));

            Sprite = new Sprite("sprites/10sechero.png", new Point(16, 48), 0);
            Sprite.Origin = new Vector2(8, 24);
            Rotation = 1.5f;

            BoundingSize = new Vector2(48f, 16f);

            Sprite.Animations.Add(new Animation("default", 0, 0, 0));
            Sprite.ChangeAnimation(0);
        }
    }
}
