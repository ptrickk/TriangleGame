using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TriangleGame
{
    public class Connector
    {
        private Tower _towerA;
        private Tower _towerB;
        private static Texture2D _texture;

        private Point _max;
        private Point _min;

        public Connector(Texture2D texture2D)
        {
            _texture = texture2D;
        }

        public Connector(Tower a, Tower b)
        {
            _towerA = a;
            _towerB = b;

            _max = new Point(a.Position.X, a.Position.Y);
            _min = new Point(a.Position.X, a.Position.Y);
            if (b.Position.X < _min.X) _min.X = b.Position.X;
            if (b.Position.Y < _min.Y) _min.Y = b.Position.Y;
            if (b.Position.X > _max.X) _max.X = b.Position.X;
            if (b.Position.Y > _max.Y) _max.Y = b.Position.Y;
        }

        public Tower TowerA
        {
            get => _towerA;
        }

        public Tower TowerB
        {
            get => _towerB;
        }

        public Point Min
        {
            get => _min;
        }

        public Point Max
        {
            get => _max;
        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            DrawLine(_spriteBatch, _towerA.Position.ToVector2(), _towerB.Position.ToVector2(), Color.White);
        }

        public float Length
        {
            get
            {
                return Vector2.Distance(_towerA.Position.ToVector2(), _towerB.Position.ToVector2());
            }
        }

        public bool Intersects(Connector connector)
        {
            float a1 = _towerB.Position.Y - _towerA.Position.Y;
            float b1 = _towerA.Position.X - _towerB.Position.X;
            float c1 = a1 * _towerA.Position.X + b1 * _towerA.Position.Y;
 
            float a2 = connector._towerB.Position.Y - connector.TowerA.Position.Y;
            float b2 = connector.TowerA.Position.X - connector._towerB.Position.X;
            float c2 = a2 * connector.TowerA.Position.X + b2 * connector.TowerA.Position.Y;

            float delta = a1 * b2 - a2 * b1;
            //float delta = a1 * b2 - a2 * b1;

            if (delta == 0)
            {
                //DELTA IST 0
                return false;//KEINE KOLLISION
            }
            else
            {
                Vector2 vec = new Vector2((b2 * c1 - b1 * c2) / delta, (a1 * c2 - a2 * c1) / delta);
                Point point = vec.ToPoint();

                if (point.Equals(_towerA.Position) || point.Equals(_towerB.Position) ||
                    point.Equals(connector.TowerA.Position) ||
                    point.Equals(connector.TowerB.Position))
                {
                    return false;
                }

                if (point.X <= _min.X || point.Y <= _min.Y || point.X >= _max.X || point.Y >= _max.Y ||
                    point.X <= connector.Min.X || point.Y <= connector.Min.Y || point.X >= connector.Max.X || point.Y >= connector.Max.Y)
                {
                    return false;
                }

                return IsIntersecting(_towerA.Position, _towerB.Position, connector._towerA.Position,
                                           connector._towerB.Position);
            }
        }

        public bool Contains(Tower t)
        {
            return _towerA.Equals(t) || _towerB.Equals(t);
        }

        private void DrawLine(SpriteBatch spriteBatch, Vector2 point1, Vector2 point2, Color color,
            float thickness = 1f)
        {
            var distance = Vector2.Distance(point1, point2);
            var angle = (float)Math.Atan2(point2.Y - point1.Y, point2.X - point1.X);
            DrawLine(spriteBatch, point1, distance, angle, color, thickness);
        }

        private void DrawLine(SpriteBatch spriteBatch, Vector2 point, float length, float angle, Color color,
            float thickness = 1f)
        {
            var origin = new Vector2(0f, 0.5f);
            var scale = new Vector2(length, thickness);
            spriteBatch.Draw(_texture, point, null, color, angle, origin, scale, SpriteEffects.None, 0);
        }
        
        private bool IsIntersecting(Point a, Point b, Point c, Point d)
        {
            float denominator = ((b.X - a.X) * (d.Y - c.Y)) - ((b.Y - a.Y) * (d.X - c.X));
            float numerator1 = ((a.Y - c.Y) * (d.X - c.X)) - ((a.X - c.X) * (d.Y - c.Y));
            float numerator2 = ((a.Y - c.Y) * (b.X - a.X)) - ((a.X - c.X) * (b.Y - a.Y));

            // Detect coincident lines (has a problem, read below)
            if (denominator == 0) return numerator1 == 0 && numerator2 == 0;

            float r = numerator1 / denominator;
            float s = numerator2 / denominator;

            return (r >= 0 && r <= 1) && (s >= 0 && s <= 1);
        }
    }
}