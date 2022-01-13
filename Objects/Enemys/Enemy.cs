using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TriangleGame.GameObjects;
using TriangleGame.Manager;
using TriangleGame.UI;

namespace TriangleGame.Objects.Enemys
{
    public class Enemy : Entity
    {
        protected int _hitpoints;
        protected int _strength;
        protected int _speed;
        protected int _range;
        
        protected HealthBar _healthBar;

        protected Tower _selected;

        private bool _animation;
        
        public Enemy(Point position, Point dimensions, Texture2D texture2D, Color color, int hitpoints, int strength, int speed): base(position, texture2D, hitpoints, color)
        {
            _strength = strength;
            _speed = speed;
            _dimensions = new Rectangle(position, dimensions);
            _range = 60;
            _hitpoints = hitpoints;
            
            _selected = null;

            _animation = false;
            
            _healthBar = new HealthBar(_hitpoints, _hitpoints,
                new Rectangle(new Point(_dimensions.X - _dimensions.Width / 2, Position.Y - _dimensions.Height / 2), Size));
        }
        
        public HealthBar HealthBar
        {
            get => _healthBar;
        }

        public void Select(Tower select)
        {
            _selected = select;
        }

        public EnemyState Action()
        {
            if (_hitpoints == 0)
            {
                return EnemyState.DEAD;
            }
            if (_selected == null)
            {
                _animation = false;
                return EnemyState.SEARCH;//no tower selected
            }
            else if (_selected.Intersects(_dimensions.Location, _range))
            {
                Attack();
                _animation = !_animation;
                
                return EnemyState.ATTACK;
            }
            else
            {
                _animation = false;
                Move();//Move closer to selected target
                return EnemyState.MOVE;
            }
            
        }

        private void Move()
        {
            if (_selected != null)
            {
                Vector2 direction = _selected.Position.ToVector2() - Position.ToVector2();
                direction.Normalize();
                direction.Round();
                Point travel = new Point((int) direction.X * _speed, (int) direction.Y * _speed);

                _dimensions.Location += travel;
                
                _healthBar.UpdatePosition(_dimensions.Location);
            }
        }

        private void Attack()
        {
            if (_selected.TakeDamage(_strength))
            {
                _animation = false;
                _selected = null;
            }
        }
        
        public bool TakeDamage(int damage)
        {
            _hitpoints -= damage;
            _healthBar.Active = true;
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
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (_animation && _selected != null)
            {
                DrawFuntions.DrawLine(spriteBatch, TextureManager.Instance.Sprites["pixel"], Position.ToVector2(), _selected.Position.ToVector2(), Color.Red);
            }
            
            base.Draw(spriteBatch);
        }
    }
}