using SharpDX.XInput;

namespace SM.Optionals.Controls
{
    public struct GameControllerStateDPad
    {
        public static GameControllerStateDPad Default = new GameControllerStateDPad(GamepadButtonFlags.None);

        public bool Up;
        public bool Down;
        public bool Left;
        public bool Right;

        internal GameControllerStateDPad(GamepadButtonFlags flags)
        {
            Up = flags.HasFlag(GamepadButtonFlags.DPadUp);
            Down = flags.HasFlag(GamepadButtonFlags.DPadDown);
            Left = flags.HasFlag(GamepadButtonFlags.DPadLeft);
            Right = flags.HasFlag(GamepadButtonFlags.DPadRight);
        }

        public override string ToString()
        {
            return
                $"Up: {(Up ? "1" : "0")}; Down: {(Down ? "1" : "0")}; Left: {(Left ? "1" : "0")}; Right: {(Right ? "1" : "0")};";
        }
    }
}