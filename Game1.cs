using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TriangleGame.GameObjects;
using TriangleGame.Manager;

namespace TriangleGame
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private Texture2D test;
        private Texture2D outerTowerTexture;
        private Texture2D innerTowerTexture;
        
        private SpriteFont Font;
        private TowerManager _towerManager;
        
        private Camera _camera;
        private Rectangle _boundaries;

        private ButtonState lastState = ButtonState.Released;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            IsMouseVisible = true;
            Content.RootDirectory = @"Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
            
            _boundaries = new Rectangle(0, 0, 2000, 2000);
            _camera = new Camera(_boundaries.Center.ToVector2(), 2);
            
            _towerManager = new TowerManager();
            _towerManager.GenerateOres(_boundaries, test);
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            test = Content.Load<Texture2D>("pixel");
            innerTowerTexture = Content.Load<Texture2D>("tower_inner");
            outerTowerTexture = Content.Load<Texture2D>("tower_outer");
            Font = Content.Load<SpriteFont>("basicfont");
            var connector = new Connector(test);
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (!IsActive) return;

            _camera.Update();
            if (_camera.Position.Y + graphics.PreferredBackBufferHeight > _boundaries.Location.Y + _boundaries.Size.Y)
            {
                _camera.Move(0,
                    _boundaries.Location.Y + _boundaries.Size.Y -
                    (_camera.Position.Y + graphics.PreferredBackBufferHeight));
            }
            else if (_camera.Position.Y < _boundaries.Location.Y)
            {
                _camera.Move(0, _boundaries.Location.Y - _camera.Position.Y);
            }
            else if (_camera.Position.X < _boundaries.Location.X)
            {
                _camera.Move(_boundaries.Location.X - _camera.Position.X, 0);
            }
            else if (_camera.Position.X + graphics.PreferredBackBufferWidth >
                     _boundaries.Location.X + _boundaries.Size.X)
            {
                _camera.Move(
                    _boundaries.Location.X + _boundaries.Size.X -
                    (_camera.Position.X + graphics.PreferredBackBufferWidth), 0);
            }


            if (_camera.Position.Y < _boundaries.Location.Y + _boundaries.Size.Y)
            {
            }


            if (Mouse.GetState().LeftButton == ButtonState.Pressed && lastState == ButtonState.Released)
            {
                Vector2 position = Mouse.GetState().Position.ToVector2() + _camera.Position;
                TowerType type = TowerType.Default;
                if (_towerManager.Towers.Count == 0)
                {
                    type = TowerType.Base;
                }
                else
                {
                    Random rand = new Random();
                    int t = rand.Next() % 3;
                    switch (t)
                    {
                        case 0: type = TowerType.Default;
                            break;
                        case 1: type = TowerType.Attacker;
                            break;
                        case 2: type = TowerType.Collector;
                            break;
                    }
                }

                Tower newTower = new Tower(position.ToPoint(), innerTowerTexture, outerTowerTexture, Color.Red, type);

                if (_towerManager.AddTower(newTower)) //Falls der Turm platziert werden kann
                {
                    _towerManager.Connect(newTower);
                }
            }

            lastState = Mouse.GetState().LeftButton;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //MAP
            spriteBatch.Begin(transformMatrix: Matrix.CreateTranslation(new Vector3(-_camera.Position, 0)));

            foreach (var ore in _towerManager.Ores)
            {
                ore.Draw(spriteBatch);
            }

            foreach (var area in _towerManager.Areas)
            {
                area.Draw(spriteBatch);
            }

            foreach (var tower in _towerManager.Towers)
            {
                tower.Draw(spriteBatch);
            }


            spriteBatch.End();

            //UI
            spriteBatch.Begin();

            spriteBatch.DrawString(Font, "Area: " + _towerManager.SumArea(), new Vector2(10, 10), Color.White);
            spriteBatch.DrawString(Font, "Camera Position: " + _camera.Position.ToString(), new Vector2(10, 25),
                Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}