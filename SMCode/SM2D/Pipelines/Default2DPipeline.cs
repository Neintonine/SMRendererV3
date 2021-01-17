#region usings

using System.Collections.Generic;
using OpenTK.Graphics.OpenGL4;
using SM.Base;
using SM.Base.Contexts;
using SM.Base.Scene;
using SM.OGL.Framebuffer;
using SM2D.Shader;

#endregion

namespace SM2D.Pipelines
{
    public class Default2DPipeline : RenderPipeline<Scene.Scene>
    {
        public static Default2DPipeline Pipeline = new Default2DPipeline();

        
        private Default2DPipeline()
        {

        }

        protected override void Initialization(IGenericWindow window)
        {
            MainFramebuffer = CreateWindowFramebuffer();
        }

        protected override void RenderProcess(ref DrawContext context, Scene.Scene scene)
        {
            if (scene != null)
            {
                scene.DrawBackground(context);

                scene.DrawMainObjects(context);

                scene.DrawHUD(context);
                scene.DrawDebug(context);
            }
        }

        protected override void SceneChanged(Scene.Scene scene)
        {
            base.SceneChanged(scene);
        }
    }
}