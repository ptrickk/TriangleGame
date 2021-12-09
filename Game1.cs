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
        public GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;

        
        private TextureManager _textureManager;
        private GameManager _gameManager;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            IsMouseVisible = true;
            Content.RootDirectory = @"Content";
        }

        protected override void Initialize()
        {
            base.Initialize();

            _gameManager = new GameManager(this);
            _gameManager.Initialize();
            _gameManager.StartGame();
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
            if (!IsActive) return;
            _gameManager.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(218, 105, 50, 255));
            
            _gameManager.Draw(spriteBatch);
            base.Draw(gameTime);
        }
    }
}