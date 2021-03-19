using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using SM.OGL.Mesh;

namespace SM2D.Object
{
    /// <summary>
    /// Allows different type of lines.
    /// </summary>
    public enum PolyLineType
    {
        /// <summary>
        /// Those lines are not connected to each other.
        /// <para>Every two points starts a new line.</para>
        /// </summary>
        NotConnected = 1,
        /// <summary>
        /// Those lines are connected with each other, but don't connect the start and the end.
        /// </summary>
        Connected = 3,
        /// <summary>
        /// Those lines are connected and they connect start and end.
        /// </summary>
        ConnectedLoop = 2
    }

    /// <summary>
    /// Creates new poly line.
    /// </summary>
    public class PolyLine : Polygon, ILineMesh
    {
        /// <summary>
        /// Creates a new polyline by using <see cref="Vector2"/>.
        /// </summary>
        /// <param name="vertices"></param>
        /// <param name="lineType"></param>
        public PolyLine(ICollection<Vector2> vertices, PolyLineType lineType = PolyLineType.NotConnected) : base(vertices)
        {
            UVs.Active = false;

            PrimitiveType = (PrimitiveType)lineType;
        }

        /// <summary>
        /// Creates a new polyline by using <see cref="PolygonVertex"/>.
        /// </summary>
        /// <param name="vertices"></param>
        /// <param name="lineType"></param>
        public PolyLine(ICollection<PolygonVertex> vertices, PolyLineType lineType = PolyLineType.NotConnected) : base(vertices)
        {
            UVs.Active = false;
            PrimitiveType = (PrimitiveType)lineType;
        }

    }
}