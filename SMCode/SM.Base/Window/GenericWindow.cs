using System;
using System.IO;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using SM.Base.Contexts;
using SM.Base.Objects.Static;
using SM.Base.Scene;
using SM.OGL;
using SM.OGL.Shaders;

namespace SM.Base
{
    public class GenericWindow<TScene, TItem, TCamera> : GameWindow
        where TScene : GenericScene<TCamera, TItem>, new()
        where TItem : IShowItem
        where TCamera : GenericCamera, new()
    {
        private TCamera _viewportCamera;

        public TScene CurrentScene { get; private set; }
        public bool ForceViewportCamera { get; set; } = false;

        public Vector2? Scaling { get; set; }
        public Vector2 WorldScale { get; private set; }= Vector2.Zero;
        public float Aspect { get; private set; } = 0f;

        public GenericWindow() : base(1280, 720, GraphicsMode.Default, "Testing", GameWindowFlags.Default, DisplayDevice.Default, 0, 0, GraphicsContextFlags.Default, null, true)
        {
            _viewportCamera = new TCamera();
        }

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
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            DrawContext drawContext = new DrawContext()
            {
                World = _viewportCamera.World,
                View = _viewportCamera.CalculateViewMatrix(),
                Instances = new[] { new Instance {ModelMatrix = Matrix4.Identity, TexturePosition = Vector2.Zero, TextureScale = Vector2.One } },
                Mesh = Plate.Object,
                ForceViewport = ForceViewportCamera,
                WorldScale = WorldScale
            };

            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            CurrentScene.Draw(drawContext);

            SwapBuffers();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            Aspect = (float)Width / Height;
            WorldScale = new Vector2(Width, Height);
            if (Scaling.HasValue)
            {
                if (Scaling.Value.X > 0 && Scaling.Value.Y > 0) WorldScale = Scaling.Value;
                else if(Scaling.Value.X > 0) WorldScale = new Vector2(Scaling.Value.X, Scaling.Value.X / Aspect);
                else if(Scaling.Value.Y > 0) WorldScale = new Vector2(Aspect * Scaling.Value.Y, Scaling.Value.Y);
            }

            GL.Viewport(ClientRectangle);
            _viewportCamera.RecalculateWorld(WorldScale, Aspect);
        }

        public virtual void SetScene(TScene scene)
        {
            CurrentScene = scene;
        }
    }

    public enum WindowScaling
    {
        None,
        Width,
        Height,
        FixedSize
    }
}