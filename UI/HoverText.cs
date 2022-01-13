using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TriangleGame.Manager;

namespace TriangleGame.UI
{
    public class HoverText
    {
        private string _text;
        private Color _textColor;
        private Rectangle _dim;
        private bool _active;
        private Point _mousePosition;

        public HoverText(string text, Rectangle dim, Color textColor, bool active = false)
        {
            _text = text;
            _dim = dim;
            _textColor = textColor;
        }

        public string Text
        {
            set => _text = value;
        }

        public bool Active
        {
            get => _active;
        }

        public bool Update(Point mousePoint)
        {
            if (_dim.Contains(mousePoint))
            {
                _active = true;
                _mousePosition = mousePoint;
            }
            else _active = false;

            return _active;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (_active)
            {
                TextureManager textureManager = TextureManager.Instance;
                Texture2D background = textureManager.Sprites["pixel"];
                SpriteFont font = textureManager.Fonts["basicfont"];

                int textHeight = (int) font.MeasureString(_text).Y;
                
                spriteBatch.Draw(background, new Rectangle(new Point(_mousePosition.X, _mousePosition.Y - textHeight), font.MeasureString(_text).ToPoint()),
                    Color.Black);
                spriteBatch.DrawString(font, _text, new Point(_mousePosition.X, _mousePosition.Y - textHeight).ToVector2(), _textColor);
            }
        }
    }
}