namespace SM.OGL
{
    public class GLSettings
    {
        public static bool InfoEveryUniform = false;

        /// <summary>
        ///     Get/Sets the forced version for OpenGL.
        ///     <para>Needs to be set before init a window.</para>
        /// </summary>
        public static Version ForcedVersion { get; set; } = new Version();

        public static bool ShaderPreProcessing { get; set; } = false;
    }
}