using Microsoft.Xna.Framework.Input;

namespace Utilities
{
    public struct InputState
    {
        public MouseState mouseState { get; set; }
        public KeyboardState keyboardState { get; set; }
        public JoystickState joystickState { get; set; }
        public GamePadState gamepadState { get; set; }
        public InputState(MouseState mousestate, KeyboardState keyboardstate)
        {
            mouseState = mousestate;
            keyboardState = keyboardstate;
            joystickState = new JoystickState();
            gamepadState = new GamePadState();
        }
        public InputState(MouseState mousestate, KeyboardState keyboardstate, JoystickState joystickstate, GamePadState gamepadstate)
        {
            mouseState = mousestate;
            keyboardState = keyboardstate;
            joystickState = joystickstate;
            gamepadState = gamepadstate;
        }

        public bool IsKeyDown(Keys k)
        {
            return keyboardState.IsKeyDown(k);
        }

        public bool IsKeyUp(Keys k)
        {
            return keyboardState.IsKeyUp(k);
        }

        public bool IsLeftMouseButtonDown()
        {
            return mouseState.LeftButton == ButtonState.Pressed;
        }

        public bool IsRightMouseButtonDown()
        {
            return mouseState.RightButton == ButtonState.Pressed;
        }

        public bool IsMiddleMouseButtonDown()
        {
            return mouseState.MiddleButton == ButtonState.Pressed;
        }
    }
}
