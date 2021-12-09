using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TriangleGame.UI;

namespace TriangleGame
{
    public class AttackTower : Tower
    {
        public AttackTower(Point position, Texture2D innerTexture, Texture2D outerTexture, Color teamColor)
            : base(position, innerTexture, outerTexture, teamColor)
        {
            _hover.Text = "Angreifer";
            //_hover = new HoverText("", new Rectangle(_position, _dimensions), Color.LimeGreen);
        }
    }
}