using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TriangleGame
{
    public class Tower : Entity
    {
        private TowerType _type;

        public Tower(Point position, Texture2D texture, Color color, TowerType type):base(position, texture, color)
        {
            _type = type;
            switch (type)
            {
                case TowerType.Base:
                    Dimensions = new Point(24, 24);
                    break;
                default: break;
            }
        }

        public TowerType Type
        {
            get => _type;
        }
    }
}