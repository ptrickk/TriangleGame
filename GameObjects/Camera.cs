using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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

        public void Update(GraphicsDeviceManager graphics, Rectangle boundaries)
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
            
            if (Position.Y + graphics.PreferredBackBufferHeight >
                boundaries.Location.Y + boundaries.Size.Y)
            {
                Move(0,
                    boundaries.Location.Y + boundaries.Size.Y -
                    (Position.Y + graphics.PreferredBackBufferHeight));
            }
            else if (Position.Y < boundaries.Location.Y)
            {
                Move(0, boundaries.Location.Y - Position.Y);
            }
            else if (Position.X < boundaries.Location.X)
            {
                Move(boundaries.Location.X - Position.X, 0);
            }
            else if (Position.X + graphics.PreferredBackBufferWidth >
                     boundaries.Location.X + boundaries.Size.X)
            {
                Move(
                    boundaries.Location.X + boundaries.Size.X -
                    (Position.X + graphics.PreferredBackBufferWidth), 0);
            }

            if (Position.Y < boundaries.Location.Y + boundaries.Size.Y)
            {
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