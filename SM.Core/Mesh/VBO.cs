using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics.OpenGL4;

namespace SM.OGL.Mesh
{
    public class VBO : List<float>
    {
        public BufferUsageHint BufferUsageHint = BufferUsageHint.StaticDraw;
        public VertexAttribPointerType PointerType = VertexAttribPointerType.Float;
        public int PointerSize = 3;
        public bool Normalised = false;
        public int PointerStride = 0;
        public int PointerOffset = 0;

        public void Add(float x, float y) => AddRange(new[] {x,y});
        public void Add(float x, float y, float z) => AddRange(new[] {x,y,z});
        public void Add(float x, float y, float z, float w) => AddRange(new[] {x,y,z,w});

        public void BindBuffer(int attribID)
        {
            float[] data = ToArray();

            int buffer = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, buffer);
            GL.BufferData(BufferTarget.ArrayBuffer, data.Length * Mesh.BufferSizeMultiplier, data, BufferUsageHint);

            GL.VertexAttribPointer(attribID, PointerSize, PointerType, Normalised, PointerStride, PointerOffset);
            GL.EnableVertexAttribArray(attribID);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }
    }
}