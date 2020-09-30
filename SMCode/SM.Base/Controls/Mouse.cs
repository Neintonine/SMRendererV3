using System;
using OpenTK;
using OpenTK.Input;

namespace SM.Base.Controls
{
    /// <summary>
    /// Mouse controller
    /// </summary>
    /// <typeparam name="TWindow">The type of window this controller is connected to.</typeparam>
    public class Mouse<TWindow>
    where TWindow : GenericWindow
    {
        /// <summary>
        /// The window it is connected to.
        /// </summary>
        protected TWindow _window;

        /// <summary> 
        /// The current position of the mouse in the screen.
        /// </summary>
        public Vector2 InScreen { get; private set; }
        /// <summary>
        /// The current position of the mouse in the screen from 0..1.
        /// </summary>
        public Vector2 InScreenNormalized { get; private set; }

        protected internal Mouse(TWindow window)
        {
            _window = window;
        }

        protected void MouseMoveEvent(MouseMoveEventArgs mmea)
        {
            InScreen = new Vector2(mmea.X, mmea.Y);
            InScreenNormalized = new Vector2(mmea.X / (float)_window.Width, mmea.Y / (float)_window.Height);
        }
    }
}