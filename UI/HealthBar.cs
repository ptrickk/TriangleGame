using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TriangleGame.GameObjects;
using TriangleGame.Manager;

namespace TriangleGame.UI
{
    public class HealthBar
    {
        private int _maxValue;
        private int _currentValue;
        private bool _active;
        private Rectangle _parent;

        public HealthBar(int maxValue, int currentValue, Rectangle parent, bool active = false)
        {
            _maxValue = maxValue;
            _currentValue = currentValue;
            _parent = parent;
            _active = active;
        }

        public int CurrentValue
        {
            get => _currentValue;
            set => _currentValue = value;
        }

        public bool Active
        {
            get => _active;
            set => _active = value;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (_active)
            {
                Point start = new Point(_parent.Center.X - 25, _parent.Center.Y - _parent.Height);
                Point end = new Point(_parent.Center.X + 25, _parent.Center.Y - _parent.Height);
                DrawFuntions.DrawLine(spriteBatch, TextureManager.Instance.Sprites["pixel"], start.ToVector2(), end.ToVector2(), Color.Red, 3);
                
                int value = (int) ((float)_currentValue / _maxValue * 50f) - 25;
                end = new Point(_parent.Center.X + (int) value , _parent.Center.Y - _parent.Height);
                DrawFuntions.DrawLine(spriteBatch, TextureManager.Instance.Sprites["pixel"], start.ToVector2(), end.ToVector2(), Color.Green, 3);
            }
        }
    }
}