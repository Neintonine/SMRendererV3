using OpenTK.Graphics.OpenGL4;
using SM.Base.Legacy.PostProcessing;
using SM.Base.PostEffects;
using SM.Base.Window;
using SM.Intergrations.ShaderTool;
using SM.OGL.Framebuffer;
using SM.OGL.Texture;

namespace SM_TEST
{
    public class TestRenderPipeline : RenderPipeline
    {
        private BloomEffectOld _bloomObsolete;
        private BloomEffect _bloom;
        private STPostProcessEffect _vittage;

        private Framebuffer _postBuffer;

        public override void Initialization()
        {

            MainFramebuffer = CreateWindowFramebuffer(0, PixelInformation.RGBA_HDR, true);

            _bloom = new BloomEffect(true)
            {
                Radius = 20,
            };
            PostProcessEffects.Add(_bloom);

            /*_vittage = new STPostProcessEffect(Program.portal.DrawNodes.Find(a => a.Variables.ContainsKey("_ViewportSize")))
            {
                Arguments =
                {
                    {"CheckSize", 10f},
                    {"Strength", .25f},
                    {"TargetSize", 5f},
                    {"Move", 3.33f}
                }
            };
            _vittage.Initilize(this);*/
            InitizePostProcessing();

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
            //_postBuffer.Activate(ClearBufferMask.ColorBufferBit);
            //PostProcessUtility.ResolveMultisampledBuffers(MainFramebuffer, _postBuffer);

            //_vittage.Draw(MainFramebuffer["color"], context);
            //_bloom.Draw(MainFramebuffer["color"], context);
            _bloomObsolete.Draw(MainFramebuffer["color"], context);
            Framebuffer.Screen.Activate(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            PostProcessUtility.FinalizeHDR(MainFramebuffer["color"], 1f);

            //context.Scene.DrawDebug(context);
        }
    }
}