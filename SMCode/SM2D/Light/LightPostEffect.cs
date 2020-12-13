using System.Reflection;
using SM.Base.PostProcess;
using SM.Base.Scene;
using SM.OGL.Framebuffer;
using SM.Utility;

namespace SM2D.Light
{
    public class LightPostEffect : PostProcessEffect
    {
        private PostProcessShader _shader = new PostProcessShader(AssemblyUtility.ReadAssemblyFile("SM2D.Light.light.frag"));
        private LightSceneExtension sceneExtension;

        public override void Draw(Framebuffer main)
        {
            base.Draw(main);
            
            _shader.Draw(main.ColorAttachments["color"], collection =>
            {
                collection["Ambient"].SetUniform4(sceneExtension.Ambient);
            });
        }

        public override void SceneChanged(GenericScene scene)
        {
            base.SceneChanged(scene);
            sceneExtension = scene.GetExtension<LightSceneExtension>();
        }
    }
}