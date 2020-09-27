using System;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using SM.Base;
using SM2D.Scene;
using SM2D.Shader;

namespace SM2D
{
    public class GLWindow2D : GenericWindow<Scene.Scene, I2DShowItem, Camera>
    {


        protected override void OnLoad(EventArgs e)
        {
            Defaults.DefaultShader = new Default2DShader();

            base.OnLoad(e);
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Disable(EnableCap.DepthTest);
            base.OnRenderFrame(e);
        }
    }
}