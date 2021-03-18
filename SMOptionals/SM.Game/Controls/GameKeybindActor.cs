using OpenTK.Input;

namespace SM.Game.Controls
{
    public enum GameKeybindActorType
    {
        AI,
        Keyboard,
        Controller
    }

    public struct GameKeybindActor
    {
        private GameKeybindActorType _type;
        private GameController? _controller;

        private GameKeybindHost _keybindHost;

        public GameKeybindActorType Type => _type;
        public GameController? Controller => _controller;

        public object[] Parameter;

        private GameKeybindActor(GameKeybindActorType type, GameController? controller)
        {
            _type = type;
            _controller = controller;

            _keybindHost = null;

            Parameter = new object[0];
        }

        public void ConnectHost(GameKeybindHost host)
        {
            _keybindHost = host;
        }

        public ReturnType Get<ReturnType>(string name, params object[] param)
        {
            return (ReturnType) Get(name, param);
        }

        public object Get(string name, params object[] objects)
        {
            if (_keybindHost == null) return null;
            if (!_keybindHost._actions.ContainsKey(name)) return null;

            GameKeybind keybind = _keybindHost._actions[name];

            GameKeybindContext context = new GameKeybindContext()
            {
                Actor = this,
                Host = _keybindHost,

                ActorParameter = Parameter,
                InstanceParameter = objects,

                KeyboardState = Keyboard.GetState(),
                MouseState = Mouse.GetState(),
                ControllerState = Controller?.GetState(),
            };

            return keybind[Type].Invoke(context);
        }

        public static GameKeybindActor CreateAIActor()
        {
            return new GameKeybindActor(GameKeybindActorType.AI, null);
        }

        public static GameKeybindActor CreateKeyboardActor()
        {
            return new GameKeybindActor(GameKeybindActorType.Keyboard, null);
        }

        public static GameKeybindActor CreateControllerActor(int id)
        {
            return CreateControllerActor(new GameController(id));
        }

        public static GameKeybindActor CreateControllerActor(GameController controller)
        {
            return new GameKeybindActor(GameKeybindActorType.Controller, controller);
        }

    }
}