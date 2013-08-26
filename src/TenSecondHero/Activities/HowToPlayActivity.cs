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
    class HowToPlayActivity : Activity<bool>
    {
        /// <summary>
        /// Level map.
        /// </summary>
        private SpriteFont _font;
        private SpriteFont _smallFont;
        private bool? _lastEscState;

        private string[] _texts = new string[]
        {
            "You have 10 seconds to do hero things!",
            "",
            "Use the Arrow/WASD keys move your hero.",
            "Fly above a target to pick it up,",
            "drop the correct target on the green area.",
            "Get points for completing the correct objective,",
            "lose points for droping the wrong target."
        };
        private int _rnd;

        public HowToPlayActivity(MainGame game)
            : base(game)
        {
            _font = game.Content.Load<SpriteFont>("fonts/DefaultFont");
            _smallFont = game.Content.Load<SpriteFont>("fonts/DefaultFont");
            _rnd = new Random(DateTime.Now.Millisecond).Next(0, 4);
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            var escPressed = GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape);
            if (escPressed && _lastEscState == false)
                Exit(true);
            _lastEscState = escPressed;
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            string msg = "Press [ESC] to return to the Title Screen" ;
            int height = 50;

            GraphicsDevice.Clear(Color.TransparentBlack);
            SpriteBatch.Begin();

            Vector2 size = _smallFont.MeasureString(msg);
            Vector2 position = new Vector2(Game.Window.ClientBounds.Center.X - (size.X / 2), 420);
            height += (int)(size.Y + 10);

            SpriteBatch.DrawString(_smallFont, msg, position, Color.Red);

            

            foreach( string str in _texts )
            {
                size = _font.MeasureString(str);
                position = new Vector2(Game.Window.ClientBounds.Center.X - (size.X / 2), height);
                height += (int)(size.Y + 10);

                SpriteBatch.DrawString(_font, str, position, Color.White);
            }

            SpriteBatch.End();
        }
    }
}
