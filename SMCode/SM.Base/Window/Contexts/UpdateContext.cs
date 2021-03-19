#region usings

using SM.Base.Scene;

#endregion

namespace SM.Base.Window
{
    /// <summary>
    /// A context that gets send when a window want to update the scene.
    /// </summary>
    public struct UpdateContext
    {
        /// <summary>
        /// The window what triggered the updated.
        /// </summary>
        public IGenericWindow Window;

        /// <summary>
        /// A current update delta time. Equivalent to <see cref="SMRenderer.DefaultDeltatime"/>.
        /// </summary>
        public float Deltatime => SMRenderer.DefaultDeltatime.DeltaTime;

        /// <summary>
        /// The scene that gets updated.
        /// </summary>
        public GenericScene Scene;
    }
}