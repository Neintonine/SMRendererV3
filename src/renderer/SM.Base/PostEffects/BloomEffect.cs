using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using SM.Base.Drawing;
using SM.Base.PostProcess;
using SM.Base.Types;
using SM.Base.Utility;
using SM.Base.Window;
using SM.OGL.Framebuffer;
using SM.OGL.Shaders;
using SM.OGL.Texture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Base.PostEffects
{
    /// <summary>
    /// The recommended bloom effect, that looks way better than the old one.
    /// <para>Based on Blender's implermentation, which is based on COD: Infinite Warfare.</para>
    /// </summary>
    public class BloomEffect : PostProcess.PostProcessEffect
    {
        private static readonly ShaderFile samplingFile = new ShaderFile(AssemblyUtility.ReadAssemblyFile(SMRenderer.PostProcessPath + ".bloom.sampling.frag"));

        private static readonly PostProcessShader _filterShader = new PostProcessShader(
            AssemblyUtility.ReadAssemblyFile(SMRenderer.PostProcessPath + ".bloom.filter.frag")
            );
        private static readonly PostProcessShader _downsampleShader = new PostProcessShader(
            AssemblyUtility.ReadAssemblyFile(SMRenderer.PostProcessPath + ".bloom.downsample.frag")
            );
        private static readonly PostProcessShader _upsampleShader = new PostProcessShader(
            AssemblyUtility.ReadAssemblyFile(SMRenderer.PostProcessPath + ".bloom.upsample.frag")
            );
        private static readonly PostProcessShader _combineShader = new PostProcessShader(
            new ShaderFile(AssemblyUtility.ReadAssemblyFile(SMRenderer.PostProcessPath + ".bloom.combine.vert")),
            AssemblyUtility.ReadAssemblyFile(SMRenderer.PostProcessPath + ".bloom.combine.frag")
            );

        static BloomEffect()
        {
            _upsampleShader.ShaderFiles.Fragment[0].GLSLExtensions.Add(samplingFile);
            _combineShader.ShaderFiles.Fragment[0].GLSLExtensions.Add(samplingFile);
        }

        const int MAXBLOOMSTEPS = 8;
        const float INTENSITY = .1f;

        private readonly bool _hdr;

        private List<Framebuffer> _downsampler;
        private List<Framebuffer> _upsample;

        private int _iterations;
        private float _sampleSize;
        private Vector4 _thresholdCurve;
        private Color4 _bloomColor;

        /// <summary>
        /// The threshold, where the effect decided what is bright.
        /// </summary>
        public float Threshold = .8f;
        /// <summary>
        /// The radius of the effect.
        /// </summary>
        public float Radius = 6.5f;
        /// <summary>
        /// Makes transition between under/over-threshold gradual.
        /// </summary>
        public float Knee = .5f;
        /// <summary>
        /// The intensity of the effect.
        /// </summary>
        public float Intensity = .5f;
        /// <summary>
        /// The tint of the effect.
        /// </summary>
        public Color4 Color = Color4.White;

        /// <summary>
        /// An amount map specifices where the bloom effect should be visible.
        /// <para>Reads only in the "R"-channel.</para>
        /// </summary>
        public TextureBase AmountMap;
        /// <summary>
        /// Allows you to transform the texture coordnates for <see cref="AmountMap"/>
        /// </summary>
        public TextureTransformation AmountMapTransform = new TextureTransformation();
        /// <summary>
        /// Specifices limits, how the <see cref="AmountMap"/> is read.
        /// <para>Default: <see cref="MinMax.Default"/></para>
        /// </summary>
        public MinMax AmountLimits = MinMax.Default;

        /// <summary>
        /// This creates a more prettier bloom effect.
        /// </summary>
        /// <param name="hdr">This allows to enable hdr returns.</param>
        public BloomEffect(bool hdr = false)
        {
            _hdr = hdr;
        }
        /// <inheritdoc/>
        protected override void InitProcess() => CreateFramebuffers();

        private void CreateFramebuffers()
        {
            if (_downsampler != null) _downsampler.ForEach(a => a.Reset());
            if (_upsample != null) _upsample.ForEach(a => a.Reset());

            _downsampler = new List<Framebuffer>();
            _upsample = new List<Framebuffer>();

            Vector2 windowSize = Pipeline.ConnectedWindow.WindowSize;

            float minDim = (float)Math.Min(windowSize.X, windowSize.Y);
            float maxIter = (Radius - 8.0f) + (float)(Math.Log(minDim) / Math.Log(2));
            int maxIterInt = (int)maxIter;

            _iterations = Math.Max(Math.Min(MAXBLOOMSTEPS, maxIterInt), 1);

            _sampleSize = .5f + maxIter - maxIterInt;
            _thresholdCurve = new Vector4(
                Threshold - Knee,
                Knee * 2,
                0.25f / Math.Max(1e-5f, Knee),
                Threshold);

            float intens = (Intensity * INTENSITY);
            _bloomColor = new Color4(Color.R * intens, Color.G * intens, Color.B * intens, 1f);

            PixelInformation pixel = new PixelInformation(PixelInternalFormat.R11fG11fB10f, PixelFormat.Rgb, PixelType.Float);
            
            Vector2 texSize = windowSize;
            Framebuffer f = new Framebuffer(texSize);
            f.Append("0", new ColorAttachment(0, pixel));
            f.Append("1", new ColorAttachment(1, pixel));
            _downsampler.Add(f);
            for (int i = 0; i < _iterations; i++)
            {
                texSize /= 2;



                f = new Framebuffer(texSize);
                f.Append("0", new ColorAttachment(0, pixel));
                _downsampler.Add(f);

                if (i == _iterations - 1) break;
                f = new Framebuffer(texSize);
                f.Append("0", new ColorAttachment(0, pixel));
                _upsample.Add(f);
            }
        }

        /// <inheritdoc/>
        public override void ScreenSizeChanged(IGenericWindow window)
        {
            CreateFramebuffers();
        }

        /// <inheritdoc/>
        protected override void Drawing(ColorAttachment source, DrawContext context)
        {
            Framebuffer target = Framebuffer.GetCurrentlyActive();

            // Filtering
            _downsampler[0].Activate(true);
            _filterShader.Draw(source, col =>
            {
                col["ThresholdCurve"].SetVector4(_thresholdCurve);
            });

            // Downsampling
            ColorAttachment last = _downsampler[0]["0"];
            for(int i = 1; i < _iterations; i++)
            {
                ColorAttachment downsampleSource = last;
                Framebuffer downsampleTarget = _downsampler[i];
                downsampleTarget.Activate(true);
                _downsampleShader.Draw(downsampleSource);

                last = downsampleTarget["0"];
            }
            
            // Upsampling
            for (int i = _iterations - 2; i >= 0; i--)
            {
                ColorAttachment downsampleSource = _downsampler[i]["0"];
                Framebuffer upsampleTarget = _upsample[i];
                
                upsampleTarget.Activate(true);

                _upsampleShader.Draw(last, (a) =>
                {
                    if (last != null) a["baseBuffer"].SetTexture(downsampleSource);
                    a["sampleSize"].SetFloat(_sampleSize);
                });

                last = upsampleTarget["0"];
            }

            // combine
            target.Activate(true);
            _combineShader.Draw(last, (a) =>
            {
                a["sampleSize"].SetFloat(_sampleSize);

                a["scene"].SetTexture(_downsampler[0]["1"]);
                a["bloomColor"].SetColor(_bloomColor);

                if (AmountMap != null)
                {
                    a["amountTransform"].SetMatrix3(AmountMapTransform.GetMatrix());
                    a["amountMap"].SetTexture(AmountMap, a["hasAmountMap"]);
                    a["amountLimit"].SetVector2((Vector2)AmountLimits);

                }

                a["HDR"].SetBool(_hdr);
            });
        }
    }
}
