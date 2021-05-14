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

        /// <summary>
        /// The bearing for this char.
        /// </summary>
        public float BearingX;

        /// <summary>
        ///     The width of the character.
        /// </summary>
        public float Width;


        /// <summary>
        /// Matrix for the texture.
        /// </summary>
        public Matrix3 TextureMatrix;
    }
}