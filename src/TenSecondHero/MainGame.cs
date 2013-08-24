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
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        TaskCompletionSource<bool> timeOutCompletionTask;

        public TimeSpan GameTimeOut { get; private set; }
        public TimeSpan RemainingTime { get { return GameTimeOut - GameTime.TotalGameTime; } }

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
            // TODO: Add your initialization logic here
            Run(Activity<bool>.Create(this, Play));

            graphics.PreferredBackBufferWidth = 640;
            graphics.PreferredBackBufferHeight = 480;

            base.Initialize();
        }

        #region Content
        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }
        #endregion

        GamePlayActivity LoadLevel(int levelNumber)
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

        /// <summary>
        /// Controls the game activities sequence, from intro to ending,
        /// including gameplay and settings activity.
        /// </summary>
        /// <returns>true</returns>
        async Task<bool> Play()
        {
            int levelCount = 8;
            var rnd = new Random(Environment.TickCount);

            // TODO: Run logo/intro, start screen, gameplay/settings.
            //await Run(new IntroActivity(this));
            while (true)
            {
                SoundManager.PlayBGM("credits");
                await Run(new TitleActivity(this));

                SoundManager.PlayBGM(rnd.Next()%2 == 0? "no name" : "save me");
                var levelOrder = Enumerable.Range(0, levelCount).OrderBy(n => rnd.Next());

                var timeOutTask = StartTimeout();
                foreach (var levelNumber in levelOrder)
                {
                    var level = LoadLevel(levelNumber);
                    var succeded = await RunLevel(level, timeOutTask);

                    if (!succeded)
                        break;
                }

                //if (!gameTimeoutTask.IsCompleted)
                //    await Run(new EndingActivity(this));
            }

            Exit();
            return true;
        }

        private Task StartTimeout()
        {
            GameTimeOut = GameTime.TotalGameTime + TimeSpan.FromSeconds(10);
            timeOutCompletionTask = new TaskCompletionSource<bool>();
            return timeOutCompletionTask.Task;
        }

        private async Task<bool> RunLevel(GamePlayActivity level, Task timeOut)
        {
            var runLevel = Run(level);

            using (currentActivity.Activate())
            {
                if (await Task.WhenAny(runLevel, timeOut) == timeOut)
                    level.OnTimeout();
            }

            return await runLevel;
        }
    }
}
