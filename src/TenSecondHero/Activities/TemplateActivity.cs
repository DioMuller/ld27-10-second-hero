using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using TenSecondHero.Core;

namespace TenSecondHero.Activities
{
    /// <summary>
    /// Default MonoGame project logic, shows how an activity can be created.
    /// </summary>
    class TemplateActivity : Activity<bool>
    {
        public TemplateActivity(Game game) : base(game) { }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit(true);

            // TODO: Add your update logic here
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
        }
    }
}
