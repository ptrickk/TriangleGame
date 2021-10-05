using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TriangleGame.Resources;

namespace TriangleGame
{
    public class Ore : Entity
    {
        private Resource _resource;
        public Ore(Point position, Point dimension, Texture2D texture2D, Color color, int amount):base(position, texture2D, color)
        {
            Dimensions = dimension;
        }
    }
}