using Microsoft.Xna.Framework.Graphics;

namespace TriangleGame
{
    public class Area
    {
        private Connector _connectorA;
        private Connector _connectorB;
        private Connector _connectorC;

        public Area(Connector a, Connector b, Connector c)
        {
            _connectorA = a;
            _connectorB = b;
            _connectorC = c;
        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            _connectorA.Draw(_spriteBatch);
            _connectorB.Draw(_spriteBatch);
            _connectorC.Draw(_spriteBatch);
        }
    }
}