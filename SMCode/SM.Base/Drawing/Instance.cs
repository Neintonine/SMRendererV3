#region usings

using OpenTK;

#endregion

namespace SM.Base.Scene
{
    /// <summary>
    ///     This represens a drawing instance.
    /// </summary>
    public struct Instance
    {
        /// <summary>
        ///     The model matrix.
        /// </summary>
        public Matrix4 ModelMatrix;

        /// <summary>
        ///     The texture offset.
        /// </summary>
        public Vector2 TexturePosition;

        /// <summary>
        ///     The texture scale.
        /// </summary>
        public Vector2 TextureScale;
    }
}