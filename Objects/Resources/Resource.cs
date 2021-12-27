using Microsoft.Xna.Framework.Graphics;

namespace TriangleGame.Resources
{
    /// <summary>
    /// A resource is a type of resource in the inventory of a player
    /// ist has an maximum amount that it can not surpass and a current amount
    /// </summary>
    public class Resource
    {
        /// <summary>
        /// The kind of Resource that is managed by this object
        /// </summary>
        private ResourceType _type;
        /// <summary>
        /// current amount of the resource currently stored
        /// </summary>
        private int _amount;
        /// <summary>
        /// The maximum amount, that can currently be stored
        /// </summary>
        private int _maxAmount;
        
        /// <summary>
        /// Basic constructor that only sets the variables
        /// </summary>
        /// <param name="type">Type of Resource to be stored</param>
        /// <param name="startAmount">Start amount of resource (default 0)</param>
        /// <param name="maxAmount">Maximum amount of storage to begin the game (default 1000)</param>
        public Resource(ResourceType type, int startAmount = 0, int maxAmount = 1000)
        {
            _type = type;
            _amount = startAmount;
            _maxAmount = maxAmount;
        }
        /// <summary>
        /// Get fo Amount
        /// </summary>
        public int Amount
        {
            get => _amount;
        }
        /// <summary>
        /// Get for maximum storage
        /// </summary>
        public int Maximum
        {
            get => _maxAmount;
        }
        /// <summary>
        /// Increases current Amount of resource
        /// </summary>
        /// <param name="amount"></param>
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
        /// <summary>
        /// Increases (or decreases) maximum amount
        /// </summary>
        /// <param name="increase"></param>
        public void IncreaseMaxAmount(int increase)
        {
            _maxAmount += increase;
            if (_maxAmount < _amount)
            {
                _amount = _maxAmount;
            }
        }
    }
}