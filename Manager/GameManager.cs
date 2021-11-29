using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TriangleGame.GameObjects;
using TriangleGame.Resources;

namespace TriangleGame.Manager
{
    public class GameManager
    {
        //Game and UI control
        private TowerManager _towerManager;
        private UIManager _uiManager;
        private Game1 _game1;
        
        //Camera controls
        private Camera _camera;
        private Rectangle _boundaries;

        private ButtonState _lastState = ButtonState.Released;
        
        private Dictionary<string, Resource> _resources;

        private GraphicsDeviceManager _graphics;

        private int _lastIntervall = 0;
        
        public GameManager(Game1 game1)
        {
            _resources = new Dictionary<string, Resource>();
            _game1 = game1;
        }

        public void StartGame()
        {
            _lastIntervall = 0;
        }

        public void AssignCollector(Tower collector)
        {
            List<Area> areas = _towerManager.GetAreasOfTower(collector);
            List<Ore> ores = new List<Ore>();
            foreach (var area in areas)
            {
                ores.AddRange(_towerManager.GetUnoccupiedOresInArea(area));
            }

            foreach (var ore in ores)
            {
                if (!ore.Occupied)
                {
                    collector.Occupie(ore);
                    Console.WriteLine("succesfull occupation!!!");
                    return;
                }
            }
        }

        public void AddRessources(Dictionary<string, int> res)
        {
            foreach (var pair in res)
            {
                if (_resources.ContainsKey(pair.Key))
                {
                    _resources[pair.Key].IncreaseAmount(pair.Value);
                }
            }
        }

        public void Initialize()
        {
            Texture2D resourceTexture = TextureManager.Instance.Sprites["pixel"];
            _resources.Add("metal", new Resource(resourceTexture, ResourceType.Metal, 100));
            _resources.Add("gas", new Resource(resourceTexture, ResourceType.Gas, 100));
            _resources.Add("crystal",new Resource(resourceTexture, ResourceType.Crystals, 100));
            
            _boundaries = new Rectangle(0, 0, 2000, 2000);
            _camera = new Camera(_boundaries.Center.ToVector2(), 2);

            _towerManager = new TowerManager();
            _towerManager.GenerateOres(_boundaries, TextureManager.Instance.Sprites["pixel"]);

            _uiManager = new UIManager(_game1.graphics, new Point(64, 64));
            _uiManager.Initialize();
        }

        public void Update(GameTime gameTime)
        {
            bool intervall = false;
            int seconds = gameTime.TotalGameTime.Seconds;
            if (seconds - _lastIntervall > 2)
            {
                Console.WriteLine("INTERVALL");
                intervall = true;
                _lastIntervall = seconds;
            }
            
            _uiManager.Update(_resources["metal"].Amount, _resources["gas"].Amount, _resources["crystal"].Amount, Mouse.GetState().Position);
            AddRessources(_towerManager.Update(Mouse.GetState().Position, intervall));
            
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                _game1.Exit();

            if (!_game1.IsActive) return;

            _camera.Update();
            
            
            if (_camera.Position.Y + _game1.graphics.PreferredBackBufferHeight > _boundaries.Location.Y + _boundaries.Size.Y)
            {
                _camera.Move(0,
                    _boundaries.Location.Y + _boundaries.Size.Y -
                    (_camera.Position.Y + _game1.graphics.PreferredBackBufferHeight));
            }
            else if (_camera.Position.Y < _boundaries.Location.Y)
            {
                _camera.Move(0, _boundaries.Location.Y - _camera.Position.Y);
            }
            else if (_camera.Position.X < _boundaries.Location.X)
            {
                _camera.Move(_boundaries.Location.X - _camera.Position.X, 0);
            }
            else if (_camera.Position.X + _game1.graphics.PreferredBackBufferWidth >
                     _boundaries.Location.X + _boundaries.Size.X)
            {
                _camera.Move(
                    _boundaries.Location.X + _boundaries.Size.X -
                    (_camera.Position.X + _game1.graphics.PreferredBackBufferWidth), 0);
            }

            if (_camera.Position.Y < _boundaries.Location.Y + _boundaries.Size.Y)
            {
            }


            if (Mouse.GetState().LeftButton == ButtonState.Pressed && _lastState == ButtonState.Released)
            {
                if (!_uiManager.UiInteraction(Mouse.GetState().Position))
                {
                    Vector2 position = Mouse.GetState().Position.ToVector2() + _camera.Position;
                    TowerType type = _uiManager.SelectedTower();
                    
                    if (_towerManager.Towers.Count == 0)
                    {
                        type = TowerType.Base;
                    }

                    Tower newTower = new Tower(position.ToPoint(), TextureManager.Instance.Sprites["innerTower"],
                        TextureManager.Instance.Sprites["outerTower"], Color.Red, type);

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

                        if (newTower.Type == TowerType.Collector)
                        {
                            Console.WriteLine("started assign process");
                            AssignCollector(newTower);
                            Console.WriteLine("ended assign process");
                        }
                    }
                }
                
                
            }

            _lastState = Mouse.GetState().LeftButton;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //MAP
            spriteBatch.Begin(transformMatrix: Matrix.CreateTranslation(new Vector3(-_camera.Position, 0)));
            
            _towerManager.Draw(spriteBatch);

            spriteBatch.End();

            //UI
            spriteBatch.Begin();

            /*spriteBatch.DrawString(TextureManager.Instance.Fonts["basicfont"], "Area: " + _towerManager.SumArea(),
                new Vector2(10, 10), Color.White);
            spriteBatch.DrawString(TextureManager.Instance.Fonts["basicfont"],
                "Camera Position: " + _camera.Position.ToString(), new Vector2(10, 25),
                Color.White);*/
            
            _uiManager.Draw(spriteBatch);
            
            //spriteBatch.Draw(_textureManager.Sprites["pixel"], new Rectangle(Mouse.GetState().Position, new Point( 20, 20)), Color.Red);

            spriteBatch.End();
        }
    }
}