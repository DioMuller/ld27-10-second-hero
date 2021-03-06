﻿using System;
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
    class NPC : BaseEntity
    {
        public string Name { get; private set; }

        public NPC(string name, Vector2 size)
            : base()
        {
            Name = name;

            //TODO: Add behaviors;
            //Behaviors.Add(new PickableBehavior(this));

            Sprite = new Sprite("sprites/" + name.ToLower() + ".png", new Point((int) size.X, (int) size.Y), 0);

            BoundingSize = size;

            Sprite.Animations.Add(new Animation("default", 0, 0, 0));
            Sprite.ChangeAnimation(0);
        }
    }
}
