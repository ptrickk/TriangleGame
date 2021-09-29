using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TriangleGame
{
    public class Ore : Entity
    {
        public Ore(Point position, Point dimension, Texture2D texture2D, Color color, int amount):base(position, texture2D, color)
        {
            Dimensions = dimension;
        }
    }
}