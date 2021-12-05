using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TriangleGame.UI.Buttons
{
    public class TowerButton : Button
    {
        /// <summary>
        /// The type of Tower the Button represents
        /// </summary>
        private TowerType _towerType;
        
        
        public TowerButton(Texture2D texture, Texture2D frame, Rectangle dimensions, TowerType towerType, string text, bool selected = false):base(texture, frame, dimensions, text, selected)
        {
            _towerType = towerType;
        }
        
        public TowerType TowerType
        {
            get => _towerType;
        }
    }
}