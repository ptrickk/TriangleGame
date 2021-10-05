using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;


namespace TriangleGame.Manager
{
    public class TextureManager
    {
        public bool Loaded = false;
        public Dictionary<string, Texture2D> Sprites { get; private set; } = new Dictionary<string, Texture2D>();
        public Dictionary<string, SpriteFont> Fonts { get; private set; } = new Dictionary<string, SpriteFont>();

        public void LoadContent(ContentManager Content)
        {
            //Sprites.Add("circle", Content.Load<Texture2D>("circle_white"));
            Sprites.Add("pixel", Content.Load<Texture2D>("pixel"));
            Sprites.Add("outerTower", Content.Load<Texture2D>("tower_outer"));
            Sprites.Add("innerTower", Content.Load<Texture2D>("tower_inner"));
            
            Fonts.Add("basicfont", Content.Load<SpriteFont>("basicfont"));
            
            Loaded = true;
        }
    }
}