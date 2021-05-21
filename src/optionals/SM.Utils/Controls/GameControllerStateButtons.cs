using SharpDX.XInput;

namespace SM.Utils.Controls
{
    public struct GameControllerStateButtons
    {
        public static GameControllerStateButtons Default = new GameControllerStateButtons(GamepadButtonFlags.None);

        private GamepadButtonFlags _buttonFlags;

        public bool X;
        public bool Y;
        public bool A;
        public bool B;

        public bool LB;
        public bool RB;

        public bool LeftThumb;
        public bool RightThumb;

        public bool Start;
        public bool Back;

        public bool this[GamepadButtonFlags flags] => _buttonFlags.HasFlag(flags);

        public bool AnyInteraction { get; }

        internal GameControllerStateButtons(GamepadButtonFlags flags)
        {
            _buttonFlags = flags;

            X = flags.HasFlag(GamepadButtonFlags.X);
            Y = flags.HasFlag(GamepadButtonFlags.Y);
            A = flags.HasFlag(GamepadButtonFlags.A);
            B = flags.HasFlag(GamepadButtonFlags.B);

            LB = flags.HasFlag(GamepadButtonFlags.LeftShoulder);
            RB = flags.HasFlag(GamepadButtonFlags.RightShoulder);

            LeftThumb = flags.HasFlag(GamepadButtonFlags.LeftThumb);
            RightThumb = flags.HasFlag(GamepadButtonFlags.RightThumb);

            Start = flags.HasFlag(GamepadButtonFlags.Start);
            Back = flags.HasFlag(GamepadButtonFlags.Back);

            AnyInteraction = (int) flags >= 16;
        }

        public override string ToString()
        {
            return $"X: {(X ? "1" : "0")}; Y: {(Y ? "1" : "0")}; A: {(A ? "1" : "0")}; B: {(B ? "1" : "0")}; LB: {(LB ? "1" : "0")}; RB: {(RB ? "1" : "0")}; LT: {(LeftThumb ? "1" : "0")}; RT: {(RightThumb ? "1" : "0")}";
        }
    }
}