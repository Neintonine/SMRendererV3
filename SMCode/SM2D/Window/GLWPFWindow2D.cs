using System;
using System.Windows.Media;
using OpenTK;
using OpenTK.Graphics.ES11;
using SM.Base;
using SM2D.Controls;
using SM2D.Pipelines;
using SM2D.Scene;
using SM2D.Shader;

namespace SM2D
{
    public class GLWPFWindow2D : GenericWPFWindow<Scene.Scene, Camera>, IGLWindow2D
    {
        public GLWPFWindow2D()
        {
            Mouse = new Mouse2D(this);
        }

        public Vector2? Scaling { get; set; }
        public Mouse2D Mouse { get; }

        protected override void Init()
        {
            base.Init();

            SMRenderer.DefaultMaterialShader = Default2DShader.MaterialShader;
            
            SetRenderPipeline(Default2DPipeline.Pipeline);
        }

        protected override void Rendering(TimeSpan delta)
        {
            GL.Disable(EnableCap.DepthTest);
            base.Rendering(delta);
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
    }
}