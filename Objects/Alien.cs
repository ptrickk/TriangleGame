using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TriangleGame
{
    public class Alien : Entity
    {
        private int _hitpoints;
        private int _strength;
        private int _speed;

        private Tower _selected;
        
        public Alien(Point position, Point dimensions, Texture2D texture2D, Color color, int hitpoints, int strength, int speed): base(position, texture2D, color)
        {
            _hitpoints = hitpoints;
            _strength = strength;
            _speed = speed;
            Dimensions = dimensions;

            _selected = null;
        }

        public void Select(Tower select)
        {
            _selected = select;
        }

        public void Action()
        {
            //if(_selected.)
        }
        
        public void Move()
        {
            if (_selected != null)
            {
                Vector2 direction = _selected.Position.ToVector2() - Position.ToVector2();
                direction.Normalize();
                direction.Round();
                Point travel = new Point((int) direction.X * _speed, (int) direction.Y * _speed);

                _position += travel;
            }
        }

        public void Attack()
        {
            
        }
    }
}