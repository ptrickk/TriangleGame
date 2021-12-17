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
                         new Rectangle(new Point(Position.X - _dimensions.X / 2, Position.Y - _dimensions.Y / 2), Dimensions),
                         _color);
            
            spriteBatch.Draw(_outerTexture,
                new Rectangle(new Point(Position.X - _dimensions.X / 2, Position.Y - _dimensions.Y / 2), Dimensions),
                Color.White);
        }
    }
}