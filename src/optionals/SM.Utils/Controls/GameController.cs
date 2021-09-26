using SharpDX.XInput;
using SM.Base;

namespace SM.Utils.Controls
{
    public class GameController
    {
        public static float GlobalDeadband = .1F;
        
        private Controller _controller;
        private ulong _lastFrame;

        internal GamepadButtonFlags _lastPressedButtons;

        public float Deadband { get; set; }
        public bool IsConnected => _controller.IsConnected;

        public GameControllerState LastState { get; private set; }

        public UserIndex Index { get; private set; }
        
        public GameController(int id) : this((UserIndex)id)
        {}

        public GameController(UserIndex index = UserIndex.Any)
        {
            _lastPressedButtons = GamepadButtonFlags.None;
            _controller = new Controller(index);
            Index = index;
            Deadband = GlobalDeadband;
        }

        public GameControllerState GetState(bool force = false)
        {
            if (!force && _lastFrame == SMRenderer.CurrentFrame)
            {
                return LastState;
            }

            GameControllerState st = new GameControllerState(true);
            if (IsConnected)
            {
                Gamepad state = _controller.GetState().Gamepad;
                st = new GameControllerState(state, this);
                _lastPressedButtons = state.Buttons;
            }
            
            LastState = st;

            _lastFrame = SMRenderer.CurrentFrame;

            return st;
        }

    }
}