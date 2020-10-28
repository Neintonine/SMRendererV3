#region usings

using SM.Base.Drawing;
using SM.Base.Drawing.Text;
using SM.Base.Objects.Static;
using SM.Base.Scene;
using SM.OGL.Mesh;
using SM.OGL.Shaders;
using SM.Utility;

#endregion

namespace SM.Base
{
    /// <summary>
    ///     Contains different information about this renderer.
    /// </summary>
    public class SMRenderer
    {
        /// <summary>
        ///     Defines, how many instances the 'SM_base_vertex_basic'-extension can handle.
        /// </summary>
        public const int MaxInstances = 32;

        /// <summary>
        ///     The default mesh.
        /// </summary>
        public static GenericMesh DefaultMesh = Plate.Object;

        /// <summary>
        ///     The default font.
        /// </summary>
        public static Font DefaultFont;

        /// <summary>
        ///     The default deltatime helper.
        /// </summary>
        public static Deltatime DefaultDeltatime = new Deltatime();

        /// <summary>
        ///     The default material shader.
        /// </summary>
        public static MaterialShader DefaultMaterialShader;

        /// <summary>
        ///     Shows more information onto the log system.
        /// </summary>
        public static bool AdvancedDebugging = false;

        /// <summary>
        ///     Current Frame
        /// </summary>
        public static ulong CurrentFrame { get; internal set; } = 0;

        /// <summary>
        ///     Represents the current active window.
        /// </summary>
        public static GenericWindow CurrentWindow { get; internal set; }
    }
}