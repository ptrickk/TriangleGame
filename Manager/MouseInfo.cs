using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace TriangleGame.Manager
{
    public class MouseInfo
    {
        private Point _position;
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

        public bool LeftPressed
        {
            get => _leftPressed;
        }
        
        public bool RightPressed
        {
            get => _rightPressed;
        }

        public void Update()
        {
            _position = Mouse.GetState().Position;
            
            if (Mouse.GetState().LeftButton == ButtonState.Pressed && _lastLeft == ButtonState.Released)
            {
                _leftPressed = true;
            }
            
            if (Mouse.GetState().RightButton == ButtonState.Pressed && _lastRight == ButtonState.Released)
            {
                _rightPressed = true;
            }
            
            _lastLeft = Mouse.GetState().LeftButton;
            _lastRight = Mouse.GetState().RightButton;
        }
    }
}