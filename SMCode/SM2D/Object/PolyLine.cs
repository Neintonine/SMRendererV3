using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using SM.OGL.Mesh;

namespace SM2D.Object
{
    public enum PolyLineType
    {
        NotConnected = 1,
        Connected = 3,
        ConnectedLoop = 2
    }

    public class PolyLine : Polygon, ILineMesh
    {
        public PolyLine(ICollection<Vector2> vertices, PolyLineType lineType = PolyLineType.NotConnected) : base(vertices)
        {
            UVs.Active = false;

            PrimitiveType = (PrimitiveType)lineType;
        }

        public PolyLine(ICollection<PolygonVertex> vertices, PolyLineType lineType = PolyLineType.NotConnected) : base(vertices)
        {
            UVs.Active = false;
            PrimitiveType = (PrimitiveType)lineType;
        }

    }
}