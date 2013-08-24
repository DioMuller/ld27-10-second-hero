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

        public MainGame()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            GameContent.Initialize(this.Content);

            Window.Title = "LD27 - Ten Seconds Hero!";
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
                case 0: return new SaveActivity01(this);
                case 1: return new BombExplodeActivity(this);
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
#if !DEBUG
            int startLevel = 0;
#else
            int startLevel = 0;
#endif

            // TODO: Run logo/intro, start screen, gameplay/settings.
            //await Run(new IntroActivity(this));
            while (true)
            {
                /*StartOptions startOption;
                do
                {
                    startOption = await Run(new StartScreenActivity(this));
                    if (startOption == StartOptions.Quit)
                        break;
                    if (startOption == StartOptions.Help)
                        await Run(new HelpScreenActivity(this));
                } while (startOption != StartOptions.Play);

                if (startOption == StartOptions.Quit)
                    break;*/

                var extraLives = 3;

                int currentLevelNumber = startLevel;

                var level = LoadLevel(currentLevelNumber);

                while (extraLives >= 0)
                {
                    if (level == null) // If there are no more levels to run
                    {
                        //await Run(new ShowEndingActivity(this, extraLives));
                        break;
                    }

                    var result = await RunLevel(level);

                    if (result == LevelResult.RestartGame)
                        break;

                    if (result == LevelResult.Succeded)
                        currentLevelNumber++;
                    else
                        extraLives--;

                    level = LoadLevel(currentLevelNumber);

                    //if (extraLives >= 0 && level != null)
                    //    await Run(new ShowResultsActivity(this, extraLives, result));
                }

                /*if (extraLives < 0)
                    await Run(new GameOverActivity(this));*/
            }

            Exit();
            return true;
        }

        private async Task<LevelResult> RunLevel(GamePlayActivity level)
        {
            var cancelAutoFail = new CancellationTokenSource();
            var timeOut = Task.Delay(TimeSpan.FromSeconds(10), cancelAutoFail.Token);
            var runLevel = Run(level);

            using (currentActivity.Activate())
            {
                if (await Task.WhenAny(runLevel, timeOut) == timeOut)
                    level.OnTimeout();
            }

            cancelAutoFail.Cancel();
            return await runLevel;
        }
    }
}
