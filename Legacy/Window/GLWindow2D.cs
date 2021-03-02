#region usings

using System;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;
using SM.Base;
using SM2D.Controls;
using SM2D.Pipelines;
using SM2D.Scene;
using SM2D.Shader;

#endregion

namespace SM2D
{
    public class GLWindow2D : GenericWindow<Scene.Scene, Camera>, IGLWindow2D
    {
        public GLWindow2D()
        {
            Mouse = new Mouse2D(this);
        }

        public Vector2? Scaling { get; set; }

        public Mouse2D Mouse { get; }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            SMRenderer.DefaultMaterialShader = Default2DShader.MaterialShader;

            SetRenderPipeline(Default2DPipeline.Pipeline);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Disable(EnableCap.DepthTest);
            base.OnRenderFrame(e);
        }

        public override void SetWorldScale()
        {
            if (Scaling.HasValue)
            {
                if (Scaling.Value.X > 0 && Scaling.Value.Y > 0) WorldScale = Scaling.Value;
                else if (Scaling.Value.X > 0) WorldScale = new Vector2(Scaling.Value.X, Scaling.Value.X / Aspect);
                else if (Scaling.Value.Y > 0) WorldScale = new Vector2(Aspect * Scaling.Value.Y, Scaling.Value.Y);
            }
        }

        protected override void OnMouseMove(MouseMoveEventArgs e)
        {
            base.OnMouseMove(e);
            Mouse.MouseMoveEvent(e);
        }
    }
}