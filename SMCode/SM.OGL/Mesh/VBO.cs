using System;
using System.Collections.Generic;
using System.Linq;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;

namespace SM.OGL.Mesh
{
    public class VBO : List<float>
    {
        public BufferUsageHint BufferUsageHint;
        public VertexAttribPointerType PointerType;
        public int PointerSize;
        public bool Normalised;
        public int PointerStride;
        public int PointerOffset;

        public VBO(BufferUsageHint bufferUsageHint = BufferUsageHint.StaticDraw,
            VertexAttribPointerType pointerType = VertexAttribPointerType.Float, int pointerSize = 3,
            int pointerStride = 0, int pointerOffset = 0, bool normalised = false)
        {
            BufferUsageHint = bufferUsageHint;
            PointerType = pointerType;
            PointerSize = pointerSize;
            PointerStride = pointerStride;
            PointerOffset = pointerOffset;
            Normalised = normalised;
        }

        public void Add(float x, float y) => AddRange(new[] {x,y});
        public void Add(float x, float y, float z) => AddRange(new[] {x,y,z});
        public void Add(float x, float y, float z, float w) => AddRange(new[] {x,y,z,w});
        public void Add(Vector2 vector) => Add(vector.X, vector.Y);
        public void Add(Vector2 vector, float z) => Add(vector.X, vector.Y, z);
        public void Add(Vector2 vector, float z, float w) => Add(vector.X, vector.Y, z, w);
        public void Add(Vector3 vector) => Add(vector.X, vector.Y, vector.Z);
        public void Add(Vector4 vector) => Add(vector.X, vector.Y, vector.Z, vector.W);
        public void Add(Color4 color) => Add(color.R, color.G, color.B, color.A);

        public void BindBuffer(int attribID)
        {
            float[] data = ToArray();

            int buffer = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, buffer);
            GL.BufferData(BufferTarget.ArrayBuffer, data.Length * sizeof(float), data, BufferUsageHint);

            GL.VertexAttribPointer(attribID, PointerSize, PointerType, Normalised, PointerStride, PointerOffset);
            GL.EnableVertexAttribArray(attribID);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }
    }
}