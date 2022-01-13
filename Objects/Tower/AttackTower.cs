using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TriangleGame.GameObjects;
using TriangleGame.Manager;
using TriangleGame.Objects.Enemys;
using TriangleGame.UI;

namespace TriangleGame
{
    public class AttackTower : Tower
    {
        private Enemy _target;
        private int _range;
        private int _strength;
        
        private bool _animation;
        
        public AttackTower(Point position, Texture2D innerTexture, Texture2D outerTexture, int hitpoints, int range, int strength, Color teamColor)
            : base(position, innerTexture, outerTexture, hitpoints, teamColor)
        {
            _hover.Text = "Angreifer";
            _range = range;
            _strength = strength;
            _animation = false;
            
            _target = null;
        }

        public bool TargetSelected()
        {
            return _target != null;
        }
        
        public void TargetEnemy(Enemy enemy)
        {
            _target = enemy;
        }

        public void Attack()
        {
            _animation = !_animation;
            if (_target.TakeDamage(_strength))
            {
                _animation = false;
                _target = null;
            }
        }
        
        public override void Update()
        {
            if (_target != null)
            {
                float distance = Vector2.Distance(_target.Position.ToVector2(), _dimensions.Location.ToVector2());
                if (distance < _range)
                {
                    Attack();
                }
                else
                {
                    _target = null;
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (_animation && _target != null)
            {
                DrawFuntions.DrawLine(spriteBatch, TextureManager.Instance.Sprites["pixel"], Position.ToVector2(), _target.Position.ToVector2(), Color.Violet);
            }
            
            DrawFuntions.DrawCircle(spriteBatch, new Point(_dimensions.X - _range / 2, _dimensions.Y - _range / 2), _range, new Color(200, 0, 0 , 50));
            
            base.Draw(spriteBatch);
        }
    }
}