namespace SM.OGL.Shaders
{
    /// <summary>
    /// Uniform interface
    /// </summary>
    public interface IUniform
    {
        /// <summary>
        /// Location of the uniforms
        /// </summary>
        int Location { get; }
    }
}