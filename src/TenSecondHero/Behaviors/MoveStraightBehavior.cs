using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLib.Core.Entities;
using MonoGameLib.Tiled;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenSecondHero.Entities;

namespace TenSecondHero.Behaviors
{
    class MoveStraightBehavior : Behavior
    {
        public Vector2 Direction { get; set; }
        new BaseEntity Entity { get { return (BaseEntity)base.Entity; } }

        public MoveStraightBehavior(BaseEntity parent, Vector2 diretion)
            : base(parent)
        {
            Direction = diretion;
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            Entity.LastPosition = Entity.Position;
            Entity.Position += Direction;
        }
    }
}
