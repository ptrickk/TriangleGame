using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TriangleGame
{
    public class Alien : Entity
    {
        private int _hitpoints;
        private int _strength;
        private int _speed;
        
        public Alien(Point position, Texture2D texture2D, Color color, int hitpoints, int strength, int speed): base(position, texture2D, color)
        {
            _hitpoints = hitpoints;
            _strength = strength;
            _speed = speed;
        }
    }
}