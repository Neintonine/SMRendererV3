namespace SM.OGL.Framebuffer
{
    /// <summary>
    /// A interface, so the framebuffer system can react to changes of windows.
    /// </summary>
    public interface IFramebufferWindow
    {
        int Width { get; }
        int Height { get; }
    }
}