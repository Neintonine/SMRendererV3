using OpenTK;
using OpenTK.Graphics.OpenGL4;
using SM.Base.StaticObjects;
using SM.OGL.Shaders;

namespace SM.Base
{
    public class TestShader : GenericShader
    {
        public TestShader(ShaderFileCollection shaderFileFiles) : base(shaderFileFiles)
        {
        }

        public void Draw(Matrix4 mvp)
        {
            GL.UseProgram(this);

            Uniforms["MVP"].SetMatrix4(mvp);

            DrawObject(Plate.Object, true);

            GL.UseProgram(0);
        }
    }
}