using System.Collections.Generic;
using OpenTK.Graphics.OpenGL4;
using SM.Base;
using SM.Base.Contexts;
using SM.Base.Drawing;
using SM.Base.PostEffects;
using SM.OGL.Framebuffer;
using SM2D.Scene;

namespace SM_TEST
{
    public class TestRenderPipeline : RenderPipeline<Scene>
    {
        private BloomEffect _bloom;
        
        protected override void Initialization(GenericWindow window)
        {
            base.Initialization(window);
            _bloom = new BloomEffect(1);

            MainFramebuffer = CreateWindowFramebuffer();

            _bloom.Initilize(this);
        }

        protected override void RenderProcess(ref DrawContext context, Scene scene)
        {
            MainFramebuffer.Activate(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            scene.DrawBackground(context);
            scene.DrawMainObjects(context);

            Framebuffer.Screen.Activate(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            _bloom.Draw(context);

            scene.DrawHUD(context);
            scene.DrawDebug(context);
        }
    }
}