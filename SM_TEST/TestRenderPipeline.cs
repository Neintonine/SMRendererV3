using System.Collections.Generic;
using OpenTK.Graphics.OpenGL4;
using SM.Base;
using SM.Base;
using SM.Base.Drawing;
using SM.Base.PostEffects;
using SM.Base.Windows;
using SM.OGL.Framebuffer;
using SM2D.Scene;

namespace SM_TEST
{
    public class TestRenderPipeline : RenderPipeline
    {
        private BloomEffect _bloom;

        public override void Initialization()
        {
            _bloom = new BloomEffect(hdr: true)
            {
                Threshold = .5f,
            };

            MainFramebuffer = CreateWindowFramebuffer();

            _bloom.Initilize(this);
            base.Initialization();
        }

        protected override void RenderProcess(ref DrawContext context)
        {
            MainFramebuffer.Activate(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            context.Scene.DrawBackground(context);
            context.Scene.DrawMainObjects(context);

            Framebuffer.Screen.Activate(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            _bloom.Draw(context);

            context.Scene.DrawHUD(context);
            context.Scene.DrawDebug(context);
        }
    }
}