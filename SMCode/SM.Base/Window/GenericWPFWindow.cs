using System;
using System.Drawing;
using System.Windows;
using System.Windows.Media;
using OpenTK;
using OpenTK.Wpf;
using SM.Base.Scene;
using SM.OGL;

namespace SM.Base
{
    public class GenericWPFWindow : OpenTK.Wpf.GLWpfControl, IGenericWindow
    {
        private bool _renderContinuesly;

        protected GenericCamera _viewportCamera;

        public bool Loading => !base.IsInitialized;
        public float Aspect { get; set; }
        public GenericCamera ViewportCamera => _viewportCamera;
        public bool ForceViewportCamera { get; set; }
        public int Width => (int) base.ActualWidth;
        public int Height => (int) base.ActualHeight;
        public Rectangle ClientRectangle => new Rectangle((int)base.RenderTransformOrigin.X, (int)RenderTransformOrigin.Y, Width, Height);
        public Vector2 WorldScale { get; set; }
        
        public GenericWPFWindow(bool renderContinuesly = false)
        {
            _renderContinuesly = renderContinuesly;

            Ready += Init;
            Render += Rendering;
        }

        protected virtual void Init()
        {
            GenericWindowCode.Load(this);
        }

        protected virtual void Rendering(TimeSpan delta)
        {

        }

        protected override void OnRenderSizeChanged(SizeChangedInfo info)
        {
            base.OnRenderSizeChanged(info);

            GenericWindowCode.Resize(this);
        }

        public void Start()
        {
            GLWpfControlSettings settings = new GLWpfControlSettings()
            {
                MajorVersion = GLSettings.ForcedVersion.MajorVersion,
                MinorVersion = GLSettings.ForcedVersion.MinorVersion,
                RenderContinuously = _renderContinuesly
            };
            base.Start(settings);
        }

        public virtual void SetWorldScale()
        { }
    }

    public class GenericWPFWindow<TScene, TCamera> : GenericWPFWindow, IGenericWindow<TScene, TCamera>
        where TScene : GenericScene, new()
        where TCamera : GenericCamera, new()
    {
        private TScene _scene;
        private RenderPipeline<TScene> _renderPipeline;

        public TScene CurrentScene => _scene;
        public RenderPipeline<TScene> RenderPipeline => _renderPipeline;

        public GenericWPFWindow(bool renderContinuesly = false) : base(renderContinuesly)
        {
            _viewportCamera = new TCamera();
        }

        protected override void Rendering(TimeSpan delta)
        {
            base.Rendering(delta);

            GenericWindowCode.Render(this, (float)delta.TotalSeconds);
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo info)
        {
            base.OnRenderSizeChanged(info);

            GenericWindowCode.Resize(this);
        }

        public void SetScene(TScene scene)
        {
            _scene = scene;
            scene.Activate();
            RenderPipeline.SceneChanged(scene);
        }

        public void SetRenderPipeline(RenderPipeline<TScene> renderPipeline)
        {
            _renderPipeline = renderPipeline;
            renderPipeline.Activate(this);
        }
    }
}