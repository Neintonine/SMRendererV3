using System;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL4;

namespace SM.OGL.Mesh
{
    public class Mesh : GLObject
    {
        public static int BufferSizeMultiplier = 3;

        protected override bool AutoCompile { get; } = true;
        public override ObjectLabelIdentifier TypeIdentifier { get; } = ObjectLabelIdentifier.VertexArray;

        public virtual PrimitiveType PrimitiveType { get; } = PrimitiveType.Triangles;

        public virtual VBO Vertex { get; }
        public virtual VBO UVs { get; }
        public virtual VBO Normals { get; }

        public virtual Dictionary<int, VBO> AttribDataIndex { get; }

        public Mesh()
        {
            AttribDataIndex = new Dictionary<int, VBO>()
            {
                {0, Vertex},
                {1, UVs},
                {2, Normals},
            };
        }

        protected override void Compile()
        {
            _id = GL.GenVertexArray();
            GL.BindVertexArray(_id);
            
            if (AttribDataIndex == null) throw new Exception("[Critical] The model requires a attribute data index.");

            foreach (KeyValuePair<int, VBO> kvp in AttribDataIndex) kvp.Value?.BindBuffer(kvp.Key);

            GL.BindVertexArray(0);
        }
    }
}