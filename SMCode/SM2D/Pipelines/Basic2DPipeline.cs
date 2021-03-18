using SM.Base.Shaders;
using SM.Base.Window;
using SM2D.Shader;

namespace SM2D.Pipelines
{
    public class Basic2DPipeline : RenderPipeline
    {
        public static Basic2DPipeline Pipeline = new Basic2DPipeline();

        public override MaterialShader DefaultShader { get; protected set; } = ShaderCollection.Instanced;


        protected override void RenderProcess(ref DrawContext context)
        {
            context.Scene?.Draw(context);
        }
    }
}