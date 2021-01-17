using System;
using System.Collections.Generic;
using OpenTK.Input;

namespace SM.Game.Controls
{
    public class GameKeybindHost
    {
        internal Dictionary<string, GameKeybind> _actions = new Dictionary<string, GameKeybind>();

        public GameKeybindHost()
        { }

        public GameKeybindHost(GameKeybindList setup)
        {
            for (int i = 0; i < setup.Count; i++)
            {
                _actions[setup[i].Key] = setup[i].Value;
            }
        }
        
        public void Setup(string name, Func<GameKeybindContext, object> keyboard = null, Func<GameKeybindContext, object> gameController = null, Func<GameKeybindContext, object> ai = null)
        {
            GameKeybind bind;
            if (_actions.ContainsKey(name))
            {
                bind = _actions[name];
            }
            else
            {
                bind = new GameKeybind();
                _actions.Add(name, bind);
            }

            bind.Keyboard = keyboard;
            bind.Controller = gameController;
            bind.AI = ai;
        }
    }
}