#region Using Statements
using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using System.Threading.Tasks;
using TenSecondHero.Core;
#endregion

namespace TenSecondHero
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public partial class MainGame : Game
    {
        #region Attributes
        Stack<ActivityRuntime> activityStack = new Stack<ActivityRuntime>();
        ActivityRuntime currentActivity;
        #endregion

        #region MonoGame
        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (currentActivity != null)
            {
                using (currentActivity.Activate())
                {
                    currentActivity.Level.Update(gameTime);
                    currentActivity.RunEvents();
                }
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            if (currentActivity != null)
            {
                using (currentActivity.Activate())
                    currentActivity.Level.Draw(gameTime);
            }

            base.Draw(gameTime);
        }
        #endregion

        public Task<T> Run<T>(IActivity<T> level)
        {
            var oldLevel = activityStack.LastOrDefault();
            if (oldLevel != null)
            {
                using (oldLevel.Activate())
                    oldLevel.Level.Deactivated();
            }

            currentActivity = new ActivityRuntime(level);
            activityStack.Push(currentActivity);

            using (currentActivity.Activate())
            {
                level.Started();
                level.Activated();

                return level.Run().ContinueWith(t =>
                {
                    using (currentActivity.Activate())
                    {
                        level.Deactivated();
                        level.Completed();
                    }

                    currentActivity = oldLevel;

                    if (oldLevel != null)
                    {
                        using (oldLevel.Activate())
                            oldLevel.Level.Activated();
                    }

                    return t.Result;
                }, TaskScheduler.FromCurrentSynchronizationContext());
            }
        }
    }
}
