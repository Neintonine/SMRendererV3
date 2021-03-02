using System.Windows.Controls;
using OpenTK.Graphics.OpenGL4;
using SM.Base.PostProcess;
using SM.Base.Windows;
using SM.OGL.Framebuffer;
using SM.Utility;

namespace SM.Base.PostEffects
{
    public class PostProcessFinals
    {
        static PostProcessShader _hdrExposureShader = new PostProcessShader(AssemblyUtility.ReadAssemblyFile(SMRenderer.PostProcessPath+".finalize_hdr.glsl"));
        static PostProcessShader _gammaShader = new PostProcessShader(AssemblyUtility.ReadAssemblyFile(SMRenderer.PostProcessPath + ".finalize_gamma.glsl"));

        public static float Gamma = 2.2f;

        public static void ResolveMultisampledBuffers(Framebuffer multisampledBuffers, Framebuffer target)
        {
            multisampledBuffers.Activate(FramebufferTarget.ReadFramebuffer);
            target.Activate(FramebufferTarget.DrawFramebuffer);
            GL.BlitFramebuffer(0, 0, (int)multisampledBuffers.Size.X, (int)multisampledBuffers.Size.Y, 0, 0, (int)target.Size.X, (int)target.Size.Y, ClearBufferMask.ColorBufferBit, BlitFramebufferFilter.Nearest);

            target.Activate();
        }

        public static void FinalizeHDR(ColorAttachment attachment, float exposure)
        {

            _hdrExposureShader.Draw(u =>
            {
                u["Gamma"].SetUniform1(Gamma);
                u["Exposure"].SetUniform1(exposure);
                u["Scene"].SetTexture(attachment);
            });
        }

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