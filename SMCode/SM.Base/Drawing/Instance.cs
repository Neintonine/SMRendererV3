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
        ///     The texture offset.
        /// </summary>
        public Vector2 TexturePosition = Vector2.Zero;

        /// <summary>
        ///     The texture scale.
        /// </summary>
        public Vector2 TextureScale = Vector2.One;
    }
}