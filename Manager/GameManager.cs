using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
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
        private MouseInfo _mouse;

        private Color teamColor = Color.Green;

        //Camera controls
        private Camera _camera;
        private Rectangle _boundaries;

        private ButtonState _lastState = ButtonState.Released;

        private Dictionary<string, Resource> _resources;

        private GraphicsDeviceManager _graphics;

        private double _lastMiningInterval = 0;
        private double _miningIntervalSpeed = 2;
        
        private double _lastMinionInterval = 0;
        private double _minionIntervalSpeed = 0.2;

        private Alien test;

        public GameManager(Game1 game1)
        {
            _resources = new Dictionary<string, Resource>();
            _game1 = game1;
            _mouse = new MouseInfo();
        }

        public void StartGame()
        {
            _lastMiningInterval = 0;
            _lastMinionInterval = 0;
        }

        public void AssignCollector(CollectorTower collector)
        {
            List<Area> areas = _towerManager.GetAreasOfTower(collector);
            List<Ore> ores = new List<Ore>();
            foreach (var area in areas)
            {
                ores.AddRange(_towerManager.GetUnoccupiedOresInArea(area, collector.Prefered));
                if (ores.Count > 0) break;
            }

            foreach (var ore in ores)
            {
                if (!ore.Occupied)
                {
                    collector.Occupie(ore);
                    return;
                }
            }
        }

        public void AddResources(Dictionary<string, int> res)
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
            _resources.Add("metal", new Resource(ResourceType.Metal, 500, 500));
            _resources.Add("gas", new Resource(ResourceType.Gas, 500, 500));
            _resources.Add("crystal", new Resource(ResourceType.Crystals, 500, 500));

            _boundaries = new Rectangle(0, 0, 2000, 2000);
            _camera = new Camera(_boundaries.Center.ToVector2(), 2);

            _towerManager = new TowerManager();
            _towerManager.GenerateOres(_boundaries, TextureManager.Instance.Sprites["pixel"]);

            _uiManager = new UIManager(_game1.graphics, new Point(64, 64));
            _uiManager.Initialize();

            test = new Alien(new Point(100, 100), new Point(16,16), TextureManager.Instance.Sprites["pixel"], Color.RosyBrown, 100, 10,
                4);
        }

        public void Update(GameTime gameTime)
        {
            _mouse.Update(_camera);
            //Console.WriteLine("SCROLL: " + Mouse.GetState().ScrollWheelValue);
            
            bool miningInterval = false;
            bool minionInterval = false;
            var seconds = gameTime.TotalGameTime.TotalSeconds;
            if (seconds - _lastMiningInterval > _miningIntervalSpeed)
            {
                Console.WriteLine("INTERVAL: " + seconds);
                miningInterval = true;
                _lastMiningInterval = seconds;
            }

            if (seconds - _lastMinionInterval > _minionIntervalSpeed)
            {
                minionInterval = true;
                _lastMinionInterval = seconds;
            }

            _uiManager.Update(_resources["metal"].Amount, _resources["gas"].Amount, _resources["crystal"].Amount,
                _resources["metal"].Maximum, _resources["gas"].Maximum, _resources["crystal"].Maximum,
                _mouse.Position);

            Vector3 mousePosition = Vector3.Transform(new Vector3(Mouse.GetState().Position.ToVector2(), 0),
                Matrix.CreateTranslation(new Vector3(_camera.Position, 0)));
            AddResources(_towerManager.Update(_mouse, miningInterval));

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                _game1.Exit();

            if (!_game1.IsActive) return;

            _camera.Update(_game1.graphics, _boundaries);

            if (Mouse.GetState().LeftButton == ButtonState.Pressed && _lastState == ButtonState.Released)
            {
                if (!_uiManager.UiInteraction(Mouse.GetState().Position))
                {
                    bool allowed = false;
                    Vector2 position = _mouse.Position.ToVector2() + _camera.Position;
                    TowerType type = _uiManager.SelectedTower();

                    if (_towerManager.Towers.Count == 0)
                    {
                        type = TowerType.Base;
                        allowed = true;
                    }

                    int[] price = new int[3];
                    if (type != TowerType.Base) //Base hat keinen Preis
                    {
                        switch (type)
                        {
                            case TowerType.Attacker:
                                price = new[] { 150, 200, 200 };
                                break;
                            case TowerType.Collector:
                                price = new[] { 50, 200, 200 };
                                break;
                            case TowerType.Storage:
                                price = new[] { 300, 50, 50 };
                                break;
                        }

                        if (_resources["metal"].Amount >= price[0] && _resources["gas"].Amount >= price[1] &&
                            _resources["crystal"].Amount >= price[2])
                        {
                            allowed = true;
                        }
                    }

                    if (allowed)
                    {
                        Tower newTower = new Tower(position.ToPoint(), TextureManager.Instance.Sprites["innerTower"],
                            TextureManager.Instance.Sprites["outerTower"], teamColor);

                        switch (type)
                        {
                            case TowerType.Attacker:
                                newTower = new AttackTower(position.ToPoint(),
                                    TextureManager.Instance.Sprites["towerAttackerTint"],
                                    TextureManager.Instance.Sprites["towerAttackerTex"], teamColor);
                                break;
                            case TowerType.Collector:
                                newTower = new CollectorTower(position.ToPoint(),
                                    TextureManager.Instance.Sprites["towerCollectorTint"],
                                    TextureManager.Instance.Sprites["towerCollectorTex"], teamColor);
                                break;
                            case TowerType.Storage:
                                newTower = new StorageTower(position.ToPoint(),
                                    TextureManager.Instance.Sprites["towerStorageTint"],
                                    TextureManager.Instance.Sprites["towerStorageTex"], teamColor);
                                break;
                            case TowerType.Base:
                                newTower = new BaseTower(position.ToPoint(),
                                    TextureManager.Instance.Sprites["towerBaseTint"],
                                    TextureManager.Instance.Sprites["towerBaseTex"], teamColor);
                                break;
                            default:
                                newTower = new Tower(position.ToPoint(), TextureManager.Instance.Sprites["innerTower"],
                                    TextureManager.Instance.Sprites["outerTower"], teamColor);
                                break;
                        }

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

                            if (type != TowerType.Base && _towerManager.Towers.Contains(newTower))
                            {
                                _resources["metal"].IncreaseAmount(-price[0]);
                                _resources["gas"].IncreaseAmount(-price[1]);
                                _resources["crystal"].IncreaseAmount(-price[2]);

                                if (newTower.GetType() == typeof(StorageTower))
                                {
                                    _resources["metal"].IncreaseMaxAmount(200);
                                    _resources["gas"].IncreaseMaxAmount(200);
                                    _resources["crystal"].IncreaseMaxAmount(200);
                                }
                            }
                        }
                        else
                        {
                            SoundManager.Instance.SetSound("invalidAction", 0.1f);
                        }
                    }
                    else
                    {
                        SoundManager.Instance.SetSound("invalidAction", 0.1f);
                    }
                }
            }

            if (miningInterval)
            {
                foreach (var tower in _towerManager.Towers)
                {
                    
                    if (tower.GetType() == typeof(CollectorTower))
                    {
                        CollectorTower ctower = (CollectorTower)tower;
                        if (ctower.Occupied == null)
                        {
                            Console.WriteLine("NEW ASSIGNE");
                            AssignCollector(ctower);
                        }
                    }
                }

                for (int i = _towerManager.Ores.Count - 1; i >= 0; i--)
                {
                    if (_towerManager.Ores[i].Amount < 1)
                    {
                        _towerManager.Ores.Remove(_towerManager.Ores[i]);
                    }
                }
            }

            if (minionInterval && _towerManager.Towers.Count > 0)
            {
                test.Select(_towerManager.Towers[0]);
                test.Move();
            }
            
            SoundManager.Instance.Update();//Plays a sound if selected

            _lastState = Mouse.GetState().LeftButton;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //MAP
            spriteBatch.Begin(transformMatrix: Matrix.CreateTranslation(new Vector3(-_camera.Position, 0)));

            _towerManager.Draw(spriteBatch);
            test.Draw(spriteBatch);

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