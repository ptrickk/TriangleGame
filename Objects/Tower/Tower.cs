using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TriangleGame.Manager;
using TriangleGame.UI;

namespace TriangleGame
{
    public class Tower : Entity
    {
        //protected TowerType _type;
        protected Texture2D _outerTexture;

        protected HoverText _hover;

        //protected Ore _occupied;

        public Tower(Point position, Texture2D innerTexture, Texture2D outerTexture, Color color):base(position, innerTexture, color)
        {
            _outerTexture = outerTexture;

            string hoverText = "error";
            
            TextureManager textureManager = TextureManager.Instance;

            _hover = new HoverText(hoverText, new Rectangle(_position, _dimensions), Color.LimeGreen);
        }

        public HoverText HoverText
        {
            get => _hover;
        }

        public void Update()
        {
            
        }

        public new void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture2D,
                new Rectangle(new Point(_position.X - _dimensions.X / 2, _position.Y - _dimensions.Y / 2), _dimensions),
                _color);
            
            spriteBatch.Draw(_outerTexture, new Rectangle(new Point(_position.X - _dimensions.X / 2, _position.Y - _dimensions.Y / 2), _dimensions),
                Color.White);
        }
    }
}