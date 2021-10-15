using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TriangleGame.UI;

namespace TriangleGame.Manager
{
    public class UIManager
    {
        private List<Button> _buttons;
        private ResourceInfo _info;
        
        private GraphicsDeviceManager _graphics;
        private Point _buttonSize;

        public UIManager(GraphicsDeviceManager graphics, Point buttonSize)
        {
            _buttons = new List<Button>();
            _graphics = graphics;
            _buttonSize = buttonSize;

            _info = new ResourceInfo(new Point(10, 10), 5);
        }

        public void Initialize()
        {
            TextureManager textureManager = TextureManager.Instance;
            _buttons.Add(new Button(textureManager.Sprites["buttonAttack"], textureManager.Sprites["buttonFrame"],
                new Rectangle(new Point(10, _graphics.PreferredBackBufferHeight - (10 + _buttonSize.Y)), _buttonSize), TowerType.Attacker, "Angreifer", true));
            _buttons.Add(new Button(textureManager.Sprites["buttonCollector"], textureManager.Sprites["buttonFrame"],
                new Rectangle(
                    new Point((10 * 2) + _buttonSize.X, _graphics.PreferredBackBufferHeight - (10 + _buttonSize.Y)),
                    _buttonSize), TowerType.Collector, "Sammler"));
            _buttons.Add(new Button(textureManager.Sprites["buttonStorage"], textureManager.Sprites["buttonFrame"],
                new Rectangle(
                    new Point((10 * 3) + (_buttonSize.X * 2),
                        _graphics.PreferredBackBufferHeight - (10 + _buttonSize.Y)), _buttonSize), TowerType.Storage, "Speicher"));
        }

        public TowerType SelectedTower()
        {
            TowerType selected = TowerType.Default;

            foreach (var _button in _buttons)
            {
                if (_button.Selected)
                {
                    return _button.TowerType;
                }
            }
            return TowerType.Default;
        }

        public bool UiInteraction(Point mousePosition)
        {
            bool interaction = false;
            foreach (var button in _buttons)
            {
                if (button.IsSelected(mousePosition)) interaction = true;
            }

            if (interaction)
            {
                foreach (var button in _buttons)
                {
                    button.Deactivate();
                    button.IsSelected(mousePosition);
                }
            }
            
            return interaction;
        }
        
        public void Update(int metal, int gas, int crystals, Point mousePoint)
        {
            Update(new int[]{metal, gas, crystals }, mousePoint);    
        }
        
        public void Update(int[] resource, Point mousePoint)
        {
            _info.Update(resource);
            foreach (var button in _buttons)
            {
                button.Update(mousePoint);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _info.Draw(spriteBatch);
            
            foreach (var button in _buttons)
            {
                button.Draw(spriteBatch);
            }

            foreach (var button in _buttons)
            {
                button.DrawHover(spriteBatch);
            }
        }
    }
}