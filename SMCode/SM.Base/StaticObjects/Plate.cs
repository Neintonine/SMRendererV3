using OpenTK.Graphics.OpenGL4;
using SM.OGL.Mesh;

namespace SM.Base.StaticObjects
{
    public class Plate : Mesh
    {
        public static Plate Object = new Plate();

        public override VBO Vertex { get; } = new VBO()
        {
            {-.5f, -.5f, 0},
            {-.5f, .5f, 0},
            {.5f, .5f, 0},
            {.5f, -.5f, 0},
            {0,0,0},
            {0,0,0},
        };

        public override PrimitiveType PrimitiveType { get; } = PrimitiveType.Quads;

        private Plate() {}
    }
}