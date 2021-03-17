using System.ComponentModel;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using SM.Base;
using SM.Base.Drawing;
using SM.Base.PostProcess;
using SM.Base.Windows;
using SM.OGL.Framebuffer;
using SM.OGL.Texture;
using SM.Utility;

namespace SM.Base.PostEffects
{
    public class BloomEffect : PostProcessEffect
    {
        private static BezierCurve _defaultCurve = new BezierCurve(Vector2.UnitY, Vector2.Zero, new Vector2(0.2f, 0f), new Vector2(1,0));

        private const float _defaultTextureScale = .75f;

        private float _textureScale = .75f;

        private Framebuffer _bloomBuffer1;
        private Framebuffer _bloomBuffer2;

        private ColorAttachment _xBuffer;
        private ColorAttachment _yBuffer;

        private PostProcessShader _shader = new PostProcessShader(AssemblyUtility.ReadAssemblyFile(SMRenderer.PostProcessPath+".bloom_blur.glsl"));
        private PostProcessShader _mergeShader = new PostProcessShader(AssemblyUtility.ReadAssemblyFile(SMRenderer.PostProcessPath+".bloom_merge_vert.glsl"), AssemblyUtility.ReadAssemblyFile(SMRenderer.PostProcessPath+".bloom_merge.glsl"));

        private bool _hdr;
        private Framebuffer _source;

        private BezierCurve _weightCurve ;
        private float[] _weights;

        public int Iterations = 8;
        public float Threshold = .8f;
        public float Power = 1;

        public bool Enable = true;

        public float MinAmount = 0;
        public float MaxAmount = 1;
        public TextureBase AmountMap;
        public TextureTransformation AmountTransform = new TextureTransformation();

        public BezierCurve WeightCurve
        {
            get => _weightCurve;
            set
            {
                _weightCurve = value;
                UpdateWeights();
            }
        }
        
        public int WeightCurvePickAmount = 4;


        public BloomEffect(Framebuffer source = null, bool hdr = false, float? textureScale = null)
        {
            _source = source;
            _hdr = hdr;
            _textureScale = textureScale.GetValueOrDefault(_defaultTextureScale);

            WeightCurve = _defaultCurve;
        }


        private void UpdateWeights()
        {
            _weights = new float[WeightCurvePickAmount];

            for (int i = 0; i < WeightCurvePickAmount; i++)
            {
                _weights[i] = _weightCurve.CalculatePoint((float)(i + 1) / (WeightCurvePickAmount + 1)).Y;
            }
        }

        

        protected override void InitProcess()
        {
            _source = Pipeline.MainFramebuffer;

            _source.ColorAttachments["color"].PixelInformation = PixelInformation.RGBA_HDR;

            _bloomBuffer1 = new Framebuffer(Pipeline.ConnectedWindow, _textureScale);
            _bloomBuffer1.Append("xBuffer", _xBuffer = new ColorAttachment(0, PixelInformation.RGBA_HDR));
            _bloomBuffer1.Compile();
            _bloomBuffer2 = new Framebuffer(Pipeline.ConnectedWindow, _textureScale);
            _bloomBuffer2.Append("yBuffer", _yBuffer = new ColorAttachment(0, PixelInformation.RGBA_HDR));
            _bloomBuffer2.Compile();

            Pipeline.Framebuffers.Add(_bloomBuffer1);
            Pipeline.Framebuffers.Add(_bloomBuffer2);
        }

        public override void Draw(DrawContext context)
        {
            if (Enable)
            {
                GL.Viewport(0,0, (int)(Pipeline.ConnectedWindow.Width * _textureScale), (int)(Pipeline.ConnectedWindow.Height * _textureScale));
    
                Framebuffer target = Framebuffer.GetCurrentlyActive();
                bool first = true, hoz = true;
                int iter = Iterations * 2;
                for (int i = 0; i < iter; i++)
                {
                    (hoz ? _bloomBuffer1 : _bloomBuffer2).Activate();
    
                    _shader.Draw(collection =>
                    {
                        collection["renderedTexture"].SetTexture(first ? _source.ColorAttachments["color"] : (hoz ? _yBuffer : _xBuffer));
                        collection["RenderScale"].SetUniform1(_textureScale);

                        collection["First"].SetUniform1(first);
                        collection["Threshold"].SetUniform1(Threshold);
    
                        collection["Horizontal"].SetUniform1(hoz);
    
                        collection["Weights"].SetUniform1(_weights);
                        collection["WeightCount"].SetUniform1(WeightCurvePickAmount);
                        collection["Power"].SetUniform1(Power);
                    });
    
                    hoz = !hoz;
                    if (first) first = false;
                }
    
                GL.Viewport(Pipeline.ConnectedWindow.ClientRectangle);
                target.Activate();
            }

            _mergeShader.Draw(collection =>
            {
                collection["Scene"].SetTexture(_source.ColorAttachments["color"]);
                collection["Bloom"].SetTexture(_yBuffer);

                collection["MinAmount"].SetUniform1(MinAmount);
                collection["MaxAmount"].SetUniform1(MaxAmount);
                collection["AmountMap"].SetTexture(AmountMap, collection["HasAmountMap"]);
                collection["TextureTransform"].SetMatrix3(AmountTransform.GetMatrix());

                collection["Exposure"].SetUniform1(context.UseCamera.Exposure);
                collection["HDR"].SetUniform1(_hdr);
            });
        }
    }
}