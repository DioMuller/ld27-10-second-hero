using Microsoft.Xna.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;
using TenSecondHero.Behaviors;
using TenSecondHero.Entities;

namespace TenSecondHero.Activities.GamePlay
{
    class TrainAccidentActivity : GamePlayActivity
    {
        TaskCompletionSource<bool> _trainAccidentEvents;

        NPC Train { get; set; }
        Checkpoint Checkpoint { get; set; }


        public TrainAccidentActivity(MainGame game)
            : base(game, "Content/maps/train_accident.tmx", "images/background_morningsky.png")
        {
            _trainAccidentEvents = new TaskCompletionSource<bool>();

            foreach (var ent in _entities)
                ent.CollidesWithMap = false;

            Train = _entities.OfType<NPC>().First(n => n.Name == "Train");
            Train.Behaviors.Add(new MoveStraightBehavior(Train, new Vector2(1, 0)));

            Checkpoint = _entities.OfType<Checkpoint>().First();
        }

        public override async System.Threading.Tasks.Task<bool> Run()
        {
            return await await Task.WhenAny(ExplosionAsync(), base.Run());
        }

        async Task<bool> ExplosionAsync()
        {
            await _trainAccidentEvents.Task;
            await Bomb.Explode(this, Checkpoint);

            return false;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (Train.BoundingBox.Intersects(Checkpoint.BoundingBox))
                _trainAccidentEvents.TrySetResult(true);

            if (_entities.OfType<Entities.Object>().Count(e => e.Name.Contains("RailRoad")) <= 0)
                Exit(true);
        }
    }
}
