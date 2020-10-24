#region usings

using OpenTK;
using OpenTK.Graphics;

#endregion

namespace SM2D.Object
{
    public struct PolygonVertex
    {
        public Vector2 Vertex;
        public Color4 Color;

        public PolygonVertex(Vector2 vertex = default, Color4 color = default)
        {
            Vertex = vertex;
            Color = color;
        }
    }
}