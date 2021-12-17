using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TriangleGame.GameObjects;
using TriangleGame.Manager;
using TriangleGame.UI;

namespace TriangleGame
{
    public class Tower : Entity
    {
        protected Texture2D _outerTexture;
        protected int _hitpoints;
        protected HealthBar _healthBar;
        
        protected HoverText _hover;

        public Tower(Point position, Texture2D innerTexture, Texture2D outerTexture, Color color) : base(position,
            innerTexture, color)
        {
            _outerTexture = outerTexture;
            _hitpoints = 100;
            
            string hoverText = "Klick";

            _hover = new HoverText(hoverText, new Rectangle(new Point(Position.X - _dimensions.X / 2, Position.Y - _dimensions.Y / 2), Dimensions), Color.LimeGreen);
            _healthBar = new HealthBar(_hitpoints, _hitpoints,
                new Rectangle(new Point(Position.X - _dimensions.X / 2, Position.Y - _dimensions.Y / 2), Dimensions));
        }

        public HoverText HoverText
        {
            get => _hover;
        }

        public HealthBar HealthBar
        {
            get => _healthBar;
        }

        public void Update()
        {
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_outerTexture,
                new Rectangle(new Point(Position.X - _dimensions.X / 2, Position.Y - _dimensions.Y / 2), Dimensions),
                Color.White);

            spriteBatch.Draw(_texture2D,
                new Rectangle(new Point(Position.X - _dimensions.X / 2, Position.Y - _dimensions.Y / 2), Dimensions),
                _color);
        }
    }
}