namespace SM.OGL
{
    /// <summary>
    ///     Settings that are only accountable for SM.OGL
    /// </summary>
    public class GLSettings
    {
        /// <summary>
        /// Send a <see cref="GLCustomActions.AtInfo"/>, for each uniform from a shader.
        /// </summary>
        public static bool InfoEveryUniform = false;

        /// <summary>
        ///     Get/Sets the forced version for OpenGL.
        ///     <para>Needs to be set before init a window.</para>
        /// </summary>
        public static Version ForcedVersion { get; set; } = new Version();

        /// <summary>
        /// Allows to disable/enable preprocessing for shaders.
        /// </summary>
        public static bool ShaderPreProcessing { get; set; } = false;
    }
}