using SharpDX.XInput;

namespace SM.Utils.Controls
{
    public struct GameControllerStateDPad
    {
        public static GameControllerStateDPad Default = new GameControllerStateDPad(GamepadButtonFlags.None);

        public bool Up;
        public bool Down;
        public bool Left;
        public bool Right;

        public bool AnyInteraction { get; }

        internal GameControllerStateDPad(GamepadButtonFlags flags)
        {
            Up = flags.HasFlag(GamepadButtonFlags.DPadUp);
            Down = flags.HasFlag(GamepadButtonFlags.DPadDown);
            Left = flags.HasFlag(GamepadButtonFlags.DPadLeft);
            Right = flags.HasFlag(GamepadButtonFlags.DPadRight);

            AnyInteraction = (int)flags > 0 && (int) flags < 16;
        }

        public override string ToString()
        {
            return
                $"Up: {(Up ? "1" : "0")}; Down: {(Down ? "1" : "0")}; Left: {(Left ? "1" : "0")}; Right: {(Right ? "1" : "0")};";
        }
    }
}