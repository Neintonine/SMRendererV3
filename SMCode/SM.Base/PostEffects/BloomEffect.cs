using System.ComponentModel;
using OpenTK.Graphics.OpenGL4;
using SM.Base.Contexts;
using SM.Base.PostProcess;
using SM.OGL.Framebuffer;
using SM.OGL.Texture;
using SM.Utility;

namespace SM.Base.PostEffects
{
    public class BloomEffect : PostProcessEffect
    {
        private Framebuffer _bloomBuffer1;
        private Framebuffer _bloomBuffer2;

        private ColorAttachment _xBuffer;
        private ColorAttachment _yBuffer;

        private PostProcessShader _shader = new PostProcessShader(AssemblyUtility.ReadAssemblyFile("SM.Base.PostEffects.Shaders.bloom_blur.glsl"));
        private PostProcessShader _mergeShader = new PostProcessShader(AssemblyUtility.ReadAssemblyFile("SM.Base.PostEffects.Shaders.bloom_merge.glsl"));

        private int _bloomLocation;
        private bool _hdr;

        public int Iterations = 5;
        public float Threshold = 0.8f;
        
        public float[] Weights = { 0.227027f, 0.1945946f, 0.1216216f, 0.054054f, 0.016216f };
        
        public BloomEffect(int bloomLocation, bool hdr = false)
        {
            _bloomLocation = bloomLocation;
            _hdr = hdr;
        }

        protected override void InitProcess()
        {
            Pipeline.MainFramebuffer.ColorAttachments["color"].PixelInformation = PixelInformation.RGBA_HDR;

            _bloomBuffer1 = new Framebuffer(SMRenderer.CurrentWindow);
            _bloomBuffer1.Append("xBuffer", _xBuffer = new ColorAttachment(0, PixelInformation.RGBA_HDR));
            _bloomBuffer1.Compile();
            _bloomBuffer2 = new Framebuffer(SMRenderer.CurrentWindow);
            _bloomBuffer2.Append("yBuffer", _yBuffer = new ColorAttachment(0, PixelInformation.RGBA_HDR));
            _bloomBuffer2.Compile();

            Pipeline.Framebuffers.Add(_bloomBuffer1);
            Pipeline.Framebuffers.Add(_bloomBuffer2);
        }

        public override void Draw(DrawContext context)
        {
            Framebuffer target = Framebuffer.GetCurrentlyActive();

            bool first = true, hoz = true;
            int iter = Iterations * 2;
            for (int i = 0; i < iter; i++)
            {
                (hoz ? _bloomBuffer1 : _bloomBuffer2).Activate();

                _shader.Draw(first ? Pipeline.MainFramebuffer.ColorAttachments["color"] : (hoz ? _yBuffer : _xBuffer), collection =>
                {
                    collection["First"].SetUniform1(first);
                    collection["Threshold"].SetUniform1(Threshold);

                    collection["Horizontal"].SetUniform1(hoz);

                    collection["Weights"].SetUniform1(Weights);
                    collection["WeightCount"].SetUniform1(Weights.Length);
                });

                hoz = !hoz;
                if (first) first = false;
            }

            target.Activate();
            _mergeShader.Draw(Pipeline.MainFramebuffer.ColorAttachments["color"], collection =>
            {
                collection["Bloom"].SetTexture(_yBuffer);

                collection["Exposure"].SetUniform1(context.UsedCamera.Exposure);
                collection["HDR"].SetUniform1(_hdr);
            });
        }
    }
}