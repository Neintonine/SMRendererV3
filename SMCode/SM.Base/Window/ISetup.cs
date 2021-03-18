namespace SM.Base.Window
{
    /// <summary>
    /// A interface to implerment a window setup.
    /// </summary>
    public interface ISetup
    {
        /// <summary>
        /// This fires when the setup is applied the window.
        /// </summary>
        void Applied(IGenericWindow window);
        /// <summary>
        /// This fires when the window is currently loading.
        /// </summary>
        void Load(IGenericWindow window);
        /// <summary>
        /// This fires when the window is done loading.
        /// </summary>
        void Loaded(IGenericWindow window);
        /// <summary>
        /// This fires when the window was resized.
        /// </summary>
        void Resize(IGenericWindow window);
    }
}