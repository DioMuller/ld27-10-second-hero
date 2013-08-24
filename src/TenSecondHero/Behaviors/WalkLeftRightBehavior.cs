using Microsoft.Xna.Framework;
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
    class WalkLeftRightBehavior : Behavior
    {
        static int Id;

        public Map Map { get; private set; }
        float _walkDirection = 1;

        float _minSpeed = 1f;
        float _maxSpeed = 2;

        new BaseEntity Entity { get { return (BaseEntity)base.Entity; } }

        public WalkLeftRightBehavior(Map map, BaseEntity parent) : base(parent)
        {
            var rnd = new Random(Id++ + Environment.TickCount);
            _walkDirection = _minSpeed + (float)(rnd.NextDouble()) * (_maxSpeed - _minSpeed);
            if (rnd.Next(0, 2) == 0)
                _walkDirection *= -1;
            Map = map;
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            Entity.LastPosition = Entity.Position;
            Entity.Position += new Vector2(_walkDirection, 0);
            if (Map.IsOutsideBorders(Entity.BoundingBox))
            {
                _walkDirection *= -1;
                Entity.Position = Entity.LastPosition + new Vector2(_walkDirection, 0);
            }
        }
    }
}
