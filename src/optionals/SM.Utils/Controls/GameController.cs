using SharpDX.XInput;

namespace SM.Optionals.Controls
{
    public struct GameController
    {
        public static float GlobalDeadband = 2500;
        
        private Controller _controller;

        public float Deadband { get; set; }
        public bool IsConnected => _controller.IsConnected;

        public UserIndex Index { get; private set; }

        public GameController(int id) : this((UserIndex)id)
        {}

        public GameController(UserIndex index = UserIndex.Any)
        {
            _controller = new Controller(index);
            Index = index;
            Deadband = GlobalDeadband;
        }

        public GameControllerState GetState()
        {
            if (!IsConnected)
            {
                return new GameControllerState(true);
            }

            Gamepad state = _controller.GetState().Gamepad;
            
            return new GameControllerState(state, ref this);
        }

    }
}