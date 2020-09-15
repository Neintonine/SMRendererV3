using System;
using System.IO;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using SM.OGL.Shaders;

namespace SM.Base
{
    public class GenericWindow : GameWindow
    {
        private TestShader shader;
        private Matrix4 _viewMatrix;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            shader = new TestShader(new ShaderFileCollection(File.ReadAllText("test/vert.glsl"), File.ReadAllText("test/frag.glsl")));
            shader.Compile();
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            shader.Draw();

        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(ClientRectangle);
            _viewMatrix = Matrix4.CreatePerspectiveFieldOfView(90, Width / Height, 0.1f, 100) *
                          Matrix4.LookAt(new Vector3(2f, 1f, -5f), Vector3.Zero, Vector3.UnitY);
        }
    }
}