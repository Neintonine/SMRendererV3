using OpenTK.Graphics.OpenGL4;
using SM.Base.PostEffects;
using SM.Base.Window;
using SM.Intergrations.ShaderTool;
using SM.OGL.Framebuffer;
using SM.OGL.Texture;

namespace SM_TEST
{
    public class TestRenderPipeline : RenderPipeline
    {
        private BloomEffect _bloom;
        private STPostProcessEffect _vittage;

        private Framebuffer _postBuffer;

        public override void Initialization()
        {

            MainFramebuffer = CreateWindowFramebuffer(16, PixelInformation.RGBA_HDR);

            _postBuffer = CreateWindowFramebuffer(0, PixelInformation.RGBA_HDR, depth: true);
            Framebuffers.Add(_postBuffer);
            _bloom = new BloomEffect(_postBuffer, hdr: true, .75f)
            {
            };
            _bloom.Initilize(this);

            _vittage = new STPostProcessEffect(Program.portal.DrawNodes.Find(a => a.Variables.ContainsKey("_ViewportSize")))
            {
                Arguments =
                {
                    {"CheckSize", 10f},
                    {"Strength", .25f},
                    {"TargetSize", 5f},
                    {"Move", 3.33f}
                }
            };
            _vittage.Initilize(this);

            base.Initialization();
        }

        protected override void RenderProcess(ref DrawContext context)
        {
            GL.Enable(EnableCap.DepthTest);
            MainFramebuffer.Activate(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            context.Scene.DrawBackground(context);
            context.Scene.DrawMainObjects(context);
            context.Scene.DrawHUD(context);

            GL.Disable(EnableCap.DepthTest);
            _postBuffer.Activate(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            PostProcessUtility.ResolveMultisampledBuffers(MainFramebuffer, _postBuffer);

            _vittage.Draw(_postBuffer["color"], context);
            _bloom.Draw(_postBuffer["color"], context);
            Framebuffer.Screen.Activate(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            PostProcessUtility.FinalizeHDR(_postBuffer["color"], .1f);

            context.Scene.DrawDebug(context);
        }
    }
}