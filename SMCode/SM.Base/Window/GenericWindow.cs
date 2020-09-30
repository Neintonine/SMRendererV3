using System;
using System.IO;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;
using SM.Base.Contexts;
using SM.Base.Objects.Static;
using SM.Base.Scene;
using SM.OGL;
using SM.OGL.Shaders;
using SM.Utility;

namespace SM.Base
{
    public abstract class GenericWindow : GameWindow
    {
        private bool _loading = false;

        public bool ForceViewportCamera { get; set; } = false;

        protected Vector2 _worldScale = Vector2.Zero;
        public float Aspect { get; private set; } = 0f;

        protected GenericWindow() : base(1280, 720, GraphicsMode.Default, "Generic OGL Title", GameWindowFlags.Default,
            DisplayDevice.Default, 0, 0, GraphicsContextFlags.Default, null, true)
        { }

        protected override void OnLoad(EventArgs e)
        {
            GLSystem.INIT_SYSTEM();

            Console.Write("----------------------\n" +
                          "--- OpenGL Loading ---\n" +
                          "----------------------------------\n" +
                          $"--- {"DeviceVersion",14}: {GLSystem.DeviceVersion,-10} ---\n" +
                          $"--- {"ForcedVersion",14}: {GLSystem.ForcedVersion,-10} ---\n" +
                          $"--- {"ShadingVersion",14}: {GLSystem.ShadingVersion,-10} ---\n" +
                          $"--- {"Debugging",14}: {GLSystem.Debugging,-10} ---\n" +
                          $"----------------------------------\n");

            base.OnLoad(e);
            _loading = true;
        }
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            Aspect = (float)Width / Height;
            _worldScale = new Vector2(Width, Height);
            SetWorldScale();
            GL.Viewport(ClientRectangle);

            if (_loading)
            {
                _loading = false;
                OnLoaded();
            }
        }

        protected virtual void OnLoaded()
        {

        }

        protected virtual void SetWorldScale() { }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
            Deltatime.UpdateDelta = (float)e.Time;
        }

        public void GrabCursor(bool makeItInvisible = true)
        {
            CursorGrabbed = true;
            CursorVisible = !makeItInvisible;
        }

        public void UngrabCursor()
        {
            CursorGrabbed = false;
            if (!CursorVisible) CursorVisible = true;
        }
    }

    public abstract class GenericWindow<TScene, TItem, TCamera> : GenericWindow
        where TScene : GenericScene<TCamera, TItem>, new()
        where TItem : IShowItem
        where TCamera : GenericCamera, new()
    {
        public TCamera ViewportCamera { get; }

        public TScene CurrentScene { get; private set; }

        protected GenericWindow()
        {
            ViewportCamera = new TCamera();
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            DrawContext drawContext = new DrawContext()
            {
                World = ViewportCamera.World,
                View = ViewportCamera.CalculateViewMatrix(),
                Instances = new[] { new Instance {ModelMatrix = Matrix4.Identity, TexturePosition = Vector2.Zero, TextureScale = Vector2.One } },
                Mesh = Plate.Object,
                ForceViewport = ForceViewportCamera,
                WorldScale = _worldScale
            };

            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            CurrentScene.Draw(drawContext);

            SwapBuffers();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            ViewportCamera.RecalculateWorld(_worldScale, Aspect);
        }

        public virtual void SetScene(TScene scene)
        {
            CurrentScene = scene;
            scene.Activate();
        }
    }
}