using OpenTK.Input;

namespace SM.Game.Controls
{
    public struct GameKeybindContext
    {
        public KeyboardState KeyboardState;
        public MouseState MouseState;
        public GameControllerState? ControllerState;

        public GameKeybindActor Actor;
        public GameKeybindHost Host;

        public object[] InstanceParameter;
        public object[] ActorParameter;
    }
}