using OpenTK.Graphics.OpenGL4;
using SM.Base.Legacy.PostProcessing;
using SM.Base.PostEffects;
using SM.Base.Textures;
using SM.Base.Window;
using SM.Intergrations.ShaderTool;
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
            MainFramebuffer = CreateWindowFramebuffer(8, PixelInformation.RGBA_HDR, true);
            _postBuffer = CreateWindowFramebuffer(0, PixelInformation.RGB_HDR, false);

            _bloom = new BloomEffect(true, true)
            {
                Radius = 20,
                AmountMap = new Texture(new System.Drawing.Bitmap("bloom_amountMap.png"))
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
            _postBuffer.Activate(ClearBufferMask.ColorBufferBit);
            PostProcessUtility.ResolveMultisampledBuffers(MainFramebuffer, _postBuffer);

            _bloom.Draw(_postBuffer["color"], context);
            Framebuffer.Screen.Activate(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            PostProcessUtility.FinalizeHDR(_postBuffer["color"], HDRColorCurve.OnlyExposure, .1f);
        }
    }
}