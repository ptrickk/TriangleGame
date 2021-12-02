using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TriangleGame.UI;

namespace TriangleGame
{
    public class AttackTower : Tower
    {
        public AttackTower(Point position, Texture2D innerTexture, Texture2D outerTexture)
            : base(position, innerTexture, outerTexture, Color.Red)
        {
            _hover = new HoverText("Angreifer", new Rectangle(_position, _dimensions), Color.LimeGreen);
        }
    }
}