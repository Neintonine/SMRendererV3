using System.Linq;
using OpenTK.Graphics.OpenGL4;

namespace SM.OGL
{
    public class GLSystem
    {
        private static bool _init = false;

        public static Version DeviceVersion { get; private set; }
        public static Version ForcedVersion { get; set; } = new Version();

        public static Version ShadingVersion { get; private set; }
        public static string[] Extensions { get; private set; }

        public static bool Debugging { get; private set; }

        public static void INIT_SYSTEM()
        {
            if (_init) return;

            DeviceVersion = Version.CreateGLVersion(GL.GetString(StringName.Version));

            ShadingVersion = Version.CreateGLVersion(GL.GetString(StringName.ShadingLanguageVersion));
            Extensions = GL.GetString(StringName.Extensions).Split(' ');

            Debugging = Extensions.Contains("KHR_debug");
            if (Debugging) GLDebugging.EnableDebugging();

            _init = true;
        }
    }
}