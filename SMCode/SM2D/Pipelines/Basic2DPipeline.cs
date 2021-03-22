using OpenTK.Graphics.OpenGL4;
using SM.Base.Shaders;
using SM.Base.Window;
using SM.OGL.Framebuffer;
using SM2D.Shader;

namespace SM2D.Pipelines
{
    /// <summary>
    /// This implements the most basic render pipeline.
    /// </summary>
    public class Basic2DPipeline : RenderPipeline
    {
        /// <summary>
        /// The access to the pipeline.
        /// </summary>
        public static Basic2DPipeline Pipeline = 
            new Basic2DPipeline();

        /// <inheritdoc />
        public override MaterialShader DefaultShader { get; protected set; } = ShaderCollection.Instanced;

        private Basic2DPipeline() {}

        /// <inheritdoc />
        protected override void RenderProcess(ref DrawContext context)
        {
            Framebuffer.Screen.Activate(FramebufferTarget.Framebuffer, ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            context.Scene?.Draw(context);
        }
    }
}