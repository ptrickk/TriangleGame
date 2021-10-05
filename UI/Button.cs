﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TriangleGame.UI
{
    public class Button
    {
        private Texture2D _texture;
        private Texture2D _frame;

        private TowerType _towerType;

        private Rectangle _dimensions;

        private bool _selected;
        
        public Button(Texture2D texture, Texture2D frame, Rectangle dimensions, TowerType towerType, bool selected = false)
        {
            _texture = texture;
            _frame = frame;
            _dimensions = dimensions;
            _towerType = towerType;
            
            _selected = selected;
        }

        public bool Selected
        {
            get => _selected;
        }

        public TowerType TowerType
        {
            get => _towerType;
        }
        
        public bool IsSelected(Point position)
        {
            if (_dimensions.Contains(position))
            {
                _selected = true;
                return true;
            }

            return false;
        }

        public void Deactivate()
        {
            _selected = false;
        }
        
        public void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(_texture, _dimensions, Color.White);
            if (_selected)
            {
                _spriteBatch.Draw(_frame, _dimensions, Color.White);
            }
        }
    }
}