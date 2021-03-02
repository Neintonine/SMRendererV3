#region usings

using System;
using System.Collections.Generic;
using System.Windows.Documents;
using OpenTK;
using OpenTK.Input;
using SM.Base.Windows;

#endregion

namespace SM.Base.Controls
{
    /// <summary>
    ///     Mouse controller
    /// </summary>
    /// <typeparam name="TWindow">The type of window this controller is connected to.</typeparam>
    public class Mouse
    {
        internal static MouseState? _mouseState;
        internal static List<MouseButton> _lastButtonsPressed = new List<MouseButton>();

        /// <summary>
        ///     The current position of the mouse in the screen.
        /// </summary>
        public static Vector2 InScreen { get; private set; }

        /// <summary>
        ///     The current position of the mouse in the screen from 0..1.
        /// </summary>
        public static Vector2 InScreenNormalized { get; private set; }

        /// <summary>
        ///     The event to update the values.
        /// </summary>
        /// <param name="mmea">The event args.</param>
        internal static void MouseMoveEvent(MouseMoveEventArgs mmea, IGenericWindow window)
        {
            InScreen = new Vector2(mmea.X, mmea.Y);
            InScreenNormalized = new Vector2(mmea.X / (float)window.Width, mmea.Y / (float)window.Height);
        }

        internal static void SetState()
        {
            if (_mouseState.HasValue)
            {
                _lastButtonsPressed = new List<MouseButton>();

                foreach (object o in Enum.GetValues(typeof(MouseButton)))
                {
                    if (_mouseState.Value[(MouseButton)o]) _lastButtonsPressed.Add((MouseButton)o);
                }
            }

            _mouseState = OpenTK.Input.Mouse.GetState();
            
        }

        public static bool IsDown(MouseButton button, bool once = false) => _mouseState?[button] == true && !(once && _lastButtonsPressed.Contains(button));

        public static bool IsUp(MouseButton button, bool once = false) =>
            _mouseState?[button] == false && !(once && !_lastButtonsPressed.Contains(button));

    }
}