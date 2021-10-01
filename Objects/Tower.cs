using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TriangleGame
{
    public class Tower : Entity
    {
        private TowerType _type;
        private Texture2D _outerTexture;

        public Tower(Point position, Texture2D innerTexture, Texture2D outerTexture, Color color, TowerType type):base(position, innerTexture, color)
        {
            _type = type;
            _outerTexture = outerTexture;

            switch (type)
            {
                case TowerType.Default:
                    _color = Color.Blue;
                    break;
                case TowerType.Attacker:
                    _color = Color.Red;
                    break;
                case TowerType.Collector:
                    _color = Color.Green;
                    break;
                case TowerType.Base:
                    _color = Color.Black;
                    Dimensions = new Point(24, 24);
                    break;
                default: break;
            }
        }

        public TowerType Type
        {
            get => _type;
        }

        public new void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(_texture2D,
                new Rectangle(new Point(_position.X - _dimensions.X / 2, _position.Y - _dimensions.Y / 2), _dimensions),
                _color);
            
            _spriteBatch.Draw(_outerTexture, new Rectangle(new Point(_position.X - _dimensions.X / 2, _position.Y - _dimensions.Y / 2), _dimensions),
                Color.White);
        }
    }
}