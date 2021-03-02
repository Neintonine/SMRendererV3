#region usings

using System;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL4;

#endregion

namespace SM.OGL.Mesh
{
    /// <summary>
    ///     Contains information for meshes
    /// </summary>
    public abstract class GenericMesh : GLObject
    {
        private bool _boundingBoxUpdated = false;

        public static int LastID { get; internal set; } = -1;

        /// <summary>
        ///     Generates the AttribDataIndex
        /// </summary>
        protected GenericMesh()
        {
            Attributes = new MeshAttributeList()
            {
                {0, "vertex", Vertex},
                {1, "uv", UVs},
                {2, "normal", Normals}
            };
        }

        /// <inheritdoc />
        protected override bool AutoCompile { get; } = true;

        /// <inheritdoc />
        public override ObjectLabelIdentifier TypeIdentifier { get; } = ObjectLabelIdentifier.VertexArray;

        /// <summary>
        ///     The primitive type, that determinants how the mesh is drawn.
        ///     <para>Default: Triangles</para>
        /// </summary>
        public virtual PrimitiveType PrimitiveType { get; protected set; } = PrimitiveType.Triangles;

        /// <summary>
        ///     Contains the vertices for the mesh.
        /// </summary>
        public virtual VBO Vertex { get; protected set; }

        /// <summary>
        ///     Contains the texture coords for the mesh.
        /// </summary>
        public virtual VBO UVs { get; protected set; }

        /// <summary>
        ///     Contains the normals for the mesh.
        /// </summary>
        public virtual VBO Normals { get; protected set; }

        /// <summary>
        ///     Represents the bounding box.
        /// </summary>
        public virtual BoundingBox BoundingBox { get; } = new BoundingBox();

        /// <summary>
        ///     Connects the different buffer objects with ids.
        /// </summary>
        public MeshAttributeList Attributes { get; }

        /// <summary>
        ///     Stores indices for a more performance friendly method to draw objects.
        /// </summary>
        public virtual int[] Indices { get; set; }

        public void UpdateBoundingBox()
        {
            BoundingBox.Update(this);
            _boundingBoxUpdated = true;
        }

        public void Activate()
        {
            GL.BindVertexArray(ID);
        }

        /// <inheritdoc />
        public override void Compile()
        {
            _id = GL.GenVertexArray();
            GL.BindVertexArray(_id);
            
            if (Attributes == null || Attributes.Count == 0) throw new Exception("[Critical] The model requires attributes.");

            if (!_boundingBoxUpdated) 
                UpdateBoundingBox();

            foreach (var kvp in Attributes) 
                kvp.ConnectedVBO?.BindBuffer(kvp.Index);

            GL.BindVertexArray(0);
        }
    }
}