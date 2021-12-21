using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TriangleGame
{
    public class Entity
    {
        protected Texture2D _texture2D;
        protected Color _color;
        protected int _hitpoint;

        protected Rectangle _dimensions;// = new Point(32, 32);

        public Entity(Point position, Texture2D texture2D, int hitpoints, Color color)
        {
            _dimensions = new Rectangle(position, new Point(32, 32));
            _texture2D = texture2D;
            _hitpoint = hitpoints;
            _color = color;
        }

        public Point Position
        {
            get => _dimensions.Location;
        }

        public Point Size
        {
            get => _dimensions.Size ;
            set => _dimensions.Size = value;
        }

        public Rectangle Dimensions
        {
            get => _dimensions;
        }

        public virtual void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(_texture2D,
                new Rectangle(new Point(_dimensions.X - _dimensions.Width / 2, Position.Y - _dimensions.Height / 2), _dimensions.Size),
                _color);
        }
    }
}