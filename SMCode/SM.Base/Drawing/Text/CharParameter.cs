#region usings

using System;

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
        ///     The position on the X-axis.
        /// </summary>
        public int X;

        /// <summary>
        ///     The width of the character.
        /// </summary>
        public float Width;

        /// <summary>
        ///     The normalized position inside the texture.
        /// </summary>
        public float NormalizedX;

        /// <summary>
        ///     The normalized width inside the texture.
        /// </summary>
        public float NormalizedWidth;
    }
}