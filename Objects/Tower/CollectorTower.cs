using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TriangleGame.UI;

namespace TriangleGame
{
    public class CollectorTower : Tower
    {
        private Ore _occupied;

        public CollectorTower(Point position, Texture2D innerTexture, Texture2D outerTexture)
            : base(position, innerTexture, outerTexture, Color.Red)
        {
            _hover = new HoverText("Sammler", new Rectangle(_position, _dimensions), Color.LimeGreen);
        }

        public KeyValuePair<string, int> Update()
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
    }
}