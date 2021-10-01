using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TriangleGame.Manager;

namespace TriangleGame
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private Texture2D test;
        private TowerManager _towerManager;

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
            _towerManager = new TowerManager();
            _towerManager.GenerateOres(new Point(0,0), new Point(800, 600), test);
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            test = Content.Load<Texture2D>("pixel");
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

            if (Mouse.GetState().LeftButton == ButtonState.Pressed && lastState == ButtonState.Released)
            {
                if (Mouse.GetState().Position.X > 0 && Mouse.GetState().Position.Y > 0)
                {
                    TowerType type = TowerType.Default;
                    if (_towerManager.Towers.Count == 0)
                    {
                        type = TowerType.Base;
                    }
                    Tower newTower = new Tower(Mouse.GetState().Position, test,Color.Red, type);
                    
                    if (_towerManager.AddTower(newTower))//Falls der Turm platziert werden kann
                    {
                        _towerManager.Connect(newTower);
                    }
                    
                }
            }
            lastState = Mouse.GetState().LeftButton;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

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

            base.Draw(gameTime);
        }
    }
}