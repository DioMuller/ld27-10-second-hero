using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using TenSecondHero.Entities;

namespace TenSecondHero.Activities
{
    class TransformActivity : GamePlayActivity
    {
        public TransformActivity(Game game) : base(game, "Content/maps/transform.tmx")
        {
            foreach( var obj in _levelMap.Objects )
            {
                if( obj.Category == "Player" )
                {
                    _entities.Add( new Player() { Position = obj.Position } );
                }
            }
        }
    }
}
