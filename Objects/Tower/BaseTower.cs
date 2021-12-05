using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TriangleGame.UI;

namespace TriangleGame
{
    public class BaseTower : Tower
    {
        public BaseTower(Point position, Texture2D innerTexture, Texture2D outerTexture)
            : base(position, innerTexture, outerTexture, Color.Black)
        {
            _hover.Text = "Base";
        }
    }
}