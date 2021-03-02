using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows.Documents;
using System.Windows.Forms;
using OpenTK.Input;
using SharpDX.Win32;

namespace SM.Base.Controls
{
    public class Keyboard
    {
        internal static KeyboardState? _keyboardState;
        internal static List<Key> _lastPressedKeys = new List<Key>();

        public static bool IsAnyKeyPressed => _keyboardState?.IsAnyKeyDown == true;


        internal static void SetStage()
        {
            if (_keyboardState.HasValue)
            {
                _lastPressedKeys = new List<Key>();

                foreach (object o in Enum.GetValues(typeof(Key)))
                {
                    if (_keyboardState.Value[(Key)o]) _lastPressedKeys.Add((Key)o);
                }
            }

            _keyboardState = OpenTK.Input.Keyboard.GetState();
        }

        public static bool IsDown(Key key, bool once = false) => _keyboardState?[key] == true && !(once && _lastPressedKeys.Contains(key));
        public static bool WasDown(Key key) => _keyboardState?[key] == false && _lastPressedKeys.Contains(key);
        public static bool IsUp(Key key, bool once = false) => _keyboardState?[key] == false && !(once && !_lastPressedKeys.Contains(key));

        public static bool AreSpecificKeysPressed(int startIndex, int endIndex, bool once = false)
        {
            if (startIndex > endIndex)
                throw new ArgumentException("The startIndex is greater than the endIndex.", nameof(startIndex));

            int length = endIndex - startIndex;
            for (int i = 0; i < length; i++)
            {
                int actualIndex = i + startIndex;
                Key key = (Key) actualIndex;
                if (IsDown(key, once)) return true;
            }

            return false;
        }

        public static bool AreSpecificKeysPressed(params Key[] keys) => AreSpecificKeysPressed(false, keys);

        public static bool AreSpecificKeysPressed(bool once, params Key[] keys)
        {
            foreach (Key key in keys)
            {
                if (IsDown(key, once)) return true;
            }

            return false;
        }

        public static bool AreSpecificKeysPressed(int startIndex, int endIndex, out Key[] pressedKeys, bool once = false)
        {
            if (startIndex > endIndex)
                throw new ArgumentException("The startIndex is greater than the endIndex.", nameof(startIndex));

            int length = endIndex - startIndex;

            bool success = false;
            List<Key> keys = new List<Key>();
            for (int i = 0; i < length; i++)
            {
                int actualIndex = i + startIndex;
                Key key = (Key)actualIndex;
                if (IsDown(key, once))
                {
                    keys.Add(key);
                    success = true;
                }

            }

            pressedKeys = keys.ToArray();
            return success;
        }

        public static bool AreSpecificKeysPressed(out Key[] pressedKey, params Key[] keys) => AreSpecificKeysPressed(false, out pressedKey, keys);
        
        public static bool AreSpecificKeysPressed(bool once, out Key[] pressedKeys, params Key[] keys)
        {
            List<Key> pressedKey = new List<Key>();
            bool success = false;

            foreach (Key key in keys)
            {
                if (IsDown(key, once))
                {
                    pressedKey.Add(key);
                    success = true;
                }
            }

            pressedKeys = pressedKey.ToArray();
            return success;
        }
    }
}