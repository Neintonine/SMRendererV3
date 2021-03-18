﻿#region usings

using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using SM.Base.Objects;
using SM.OGL.Mesh;

#endregion

namespace SM2D.Object
{
    public class Polygon : Mesh
    {
        public Polygon(ICollection<Vector2> vertices) : base(PrimitiveType.TriangleFan)
        {
            Color.Active = false;
            
            foreach (var vertex in vertices)
            {
                Vertex.Add(vertex, 0);
            }

            UpdateBoundingBox();

            if (UVs.Active) foreach (var vertex in vertices) AddUV(vertex);
        }

        public Polygon(ICollection<PolygonVertex> vertices) : base(PrimitiveType.TriangleFan)
        {
            foreach (var polygonVertex in vertices)
            {
                Color.Add(polygonVertex.Color);
                Vertex.Add(polygonVertex.Vertex, 0);
            }

            UpdateBoundingBox();

            if (UVs.Active) foreach (var vertex in vertices) AddUV(vertex.Vertex);
        }

        public override VBO Vertex { get; protected set; } = new VBO();
        public override VBO UVs { get; protected set; } = new VBO(pointerSize: 2);
        public override VBO Color { get; protected set; } = new VBO(pointerSize: 4);

        public override PrimitiveType PrimitiveType { get; protected set; } = PrimitiveType.TriangleFan;

        private void AddUV(Vector2 vertex)
        {
            var uv = Vector2.Divide(vertex - BoundingBox.Min.Xy, BoundingBox.Max.Xy - BoundingBox.Min.Xy);
            uv.Y *= -1;
            UVs.Add(uv);
        }

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