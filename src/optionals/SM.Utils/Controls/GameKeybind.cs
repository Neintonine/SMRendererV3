using System;

namespace SM.Utils.Controls
{
    public class GameKeybind
    {
        public Func<GameKeybindContext, object> Keyboard;
        public Func<GameKeybindContext, object> Controller;
        public Func<GameKeybindContext, object> AI;

        public Func<GameKeybindContext, object> this[GameKeybindActorType type]
        {
            get
            {
                switch (type)
                {
                    case GameKeybindActorType.AI:
                        return AI;
                    case GameKeybindActorType.Keyboard:
                        return Keyboard;
                    case GameKeybindActorType.Controller:
                        return Controller;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(type), type, null);
                }
            }
        }
    }
}