using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace TriangleGame.Manager
{
    public sealed class SoundManager
    {
        private static SoundManager instance;
        public Dictionary<string, SoundEffect> Sounds { get; private set; } = new Dictionary<string, SoundEffect>();
        private SoundEffect _selected;
        private float _volume;
        
        private SoundManager()
        {
        }

        public static SoundManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SoundManager();
                }

                return instance;
            }
        }


        public void ClearSound()
        {
            _selected = null;
        }

        public void SetSound(string name, float volume = 1f)
        {
            _selected = Sounds[name];
            _volume = volume;
        }

        public void Update()
        {
            if (_selected != null)
            {
                _selected.Play(_volume, 0, 0);
                _selected = null;
            }
        }

        public void LoadContent(ContentManager Content)
        {
            Sounds.Add("buttonPressed", Content.Load<SoundEffect>("buttonPressed"));
            Sounds.Add("invalidAction", Content.Load<SoundEffect>("invalidAction"));
            Sounds.Add("towerPlaced", Content.Load<SoundEffect>("towerPlaced"));
        }
    }
}