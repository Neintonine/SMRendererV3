using SharpDX.XInput;

namespace SM.Utils.Controls
{
    public struct GameControllerStateButtons
    {
        public static GameControllerStateButtons Default = new GameControllerStateButtons()
        {
            _buttonFlags = GamepadButtonFlags.None,
            _lastButtonFlags = GamepadButtonFlags.None
        };

        private GamepadButtonFlags _buttonFlags;
        private GamepadButtonFlags _lastButtonFlags;

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

        public bool this[GamepadButtonFlags flags, bool once = false] => _buttonFlags.HasFlag(flags) && !(once && _lastButtonFlags.HasFlag(flags));

        public bool AnyInteraction { get; }

        internal GameControllerStateButtons(GamepadButtonFlags flags, GameController controller)
        {
            _buttonFlags = flags;
            _lastButtonFlags = controller._lastPressedButtons;

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