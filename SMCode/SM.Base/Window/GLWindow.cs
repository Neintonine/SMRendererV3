#region usings

using System;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Input;
using SM.Base.Scene;
using SM.OGL;
using Mouse = SM.Base.Controls.Mouse;

#endregion

namespace SM.Base.Window
{
    public class GLWindow : GameWindow, IGenericWindow
    {
        private Vector2 _flagWindowSize;

        public WindowFlags WindowFlags;

        public GLWindow() : this(1280, 720, "Generic OpenGL Title", WindowFlags.Window)
        {
        }

        public GLWindow(int width, int height, string title, WindowFlags flags, VSyncMode vSync = VSyncMode.On) :
            base(width, height, default, title, (GameWindowFlags) flags, DisplayDevice.Default,
                GLSettings.ForcedVersion.MajorVersion, GLSettings.ForcedVersion.MinorVersion,
                GraphicsContextFlags.Default)
        {
            VSync = vSync;
            _flagWindowSize = new Vector2(width, height);

            ChangeWindowFlag(flags);
        }

        public bool Loading { get; private set; } = true;
        public float AspectRatio { get; set; }

        public GenericCamera ViewportCamera { get; set; }
        public bool ForceViewportCamera { get; set; }

        public bool DrawWhileUnfocused { get; set; } = true;
        public bool UpdateWhileUnfocused { get; set; } = false;

        public Vector2 WindowSize { get; set; }

        public ISetup AppliedSetup { get; private set; }
        public event Action<IGenericWindow> Resize;
        public event Action<IGenericWindow> Load;

        public GenericScene CurrentScene { get; private set; }
        public RenderPipeline CurrentRenderPipeline { get; private set; }

        public void Update(UpdateContext context)
        {
        }

        public void ApplySetup(ISetup setup)
        {
            AppliedSetup = setup;
            setup.Applied(this);
        }

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

        public void TriggerLoad()
        {
            Load?.Invoke(this);
        }

        public void TriggerResize()
        {
            Resize?.Invoke(this);
        }

        public event Action<IGenericWindow> Loaded;

        protected override void OnLoad(EventArgs e)
        {
            WindowCode.Load(this);
            SMRenderer.CurrentWindow = this;

            base.OnLoad(e);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            WindowCode.Resize(this);

            if (WindowFlags == WindowFlags.Window) _flagWindowSize = WindowSize;

            if (Loading)
            {
                Loading = false;
                Loaded?.Invoke(this);
                AppliedSetup?.Loaded(this);
            }
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            if (!Focused && !UpdateWhileUnfocused) return;

            base.OnUpdateFrame(e);

            WindowCode.Update(this, (float) e.Time);
        }


        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            WindowCode.Render(this, (float) e.Time);

            SwapBuffers();

            GLDebugging.CheckGLErrors();
        }

        protected override void OnMouseMove(MouseMoveEventArgs e)
        {
            base.OnMouseMove(e);
            Mouse.MouseMoveEvent(e, this);
        }

        public void ChangeWindowFlag(WindowFlags newFlag)
        {
            WindowFlags = newFlag;

            switch (newFlag)
            {
                case WindowFlags.Window:
                    Width = (int) _flagWindowSize.X;
                    Height = (int) _flagWindowSize.Y;

                    WindowBorder = WindowBorder.Resizable;
                    break;
                case WindowFlags.BorderlessWindow:
                    WindowBorder = WindowBorder.Hidden;

                    X = Screen.PrimaryScreen.Bounds.Left;
                    Y = Screen.PrimaryScreen.Bounds.Top;
                    Width = Screen.PrimaryScreen.Bounds.Width;
                    Height = Screen.PrimaryScreen.Bounds.Height;

                    break;
                case WindowFlags.ExclusiveFullscreen:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(newFlag), newFlag, null);
            }

            if (newFlag == WindowFlags.BorderlessWindow)
            {
                WindowBorder = WindowBorder.Hidden;
                X = Screen.PrimaryScreen.Bounds.X;
                Y = Screen.PrimaryScreen.Bounds.Y;
            }
        }
    }
}