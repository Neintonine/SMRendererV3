using OpenTK.Input;
using SM.Utility;

namespace SM.Base.Contexts
{
    /// <summary>
    /// The update context.
    /// </summary>
    public struct UpdateContext
    {
        /// <summary>
        /// The delta time.
        /// </summary>
        public float Deltatime => Defaults.DefaultDeltatime.DeltaTime;

        /// <summary>
        /// The current keyboard state.
        /// </summary>
        public KeyboardState KeyboardState;

        /// <summary>
        /// The current mouse state.
        /// </summary>
        public MouseState MouseState;
    }
}