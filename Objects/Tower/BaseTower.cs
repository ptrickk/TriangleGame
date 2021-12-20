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
                         new Rectangle(new Point(_dimensions.X - _dimensions.Width / 2, Position.Y - _dimensions.Height / 2), Size),
                         _color);
            
            spriteBatch.Draw(_outerTexture,
                new Rectangle(new Point(_dimensions.X - _dimensions.Width / 2, Position.Y - _dimensions.Height / 2), Size),
                Color.White);
        }
    }
}