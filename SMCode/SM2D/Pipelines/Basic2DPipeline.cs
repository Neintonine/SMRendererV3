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

        protected override MaterialShader _defaultShader { get; } = Basic2DShader.Shader;

        private Basic2DPipeline()
        {
            Console.WriteLine();
        }

        protected override void Render(ref DrawContext context, Scene.Scene scene)
        {
            base.Render(ref context, scene);

            Framebuffer.Screen.Activate(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            if (scene != null) scene.Draw(context);
        }
    }
}