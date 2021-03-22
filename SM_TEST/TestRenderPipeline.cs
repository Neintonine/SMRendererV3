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

            MainFramebuffer = CreateWindowFramebuffer(0);

            _postBuffer = CreateWindowFramebuffer(0);
            Framebuffers.Add(_postBuffer);
            _bloom = new BloomEffect(MainFramebuffer, hdr: true, .75f)
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

            
            Framebuffer.Screen.Activate(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            _bloom.Draw(context);

            context.Scene.DrawDebug(context);
        }
    }
}