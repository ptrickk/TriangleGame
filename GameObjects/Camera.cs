using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace TriangleGame.GameObjects
{
    public class Camera
    {
        private Vector2 _position;
        private int _speed;

        public Camera(Vector2 position, int speed)
        {
            _position = position;
            _speed = speed;
        }

        public Vector2 Position
        {
            get => _position;
            set => _position = value;
        }

        public void Update()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Up) || Keyboard.GetState().IsKeyDown(Keys.W))
            {
                _position.Y -= _speed;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Down) || Keyboard.GetState().IsKeyDown(Keys.S))
            {
                _position.Y += _speed;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Right) || Keyboard.GetState().IsKeyDown(Keys.D))
            {
                _position.X += _speed;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left) || Keyboard.GetState().IsKeyDown(Keys.A))
            {
                _position.X -= _speed;
            }
        }

        public void Move(int x, int y)
        {
            Move(x, y);
        }
        public void Move(float x, float y)
        {
            _position.X += x;
            _position.Y += y;
        }
    }
}