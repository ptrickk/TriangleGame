using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TriangleGame.UI;

namespace TriangleGame
{
    public class BaseTower : Tower
    {
        public BaseTower(Point position, Texture2D innerTexture, Texture2D outerTexture, Color teamColor)
            : base(position, innerTexture, outerTexture, teamColor)
        {
            _hover.Text = "Base";
        }
        
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture2D,
                         new Rectangle(new Point(_position.X - _dimensions.X / 2, _position.Y - _dimensions.Y / 2), _dimensions),
                         _color);
            
            spriteBatch.Draw(_outerTexture,
                new Rectangle(new Point(_position.X - _dimensions.X / 2, _position.Y - _dimensions.Y / 2), _dimensions),
                Color.White);
        }
    }
}