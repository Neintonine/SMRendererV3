#region usings

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
    public class Basic2DPipeline : RenderPipeline<Scene.Scene>
    {
        private Framebuffer _tempWindow;
        private Light.LightPostEffect _lightEffect;

        protected override void Initialization(GenericWindow window)
        {
            _tempWindow = CreateWindowFramebuffer();
            _lightEffect = new LightPostEffect();
        }

        protected override void Render(ref DrawContext context, Scene.Scene scene)
        {
            base.Render(ref context, scene);

            _tempWindow.Activate(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            if (scene != null)
            {
                scene.DrawBackground(context);

                scene.DrawMainObjects(context);
                
                Framebuffer.Screen.Activate(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
                _lightEffect.Draw(_tempWindow);

                scene.DrawHUD(context);
            }
        }
    }
}