using System;
using System.IO;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using SM.OGL.Shaders;

namespace SM.Base
{
    public class GenericWindow : GameWindow
    {
        private TestShader shader;
        private Matrix4 _viewMatrix;

        public GenericWindow() : base(1280, 720, GraphicsMode.Default, "Testing", GameWindowFlags.Default, DisplayDevice.Default, 0, 0, GraphicsContextFlags.Default, null, true)
        {

        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            shader = new TestShader(new ShaderFileCollection(File.ReadAllText("test/test.vert"), File.ReadAllText("test/test.frag")));
            shader.Compile();
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            shader.Draw(_viewMatrix);

            SwapBuffers();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(ClientRectangle);
            _viewMatrix = Matrix4.LookAt(Vector3.UnitZ, Vector3.Zero, Vector3.UnitY) *
                          Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(90), Width / Height, 0.1f, 100);
        }
    }
}