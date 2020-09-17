using System;
using System.IO;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using SM.Base.Contexts;
using SM.Base.Scene;
using SM.Base.StaticObjects;
using SM.OGL.Shaders;

namespace SM.Base
{
    public class GenericWindow<TScene, TCamera> : GameWindow
        where TScene : GenericScene<TCamera>, new()
        where TCamera : GenericCamera, new()
    {
        private TCamera _viewportCamera;

        public TScene CurrentScene { get; private set; }
        public bool ForceViewportCamera { get; set; } = false;

        public GenericWindow() : base(1280, 720, GraphicsMode.Default, "Testing", GameWindowFlags.Default, DisplayDevice.Default, 0, 0, GraphicsContextFlags.Default, null, true)
        {
            _viewportCamera = new TCamera();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            DrawContext drawContext = new DrawContext()
            {
                World = _viewportCamera.World,
                View = _viewportCamera.CalculateViewMatrix(),
                ModelMatrix = Matrix4.Identity,
                Mesh = Plate.Object,
                ForceViewport = ForceViewportCamera
            };

            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            CurrentScene.Draw(drawContext);

            SwapBuffers();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(ClientRectangle);
            _viewportCamera.RecalculateWorld(Width, Height);
        }

        public virtual void SetScene(TScene scene)
        {
            CurrentScene = scene;
        }
    }
}