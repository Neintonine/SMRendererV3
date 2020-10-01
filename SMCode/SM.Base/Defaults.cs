using SM.Base.Objects;
using SM.Base.Objects.Static;
using SM.Base.Scene;
using SM.OGL.Mesh;
using SM.Utility;

namespace SM.Base
{
    /// <summary>
    /// The default options.
    /// </summary>
    public class Defaults
    {
        /// <summary>
        /// The default shader.
        /// </summary>
        public static IShader DefaultShader;
        /// <summary>
        /// The default mesh.
        /// </summary>
        public static GenericMesh DefaultMesh = Plate.Object;

        /// <summary>
        /// The default deltatime helper.
        /// </summary>
        public static Deltatime DefaultDeltatime = new Deltatime();
    }
}