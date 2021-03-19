namespace SM.OGL.Framebuffer
{
    /// <summary>
    /// A interface, so the framebuffer system can react to changes of windows.
    /// </summary>
    public interface IFramebufferWindow
    {
        /// <summary>
        /// The width of the window.
        /// </summary>
        int Width { get; }
        /// <summary>
        /// The height of the window.
        /// </summary>
        int Height { get; }
    }
}