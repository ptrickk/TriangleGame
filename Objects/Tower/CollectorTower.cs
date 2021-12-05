﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TriangleGame.Manager;
using TriangleGame.Resources;
using TriangleGame.UI;

namespace TriangleGame
{
    public class CollectorTower : Tower
    {
        private Ore _occupied;
        private ResourceType _preffered;
        private CollectorUI _ui;

        public CollectorTower(Point position, Texture2D innerTexture, Texture2D outerTexture)
            : base(position, innerTexture, outerTexture, Color.Red)
        {
            _preffered = ResourceType.None;
            _hover.Text = "Menü öffnen";
            _ui = new CollectorUI(_position, ResourceType.None, new Rectangle(_position, _dimensions));
        }

        public new KeyValuePair<string, int> Update(Point mousePoint, bool pressed = false)
        {
            if (pressed)
            {
                //pressed is always false
                Console.WriteLine("MOUSE PRESSED");
                _ui.Update(mousePoint);
            }
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
                DrawLine(spriteBatch, _position.ToVector2(), _occupied.Position.ToVector2(), Color.Purple);
            }

            spriteBatch.Draw(_texture2D,
                new Rectangle(new Point(_position.X - _dimensions.X / 2, _position.Y - _dimensions.Y / 2), _dimensions),
                _color);
            
            spriteBatch.Draw(_outerTexture, new Rectangle(new Point(_position.X - _dimensions.X / 2, _position.Y - _dimensions.Y / 2), _dimensions),
                Color.White);
            
            _ui.Draw(spriteBatch);
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
            Texture2D texture = TextureManager.Instance.Sprites["pixel"];
            var origin = new Vector2(0f, 0.5f);
            var scale = new Vector2(length, thickness);
            spriteBatch.Draw(texture, point, null, color, angle, origin, scale, SpriteEffects.None, 0);
        }
    }
}