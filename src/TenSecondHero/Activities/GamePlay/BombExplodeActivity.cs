using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using TenSecondHero.Entities;

namespace TenSecondHero.Activities.GamePlay
{
    class BombExplodeActivity : GamePlayActivity
    {
        public BombExplodeActivity(Game game)
            : base(game, "Content/maps/transform.tmx")
        {
            
        }
    }
}
