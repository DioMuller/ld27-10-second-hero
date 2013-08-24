using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using MonoGameLib.Core.Entities;
using TenSecondHero.Entities;

namespace TenSecondHero.Behaviors
{
    class PickableBehavior : Behavior
    {
        public BaseEntity Parent
        {
            get { return Entity as BaseEntity; }
        }

        public PickableBehavior(Entity parent) : base(parent)
        {
        }

        public override void Update(GameTime gameTime)
        {
            if( Parent.Parent != null )
            {            
                Parent.Position = Parent.Parent.Position;
                Parent.LastPosition = Parent.Position; //Yes, it will not collide. Ugly workaround.
            }
        }
    }
}
