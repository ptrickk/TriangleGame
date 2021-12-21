using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TriangleGame.UI;

namespace TriangleGame
{
    public class StorageTower : Tower
    {
        public StorageTower(Point position, Texture2D innerTexture, Texture2D outerTexture, int hitpoints, Color teamColor)
            : base(position, innerTexture, outerTexture, hitpoints, teamColor)
        {
            _hover.Text = "Speicher";
        }
    }
}