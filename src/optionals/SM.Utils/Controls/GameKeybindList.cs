using System;
using System.Collections.Generic;

namespace SM.Utils.Controls
{
    public class GameKeybindList : List<KeyValuePair<string, GameKeybind>>
    {
        public void Add(string name, GameKeybind keybind)
        {
            Add(new KeyValuePair<string, GameKeybind>(name, keybind));
        }

        public void Add(string name, Func<GameKeybindContext, object> keyboard = null,
            Func<GameKeybindContext, object> controller = null)
        {
            Add(new KeyValuePair<string, GameKeybind>(name, new GameKeybind()
            {
                AI = null,
                Controller = controller,
                Keyboard = keyboard
            }));
        }
    }
}