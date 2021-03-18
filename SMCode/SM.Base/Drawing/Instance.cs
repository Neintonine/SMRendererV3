#region usings

using OpenTK;

#endregion

namespace SM.Base.Drawing
{
    /// <summary>
    ///     This represens a drawing instance.
    /// </summary>
    public class Instance
    {
        /// <summary>
        ///     The model matrix.
        /// </summary>
        public Matrix4 ModelMatrix = Matrix4.Identity;

        /// <summary>
        /// The Texture matrix
        /// </summary>
        public Matrix3 TextureMatrix = Matrix3.Identity;
    }
}