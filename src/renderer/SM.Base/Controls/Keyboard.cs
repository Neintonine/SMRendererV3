#region usings

using System;
using System.Collections.Generic;
using OpenTK.Input;

#endregion

namespace SM.Base.Controls
{
    /// <summary>
    /// A static class to get keyboard inputs.
    /// </summary>
    public static class Keyboard
    {
        private static KeyboardState? _keyboardState;
        private static List<Key> _lastPressedKeys = new List<Key>();

        /// <summary>
        /// True, when ANY key pressed.
        /// </summary>
        public static bool IsAnyKeyPressed => _keyboardState?.IsAnyKeyDown == true;


        internal static void SetStage()
        {
            if (_keyboardState.HasValue)
            {
                _lastPressedKeys = new List<Key>();

                foreach (object o in Enum.GetValues(typeof(Key)))
                    if (_keyboardState.Value[(Key) o])
                        _lastPressedKeys.Add((Key) o);
            }

            _keyboardState = OpenTK.Input.Keyboard.GetState();
        }

        /// <summary>
        /// Checks if a key is down.
        /// </summary>
        /// <param name="key">The key</param>
        /// <param name="once">If true, the method doesn't return true, when it was pressed one stage before.</param>
        /// <returns></returns>
        public static bool IsDown(Key key, bool once = false)
        {
            return _keyboardState?[key] == true && !(once && _lastPressedKeys.Contains(key));
        }

        /// <summary>
        /// Checks if a key was down but not anymore.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool WasDown(Key key)
        {
            return _keyboardState?[key] == false && _lastPressedKeys.Contains(key);
        }

        /// <summary>
        /// Check if a is up.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="once">If true, the method doesn't return true, when it was up one stage before.</param>
        /// <returns></returns>
        public static bool IsUp(Key key, bool once = false)
        {
            return _keyboardState?[key] == false && !(once && !_lastPressedKeys.Contains(key));
        }

        /// <summary>
        /// Checks if specific keys are down.
        /// </summary>
        /// <param name="startIndex">Startindex</param>
        /// <param name="endIndex">Endindex</param>
        /// <param name="once">If true, it ignores keys that were down a state before.</param>
        /// <returns>True if any of the specific keys where found down.</returns>
        /// <exception cref="ArgumentException">The start index can't be greater then the end index.</exception>
        public static bool AreSpecificKeysPressed(int startIndex, int endIndex, bool once = false)
        {
            if (startIndex > endIndex)
                throw new ArgumentException("The startIndex is greater than the endIndex.", nameof(startIndex));

            int length = endIndex - startIndex;
            for (int i = 0; i < length + 1; i++)
            {
                int actualIndex = i + startIndex;
                Key key = (Key) actualIndex;
                if (IsDown(key, once)) return true;
            }

            return false;
        }

        /// <summary>
        /// Checks if any of the specific keys are pressed.
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public static bool AreSpecificKeysPressed(params Key[] keys)
        {
            return AreSpecificKeysPressed(false, keys);
        }

        /// <summary>
        /// Checks if any of the specific keys are pressed.
        /// </summary>
        /// <param name="once">If true, it ignores keys that were down a state before.</param>
        /// <param name="keys"></param>
        /// <returns></returns>
        public static bool AreSpecificKeysPressed(bool once, params Key[] keys)
        {
            foreach (Key key in keys)
                if (IsDown(key, once))
                    return true;

            return false;
        }

        /// <summary>
        /// Checks if specific keys are down and returns the pressed keys.
        /// </summary>
        /// <param name="startIndex">Startindex</param>
        /// <param name="endIndex">Endindex</param>
        /// <param name="pressedKeys"></param>
        /// <param name="once">If true, it ignores keys that were down a state before.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static bool AreSpecificKeysPressed(int startIndex, int endIndex, out Key[] pressedKeys,
            bool once = false)
        {
            if (startIndex > endIndex)
                throw new ArgumentException("The startIndex is greater than the endIndex.", nameof(startIndex));

            int length = endIndex - startIndex;

            bool success = false;
            List<Key> keys = new List<Key>();
            for (int i = 0; i < length + 1; i++)
            {
                int actualIndex = i + startIndex;
                Key key = (Key) actualIndex;
                if (IsDown(key, once))
                {
                    keys.Add(key);
                    success = true;
                }
            }

            pressedKeys = keys.ToArray();
            return success;
        }

        /// <summary>
        /// Checks if any of the specific keys are pressed and returns them.
        /// </summary>
        /// <param name="pressedKey"></param>
        /// <param name="keys"></param>
        /// <returns></returns>
        public static bool AreSpecificKeysPressed(out Key[] pressedKey, params Key[] keys)
        {
            return AreSpecificKeysPressed(false, out pressedKey, keys);
        }

        /// <summary>
        /// Checks if any of the specific keys are pressed and returns them.
        /// </summary>
        /// <param name="once">If true, it ignores keys that were down a state before.</param>
        /// <param name="pressedKeys"></param>
        /// <param name="keys"></param>
        /// <returns></returns>
        public static bool AreSpecificKeysPressed(bool once, out Key[] pressedKeys, params Key[] keys)
        {
            List<Key> pressedKey = new List<Key>();
            bool success = false;

            foreach (Key key in keys)
                if (IsDown(key, once))
                {
                    pressedKey.Add(key);
                    success = true;
                }

            pressedKeys = pressedKey.ToArray();
            return success;
        }
    }
}