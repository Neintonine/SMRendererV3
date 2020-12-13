using System.Reflection;
using SM.Base.PostProcess;
using SM.Base.Scene;
using SM.OGL.Framebuffer;
using SM.Utility;

namespace SM2D.Light
{
    public class LightPostEffect : PostProcessEffect
    {
        PostProcessShader _shader = new PostProcessShader(AssemblyUtility.ReadAssemblyFile("SM2D.Light.light.frag"));

        public override void Draw(Framebuffer main)
        {
            base.Draw(main);

            _shader.Draw(main.ColorAttachments["color"]);
        }
    }
}