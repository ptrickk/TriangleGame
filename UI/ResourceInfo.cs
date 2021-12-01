using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TriangleGame.Manager;

namespace TriangleGame.UI
{
    public class ResourceInfo
    {
        private Point _position;
        private int _padding;
        private int[] _value = new int[3];
        private int[] _max = new int[3];

        public ResourceInfo(Point position, int padding)
        {
            _position = position;
            _padding = padding;
        }

        public void Update(int[] resource, int[] maxAmounts)
        {
            _value = resource;
            _max = maxAmounts;
        }
        
        

        public void Draw(SpriteBatch spriteBatch)
        {
            TextureManager textureManager = TextureManager.Instance;
            SpriteFont font = textureManager.Fonts["basicfont"];
            Texture2D background = textureManager.Sprites["pixel"];

            string[] info = new string[3];
            int maxLength = -1;
            for (var i = 0; i < 3; i++)
            {
                string name = "error";
                switch (i)
                {
                    case 0: name = "Metal: ";
                        break;
                    case 1: name = "Gas: ";
                        break;
                    case 2: name = "Crystals: ";
                        break;
                }
                info[i] = name + _value[i] + " / " + _max[i];
                if (font.MeasureString(info[i]).X > maxLength) maxLength = (int) font.MeasureString(info[i]).X;
            }

            Rectangle dimension =
                new Rectangle(_position, new Point(maxLength + _padding * 2, (int)  (font.MeasureString(info[0]).Y * 3) + (_padding * 4) ));
            
            spriteBatch.Draw(background, dimension, Color.Black);
            for (var i = 0; i < 3; i++)
            {
                Point textPos = new Point(_position.X + _padding,(int)(_position.Y + (font.MeasureString(info[0]).Y * i + _padding * (1 + i))));
                spriteBatch.DrawString(font, info[i], textPos.ToVector2(), Color.LimeGreen);
            }
        }
    }
}