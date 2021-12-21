using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TriangleGame.GameObjects;
using TriangleGame.Manager;

namespace TriangleGame.Objects.Enemys
{
    public class Enemy : Entity
    {
        protected int _strength;
        protected int _speed;
        protected int _range;
        protected EnemyState _state; 

        protected Tower _selected;

        private bool _animation;
        
        public Enemy(Point position, Point dimensions, Texture2D texture2D, Color color, int hitpoints, int strength, int speed): base(position, texture2D, hitpoints, color)
        {
            _strength = strength;
            _speed = speed;
            _dimensions = new Rectangle(position, dimensions);
            _range = 60;
            
            _selected = null;

            _animation = false;
        }

        public void Select(Tower select)
        {
            _selected = select;
        }

        public EnemyState Action()
        {
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
        
        public void Move()
        {
            if (_selected != null)
            {
                Vector2 direction = _selected.Position.ToVector2() - Position.ToVector2();
                direction.Normalize();
                direction.Round();
                Point travel = new Point((int) direction.X * _speed, (int) direction.Y * _speed);

                _dimensions.Location += travel;
            }
        }

        public void Attack()
        {
            if (_selected.TakeDamage(_strength))
            {
                _animation = false;
                _selected = null;
            }
        }

        public override void Draw(SpriteBatch _spriteBatch)
        {
            if (_animation && _selected != null)
            {
                DrawFuntions.DrawLine(_spriteBatch, TextureManager.Instance.Sprites["pixel"], Position.ToVector2(), _selected.Position.ToVector2(), Color.Red);
            }
            
            base.Draw(_spriteBatch);
        }
    }
}