using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using MonoGameLib.Core.Entities;

namespace TenSecondHero.Entities
{
    class BaseEntity : Entity
    {
        public bool CollidesWithMap { get; set; }

        public Vector2 BoundingSize { get; set; }
        public Rectangle BoundingBox
        {
            get
            {
                return new Rectangle( (int) Position.X, (int) Position.Y , (int) BoundingSize.X, (int) BoundingSize.Y );
            }
        }
        public Vector2 LastPosition { get; set; }

        public BaseEntity()
        {
            CollidesWithMap = true;
        }
    }
}
