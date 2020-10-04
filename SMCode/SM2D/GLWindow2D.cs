using System;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;
using SM.Base;
using SM.Base.Controls;
using SM2D.Controls;
using SM2D.Pipelines;
using SM2D.Scene;
using SM2D.Shader;
using Vector2 = OpenTK.Vector2;

namespace SM2D
{
    public class GLWindow2D : GenericWindow<Scene.Scene, Camera>
    {
        public Vector2? Scaling { get; set; }
        public Vector2 WorldScale => _worldScale;

        public Mouse2D Mouse { get; }

        public GLWindow2D()
        {
            Mouse = new Mouse2D(this);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            SetRenderPipeline(new Basic2DPipeline());
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Disable(EnableCap.DepthTest);
            base.OnRenderFrame(e);
        }

        protected override void SetWorldScale()
        {
            if (Scaling.HasValue)
            {
                if (Scaling.Value.X > 0 && Scaling.Value.Y > 0) _worldScale = Scaling.Value;
                else if (Scaling.Value.X > 0) _worldScale = new Vector2(Scaling.Value.X, Scaling.Value.X / Aspect);
                else if (Scaling.Value.Y > 0) _worldScale = new Vector2(Aspect * Scaling.Value.Y, Scaling.Value.Y);
            }
        }

        protected override void OnMouseMove(MouseMoveEventArgs e)
        {
            base.OnMouseMove(e);
            Mouse.MouseMoveEvent(e);
        }
    }
}