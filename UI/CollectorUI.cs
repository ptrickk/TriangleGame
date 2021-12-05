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
                new Rectangle(position.X - 24, position.Y - 36, 16, 16), ResourceType.Metal, "Metal"));
            _buttons.Add(new CollectorButton(textureManager.Sprites["iconGas"], textureManager.Sprites["iconFrame"],
                new Rectangle(position.X - 8, position.Y - 36, 16, 16), ResourceType.Metal, "Gas"));
            _buttons.Add(new CollectorButton(textureManager.Sprites["iconCrystal"], textureManager.Sprites["iconFrame"],
                new Rectangle(position.X + 8, position.Y - 36, 16, 16), ResourceType.Metal, "Crystal"));
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

        public void Update(Point mousePosition)
        {
            Console.WriteLine("CHECK");
            if (open)
            {
                foreach (var button in _buttons)
                {
                    button.Update(mousePosition);
                    if (!button.IsSelected(mousePosition))
                    {
                        button.Deactivate();
                    }
                }

                open = false;
            }
            else if (_parent.Contains(mousePosition))
            {
                open = true;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (open)
            {
                foreach (var button in _buttons)
                {
                    button.Draw(spriteBatch);
                    button.DrawHover(spriteBatch);
                }
            }
        }
    }
}