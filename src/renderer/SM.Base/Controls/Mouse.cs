#region usings

using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Input;
using SM.Base.Window;

#endregion

namespace SM.Base.Controls
{
    /// <summary>
    ///     Mouse controller
    /// </summary>
    public class Mouse
    {
        private static MouseState? _mouseState;
        private static List<MouseButton> _lastButtonsPressed = new List<MouseButton>();

        private static Vector2 _inScreen;

        /// <summary>
        ///     Gets or sets the current position of the mouse in the screen.
        /// </summary>
        public static Vector2 InScreen
        {
            get => _inScreen;
            set
            {
                _inScreen = value;
                UpdateNormalized(SMRenderer.CurrentWindow);
            }
        }

        /// <summary>
        ///     The current position of the mouse in the screen from 0..1.
        /// </summary>
        public static Vector2 InScreenNormalized { get; private set; }

        /// <summary>
        /// This returns true, if the left mouse button was pressed.
        /// <para>Its pretty much: IsDown(MouseButton.Left, true)</para>
        /// </summary>
        public static bool LeftClick => IsDown(MouseButton.Left, true);

        /// <summary>
        /// This returns true, if the right mouse button was pressed.
        /// <para>Its pretty much: IsDown(MouseButton.Right, true)</para>
        /// </summary>
        public static bool RightClick => IsDown(MouseButton.Right, true);

        /// <summary>
        /// If true, it disables the tracking of the mouse, allowing you to change the <see cref="InScreen"/> value, without the system replacing it again.
        /// </summary>
        public static bool StopTracking { get; set; }

        private static void UpdateNormalized(IGenericWindow window)
        {
            InScreenNormalized = new Vector2(_inScreen.X / (float)window.Width, _inScreen.Y / (float)window.Height);
        }

        /// <summary>
        ///     The event to update the values.
        /// </summary>
        /// <param name="mmea">The event args.</param>
        /// <param name="window">The window where the mouse is checked</param>
        internal static void MouseMoveEvent(MouseMoveEventArgs mmea, IGenericWindow window)
        {
            if (StopTracking) return;

            InScreen = new Vector2(mmea.X, mmea.Y);
            UpdateNormalized(window);
        }

        internal static void SetState()
        {
            if (_mouseState.HasValue)
            {
                _lastButtonsPressed = new List<MouseButton>();

                foreach (object o in Enum.GetValues(typeof(MouseButton)))
                    if (_mouseState.Value[(MouseButton) o])
                        _lastButtonsPressed.Add((MouseButton) o);
            }

            _mouseState = OpenTK.Input.Mouse.GetState();
        }

        /// <summary>
        /// Checks if the mouse is pressed.
        /// </summary>
        /// <param name="button"></param>
        /// <param name="once">If true, it will not get called, when it was pressed in the last update.</param>
        public static bool IsDown(MouseButton button, bool once = false)
        {
            return _mouseState?[button] == true && !(once && _lastButtonsPressed.Contains(button));
        }

        /// <summary>
        /// Checks if the mouse is not pressed.
        /// </summary>
        /// <param name="button"></param>
        /// <param name="once">If true, it will not get called, when it was not pressed in the last update.</param>
        public static bool IsUp(MouseButton button, bool once = false)
        {
            return _mouseState?[button] == false && !(once && !_lastButtonsPressed.Contains(button));
        }
    }
}