#region usings

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing.Text;
using System.Threading;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Input;
using SM.Base.Scene;
using SM.Base.Utility;
using SM.Base.Window.Contexts;
using SM.OGL;
using Mouse = SM.Base.Controls.Mouse;

#endregion

namespace SM.Base.Window
{
    /// <summary>
    /// This provides the main entry, by executing the window.
    /// </summary>
    public class GLWindow : GameWindow, IGenericWindow
    {
        private Vector2 _flagWindowPos;
        private Vector2 _flagWindowSize;

        private Thread _fixedUpdateThread;
        private WindowFlags _windowFlags;

        private DisplayResolution _normalResolution;
        private DisplayResolution _fullscreenResolution;


        /// <inheritdoc />
        public bool Loading { get; private set; } = true;

        /// <inheritdoc />
        public float AspectRatio { get; set; }

        /// <inheritdoc />
        public GenericCamera ViewportCamera { get; set; }

        /// <inheritdoc />
        public bool ForceViewportCamera { get; set; }

        /// <inheritdoc />
        public bool DrawWhileUnfocused { get; set; } = true;
        /// <inheritdoc />
        public bool UpdateWhileUnfocused { get; set; } = false;

        /// <inheritdoc />
        public Vector2 WindowSize { get; set; }
        /// <inheritdoc />
        public ISetup AppliedSetup { get; private set; }

        /// <inheritdoc />
        public new event Action<IGenericWindow> Resize;
        /// <inheritdoc />
        public new event Action<IGenericWindow> Load;

        /// <inheritdoc />
        public GenericScene CurrentScene { get; private set; }
        /// <inheritdoc />
        public RenderPipeline CurrentRenderPipeline { get; private set; }

        /// <summary>
        /// Gets/Sets the current window flag.
        /// </summary>
        public WindowFlags WindowFlags
        {
            get => _windowFlags;
            set
            {
                if (_windowFlags != value)
                {
                    WindowFlags oldV = _windowFlags;
                    _windowFlags = value;
                    ChangeWindowFlag(value, oldV);
                }
            }
        }

        /// <summary>
        /// Loads the window with default values.
        /// <para>Width: 1280px; Height: 720px; Title: Generic OpenGL Title; WindowFlag: Window</para>
        /// </summary>
        public GLWindow() : this(1280, 720, "Generic OpenGL Title", WindowFlags.Window)
        {
        }

        /// <summary>
        /// Loads the window with custom values.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="title"></param>
        /// <param name="flags"></param>
        /// <param name="vSync"></param>
        public GLWindow(int width, int height, string title, WindowFlags flags, VSyncMode vSync = VSyncMode.On) :
            base(width, height, default, title, GameWindowFlags.Default, DisplayDevice.Default,
                GLSettings.ForcedVersion.MajorVersion, GLSettings.ForcedVersion.MinorVersion,
                GraphicsContextFlags.Default)
        {
            VSync = vSync;
            WindowFlags = flags;

            FocusedChanged += GLWindow_FocusedChanged;
            _normalResolution = _fullscreenResolution = DisplayDevice.Default.SelectResolution(DisplayDevice.Default.Width, DisplayDevice.Default.Height, DisplayDevice.Default.BitsPerPixel, DisplayDevice.Default.RefreshRate);
        }


        /// <summary>
        /// A event that gets executed when the window is done loading.
        /// </summary>
        public event Action<IGenericWindow> Loaded;

        /// <inheritdoc />
        protected override void OnLoad(EventArgs e)
        {
            WindowCode.Load(this);
            SMRenderer.CurrentWindow = this;

            base.OnLoad(e);
        }

        /// <inheritdoc />
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            WindowCode.Resize(this);

            if (Loading)
            {
                Loading = false;
                Loaded?.Invoke(this);
                AppliedSetup?.Loaded(this);
            }
        }

        /// <inheritdoc />
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            if (!Focused && !UpdateWhileUnfocused) return;

            base.OnUpdateFrame(e);

            WindowCode.Update(this, (float) e.Time);
        }


        /// <inheritdoc />
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            WindowCode.Render(this, (float) e.Time);

            SwapBuffers();

            GLDebugging.CheckGLErrors();
        }

        /// <inheritdoc />
        protected override void OnMouseMove(MouseMoveEventArgs e)
        {
            base.OnMouseMove(e);
            Mouse.MouseMoveEvent(e, this);
        }


        /// <inheritdoc />
        public void TriggerLoad()
        {
            Load?.Invoke(this);
        }

        /// <inheritdoc />
        public void TriggerResize()
        {
            Resize?.Invoke(this);
        }

        /// <inheritdoc />
        public void Update(UpdateContext context)
        {
            
        }

        /// <inheritdoc />
        public void ApplySetup(ISetup setup)
        {
            AppliedSetup = setup;
            setup.Applied(this);
        }

        /// <inheritdoc />
        public void SetScene(GenericScene scene)
        {
            if (Loading)
            {
                Loaded += window => SetScene(scene);
                return;
            }

            WindowCode.PrepareScene(this, scene);
            CurrentScene = scene;
        }

        /// <inheritdoc />
        public void SetRenderPipeline(RenderPipeline renderPipeline)
        {
            if (Loading)
            {
                Loaded += window => SetRenderPipeline(renderPipeline);
                return;
            }

            WindowCode.PreparePipeline(this, renderPipeline);
            CurrentRenderPipeline = renderPipeline;
        }

        /// <summary>
        /// Changes the resolution in fullscreen mode.
        /// <para>Can be executed before changing to fullscreen, but with no effect until changed.</para>
        /// </summary>
        /// <param name="resolution">The resolution you get from <see cref="DisplayDevice.AvailableResolutions"/> or <see cref="DisplayDevice.SelectResolution"/></param>
        public void ChangeFullscreenResolution(DisplayResolution resolution)
        {
            _fullscreenResolution = resolution;

            if (_windowFlags == WindowFlags.ExclusiveFullscreen) ApplyFullscreenResolution();
        }

        /// <summary>
        /// Starts the fixed update loop.
        /// <para>Need to get executed before <see cref="IFixedScriptable"/> can be used.</para>
        /// </summary>
        /// <param name="updatesPerSecond"></param>
        public void RunFixedUpdate(float updatesPerSecond)
        {
            Deltatime.FixedUpdateDelta = 1 / (float)updatesPerSecond;

            _fixedUpdateThread = new Thread(ExecuteFixedUpdate);
            _fixedUpdateThread.Start();
        }

        private void ExecuteFixedUpdate()
        {
            Stopwatch deltaStop = new Stopwatch();
            while (!IsExiting)
            {
                deltaStop.Restart();

                FixedUpdateContext context = new FixedUpdateContext();

                CurrentScene?.FixedUpdate(context);

                long delta = deltaStop.ElapsedMilliseconds;
                int waitTime = Math.Max((int)(Deltatime.FixedUpdateDelta * 1000) - (int)delta, 0);
                Thread.Sleep(waitTime);
            }
        }

        private void GLWindow_FocusedChanged(object sender, EventArgs e)
        {
            if (_windowFlags == WindowFlags.ExclusiveFullscreen)
            {
                if (!Focused)
                {
                    DisplayDevice.Default.ChangeResolution(_normalResolution);
                    WindowState = WindowState.Minimized;
                }
                else
                {
                    ApplyFullscreenResolution();
                }
            }

        }
        private void ChangeWindowFlag(WindowFlags newFlag, WindowFlags oldFlag)
        {
            if (oldFlag == WindowFlags.Window)
            {
                _flagWindowSize = WindowSize;
                _flagWindowPos = new Vector2(X, Y);
            }

            switch (newFlag)
            {
                case WindowFlags.Window:
                    DisplayDevice.Default.ChangeResolution(_normalResolution);
                    WindowState = WindowState.Normal;
                    Width = (int)_flagWindowSize.X;
                    Height = (int)_flagWindowSize.Y;

                    X = (int) _flagWindowPos.X;
                    Y = (int) _flagWindowPos.Y;

                    WindowBorder = WindowBorder.Resizable;


                    break;
                case WindowFlags.BorderlessWindow:
                    DisplayDevice.Default.ChangeResolution(_normalResolution);
                    WindowState = WindowState.Maximized;
                    WindowBorder = WindowBorder.Hidden;

                    X = 0;
                    Y = 0;
                    Width = DisplayDevice.Default.Width;
                    Height = DisplayDevice.Default.Height;

                    break;
                case WindowFlags.ExclusiveFullscreen:


                    WindowState = WindowState.Fullscreen;
                    ApplyFullscreenResolution();

                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(newFlag), newFlag, null);
            }

            
        }

        private void ApplyFullscreenResolution()
        {
            DisplayDevice.Default.ChangeResolution(_fullscreenResolution);
            Width = _fullscreenResolution.Width;
            Height = _fullscreenResolution.Height;
        }
    }
}