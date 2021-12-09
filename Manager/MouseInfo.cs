using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using TriangleGame.GameObjects;

namespace TriangleGame.Manager
{
    public class MouseInfo
    {
        private Point _position;
        private Point _relativPosition;
        private bool _leftPressed;
        private ButtonState _lastLeft;

        private bool _rightPressed;
        private ButtonState _lastRight;

        public MouseInfo()
        {
            _position = Point.Zero;
            _leftPressed = false;
            _rightPressed = false;
            _lastLeft = ButtonState.Released;
            _lastRight = ButtonState.Released;
        }

        public Point Position
        {
            get => _position;
        }

        public Point RelativPosition
        {
            get => _relativPosition;
        }
        
        public bool LeftPressed
        {
            get => _leftPressed;
        }

        public bool RightPressed
        {
            get => _rightPressed;
        }

        public void Update(Camera camera)
        {
            _position = Mouse.GetState().Position;
            
            Vector3 temp = Vector3.Transform(new Vector3(Mouse.GetState().Position.ToVector2(), 0),
                Matrix.CreateTranslation(new Vector3(camera.Position, 0)));
            _relativPosition = new Vector2(temp.X, temp.Y).ToPoint();

            if (Mouse.GetState().LeftButton == ButtonState.Pressed && _lastLeft == ButtonState.Released)
            {
                _leftPressed = true;
            }
            else
            {
                _leftPressed = false;
            }

            if (Mouse.GetState().RightButton == ButtonState.Pressed && _lastRight == ButtonState.Released)
            {
                _rightPressed = true;
            }
            else
            {
                _rightPressed = false;
            }

            _lastLeft = Mouse.GetState().LeftButton;
            _lastRight = Mouse.GetState().RightButton;
        }
    }
}