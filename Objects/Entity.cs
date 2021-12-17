using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TriangleGame
{
    public class Entity
    {
        protected Point _position;
        protected Texture2D _texture2D;
        protected Color _color;

        protected Point _dimensions = new Point(32, 32);

        public Entity(Point position, Texture2D texture2D, Color color)
        {
            _position = position;
            _texture2D = texture2D;
            _color = color;
        }

        public Point Position
        {
            get => _position;
        }

        public Point Dimensions
        {
            get => _dimensions;
            set => _dimensions = value;
        }

        public Rectangle getRectangle()
        {
            return new Rectangle(_position, _dimensions);
        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(_texture2D,
                new Rectangle(new Point(_position.X - _dimensions.X / 2, _position.Y - _dimensions.Y / 2), _dimensions),
                _color);
        }
    }
}