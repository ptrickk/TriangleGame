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

        private TowerManager _towerManager;
        private TextureManager _textureManager;
        private UIManager _uiManager;

        private Camera _camera;
        private Rectangle _boundaries;

        private ButtonState lastState = ButtonState.Released;

        private int test = 0;
        
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
            _towerManager.GenerateOres(_boundaries, _textureManager.Sprites["pixel"]);

            _uiManager = new UIManager(graphics, new Point(64, 64));
            _uiManager.Initialize();
        }

        protected override void LoadContent()
        {
            _textureManager = TextureManager.Instance;
            _textureManager.LoadContent(Content);
            spriteBatch = new SpriteBatch(GraphicsDevice);
            var connector = new Connector(_textureManager.Sprites["pixel"]);
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            test++;
            _uiManager.Update(test * 10, test * 100, test * 2);
            
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
                if (!_uiManager.UiInteraction(Mouse.GetState().Position))
                {
                    Vector2 position = Mouse.GetState().Position.ToVector2() + _camera.Position;
                    TowerType type = _uiManager.SelectedTower();
                    
                    if (_towerManager.Towers.Count == 0)
                    {
                        type = TowerType.Base;
                    }

                    Tower newTower = new Tower(position.ToPoint(), _textureManager.Sprites["innerTower"],
                        _textureManager.Sprites["outerTower"], Color.Red, type);

                    if (_towerManager.AddTower(newTower)) //Falls der Turm platziert werden kann
                    {
                        if (_towerManager.Towers.Count < 3)
                        {
                            _towerManager.Connect(newTower, true);
                        }
                        else
                        {
                            _towerManager.Connect(newTower);
                        }
                    }
                }
                
                
            }

            lastState = Mouse.GetState().LeftButton;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(218, 105, 50, 255));

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

            spriteBatch.DrawString(_textureManager.Fonts["basicfont"], "Area: " + _towerManager.SumArea(),
                new Vector2(10, 10), Color.White);
            spriteBatch.DrawString(_textureManager.Fonts["basicfont"],
                "Camera Position: " + _camera.Position.ToString(), new Vector2(10, 25),
                Color.White);
            
            _uiManager.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}