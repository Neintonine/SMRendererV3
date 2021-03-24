using OpenTK.Graphics.OpenGL4;
using SM.Base.PostEffects;
using SM.Base.Window;
using SM.OGL.Framebuffer;
using SM.OGL.Texture;

namespace SM_TEST
{
    public class TestRenderPipeline : RenderPipeline
    {
        private BloomEffect _bloom;
        private Framebuffer _postBuffer;

        public override void Initialization()
        {

            MainFramebuffer = CreateWindowFramebuffer(16, PixelInformation.RGBA_HDR);

            _postBuffer = CreateWindowFramebuffer(0, PixelInformation.RGBA_HDR, depth: false);
            Framebuffers.Add(_postBuffer);
            _bloom = new BloomEffect(_postBuffer, hdr: true, .5f)
            {
                Threshold = .5f,
            };


            _bloom.Initilize(this);
            base.Initialization();
        }

        protected override void RenderProcess(ref DrawContext context)
        {
            MainFramebuffer.Activate(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            context.Scene.DrawBackground(context);
            context.Scene.DrawMainObjects(context);
            context.Scene.DrawHUD(context);

            PostProcessUtility.ResolveMultisampledBuffers(MainFramebuffer, _postBuffer);

            _bloom.Draw(context);
            Framebuffer.Screen.Activate(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            PostProcessUtility.FinalizeHDR(_postBuffer["color"], .5f);

            context.Scene.DrawDebug(context);
        }
    }
}