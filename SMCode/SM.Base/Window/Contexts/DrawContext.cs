using OpenTK;
using SM.Base.Scene;
using SM.OGL.Mesh;

namespace SM.Base.Contexts
{
    public struct DrawContext
    {
        public bool ForceViewport;

        public Matrix4 World;
        public Matrix4 View;
        public Matrix4 ModelMatrix;

        public Mesh Mesh;

    }
}