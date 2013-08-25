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
    class CreditsActivity : Activity<bool>
    {
        /// <summary>
        /// Level map.
        /// </summary>
        private SpriteFont _font;
        private SpriteFont _smallFont;

        private string[] _texts = new string[] { "Diogo Muller (Programming)", "João Vitor (Programming)", "Melanie Young (Art)", "Moisés 'Musashi' Santana (Music)" };
        private int _rnd;

        public CreditsActivity(MainGame game)
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit(true);
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

            SpriteBatch.DrawString(_smallFont, msg, position, Color.White);

            

            for( int i = 0; i < _texts.Length; i++ )
            {
                string str = _texts[(i + _rnd) % _texts.Length];
                size = _font.MeasureString(str);
                position = new Vector2(Game.Window.ClientBounds.Center.X - (size.X / 2), height);
                height += (int)(size.Y + 10);

                SpriteBatch.DrawString(_font, str, position, Color.White);
            }

            SpriteBatch.End();
        }
    }
}
