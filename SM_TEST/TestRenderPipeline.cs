using OpenTK.Graphics.OpenGL4;
using SM.Base.PostEffects;
using SM.Base.Window;
using SM.OGL.Framebuffer;

namespace SM_TEST
{
    public class TestRenderPipeline : RenderPipeline
    {
        private BloomEffect _bloom;
        private Framebuffer _postBuffer;

        public override void Initialization()
        {

            MainFramebuffer = CreateWindowFramebuffer(2);

            _postBuffer = CreateWindowFramebuffer();
            Framebuffers.Add(_postBuffer);


            _bloom = new BloomEffect(_postBuffer, hdr: true);
            _bloom.Initilize(this);
            base.Initialization();
        }

        protected override void RenderProcess(ref DrawContext context)
        {
            MainFramebuffer.Activate(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            context.Scene.DrawBackground(context);
            context.Scene.DrawMainObjects(context);
            context.Scene.DrawHUD(context);

            PostProcessFinals.ResolveMultisampledBuffers(MainFramebuffer, _postBuffer);

            Framebuffer.Screen.Activate(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            _bloom.Draw(context);

            context.Scene.DrawDebug(context);
        }
    }
}