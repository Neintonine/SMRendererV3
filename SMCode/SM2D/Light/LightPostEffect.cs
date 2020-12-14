using System.Reflection;
using SM.Base.PostProcess;
using SM.Base.Scene;
using SM.OGL.Framebuffer;
using SM.OGL.Shaders;
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
                collection["LightCount"].SetUniform1(sceneExtension.Lights.Count);
                UniformArray array = collection.GetArray("Lights");

                for (int i = 0; i < sceneExtension.Lights.Count; i++)
                {
                    sceneExtension.Lights[i].SetUniforms(array[i]);
                }
            });
        }

        public override void SceneChanged(GenericScene scene)
        {
            base.SceneChanged(scene);
            sceneExtension = scene.GetExtension<LightSceneExtension>();
        }
    }
}