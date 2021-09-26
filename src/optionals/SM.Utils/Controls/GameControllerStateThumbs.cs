using System;
using OpenTK;
using SharpDX.XInput;

namespace SM.Utils.Controls
{
    public struct GameControllerStateThumbs
    {
        public static GameControllerStateThumbs Default = new GameControllerStateThumbs()
            {Left = Vector2.Zero, Right = Vector2.Zero};

        public Vector2 Left;
        public Vector2 Right;

        public bool PressedLeft;
        public bool PressedRight;

        public bool AnyInteraction => Left != Vector2.Zero || Right != Vector2.Zero || PressedLeft || PressedRight;

        public GameControllerStateThumbs(GameController controller, Gamepad state)
        {
            Vector2 left = new Vector2(state.LeftThumbX, state.LeftThumbY) / short.MaxValue;
            Vector2 right = new Vector2(state.RightThumbX, state.RightThumbY) / short.MaxValue;

            Left = new Vector2(Math.Abs(left.X) < controller.Deadband ? 0 : left.X, Math.Abs(left.Y) < controller.Deadband ? 0 : left.Y);
            Right = new Vector2(Math.Abs(right.X) < controller.Deadband ? 0 : right.X, Math.Abs(right.Y) < controller.Deadband ? 0 : right.Y);

            PressedLeft = state.Buttons.HasFlag(GamepadButtonFlags.LeftThumb);
            PressedRight = state.Buttons.HasFlag(GamepadButtonFlags.RightThumb);
        }

        public override string ToString()
        {
            return $"Left: ({Left.X}; {Left.Y}){(PressedLeft ? " Pressed" : "")}; Right: ({Right.X}; {Right.Y}){(PressedRight ? " Pressed" : "")}";
        }
    }
}