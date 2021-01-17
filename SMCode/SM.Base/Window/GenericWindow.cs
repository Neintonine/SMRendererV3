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
    public abstract class GenericWindow : GameWindow, IGenericWindow
    {
        protected GenericCamera _viewportCamera;

        internal bool _loading = true;
        internal List<Action> _actionsAfterLoading = new List<Action>();

        public bool Loading => _loading;

        /// <summary>
        ///     This tells you the current world scale.
        /// </summary>
        public Vector2 WorldScale { get; set; } = Vector2.Zero;

        /// <summary>
        ///     This tells you the current aspect ratio of this window.
        /// </summary>
        public float Aspect { get; set; }

        /// <summary>
        ///     If false, the window will not react on updates and will not render something.
        ///     <para>
        ///         Default: false
        ///     </para>
        /// </summary>
        public bool ReactWhileUnfocused = false;

        public GenericCamera ViewportCamera => _viewportCamera;
        public bool ForceViewportCamera { get; set; }

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
            GenericWindowCode.Load(this);

            base.OnLoad(e);
        }

        /// <inheritdoc />
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            GenericWindowCode.Resize(this);

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
        public virtual void SetWorldScale()
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
    public abstract class GenericWindow<TScene, TCamera> : GenericWindow, IGenericWindow<TScene, TCamera>
        where TScene : GenericScene, new()
        where TCamera : GenericCamera, new()
    {
        private RenderPipeline<TScene> _renderPipeline;
        private TScene _scene;

        /// <inheritdoc />
        protected GenericWindow()
        {
            _viewportCamera = new TCamera();
        }

        /// <summary>
        ///     The viewport camera.
        /// </summary>
        public TCamera ViewportCamera { 
            get => (TCamera)_viewportCamera;
            set => _viewportCamera = value;
        }

        /// <summary>
        ///     This forces the render to use the viewport camera.
        /// </summary>
        public bool ForceViewportCamera { get; set; } = false;

        /// <summary>
        ///     The current scene.
        /// </summary>
        public TScene CurrentScene => _scene;

        /// <summary>
        ///     Controls how a scene is rendered.
        /// </summary>
        public RenderPipeline<TScene> RenderPipeline => _renderPipeline;

        /// <inheritdoc />
        protected override void Update(FrameEventArgs e, ref UpdateContext context)
        {
            base.Update(e, ref context);
            context.CurrentScene = CurrentScene;
            CurrentScene?.Update(context);
        }

        /// <inheritdoc />
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            if (!ReactWhileUnfocused && !Focused) return;

            base.OnRenderFrame(e);

            GenericWindowCode.Render(this, (float)e.Time);

            SwapBuffers();

            GLDebugging.CheckGLErrors();
        }

        /// <inheritdoc />
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            GenericWindowCode.Resize(this);
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

            _scene = scene;
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

            _renderPipeline = pipeline;
            pipeline.Activate(this);
        }
    }
}