#region usings

using System.Collections.Generic;
using OpenTK.Graphics.OpenGL4;
using SM.Base;
using SM.Base.Contexts;
using SM.Base.Scene;
using SM.OGL.Framebuffer;
using SM2D.Light;
using SM2D.Shader;

#endregion

namespace SM2D.Pipelines
{
    public class Default2DPipeline : RenderPipeline<Scene.Scene>
    {
        public static Default2DPipeline Pipeline = new Default2DPipeline();

        private Framebuffer _tempWindow;
        private Light.LightPostEffect _lightEffect;

        protected override List<Framebuffer> _framebuffers { get; } = new List<Framebuffer>();

        private Default2DPipeline()
        {

        }

        protected override void Initialization(GenericWindow window)
        {
            _tempWindow = CreateWindowFramebuffer();
            _lightEffect = new LightPostEffect();

            _framebuffers.Add(_tempWindow);

            _lightEffect.Init(_tempWindow);
        }

        protected override void Render(ref DrawContext context, Scene.Scene scene)
        {
            base.Render(ref context, scene);

            if (scene != null)
            {
                _tempWindow.Activate(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
                scene.DrawBackground(context);

                scene.DrawMainObjects(context);
                
                Framebuffer.Screen.Activate();
                _lightEffect.Draw(_tempWindow, Framebuffer.Screen);

                scene.DrawHUD(context);
                scene.DrawDebug(context);
            }
        }

        protected override void SceneChanged(Scene.Scene scene)
        {
            base.SceneChanged(scene);
            _lightEffect.SceneChanged(scene);
        }
    }
}