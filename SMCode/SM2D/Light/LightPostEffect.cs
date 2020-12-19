using System.Reflection;
using SM.Base;
using SM.Base.PostProcess;
using SM.Base.Scene;
using SM.OGL.Framebuffer;
using SM.OGL.Shaders;
using SM.Utility;
using SM2D.Scene;

namespace SM2D.Light
{
    public class LightPostEffect : PostProcessEffect
    {
        private PostProcessShader _shader = new PostProcessShader(AssemblyUtility.ReadAssemblyFile("SM2D.Light.light.frag"));
        private LightSceneExtension sceneExtension;

        public override void Init(Framebuffer main)
        {
            base.Init(main);
            main.Append("occluder", 1);
        }

        public override void Draw(Framebuffer main, Framebuffer target)
        {
            _shader.Draw(main.ColorAttachments["color"], collection =>
            {
                collection["FragSize"].SetUniform2((SMRenderer.CurrentWindow as GLWindow2D).WorldScale);

                collection["Ambient"].SetUniform4(sceneExtension.Ambient);
                collection["LightCount"].SetUniform1(sceneExtension.Lights.Count);
                collection["OccluderMap"].SetTexture(main.ColorAttachments["occluder"]);
                collection["ShadowSensitivty"].SetUniform1(1f);
                
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