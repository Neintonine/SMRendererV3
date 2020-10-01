using System.Collections.Generic;
using OpenTK;
using SM.Base.Scene;
using SM.OGL.Mesh;

namespace SM.Base.Contexts
{
    /// <summary>
    /// Contains important information for drawing.
    /// </summary>
    public struct DrawContext
    {
        /// <summary>
        /// This says if it was forced to use the viewport camera.
        /// </summary>
        public bool ForceViewport;

        /// <summary>
        /// The current world matrix.
        /// </summary>
        public Matrix4 World;
        /// <summary>
        /// The current view matrix.
        /// </summary>
        public Matrix4 View;
        /// <summary>
        /// The drawing instances.
        /// <para>If there is only one, it's index 0</para>
        /// </summary>
        public Instance[] Instances;

        /// <summary>
        /// The mesh.
        /// </summary>
        public GenericMesh Mesh;
        /// <summary>
        /// The material.
        /// </summary>
        public Material Material;

        /// <summary>
        /// The current world scale.
        /// </summary>
        public Vector2 WorldScale;
    }
}