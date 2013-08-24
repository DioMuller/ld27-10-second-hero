using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace TenSecondHero.Activities
{
    class TransformActivity : TemplateActivity
    {
        public TransformActivity(Game game) : base(game, "Content/maps/transform.tmx")
        {
        }
    }
}
