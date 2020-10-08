using SM.Base.Objects.Static;
using SM.Base.Text;
using SM.OGL.Mesh;
using SM.Utility;

namespace SM.Base
{
    /// <summary>
    /// Contains different information about this renderer.
    /// </summary>
    public class SMRenderer
    {
        /// <summary>
        /// The default mesh.
        /// </summary>
        public static GenericMesh DefaultMesh = Plate.Object;

        /// <summary>
        /// The default font.
        /// </summary>
        public static Font DefaultFont;

        /// <summary>
        /// The default deltatime helper.
        /// </summary>
        public static Deltatime DefaultDeltatime = new Deltatime();

        /// <summary>
        /// Current Frame
        /// </summary>
        public static ulong CurrentFrame { get; internal set; } = 0;
    }
}