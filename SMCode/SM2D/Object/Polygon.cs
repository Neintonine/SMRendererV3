using System;
using System.Collections;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using SM.Base.Objects;
using SM.OGL.Mesh;

namespace SM2D.Object
{
    public class Polygon : Mesh
    {
        public override VBO Vertex { get; } = new VBO();
        public override VBO UVs { get; } = new VBO(pointerSize:2);
        public override VBO Color { get; } = new VBO(pointerSize: 4);

        public override PrimitiveType PrimitiveType { get; } = PrimitiveType.TriangleFan;



        public Polygon(ICollection<Vector2> vertices)
        {
            foreach (Vector2 vertex in vertices)
            {
                Color.Add(Color4.White);
                AddVertex(vertex);
            }

            foreach (Vector2 vertex in vertices)
            {
                AddUV(vertex);
            }
        }

        public Polygon(ICollection<PolygonVertex> vertices)
        {
            foreach (PolygonVertex polygonVertex in vertices)
            {
                Color.Add(polygonVertex.Color);
                AddVertex(polygonVertex.Vertex);
            }

            foreach (PolygonVertex vertex in vertices)
            {
                AddUV(vertex.Vertex);
            }
        }

        private void AddVertex(Vector2 vertex)
        {
            BoundingBox.Update(vertex);
            Vertex.Add(vertex, 0);
        }

        private void AddUV(Vector2 vertex)
        {
            Vector2 uv = Vector2.Divide(vertex, BoundingBox.Max.Xy) + BoundingBox.Min.Xy;
            UVs.Add(uv);
        }
    }
}