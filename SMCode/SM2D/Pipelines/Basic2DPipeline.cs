using System;
using OpenTK.Graphics.OpenGL4;
using SM.Base;
using SM.Base.Contexts;
using SM.Base.Drawing;
using SM.OGL.Framebuffer;
using SM2D.Shader;

namespace SM2D.Pipelines
{
    public class Basic2DPipeline : RenderPipeline<Scene.Scene>
    {
        public static Basic2DPipeline Pipeline = new Basic2DPipeline();


        private Basic2DPipeline()
        {
            _defaultShader = Basic2DShader.Shader;
        }

        protected override void RenderProcess(ref DrawContext context, Scene.Scene scene)
        {

            Framebuffer.Screen.Activate(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            if (scene != null) scene.Draw(context);
        }
    }
}