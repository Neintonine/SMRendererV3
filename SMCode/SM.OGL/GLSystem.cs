using System.Linq;
using OpenTK.Graphics.OpenGL4;

namespace SM.OGL
{
    /// <summary>
    /// Contains data about the current OpenGL system.
    /// </summary>
    public class GLSystem
    {
        private static bool _init = false;

        /// <summary>
        /// Contains the device version of OpenGL.
        /// </summary>
        public static Version DeviceVersion { get; private set; }
        /// <summary>
        /// Get/Sets the forced version for OpenGL.
        /// <para>Needs to be set before init a window.</para>
        /// </summary>
        public static Version ForcedVersion { get; set; } = new Version();

        /// <summary>
        /// Contains the shader version for GLSL.
        /// </summary>
        public static Version ShadingVersion { get; private set; }
        /// <summary>
        /// Contains the extensions for OpenGL.
        /// </summary>
        public static string[] Extensions { get; private set; }

        /// <summary>
        /// Checks if proper Debugging is for this system available.
        /// <para>Determent, if the system has the "KHR_debug"-extension. </para>
        /// </summary>
        public static bool Debugging { get; private set; }

        /// <summary>
        /// Initialize the system data.
        /// <para>Does nothing after the data was already collected.</para> 
        /// </summary>
        public static void INIT_SYSTEM()
        {
            if (_init) return;

            DeviceVersion = Version.CreateGLVersion(GL.GetString(StringName.Version));

            ShadingVersion = Version.CreateGLVersion(GL.GetString(StringName.ShadingLanguageVersion));
            Extensions = GL.GetString(StringName.Extensions).Split(' ');

            Debugging = Extensions.Contains("khr_debug");
            if (Debugging) GLDebugging.EnableDebugging();

            _init = true;
        }
    }
}