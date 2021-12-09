using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TriangleGame.Manager;
using TriangleGame.UI.Buttons;
using ResourceType = TriangleGame.Resources.ResourceType;

namespace TriangleGame.UI
{
    public class CollectorUI
    {
        private List<CollectorButton> _buttons = new List<CollectorButton>();
        private bool open;
        private Rectangle _parent;

        public CollectorUI(Point position, ResourceType selected, Rectangle parent)
        {
            open = false;
            _parent = parent;

            TextureManager textureManager = TextureManager.Instance;

            _buttons.Add(new CollectorButton(textureManager.Sprites["iconMetal"], textureManager.Sprites["iconFrame"],
                new Rectangle(position.X - 36, position.Y - 44, 24, 24), ResourceType.Metal, "Metal"));
            _buttons.Add(new CollectorButton(textureManager.Sprites["iconGas"], textureManager.Sprites["iconFrame"],
                new Rectangle(position.X - 12, position.Y - 44, 24, 24), ResourceType.Gas, "Gas"));
            _buttons.Add(new CollectorButton(textureManager.Sprites["iconCrystal"], textureManager.Sprites["iconFrame"],
                new Rectangle(position.X + 12, position.Y - 44, 24, 24), ResourceType.Crystals, "Crystal"));
        }

        public bool Open
        {
            get => open;
        }

        public ResourceType Selected()
        {
            foreach (var button in _buttons)
            {
                if (button.Selected)
                {
                    return button.ResourceType;
                }
            }

            return ResourceType.None;
        }

        private void ToggleButtons(CollectorButton selected)
        {
            foreach (var button in _buttons)
            {
                if (!button.Equals(selected))
                {
                    button.Deactivate();
                }
            }
        }

        public void Update(MouseInfo mouse)
        {
            if (mouse.LeftPressed)
            {
                Console.WriteLine("CHECK");
                if (open)
                {
                    Console.WriteLine("OPEN");
                    foreach (var button in _buttons)
                    {
                        button.Update(mouse.RelativPosition);
                        if (button.IsSelected(mouse.RelativPosition))
                        {
                            ToggleButtons(button);
                        }
                    }
                    open = false;
                }
                else if (_parent.Contains(mouse.RelativPosition))
                {
                    Console.WriteLine("OPENED");
                    open = true;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (open)
            {
                foreach (CollectorButton button in _buttons)
                {
                    button.Draw(spriteBatch);
                    //button.DrawHover(spriteBatch);
                }
            }
        }
    }
}