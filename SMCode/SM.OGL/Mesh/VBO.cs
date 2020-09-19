using System;
using System.Collections.Generic;
using OpenTK;
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