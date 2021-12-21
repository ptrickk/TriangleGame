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
        
        protected int _radius;

        protected bool _alive;

        public Tower(Point position, Texture2D innerTexture, Texture2D outerTexture, int hitpoints, Color color) : base(position,
            innerTexture, hitpoints, color)
        {
            _outerTexture = outerTexture;
            _hitpoints = 100;

            _radius = _dimensions.Size.X / 2;
            _alive = true;
            
            _hover = new HoverText("Klick", new Rectangle(new Point(_dimensions.X - _dimensions.Width / 2, Position.Y - _dimensions.Height / 2), Size), Color.LimeGreen);
            _healthBar = new HealthBar(_hitpoints, _hitpoints,
                new Rectangle(new Point(_dimensions.X - _dimensions.Width / 2, Position.Y - _dimensions.Height / 2), Size));
        }

        public HoverText HoverText
        {
            get => _hover;
        }

        public HealthBar HealthBar
        {
            get => _healthBar;
        }

        public int Hitpoints
        {
            get => _hitpoints;
            set => _hitpoints = value;
        }

        public bool Alive
        {
            get => _alive;
        }

        public bool Intersects(Point position, int radius)
        {
            int distance = (int) Vector2.Distance(_dimensions.Location.ToVector2(), position.ToVector2());

            if (distance < radius || distance < _radius)
            {
                return true;
            }

            return false;
        }

        public void Update()
        {
        }

        public bool TakeDamage(int damage)
        {
            _hitpoints -= damage;
            if (_hitpoints <= 0)
            {
                _hitpoints = 0;
                Die();
                return true;
            }
            _healthBar.CurrentValue = _hitpoints;
            return false;
        }

        public void Die()
        {
            _alive = false;
        }
        
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_outerTexture,
                new Rectangle(new Point(_dimensions.X - _dimensions.Width / 2, Position.Y - _dimensions.Height / 2), Size),
                Color.White);

            spriteBatch.Draw(_texture2D,
                new Rectangle(new Point(_dimensions.X - _dimensions.Width / 2, Position.Y - _dimensions.Height / 2), Size),
                _color);
        }
    }
}