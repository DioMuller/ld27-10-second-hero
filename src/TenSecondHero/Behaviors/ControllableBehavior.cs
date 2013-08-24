using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using MonoGameLib.Core.Entities;
using MonoGameLib.Core.Input;
using TenSecondHero.Entities;

namespace TenSecondHero.Behaviors
{
    class ControllableBehavior : Behavior
    {
        private GenericInput _input;

        public BaseEntity Parent
        {
            get { return Entity as BaseEntity; }
        }

        public ControllableBehavior(Entity parent, GenericInput input) : base(parent)
        {
            _input = input;
        }

        /// <summary>
        /// Updates the Controllable entity.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            Parent.LastPosition = Parent.Position;
            Parent.Position += _input.LeftDirectional * 8;
        }
    }
}
