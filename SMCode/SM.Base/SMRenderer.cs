﻿#region usings

using SM.Base.Drawing.Text;
using SM.Base.Objects.Static;
using SM.Base.Shaders;
using SM.Base.Utility;
using SM.Base.Window;
using SM.OGL.Mesh;

#endregion

namespace SM.Base
{
    /// <summary>
    ///     Contains different information about this renderer.
    /// </summary>
    public class SMRenderer
    {
        internal const string PostProcessPath = "SM.Base.PostEffects.Shaders";

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
        public static ulong CurrentFrame { get; internal set; } = 1;

        /// <summary>
        ///     Represents the current active window.
        /// </summary>
        public static IGenericWindow CurrentWindow { get; internal set; }
    }
}