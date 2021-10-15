using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TriangleGame.UI;

namespace TriangleGame
{
    public class Tower : Entity
    {
        private TowerType _type;
        private Texture2D _outerTexture;

        private HoverText _hover;

        public Tower(Point position, Texture2D innerTexture, Texture2D outerTexture, Color color, TowerType type):base(position, innerTexture, color)
        {
            _type = type;
            _outerTexture = outerTexture;

            string hoverText = "error";
            
            switch (type)
            {
                case TowerType.Default:
                    _color = Color.White;
                    break;
                case TowerType.Attacker:
                    _color = Color.Red;
                    hoverText = "Angreifer";
                    break;
                case TowerType.Collector:
                    _color = Color.Yellow;
                    hoverText = "Sammler";
                    break;
                case TowerType.Storage:
                    _color = Color.Green;
                    hoverText = "Speicher";
                    break;
                case TowerType.Base:
                    _color = Color.Black;
                    hoverText = "Basis";
                    Dimensions = new Point(24, 24);
                    break;
            }

            _hover = new HoverText(hoverText, new Rectangle(_position, _dimensions), Color.LimeGreen);
        }

        public TowerType Type
        {
            get => _type;
        }

        public HoverText HoverText
        {
            get => _hover;
        }

        public new void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture2D,
                new Rectangle(new Point(_position.X - _dimensions.X / 2, _position.Y - _dimensions.Y / 2), _dimensions),
                _color);
            
            spriteBatch.Draw(_outerTexture, new Rectangle(new Point(_position.X - _dimensions.X / 2, _position.Y - _dimensions.Y / 2), _dimensions),
                Color.White);
        }
    }
}