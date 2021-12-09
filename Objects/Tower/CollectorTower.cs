using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TriangleGame.GameObjects;
using TriangleGame.Manager;
using TriangleGame.Resources;
using TriangleGame.UI;

namespace TriangleGame
{
    public class CollectorTower : Tower
    {
        private Ore _occupied;
        private ResourceType _prefered;
        private bool _reassign = false;
        
        private CollectorUI _ui;

        public CollectorTower(Point position, Texture2D innerTexture, Texture2D outerTexture, Color teamColor)
            : base(position, innerTexture, outerTexture, teamColor)
        {
            _prefered = ResourceType.None;
            _reassign = false;
            
            _hover.Text = "Menü öffnen";
            _ui = new CollectorUI(_position, ResourceType.None, new Rectangle(new Point(_position.X - (_dimensions.X/2), _position.Y - (_dimensions.Y/2)), _dimensions));
        }

        public ResourceType Prefered
        {
            get => _prefered;
        }

        public void UpdateUI(MouseInfo mouse)
        {
            if (mouse.LeftPressed)
            {
                Console.WriteLine("MOUSE PRESSED");
                _ui.Update(mouse);
                ResourceType prev = _prefered;
                _prefered = _ui.Selected();

                if (prev != _prefered && _occupied.Resource != _prefered)
                {
                    _occupied.Release();
                    RemoveOccupied();
                }
            }
        }

        public new KeyValuePair<string, int> Update()
        {
            KeyValuePair<string, int> body;
            if (_occupied != null)
            {
                string key = _occupied.ResourceToString();
                int amount = _occupied.Mine();

                body = new KeyValuePair<string, int>(key, amount);

                if (!_occupied.Occupied)
                {
                    RemoveOccupied();
                }
            }
            else
            {
                body = new KeyValuePair<string, int>("none", -1);
            }

            return body;
        }

        public void Occupie(Ore ore)
        {
            if (ore.SetAsOccupied())
            {
                _occupied = ore;
            }
            else
            {
                Console.WriteLine("ERROR: occupied ore can't be occupied!");
            }
        }

        public void RemoveOccupied()
        {
            _occupied = null;
        }

        public Ore Occupied
        {
            get => _occupied;
        }

        public new void Draw(SpriteBatch spriteBatch)
        {
            if (_occupied != null)
            {
                DrawFuntions.DrawLine(spriteBatch, TextureManager.Instance.Sprites["pixel"], _position.ToVector2(), _occupied.Position.ToVector2(), Color.Purple);
            }

            spriteBatch.Draw(_texture2D,
                new Rectangle(new Point(_position.X - _dimensions.X / 2, _position.Y - _dimensions.Y / 2), _dimensions),
                _color);

            spriteBatch.Draw(_outerTexture,
                new Rectangle(new Point(_position.X - _dimensions.X / 2, _position.Y - _dimensions.Y / 2), _dimensions),
                Color.White);

            _ui.Draw(spriteBatch);
        }
    }
}