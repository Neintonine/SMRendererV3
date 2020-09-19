using System;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using SM.Base;
using SM2D.Scene;

namespace SM2D
{
    public class GLWindow2D : GenericWindow<Scene.Scene, Camera>
    {


        protected override void OnLoad(EventArgs e)
        {
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