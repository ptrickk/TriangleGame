using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TriangleGame.UI.Buttons
{
    public class Button
    {
        /// <summary>
        /// The texture of the Button
        /// </summary>
        private Texture2D _texture;
        /// <summary>
        /// The frame that is displayed if the button is selected
        /// </summary>
        protected Texture2D _frame;

        protected Rectangle _dimensions;

        protected bool _selected;

        protected HoverText _hover;
        
        public Button(Texture2D texture, Texture2D frame, Rectangle dimensions, string text, bool selected = false)
        {
            _texture = texture;
            _frame = frame;
            _dimensions = dimensions;

            _selected = selected;

            _hover = new HoverText(text, dimensions, Color.LimeGreen);
        }

        public bool Selected
        {
            get => _selected;
        }

        public bool IsSelected(Point position)
        {
            if (_dimensions.Contains(position))
            {
                Console.WriteLine("BUTTON SELECTED");
                _selected = true;
                return true;
            }

            return false;
        }

        public void Deactivate()
        {
            _selected = false;
        }

        public void Update(Point mousePoint)
        {
            _hover.Update(mousePoint);
        }
        
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _dimensions, Color.White);
            if (_selected)
            {
                spriteBatch.Draw(_frame, _dimensions, Color.White);
            }
        }

        public void DrawHover(SpriteBatch spriteBatch)
        {
            _hover.Draw(spriteBatch);
        }
    }
}