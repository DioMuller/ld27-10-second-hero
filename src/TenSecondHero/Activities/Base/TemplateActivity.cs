using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using TenSecondHero.Core;
using MonoGameLib.Tiled;
using MonoGameLib.Core.Entities;
using System.Collections.Generic;
using TenSecondHero.Entities;

namespace TenSecondHero.Activities
{
    /// <summary>
    /// Default MonoGame project logic, shows how an activity can be created.
    /// </summary>
    class TemplateActivity : Activity<bool>
    {
        /// <summary>
        /// Level map.
        /// </summary>
        protected Map _levelMap;
        protected List<BaseEntity> _entities;


        public TemplateActivity(Game game, string map) : base(game) 
        {
            _levelMap = MapLoader.LoadMap(map);
            _entities = new List<BaseEntity>();
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

            foreach( var ent in _entities )
            {
                ent.Update(gameTime);

                if( _levelMap.Collides(ent.BoundingBox) )
                {
                    ent.Position = ent.LastPosition;
                }
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            SpriteBatch.Begin();
            // TODO: Add your drawing code here
            _levelMap.Draw(gameTime, SpriteBatch, Vector2.Zero);

            foreach (var ent in _entities)
            {
                ent.Draw(gameTime, SpriteBatch);
            }

            SpriteBatch.End();
        }
    }
}
