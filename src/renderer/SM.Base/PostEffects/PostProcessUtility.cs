#region usings

using OpenTK.Graphics.OpenGL4;
using SM.Base.PostProcess;
using SM.Base.Utility;
using SM.OGL.Framebuffer;

#endregion

namespace SM.Base.PostEffects
{
    /// <summary>
    /// This class has some utility for render pipelines 
    /// </summary>
    public static class PostProcessUtility
    {
        private static readonly PostProcessShader _hdrExposureShader =
            new PostProcessShader(AssemblyUtility.ReadAssemblyFile(SMRenderer.PostProcessPath + ".finalize_hdr.glsl"));

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
                BlitFramebufferFilter.Nearest);

            target.Activate();
        }

        /// <summary>
        /// This converts HDR to LDR and applys gamma.
        /// </summary>
        /// <param name="attachment"></param>
        /// <param name="exposure"></param>
        public static void FinalizeHDR(ColorAttachment attachment, float exposure)
        {
            _hdrExposureShader.Draw(u =>
            {
                u["Gamma"].SetUniform1(Gamma);
                u["Exposure"].SetUniform1(exposure);
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
                u["Gamma"].SetUniform1(Gamma);
                u["Scene"].SetTexture(attachment);
            });
        }
    }
}