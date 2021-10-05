using Microsoft.Xna.Framework.Graphics;

namespace TriangleGame.Resources
{
    public class Resource
    {
        protected Texture2D _texture;
        protected string _name;
        protected ResourceType _type;
        protected int _amount;
        protected int _maxAmount;
        
        public Resource(string name, Texture2D texture, ResourceType type, int startAmount = 0, int maxAmount = 1000)
        {
            _texture = texture;
            _name = name;
            _type = type;
            _amount = startAmount;
            _maxAmount = maxAmount;
        }

        public void IncreaseAmount(int amount)
        {
            if (_amount + amount < _maxAmount)
            {
                _amount += amount;
            }
            else
            {
                _amount = _maxAmount;
            }
        }

        public void IncreaseMaxAmount(int increase)
        {
            _maxAmount += increase;
        }
    }
}