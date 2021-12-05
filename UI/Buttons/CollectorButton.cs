using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TriangleGame.Resources;

namespace TriangleGame.UI.Buttons
{
    public class CollectorButton : Button
    {
        /// <summary>
        /// The type of Resource the Button represents
        /// </summary>
        private ResourceType _resourceType;
        
        public CollectorButton(Texture2D texture, Texture2D frame, Rectangle dimensions, ResourceType resourceType,
            string text, bool selected = false):base(texture, frame, dimensions, text, selected)
        {
            _resourceType = resourceType;
        }

        public ResourceType ResourceType
        {
            get => _resourceType;
        }
    }
}