using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TriangleGame.Resources;
using TriangleGame.UI;

namespace TriangleGame
{
    public class Ore : Entity
    {
        private ResourceType _resource;
        private int _amount;
        private bool _occupied;

        private HoverText _hover;
        
        public Ore(Point position, Point dimension, ResourceType resource, Texture2D texture2D, Color color, int amount, bool occupied = false):base(position, texture2D, color)
        {
            Dimensions = dimension;
            _amount = amount;
            _occupied = occupied;
            _resource = resource;

            switch (resource)
            {
                case ResourceType.Crystals:
                    _color = Color.Aqua;
                    break;
                case ResourceType.Gas:
                    _color = Color.LimeGreen;
                    break;
                case ResourceType.Metal:
                    _color = Color.Silver;
                    break;
            }
            
            _hover = new HoverText(resource.ToString() + ":", new Rectangle(_position, _dimensions), Color.LimeGreen);
        }

        public ResourceType Resource
        {
            get => _resource;
        }
        
        public HoverText HoverText
        {
            get => _hover;
        }

        public int Amount
        {
            get => _amount;
        }

        public string ResourceToString()
        {
            if (_resource == ResourceType.Metal) return "metal";
            else if (_resource == ResourceType.Gas) return "gas";
            else if (_resource == ResourceType.Crystals) return "crystal";
            else return "error";
        }
        
        public bool Occupied
        {
            get => _occupied;
        }

        public bool SetAsOccupied()
        {
            if (_occupied)
            {
                return false;
            }
            else
            {
                _occupied = true;
                return true;
            }
        }
        
        public int Mine()
        {
            int material = 0;
            if (_occupied)
            {
                if (_amount <= 10)
                {
                    material = _amount;
                    _amount = 0;
                    _occupied = false;
                }
                else
                {
                    _amount -= 10;
                    material = 10;
                }
            }
            return material;
        }
    }
}