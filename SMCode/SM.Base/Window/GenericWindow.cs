#region usings

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;
using SM.Base.Contexts;
using SM.Base.Drawing;
using SM.Base.Objects.Static;
using SM.Base.PostProcess;
using SM.Base.Scene;
using SM.Base.ShaderExtension;
using SM.Base.Time;
using SM.OGL;
using SM.OGL.Framebuffer;
using SM.Utility;

#endregion

namespace SM.Base
{
    /// <summary>
    ///     The base window.
    /// </summary>
    public abstract class GenericWindow : GameWindow
    {
        internal bool _loading = true;
        internal List<Action> _actionsAfterLoading = new List<Action>();

        /// <summary>
        ///     This tells you the current world scale.
        /// </summary>
        protected Vector2 _worldScale = Vector2.Zero;

        /// <summary>
        ///     This tells you the current aspect ratio of this window.
        /// </summary>
        public float Aspect { get; private set; }

        /// <summary>
        ///     If false, the window will not react on updates and will not render something.
        ///     <para>
        ///         Default: false
        ///     </para>
        /// </summary>
        public bool ReactWhileUnfocused = false;

        /// <inheritdoc />
        protected GenericWindow() : this(1280, 720, "Generic OGL Title", GameWindowFlags.Default)
        {
        }

        /// <summary>
        /// Creates a window...
        /// </summary>
        protected GenericWindow(int width, int height, string title, GameWindowFlags flags, bool vSync = true) : base(width, height,
            GraphicsMode.Default, title, flags, DisplayDevice.Default, GLSettings.ForcedVersion.MajorVersion,
            GLSettings.ForcedVersion.MinorVersion, GraphicsContextFlags.Default)
        {
            VSync = vSync ? VSyncMode.On : VSyncMode.Off;
        }


        /// <inheritdoc />
        protected override void OnLoad(EventArgs e)
        {
            SMRenderer.CurrentWindow = this;

            GLSystem.INIT_SYSTEM();
            GLSettings.ShaderPreProcessing = true;

            var args = Environment.GetCommandLineArgs();
            if (args.Contains("--advDebugging"))
            {
                SMRenderer.AdvancedDebugging = true;
                GLSettings.InfoEveryUniform = true;
            }

            Log.Init();

            Log.Write("#", ConsoleColor.Cyan, "----------------------",
                "--- OpenGL Loading ---",
                "----------------------------------",
                $"--- {"DeviceVersion",14}: {GLSystem.DeviceVersion,-10} ---",
                $"--- {"ForcedVersion",14}: {GLSettings.ForcedVersion,-10} ---",
                $"--- {"ShadingVersion",14}: {GLSystem.ShadingVersion,-10} ---",
                $"--- {"Debugging",14}: {GLSystem.Debugging,-10} ---",
                $"--- {"AdvDebugging",14}: {SMRenderer.AdvancedDebugging,-10} ---",
                "----------------------------------");

            if (SMRenderer.AdvancedDebugging) Log.Write("Extension", ConsoleColor.DarkCyan, GLSystem.Extensions);

            ExtensionManager.InitExtensions();

            base.OnLoad(e);
        }

        /// <inheritdoc />
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            Aspect = (float) Width / Height;
            _worldScale = new Vector2(Width, Height);
            SetWorldScale();
            GL.Viewport(ClientRectangle);

            if (_loading)
            {
                _loading = false;

                OnLoaded();
                
                _actionsAfterLoading.ForEach(a => a());
                _actionsAfterLoading = null;
            }
        }

        /// <summary>
        ///     This is triggered after all the window-loading has been done.
        /// </summary>
        protected virtual void OnLoaded()
        {
        }

        /// <summary>
        ///     Sets the world scale.
        /// </summary>
        protected virtual void SetWorldScale()
        {
        }

        /// <inheritdoc />
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            if (!ReactWhileUnfocused && !Focused) return;

            base.OnUpdateFrame(e);

            Deltatime.UpdateDelta = (float) e.Time;
            var context = new UpdateContext
            {
                KeyboardState = Keyboard.GetState(),
                MouseState = Mouse.GetState()
            };

            if (context.KeyboardState[Key.AltLeft] && context.KeyboardState[Key.F4]) Close(); 

            Update(e, ref context);
        }

        /// <summary>
        ///     Updates the system.
        /// </summary>
        /// <param name="e"></param>
        /// <param name="context"></param>
        protected virtual void Update(FrameEventArgs e, ref UpdateContext context)
        {
            Stopwatch.PerformTicks(context);
        }

        /// <summary>
        ///     Grabs the cursor and make sure it doesn't leave the window.
        /// </summary>
        /// <param name="makeItInvisible">If true, it makes the cursor invisible.</param>
        public void GrabCursor(bool makeItInvisible = true)
        {
            CursorGrabbed = true;
            CursorVisible = !makeItInvisible;
        }

        /// <summary>
        ///     Ungrabs the cursor.
        /// </summary>
        public void UngrabCursor()
        {
            CursorGrabbed = false;
            if (!CursorVisible) CursorVisible = true;
        }

        /// <summary>
        /// Create a bitmap from the framebuffer.
        /// </summary>
        public Bitmap TakeScreenshot(Framebuffer framebuffer, ReadBufferMode readBuffer, int x, int y, int width, int height)
        {
            GL.GetInteger(GetPName.FramebufferBinding, out int prevFBId);
            GL.GetInteger(GetPName.DrawFramebufferBinding, out int prevFBDrawId);
            GL.GetInteger(GetPName.ReadFramebufferBinding, out int prevFBReadId);

            Bitmap b = new Bitmap(width, height);
            System.Drawing.Imaging.BitmapData bits = b.LockBits(new Rectangle(0, 0, width, height),
                System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            GL.BindFramebuffer(FramebufferTarget.ReadFramebuffer, framebuffer);
            GL.ReadBuffer(readBuffer);
            GL.ReadPixels(x, y, width, height, OpenTK.Graphics.OpenGL4.PixelFormat.Bgra, PixelType.UnsignedByte,
                bits.Scan0);

            b.UnlockBits(bits);
            b.RotateFlip(RotateFlipType.RotateNoneFlipY);

            GL.BindFramebuffer(FramebufferTarget.Framebuffer, prevFBId);
            GL.BindFramebuffer(FramebufferTarget.DrawFramebuffer, prevFBDrawId);
            GL.BindFramebuffer(FramebufferTarget.ReadFramebuffer, prevFBReadId);

            return b;
        }
    }

    /// <summary>
    ///     The base window.
    /// </summary>
    /// <typeparam name="TScene">The scene type</typeparam>
    /// <typeparam name="TCamera">The camera type</typeparam>
    public abstract class GenericWindow<TScene, TCamera> : GenericWindow
        where TScene : GenericScene, new()
        where TCamera : GenericCamera, new()
    {
        /// <inheritdoc />
        protected GenericWindow()
        {
            ViewportCamera = new TCamera();
        }

        /// <summary>
        ///     The viewport camera.
        /// </summary>
        public TCamera ViewportCamera { get; }

        /// <summary>
        ///     This forces the render to use the viewport camera.
        /// </summary>
        public bool ForceViewportCamera { get; set; } = false;

        /// <summary>
        ///     The current scene.
        /// </summary>
        public TScene CurrentScene { get; private set; }

        /// <summary>
        ///     Controls how a scene is rendered.
        /// </summary>
        public RenderPipeline<TScene> RenderPipeline { get; private set; }

        /// <inheritdoc />
        protected override void Update(FrameEventArgs e, ref UpdateContext context)
        {
            base.Update(e, ref context);
            CurrentScene?.Update(context);
        }

        /// <inheritdoc />
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            if (!ReactWhileUnfocused && !Focused) return;

            SMRenderer.CurrentFrame++;

            Deltatime.RenderDelta = (float) e.Time;
            var drawContext = new DrawContext
            {
                World = ViewportCamera.World,
                View = ViewportCamera.CalculateViewMatrix(),
                ModelMaster = Matrix4.Identity,
                Instances = new[]
                {
                    new Instance
                        {ModelMatrix = Matrix4.Identity, TexturePosition = Vector2.Zero, TextureScale = Vector2.One}
                },
                ShaderArguments = new Dictionary<string, object>(),
                Mesh = Plate.Object,
                ForceViewport = ForceViewportCamera,
                WorldScale = _worldScale,
                LastPassthough = this
            };

            base.OnRenderFrame(e);

            RenderPipeline.Render(ref drawContext, CurrentScene);

            SwapBuffers();

            GLDebugging.CheckGLErrors();
        }

        /// <inheritdoc />
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            ViewportCamera.RecalculateWorld(_worldScale, Aspect);
            RenderPipeline.Resize();

            PostProcessEffect.Model = Matrix4.CreateScale(_worldScale.X, -_worldScale.Y, 1);
            PostProcessEffect.Mvp = PostProcessEffect.Model *
                                    Matrix4.LookAt(0, 0, 1, 0, 0, 0, 0, 1, 0) *
                                    GenericCamera.OrthographicWorld;
        }

        /// <summary>
        ///     Sets the scene.
        /// </summary>
        /// <param name="scene"></param>
        public virtual void SetScene(TScene scene)
        {
            if (_loading)
            {
                _actionsAfterLoading.Add(() => SetScene(scene));
                return;
            }

            CurrentScene = scene;
            scene.Activate();
            RenderPipeline.SceneChanged(scene);
        }

        /// <summary>
        ///     Defines the render pipeline.
        /// </summary>
        /// <param name="pipeline"></param>
        public void SetRenderPipeline(RenderPipeline<TScene> pipeline)
        {
            if (_loading)
            {
                _actionsAfterLoading.Add(() => SetRenderPipeline(pipeline));
                return;
            }

            RenderPipeline = pipeline;
            pipeline.Activate(this);
        }
    }
}