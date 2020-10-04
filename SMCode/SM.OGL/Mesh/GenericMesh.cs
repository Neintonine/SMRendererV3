using System;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL4;
using Buffer = OpenTK.Graphics.OpenGL4.Buffer;

namespace SM.OGL.Mesh
{
    /// <summary>
    /// Contains information for meshes
    /// </summary>
    public abstract class GenericMesh : GLObject
    {
        /// <inheritdoc />
        protected override bool AutoCompile { get; } = true;

        /// <inheritdoc />
        public override ObjectLabelIdentifier TypeIdentifier { get; } = ObjectLabelIdentifier.VertexArray;

        /// <summary>
        /// The primitive type, that determinants how the mesh is drawn.
        /// <para>Default: Triangles</para> 
        /// </summary>
        public virtual PrimitiveType PrimitiveType { get; } = PrimitiveType.Triangles;

        /// <summary>
        /// Contains the vertices for the mesh.
        /// </summary>
        public virtual VBO Vertex { get; }
        /// <summary>
        /// Contains the texture coords for the mesh.
        /// </summary>
        public virtual VBO UVs { get; }
        /// <summary>
        /// Contains the normals for the mesh.
        /// </summary>
        public virtual VBO Normals { get; }

        /// <summary>
        /// Represents the bounding box.
        /// </summary>
        public virtual BoundingBox BoundingBox { get; } = new BoundingBox();

        /// <summary>
        /// Connects the different buffer objects with ids.
        /// </summary>
        public Dictionary<int, VBO> AttribDataIndex { get; }

        /// <summary>
        /// Stores indices for a more performance friendly method to draw objects. 
        /// </summary>
        public virtual int[] Indices { get; set; }

        /// <summary>
        /// Generates the AttribDataIndex
        /// </summary>
        protected GenericMesh()
        {
            AttribDataIndex = new Dictionary<int, VBO>()
            {
                {0, Vertex},
                {1, UVs},
                {2, Normals},
            };
        }

        /// <inheritdoc />
        public override void Compile()
        {
            _id = GL.GenVertexArray();
            GL.BindVertexArray(_id);
            
            if (AttribDataIndex == null) throw new Exception("[Critical] The model requires a attribute data index.");

            foreach (KeyValuePair<int, VBO> kvp in AttribDataIndex) kvp.Value?.BindBuffer(kvp.Key);

            GL.BindVertexArray(0);
        }
    }
}