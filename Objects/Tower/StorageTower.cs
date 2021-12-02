using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TriangleGame.UI;

namespace TriangleGame
{
    public class StorageTower : Tower
    {
        public StorageTower(Point position, Texture2D innerTexture, Texture2D outerTexture)
            : base(position, innerTexture, outerTexture, Color.Red)
        {
            _hover = new HoverText("Speicher", new Rectangle(_position, _dimensions), Color.LimeGreen);
        }
    }
}