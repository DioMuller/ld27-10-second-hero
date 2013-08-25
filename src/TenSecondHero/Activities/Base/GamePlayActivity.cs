using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using TenSecondHero.Core;
using MonoGameLib.Tiled;
using MonoGameLib.Core.Entities;
using System.Collections.Generic;
using TenSecondHero.Entities;
using System.Linq;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLib.Core;
using System;
using TenSecondHero.Behaviors;

namespace TenSecondHero.Activities.GamePlay
{
    /// <summary>
    /// Default MonoGame project logic, shows how an activity can be created.
    /// </summary>
    class GamePlayActivity : Activity<bool>
    {
        /// <summary>
        /// Level map.
        /// </summary>
        protected Map _levelMap;
        protected List<BaseEntity> _entities;
        protected Stack<BaseEntity> _toRemoveEntity, _toAddEntity;
        protected Texture2D _background;
        protected SpriteFont _font;
        protected SpriteFont _bigfont;
        private bool? _lastEscState;

        public string Description { get; set; }

        public GamePlayActivity(MainGame game, string map, string background)
            : base(game)
        {
            _levelMap = MapLoader.LoadMap(map);
            _entities = new List<BaseEntity>();
            _toRemoveEntity = new Stack<BaseEntity>();
            _toAddEntity = new Stack<BaseEntity>();
            _background = game.Content.Load<Texture2D>(background);
            _font = game.Content.Load<SpriteFont>("fonts/DefaultFont");
            _bigfont = game.Content.Load<SpriteFont>("fonts/BigFont");

            foreach (GameObject obj in _levelMap.Objects)
            {
                if (obj.Category == "Player")
                {
                    _entities.Add(new Player(_levelMap) { Position = obj.Position });
                }
                else if (obj.Category == "Item")
                {
                    _entities.Add(new TenSecondHero.Entities.Object(obj.Name, obj.Size) { Position = obj.Position });
                }
                else if (obj.Category == "NPC")
                {
                    _entities.Add(new NPC(obj.Name, obj.Size) { Position = obj.Position });
                }
                else if (obj.Category == "Enemy")
                {
                    if (obj.Name == "Bomb")
                    {
                        _entities.Add(new Bomb(this) { Position = obj.Position });
                    }
                    else
                    {
                        _entities.Add(new TenSecondHero.Entities.Enemy(obj.Name, obj.Size) { Position = obj.Position });
                    }
                }
                else if (obj.Category == "Checkpoint")
                {
                    _entities.Add(new Checkpoint(obj.Size, obj.Name) { Position = obj.Position });
                }
            }
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
                Exit(false);
            _lastEscState = escPressed;

            while (_toAddEntity.Count != 0)
            {
                _entities.Add(_toAddEntity.Pop());
            }

            while (_toRemoveEntity.Count != 0)
            {
                _entities.Remove(_toRemoveEntity.Pop());
            }

            foreach (var ent in _entities)
            {
                ent.Update(gameTime);

                if (ent.CollidesWithMap && _levelMap.Collides(ent.BoundingBox))
                {
                    ent.Position = ent.LastPosition;
                }

                if (ent is Player)
                {
                    foreach (var obj in _entities.OfType<BaseEntity>())
                    {
                        if (obj.GetBehavior<PickableBehavior>() == null)
                            continue;

                        if (obj.BoundingBox.Intersects(ent.BoundingBox))
                        {
                            if (obj.Parent == null)
                            {
                                PickupScore(obj);
                                SoundManager.PlaySound("pickup");
                            }

                            obj.Parent = ent;
                        }
                    }
                }

                if (ent is TenSecondHero.Entities.Object)
                {
                    foreach (var chk in _entities.Where(e => e != ent && (e is Checkpoint || e is Explosion)))
                    {
                        if (chk.BoundingBox.Intersects(ent.BoundingBox))
                        {
                            if (chk is Checkpoint)
                            {
                                SoundManager.PlaySound("drop");
                                DropScore(ent);
                            }
                            _toRemoveEntity.Push(ent);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.BlueViolet);

            SpriteBatch.Begin();
            SpriteBatch.Draw(_background, Vector2.Zero, Color.White);
            _levelMap.Draw(gameTime, SpriteBatch, Vector2.Zero);

            foreach (var ent in _entities.OrderBy(e => e.Position.Y + e.Size.Y))
            {
                ent.Draw(gameTime, SpriteBatch);
            }

            Vector2 textSize = _font.MeasureString(Description);
            Vector2 position = new Vector2( 10, Game.Window.ClientBounds.Height - textSize.Y - 10);
            SpriteBatch.DrawString(_font, Description, position, Color.White);
            SpriteBatch.DrawString(_bigfont, Math.Ceiling(Game.RemainingTime.TotalSeconds).ToString(), Vector2.One * 5, Color.Red); 
            
            string score = "Score:" + Game.Score.ToString();
            position = new Vector2( Game.Window.ClientBounds.Width - 10 - _bigfont.MeasureString(score).X, 10);
            SpriteBatch.DrawString(_bigfont, score , position, Color.Black);

            SpriteBatch.End();
        }

        public virtual void OnTimeout()
        {
            Exit(false);
        }

        /// <summary>
        /// Add an entity to the level.
        /// </summary>
        /// <param name="entity">Entity to be added.</param>
        public void AddEntity(BaseEntity entity)
        {
            _toAddEntity.Push(entity);
        }

        /// <summary>
        /// Removes one entity from the level.
        /// </summary>
        /// <param name="entity">Entity to be removed.</param>
        internal void RemoveEntity(BaseEntity entity)
        {
            _toRemoveEntity.Push(entity);
        }

        /// <summary>
        /// Adds pickup score.
        /// </summary>
        /// <param name="picked"></param>
        internal void PickupScore(BaseEntity picked)
        {
            int score = GetScoreFor(picked);

            Game.Score += score;
        }

        /// <summary>
        /// Adds drop score and a few seconds.
        /// </summary>
        /// <param name="droped"></param>
        internal void DropScore(BaseEntity droped)
        {
            int score = GetScoreFor(droped) * 3;

            Game.Score += score;
        }

        public virtual int GetScoreFor(BaseEntity entity)
        {
            return 1;
        }
    }
}
