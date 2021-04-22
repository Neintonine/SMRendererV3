#region usings

using System;
using OpenTK;

#endregion

namespace SM.Base.Drawing.Text
{
    /// <summary>
    ///     Contains information for a font character.
    /// </summary>
    [Serializable]
    public struct CharParameter
    {
        /// <summary>
        ///     The advance on the X-axis.
        /// </summary>
        public int Advance;

        public float BearingX;

        /// <summary>
        ///     The width of the character.
        /// </summary>
        public float Width;


        public Matrix3 TextureMatrix;

        public Vector2 Offset;
        public Vector2 Scale;
    }
}