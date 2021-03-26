#region usings

using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using SM.Base.Objects;
using SM.OGL.Mesh;

#endregion

namespace SM2D.Object
{
    /// <summary>
    /// Creates a polygon.
    /// </summary>
    public class Polygon : Mesh
    {
        /// <inheritdoc />
        public override VBO<Vector3> Vertex { get; protected set; } = new VBO<Vector3>();

        /// <inheritdoc />
        public override VBO<Vector2> UVs { get; protected set; } = new VBO<Vector2>();

        /// <inheritdoc />
        public override VBO<Color4> Color { get; protected set; } = new VBO<Color4>();

        /// <inheritdoc />
        public override PrimitiveType PrimitiveType { get; protected set; } = PrimitiveType.TriangleFan;

        /// <summary>
        /// Creates a polygon with <see cref="Vector2"/>s.
        /// </summary>
        /// <param name="vertices"></param>
        public Polygon(ICollection<Vector2> vertices) : base(PrimitiveType.TriangleFan)
        {
            Color.Active = false;
            
            foreach (var vertex in vertices)
            {
                Vertex.Add(new Vector3(vertex));
            }

            UpdateBoundingBox();

            if (UVs.Active) foreach (var vertex in vertices) AddUV(vertex);
        }

        /// <summary>
        /// Creates a polygon with <see cref="PolygonVertex"/>, what allows colors hard coded.
        /// </summary>
        /// <param name="vertices"></param>
        public Polygon(ICollection<PolygonVertex> vertices) : base(PrimitiveType.TriangleFan)
        {
            foreach (var polygonVertex in vertices)
            {
                Color.Add(polygonVertex.Color);
                Vertex.Add(new Vector3(polygonVertex.Position.X, polygonVertex.Position.Y, 0));
            }

            UpdateBoundingBox();

            if (UVs.Active) foreach (var vertex in vertices) AddUV(vertex.Position);
        }

        private void AddUV(Vector2 vertex)
        {
            var uv = Vector2.Divide(vertex - BoundingBox.Min.Xy, BoundingBox.Max.Xy - BoundingBox.Min.Xy);
            uv.Y *= -1;
            UVs.Add(uv);
        }

        /// <summary>
        /// Creates a circle.
        /// </summary>
        /// <param name="secments"></param>
        /// <returns></returns>
        public static Polygon GenerateCircle(int secments = 32)
        {
            var vertices = new List<Vector2> {Vector2.Zero};

            var step = 360f / secments;
            for (var i = 0; i < secments + 1; i++)
            {
                var vertex = new Vector2(0.5f * (float) Math.Cos(step * i * Math.PI / 180f),
                    0.5f * (float) Math.Sin(step * i * Math.PI / 180f));
                vertices.Add(vertex);
            }

            return new Polygon(vertices);
        }
    }
}