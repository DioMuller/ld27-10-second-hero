#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using System.Threading.Tasks;
using TenSecondHero.Core;
using TenSecondHero.Activities;
using MonoGameLib.Core;
using TenSecondHero.Activities.GamePlay;
using System.Threading;
using System.Linq;
using System.Diagnostics;
#endregion

namespace TenSecondHero
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public partial class MainGame : Game
    {
        const int LevelCount = 8;
        static readonly Random Random = new Random(Environment.TickCount);

        GraphicsDeviceManager graphics;
        TaskCompletionSource<bool> timeOutCompletionTask;

        public TimeSpan GameTimeOut { get; set; }
        public TimeSpan RemainingTime { get { return GameTimeOut - GameTime.TotalGameTime; } }

        public int Score { get; set; }
        public int Levels { get; set; }

        public MainGame()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            GameContent.Initialize(this.Content);

            Window.Title = "LD27 - Ten Seconds Hero!";
            SoundManager.SEFolder = "se";
            SoundManager.BGMFolder = "bgm";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = 640;
            graphics.PreferredBackBufferHeight = 480;

            Run(Activity<bool>.Create(this, Play));

            base.Initialize();
        }

        /// <summary>
        /// Controls the game activities sequence, from intro to ending,
        /// including gameplay and settings activity.
        /// </summary>
        /// <returns>true</returns>
        async Task<bool> Play()
        {
            bool exit = false;
            while (!exit)
            {
                switch (await ShowTitle())
                {
                    case TitleResult.Credits:
                        await ShowCredits();
                        break;
                    case TitleResult.HowToPlay:
                        await ShowHowToPlay();
                        break;
                    case TitleResult.Play:
                        await RunGamePlay();
                        break;
                    case TitleResult.Exit:
                        exit = true;
                        break;
                }
            }

            Exit();
            return true;
        }

        #region Screens
        private async Task<TitleResult> ShowTitle()
        {
            SoundManager.PlayBGM("Press Start");
            return await Run(new TitleActivity(this));
        }

        private async Task ShowHowToPlay()
        {
            SoundManager.PlayBGM("Credits");
            await Run(new HowToPlayActivity(this));
        }

        private async Task ShowCredits()
        {
            SoundManager.PlayBGM("Credits");
            await Run(new CreditsActivity(this));
        }

        private async Task RunGamePlay()
        {
            SoundManager.PlayBGM(Random.Next() % 2 == 0 ? "We Don't Need a Hero" : "Save Me");
            var levelOrder = Enumerable.Range(0, LevelCount).OrderBy(n => Random.Next());

            Score = 0;
            Levels = 0;

            var timeOutTask = StartTimeout();
            foreach (var levelNumber in levelOrder)
            {
                var succeded = await RunLevel(levelNumber, timeOutTask);
                if (!succeded)
                    break;

                Levels++;
            }

            await ShowScore();
        }

        private async Task ShowScore()
        {
            SoundManager.PlayBGM("Score Time");
            await Run(new ScoreActivity(this));
        }
        #endregion

        #region Helpers
        private Task StartTimeout()
        {
            GameTimeOut = GameTime.TotalGameTime + TimeSpan.FromSeconds(10);
            timeOutCompletionTask = new TaskCompletionSource<bool>();
            return timeOutCompletionTask.Task;
        }

        private GamePlayActivity LoadLevel(int levelNumber)
        {
            switch (levelNumber)
            {
                case 0: return new SaveActivity01(this) { Description = "Save the boy!" };
                case 1: return new BombExplodeActivity(this) { Description = "Discard the bomb!" };
                case 2: return new CatActivity(this) { Description = "Rescue the kitty!" };
                case 3: return new SaveActivity02(this) { Description = "Save everyone!" };
                case 4: return new ArrestThiefActivity(this) { Description = "Arrest the thief" };
                case 5: return new GrandmaCrossingActivity(this) { Description = "Help grandma to cross the street!" };
                case 6: return new GrandmaFriendActivity(this) { Description = "Help grandma meet her... 'friend'." };
                case 7: return new TrainAccidentActivity(this) { Description = "Save the train from the accident" };
            }
            return null;
        }

        private async Task<bool> RunLevel(int levelNumber, Task timeOut)
        {
            var level = LoadLevel(levelNumber);
            var runLevel = Run(level);

            using (currentActivity.Activate())
            {
                if (await Task.WhenAny(runLevel, timeOut) == timeOut)
                    level.OnTimeout();
            }

            return await runLevel;
        }
        #endregion
    }
}
