﻿#region usings

using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using SM.Base.Drawing;
using SM.Base.PostProcess;
using SM.Base.Utility;
using SM.Base.Window;
using SM.OGL.Framebuffer;
using SM.OGL.Shaders;
using SM.OGL.Texture;

#endregion

namespace SM.Base.Legacy.PostProcessing
{
    /// <summary>
    /// A bloom post process effect.
    /// </summary>
    [Obsolete("This bloom effect isn't good. Please use SM.Base.PostEffects.BloomEffect, if you want a good bloom effect.")]
    public class BloomEffectOld : PostProcessEffect
    {
        private static BezierCurve _defaultCurve = new BezierCurve(Vector2.UnitY, Vector2.Zero, new Vector2(0.4f, 0), new Vector2(.5f,0));
        private static readonly PostProcessShader _mergeShader = new PostProcessShader(
            new ShaderFile(AssemblyUtility.ReadAssemblyFile("SM.Base.Legacy.PostProcessing.bloom_merge.vert")),
            AssemblyUtility.ReadAssemblyFile("SM.Base.Legacy.PostProcessing.bloom_merge.glsl"));

        private static readonly PostProcessShader _shader =
            new PostProcessShader(AssemblyUtility.ReadAssemblyFile("SM.Base.Legacy.PostProcessing.bloom_blur.glsl"));
        private const float _defaultTextureScale = .75f;

        private Framebuffer _source;

        private Framebuffer _tempColorBuffer;
        private Framebuffer _bloomBuffer1;
        private Framebuffer _bloomBuffer2;

        private readonly bool _hdr;

        private readonly float _textureScale = .75f;

        private BezierCurve _weightCurve;
        private float[] _weights;

        private ColorAttachment _xBuffer;
        private ColorAttachment _yBuffer;

        /// <summary>
        /// A texture where you can define the amount of the bloom effect at each pixel.
        /// </summary>
        public TextureBase AmountMap;
        /// <summary>
        /// The transformation for the amount map.
        /// </summary>
        public TextureTransformation AmountTransform = new TextureTransformation();
        /// <summary>
        /// The maximal amount the amount map is clamped to.
        /// <para>Default: 1</para>
        /// </summary>
        public float MaxAmount = 1;
        /// <summary>
        /// The minimal amount the amount map is clamped to.
        /// <para>Default: 0</para>
        /// </summary>
        public float MinAmount = 0;
        
        /// <summary>
        /// The defines how often the x-y-flipflop happens.
        /// <para>Default: 8</para>
        /// </summary>
        public int Iterations = 8;
        /// <summary>
        /// The Threshold for the bloom effect.
        /// <para>Default: .8f</para>
        /// </summary>
        public float Threshold = .8f;
        /// <summary>
        /// Increases the brightness of the resulting effect.
        /// <para>Default: 1</para>
        /// </summary>
        public float Power = 1;

        /// <summary>
        /// Radius of the effect
        /// <para>Default: 2</para>
        /// </summary>
        public float Radius = 1;

        /// <summary>
        /// This defines the weight curve.
        /// </summary>
        public BezierCurve WeightCurve
        {
            get => _weightCurve;
            set
            {
                _weightCurve = value;
                UpdateWeights();
            }
        }

        /// <summary>
        /// This defines how many picks the effect should pick from the weight curve.
        /// </summary>
        public int WeightCurvePickAmount = 4;

        /// <summary>
        /// This creates a bloom effect.
        /// </summary>
        /// <param name="source">This can specify a own source framebuffer. If not set, it will take the Pipeline MainFramebuffer.</param>
        /// <param name="hdr">This allows to enable hdr returns.</param>
        /// <param name="textureScale">This allows for a increase in performance, by lowering the calculating texture scale.</param>
        public BloomEffectOld(Framebuffer source = null, bool hdr = false, float? textureScale = null)
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
                _weights[i] = _weightCurve.CalculatePoint((float) (i + 1) / (WeightCurvePickAmount + 1)).Y;
        }

        /// <inheritdoc/>
        protected override void InitProcess()
        {
            _source ??= Pipeline.MainFramebuffer;

            _source.ColorAttachments["color"].PixelInformation = PixelInformation.RGBA_HDR;

            _tempColorBuffer = new Framebuffer(Pipeline.ConnectedWindow, 1);
            _tempColorBuffer.Append("color", new ColorAttachment(0, PixelInformation.RGBA_HDR));
            _tempColorBuffer.Compile();

            _bloomBuffer1 = new Framebuffer(Pipeline.ConnectedWindow, _textureScale)
            {
                Name = "BloomX"
            };
            _bloomBuffer1.Append("xBuffer", _xBuffer = new ColorAttachment(0, PixelInformation.RGBA_HDR));
            _bloomBuffer1.Compile();
            _bloomBuffer2 = new Framebuffer(Pipeline.ConnectedWindow, _textureScale)
            {
                Name = "BloomY"
            };
            _bloomBuffer2.Append("yBuffer", _yBuffer = new ColorAttachment(0, PixelInformation.RGBA_HDR));
            _bloomBuffer2.Compile();

            Pipeline.Framebuffers.Add(_tempColorBuffer);
            Pipeline.Framebuffers.Add(_bloomBuffer1);
            Pipeline.Framebuffers.Add(_bloomBuffer2);
        }

        /// <inheritdoc/>
        protected override void Drawing(ColorAttachment source, DrawContext context)
        {
            GL.Viewport(0, 0, (int) (Pipeline.ConnectedWindow.Width * _textureScale),
                (int) (Pipeline.ConnectedWindow.Height * _textureScale));

            Framebuffer target = Framebuffer.GetCurrentlyActive();

            source.ConnectedFramebuffer.CopyTo(_tempColorBuffer);

            bool first = true, hoz = true;
            int iter = Iterations * 2;
            for (int i = 0; i < iter; i++)
            {
                (hoz ? _bloomBuffer1 : _bloomBuffer2).Activate(false);

                _shader.Draw(collection =>
                {
                    collection["renderedTexture"].SetTexture(first ? source : (hoz ? _yBuffer : _xBuffer));

                    collection["First"].SetBool(first);
                    collection["Threshold"].SetFloat(Threshold);

                    collection["Horizontal"].SetBool(hoz);

                    collection["Weights"].SetFloat(_weights);
                    collection["WeightCount"].SetFloat(WeightCurvePickAmount);
                    collection["Power"].SetFloat(Power);

                    collection["Radius"].SetFloat(_textureScale * Radius);
                });

                hoz = !hoz;
                if (first) first = false;
            }

            GL.Viewport(Pipeline.ConnectedWindow.ClientRectangle);
            target.Activate();

            _mergeShader.Draw(collection =>
            {
                collection["Scene"].SetTexture(_tempColorBuffer["color"]);
                collection["Bloom"].SetTexture(_yBuffer);

                collection["MinAmount"].SetFloat(MinAmount);
                collection["MaxAmount"].SetFloat(MaxAmount);
                collection["AmountMap"].SetTexture(AmountMap, collection["HasAmountMap"]);
                collection["TextureTransform"].SetMatrix3(AmountTransform.GetMatrix());

                collection["Exposure"].SetFloat(context.UseCamera.Exposure);
                collection["HDR"].SetBool(_hdr);
            });
        }
    }
}