using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;


namespace TriangleGame.Manager
{
    public sealed class TextureManager
    {
        private static TextureManager instance = null;

        private TextureManager()
        {
            
        }

        public static TextureManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new TextureManager();
                }
                return instance;
            }
        }
        public Dictionary<string, Texture2D> Sprites { get; private set; } = new Dictionary<string, Texture2D>();
        public Dictionary<string, SpriteFont> Fonts { get; private set; } = new Dictionary<string, SpriteFont>();

        public void LoadContent(ContentManager Content)
        {
            //Sprites.Add("circle", Content.Load<Texture2D>("circle_white"));
            Sprites.Add("pixel", Content.Load<Texture2D>("pixel"));
            Sprites.Add("outerTower", Content.Load<Texture2D>("tower_outer"));
            Sprites.Add("innerTower", Content.Load<Texture2D>("tower_inner"));
            Sprites.Add("baseTower", Content.Load<Texture2D>("tower_base"));
            Sprites.Add("buttonAttack", Content.Load<Texture2D>("icon_attack"));
            Sprites.Add("buttonCollector", Content.Load<Texture2D>("icon_collector"));
            Sprites.Add("buttonStorage", Content.Load<Texture2D>("icon_storage"));
            Sprites.Add("buttonFrame", Content.Load<Texture2D>("button_frame"));
            Sprites.Add("towerStorageTex", Content.Load<Texture2D>("tower_storage_texture"));
            Sprites.Add("towerStorageTint", Content.Load<Texture2D>("tower_storage_tint"));
            Sprites.Add("towerCollectorTex", Content.Load<Texture2D>("tower_collector_texture"));
            Sprites.Add("towerCollectorTint", Content.Load<Texture2D>("tower_collector_tint"));
            Sprites.Add("towerAttackerTint", Content.Load<Texture2D>("tower_attack_tint"));
            Sprites.Add("towerAttackerTexture", Content.Load<Texture2D>("tower_attack_texture"));
            Sprites.Add("iconMetal", Content.Load<Texture2D>("icon_metal"));
            Sprites.Add("iconGas", Content.Load<Texture2D>("icon_gas"));
            Sprites.Add("iconCrystal", Content.Load<Texture2D>("Icon_crystal"));
            Sprites.Add("iconFrame", Content.Load<Texture2D>("collector_selected_frame"));
            
            Fonts.Add("basicfont", Content.Load<SpriteFont>("minecraftianx12"));
        }
    }
}