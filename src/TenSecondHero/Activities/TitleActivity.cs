using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TenSecondHero.Core;

namespace TenSecondHero.Activities
{
    class TitleActivity  : Activity<bool>
    {
        /// <summary>
        /// Level map.
        /// </summary>
        private Texture2D _titleTexture;
        private SpriteFont _font;
        private bool? _lastEscState;

        public TitleActivity(MainGame game) : base(game)
        {
            _titleTexture = game.Content.Load<Texture2D>("images/title_screen.png");
            _font = game.Content.Load<SpriteFont>("fonts/DefaultFont");
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Start == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Enter))
                Exit(true);

            if (Keyboard.GetState().IsKeyDown(Keys.F1))
            {
                Game.ShowHowToPlay = true;
                Exit(true);
            }

            if( Keyboard.GetState().IsKeyDown(Keys.F2) )
            {
                Game.ShowCredits = true;
                Exit(true);
            }

            var escPressed = Keyboard.GetState().IsKeyDown(Keys.Escape);
            if (escPressed && _lastEscState == false)
            {
                Game.Exit();
            }

            _lastEscState = escPressed;
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            string msg = "[ENTER] Start Game - [F1] How to Play";

            GraphicsDevice.Clear(Color.TransparentBlack);
            SpriteBatch.Begin();
            SpriteBatch.Draw(_titleTexture, Vector2.Zero, Color.White);

            Vector2 size = _font.MeasureString(msg);
            Vector2 position = new Vector2(Game.Window.ClientBounds.Center.X - (size.X / 2), 420);
            SpriteBatch.DrawString(_font, msg, position, Color.Black);

            msg = "[F2] Credits - [ESC] Quit Game";
            size = _font.MeasureString(msg);
            position = new Vector2(Game.Window.ClientBounds.Center.X - (size.X / 2), 440);
            SpriteBatch.DrawString(_font, msg, position, Color.Black);

            SpriteBatch.End();
        }
    }
}
