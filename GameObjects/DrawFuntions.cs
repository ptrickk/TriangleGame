using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TriangleGame.GameObjects
{
    public static class DrawFuntions
    {
        private static GraphicsDevice _graphicsDevice;
        private static List<Color> _colorData = new List<Color>();
        private static Texture2D _texture;

        public static void Init(GraphicsDevice graphicsDevice)
        {
            _graphicsDevice = graphicsDevice;
            _colorData = new List<Color>();
        }

        public static void DrawLine(SpriteBatch spriteBatch, Texture2D texture, Vector2 point1, Vector2 point2,
            Color color,
            float thickness = 1f)
        {
            var distance = Vector2.Distance(point1, point2);
            var angle = (float)Math.Atan2(point2.Y - point1.Y, point2.X - point1.X);
            DrawLine(spriteBatch, texture, point1, distance, angle, color, thickness);
        }

        public static void DrawLine(SpriteBatch spriteBatch, Texture2D texture, Vector2 point, float length,
            float angle, Color color,
            float thickness = 1f)
        {
            var origin = new Vector2(0f, 0.5f);
            var scale = new Vector2(length, thickness);
            spriteBatch.Draw(texture, point, null, color, angle, origin, scale, SpriteEffects.None, 0);
        }

        public static void DrawCircle(SpriteBatch spriteBatch, Point position, int radius, Color color)
        {
            radius *= 2;
            float diam = radius / 2f;
            float diamsq = diam * diam;

            if (_colorData.Count != radius * radius)
            {
                _texture = new Texture2D(_graphicsDevice, radius, radius);
                _colorData.Clear();
                for (int i = 0; i < radius * radius; i++)
                {
                    _colorData.Add(Color.White);
                }

                for (int x = 0; x < radius; x++)
                {
                    for (int y = 0; y < radius; y++)
                    {
                        int index = x * radius + y;
                        Vector2 pos = new Vector2(x - diam, y - diam);
                        if (pos.LengthSquared() <= diamsq)
                        {
                            _colorData[index] = Color.White;
                        }
                        else
                        {
                            _colorData[index] = Color.Transparent;
                        }
                    }
                }
                
                _texture.SetData(_colorData.ToArray());
            }

            spriteBatch.Draw(_texture, position.ToVector2(), null, color);
        }
    }
}