using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TenSecondHero.Core
{
    public abstract class Activity<T> : IActivity<T>
    {
        #region Nested
        class DeferedLevel : Activity<T>
        {
            Func<Task<T>> run;
            Action<GameTime> update;
            Action<GameTime> draw;

            public DeferedLevel(Game game, Func<Task<T>> run, Action<GameTime> update, Action<GameTime> draw)
                : base(game)
            {
                this.update = update;
                this.draw = draw;
                this.run = run;
            }

            public override Task<T> Run()
            {
                if (this.run == null)
                    throw new InvalidOperationException();
                return this.run();
            }

            public override void Update(GameTime gameTime)
            {
                if (this.update != null)
                    this.update(gameTime);
            }

            public override void Draw(GameTime gameTime)
            {
                if (this.draw != null)
                    this.draw(gameTime);
            }
        }
        #endregion

        #region Static
        public static Activity<T> Create(Game game, Func<Task<T>> play = null, Action<GameTime> update = null, Action<GameTime> draw = null)
        {
            return new DeferedLevel(game, play, update, draw);
        }
        #endregion

        #region Attributes
        TaskCompletionSource<T> _levelCompletion;
        #endregion

        #region Properties
        protected Game Game { get; private set; }
        protected ContentManager Content { get { return Game == null ? null : Game.Content; } }
        protected GraphicsDevice GraphicsDevice { get { return Game == null ? null : Game.GraphicsDevice; } }
        protected SpriteBatch SpriteBatch { get; private set; }
        #endregion

        #region Constructors
        public Activity(Game game)
        {
            Game = game;
            SpriteBatch = new SpriteBatch(GraphicsDevice);
            _levelCompletion = new TaskCompletionSource<T>();
        }
        #endregion

        #region Life Cycle
        public virtual void Deactivated() { }
        public virtual void Started() { }
        public virtual void Activated() { }
        public virtual void Completed() { }
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(GameTime gameTime);
        #endregion

        #region Run
        public virtual Task<T> Run()
        {
            return _levelCompletion.Task;
        }

        Task IActivity.Run()
        {
            return Run();
        }

        protected void Exit(T result)
        {
            _levelCompletion.TrySetResult(result);
        }
        #endregion
    }
}
