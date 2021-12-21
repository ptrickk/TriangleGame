using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TriangleGame
{
    public class Area
    {
        private Connector[] _connectors = new Connector[3];

        public Area(Connector a, Connector b, Connector c)
        {
            _connectors[0] = a;
            _connectors[1] = b;
            _connectors[2] = c;
        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            _connectors[0].Draw(_spriteBatch);
            _connectors[1].Draw(_spriteBatch);
            _connectors[2].Draw(_spriteBatch);
        }

        public double Size()
        {
            double s =  (_connectors[0].Length + _connectors[1].Length + _connectors[2].Length) / 2;
            double area = Math.Sqrt(s * (s - _connectors[0].Length) * (s - _connectors[1].Length) * (s - _connectors[2].Length));

            return area;
        }

        public bool ContainsTower(Tower tower)
        {
            for (int i = 0; i < 2; i++)
            {
                if (_connectors[i].Contains(tower)) return true;
            }

            return false;
        }
        
        public bool Contains(Ore ore)
        {
            return Contains(ore.Position);
        }

        public bool Contains(Tower tower)
        {
            return Contains(tower.Position);
        }

        public bool Contains(Point point)
        {
            double[] a = new Double[3];
            for (int i = 0; i < 3; i++)
            {
                double t1 = Vector2.Distance(_connectors[i].TowerA.Position.ToVector2(), point.ToVector2());
                double t2 = Vector2.Distance(_connectors[i].TowerB.Position.ToVector2(), point.ToVector2());
                
                double s =  (_connectors[i].Length + t1 + t2) / 2;
                a[i] = Math.Sqrt(s * (s - _connectors[i].Length) * (s - t1) * (s - t2));
            }
            if (Math.Abs(a[0] + a[1] + a[2] - Size()) < 0.1)
            {
                return true;
            }

            return false;
        }

        public Tower[] GetTowers()
        {
            Tower a = _connectors[0].TowerA;
            Tower b = _connectors[0].TowerB;
            Tower c = null;
            if (_connectors[1].TowerA != a && _connectors[1].TowerA != b)
            {
                c = _connectors[1].TowerA;
            }
            else
            {
                c = _connectors[1].TowerB;
            }

            return new[] { a, b, c };
        }
    }
}