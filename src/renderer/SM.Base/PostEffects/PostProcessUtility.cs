#region usings

using OpenTK.Graphics.OpenGL4;
using SM.Base.PostProcess;
using SM.Base.Utility;
using SM.OGL.Framebuffer;
using SM.OGL.Shaders;
using System.Collections.Generic;

#endregion

namespace SM.Base.PostEffects
{
    public enum HDRColorCurve
    {
        OnlyExposure,
        Reinhard,
        ACES
    }

    /// <summary>
    /// This class has some utility for render pipelines 
    /// </summary>
    public static class PostProcessUtility
    {
        public static readonly ShaderFile HDRCurves = new ShaderFile(AssemblyUtility.ReadAssemblyFile(SMRenderer.PostProcessPath + ".hdr_curves.frag"));
        private static readonly string _finalizeHdrCode = AssemblyUtility.ReadAssemblyFile(SMRenderer.PostProcessPath + ".finalize_hdr.glsl");

        private static readonly Dictionary<HDRColorCurve, PostProcessShader> _hdrExposureShader = new Dictionary<HDRColorCurve, PostProcessShader>()
        {
            { HDRColorCurve.OnlyExposure, new PostProcessShader(new ShaderFile(_finalizeHdrCode) {GLSLExtensions = { HDRCurves } }) },
            { HDRColorCurve.Reinhard, new PostProcessShader(new ShaderFile(_finalizeHdrCode) { GLSLExtensions = { HDRCurves }, Defines = { "TYPE_REINHARD" } }) },
            { HDRColorCurve.ACES, new PostProcessShader(new ShaderFile(_finalizeHdrCode) { GLSLExtensions = { HDRCurves }, Defines = { "TYPE_ACES" } }) },
        };

        private static readonly PostProcessShader _gammaShader =
            new PostProcessShader(
                AssemblyUtility.ReadAssemblyFile(SMRenderer.PostProcessPath + ".finalize_gamma.glsl"));

        /// <summary>
        /// The gamma that is used for <see cref="FinalizeGamma"/> and <see cref="FinalizeHDR"/>.
        /// </summary>
        public static float Gamma = 2.2f;

        /// <summary>
        /// This resolves a multisampled framebuffer to a non-multisampled renderbuffer.
        /// <para>This removes the depth buffer.</para>
        /// </summary>
        /// <param name="multisampledBuffers"></param>
        /// <param name="target"></param>
        public static void ResolveMultisampledBuffers(Framebuffer multisampledBuffers, Framebuffer target)
        {
            multisampledBuffers.Activate(FramebufferTarget.ReadFramebuffer);
            target.Activate(FramebufferTarget.DrawFramebuffer);
            GL.BlitFramebuffer(0, 0, (int) multisampledBuffers.Size.X, (int) multisampledBuffers.Size.Y, 0, 0,
                (int) target.Size.X, (int) target.Size.Y, ClearBufferMask.ColorBufferBit,
                BlitFramebufferFilter.Linear);

            target.Activate();
        }

        /// <summary>
        /// This converts HDR to LDR and applys gamma.
        /// </summary>
        /// <param name="attachment"></param>
        /// <param name="exposure"></param>
        public static void FinalizeHDR(ColorAttachment attachment, HDRColorCurve colorCurve = HDRColorCurve.ACES, float exposure = 1)
        {
            _hdrExposureShader[colorCurve].Draw(u =>
            {
                u["Gamma"].SetFloat(Gamma);
                u["Exposure"].SetFloat(exposure);
                u["Scene"].SetTexture(attachment);
            });
        }

        /// <summary>
        /// This applys gamma
        /// </summary>
        /// <param name="attachment"></param>
        public static void FinalizeGamma(ColorAttachment attachment)
        {
            _gammaShader.Draw(u =>
            {
                u["Gamma"].SetFloat(Gamma);
                u["Scene"].SetTexture(attachment);
            });
        }
    }
}