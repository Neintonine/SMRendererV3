#region usings

using OpenTK.Graphics.OpenGL4;
using SM.Base;
using SM.Base.Contexts;
using SM.Base.Scene;
using SM.OGL.Framebuffer;
using SM2D.Shader;

#endregion

namespace SM2D.Pipelines
{
    public class Basic2DPipeline : RenderPipeline<Scene.Scene>
    {

        protected override void Render(ref DrawContext context, Scene.Scene scene)
        {
            base.Render(ref context, scene);

            Framebuffer.Screen.Activate(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            scene?.Draw(context);
        }
    }
}