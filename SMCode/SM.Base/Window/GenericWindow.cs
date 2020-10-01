using System;
using System.IO;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;
using SM.Base.Contexts;
using SM.Base.Objects.Static;
using SM.Base.Scene;
using SM.Base.Time;
using SM.OGL;
using SM.OGL.Shaders;
using SM.Utility;

namespace SM.Base
{
    /// <summary>
    /// The base window.
    /// </summary>
    public abstract class GenericWindow : GameWindow
    {
        private bool _loading = false;


        /// <summary>
        /// This tells you the current world scale.
        /// </summary>
        protected Vector2 _worldScale = Vector2.Zero;
        /// <summary>
        /// This tells you the current aspect ratio of this window.
        /// </summary>
        public float Aspect { get; private set; } = 0f;

        /// <inheritdoc />
        protected GenericWindow() : base(1280, 720, GraphicsMode.Default, "Generic OGL Title", GameWindowFlags.Default,
            DisplayDevice.Default, 0, 0, GraphicsContextFlags.Default, null, true)
        { }

        /// <inheritdoc />
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

        /// <inheritdoc />
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

        /// <summary>
        /// This is triggered after all the window-loading has been done.
        /// </summary>
        protected virtual void OnLoaded()
        {

        }

        /// <summary>
        /// Sets the world scale.
        /// </summary>
        protected virtual void SetWorldScale() { }

        /// <inheritdoc />
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
            
            Deltatime.UpdateDelta = (float)e.Time;
            UpdateContext context = new UpdateContext()
            {
                KeyboardState = Keyboard.GetState(),
                MouseState = Mouse.GetState()
            };

            Stopwatch.PerformTicks(context);
        }

        /// <summary>
        /// Grabs the cursor and make sure it doesn't leave the window.
        /// </summary>
        /// <param name="makeItInvisible">If true, it makes the cursor invisible.</param>
        public void GrabCursor(bool makeItInvisible = true)
        {
            CursorGrabbed = true;
            CursorVisible = !makeItInvisible;
        }

        /// <summary>
        /// Ungrabs the cursor.
        /// </summary>
        public void UngrabCursor()
        {
            CursorGrabbed = false;
            if (!CursorVisible) CursorVisible = true;
        }
    }

    /// <summary>
    /// The base window.
    /// </summary>
    /// <typeparam name="TScene">The scene type</typeparam>
    /// <typeparam name="TItem">The base item type</typeparam>
    /// <typeparam name="TCamera">The camera type</typeparam>
    public abstract class GenericWindow<TScene, TItem, TCamera> : GenericWindow
        where TScene : GenericScene<TCamera, TItem>, new()
        where TItem : IShowItem
        where TCamera : GenericCamera, new()
    {
        /// <summary>
        /// The viewport camera.
        /// </summary>
        public TCamera ViewportCamera { get; }
        /// <summary>
        /// This forces the render to use the viewport camera.
        /// </summary>
        public bool ForceViewportCamera { get; set; } = false;

        /// <summary>
        /// The current scene.
        /// </summary>
        public TScene CurrentScene { get; private set; }

        /// <inheritdoc />
        protected GenericWindow()
        {
            ViewportCamera = new TCamera();
        }

        /// <inheritdoc />
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

        /// <inheritdoc />
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            ViewportCamera.RecalculateWorld(_worldScale, Aspect);
        }

        /// <summary>
        /// Sets the scene.
        /// </summary>
        /// <param name="scene"></param>
        public virtual void SetScene(TScene scene)
        {
            CurrentScene = scene;
            scene.Activate();
        }
    }
}