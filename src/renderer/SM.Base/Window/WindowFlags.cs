namespace SM.Base.Window
{
    /// <summary>
    /// Flags how the window is shown.
    /// </summary>
    public enum WindowFlags
    {
        /// <summary>
        /// As default window.
        /// </summary>
        Window = 0,
        /// <summary>
        /// As a borderless window.
        /// <para>
        ///     WARNING! This automaticly fits the window to the screen and is not movable.
        /// </para>
        /// </summary>
        BorderlessWindow = 2,
        /// <summary>
        /// Contents are shown in fullscreen.
        /// </summary>
        ExclusiveFullscreen = 1
    }
}