using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using SM.OGL.Mesh;

namespace SM.Base.Objects.Static
{
    public class AxisHelper : Mesh
    {
        public static AxisHelper Object = new AxisHelper();

        private AxisHelper() {}

        public override VBO Vertex { get; } = new VBO()
        {
            {0, 0, 0},
            {.5f, 0, 0},
            {0, 0, 0},
            {0, .5f, 0},
            {0, 0, -.5f},
            {0, 0, .5f},
        };

        public override VBO Color { get; } = new VBO(pointerSize:4)
        {
            {Color4.White},
            {Color4.Red},
            {Color4.White},
            {Color4.Green},
            {Color4.White},
            {Color4.DarkBlue},
        };

        public override PrimitiveType PrimitiveType { get; } = PrimitiveType.Lines;
    }
}