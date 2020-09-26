using System.Collections.Generic;
using OpenTK;
using SM.Base.Scene;
using SM.OGL.Mesh;

namespace SM.Base.Contexts
{
    public struct DrawContext
    {
        public bool ForceViewport;
        public bool Instancing;

        public Matrix4 World;
        public Matrix4 View;
        public Instance[] Instances;

        public GenericMesh Mesh;
        public Material Material;

        public Vector2 WorldScale;
    }
}