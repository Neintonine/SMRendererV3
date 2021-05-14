#region usings

using OpenTK;
using OpenTK.Graphics;

#endregion

namespace SM2D.Object
{
    /// <summary>
    /// Allows storing more information inside a vertex.
    /// </summary>
    public struct PolygonVertex
    {
        /// <summary>
        /// The position in the polygon.
        /// </summary>
        public Vector2 Position;
        /// <summary>
        /// The color of the vertex.
        /// </summary>
        public Color4 Color;

        /// <summary>
        /// Creates a polygon vertex.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="color"></param>
        public PolygonVertex(Vector2 position = default, Color4 color = default)
        {
            Position = position;
            Color = color;
        }

        /// <summary>
        /// Automaticly translates Vector2s to PolygonVertex
        /// </summary>
        /// <param name="vec"></param>
        /// <returns></returns>
        public static implicit operator PolygonVertex(Vector2 vec) => new PolygonVertex(vec, Color4.White);
    }
}