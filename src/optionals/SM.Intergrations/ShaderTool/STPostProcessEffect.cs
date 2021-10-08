using System;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL4;
using ShaderToolParser.Nodes;
using ShaderToolParser.Nodes.Textures;
using ShaderToolParser.Variables;
using SM.Base.Drawing;
using SM.Base.PostProcess;
using SM.Base.Textures;
using SM.Base.Window;
using SM.OGL.Framebuffer;
using SM.OGL.Texture;

namespace SM.Intergrations.ShaderTool
{
    public class STPostProcessEffect : PostProcessEffect
    {
        private STPostProcessShader _shader;
        private Framebuffer tempFramebuffer;

        public ShaderArguments Arguments;

        public STPostProcessEffect(STPDrawNode postEffectNode)
        {
            Arguments = Arguments ?? new ShaderArguments();

            if (postEffectNode.OGLEffect == null)
                throw new Exception("[ERROR AT IMPORTING EFFECT] DrawNode didn't contain a OpenGL-shader.");

            _shader = new STPostProcessShader(postEffectNode);

            foreach (KeyValuePair<string, STPVariable> pair in postEffectNode.Variables)
            {

                if (pair.Value.Type == STPBasisType.Texture)
                {
                    if (pair.Value.Texture == null) continue;
                    Arguments[pair.Key] = new Texture(((STPTextureNode)pair.Value.Texture).Bitmap);
                }
            }
        }

        protected override void InitProcess()
        {
            base.InitProcess();
            tempFramebuffer = Pipeline.CreateWindowFramebuffer(0, PixelInformation.RGB_HDR, false);
            tempFramebuffer.Compile();
        }

        public override void ScreenSizeChanged(IGenericWindow window)
        {
            tempFramebuffer.Recompile();
        }

        protected override void Drawing(ColorAttachment source, DrawContext context)
        {
            Arguments["_Scene"] = (TextureBase)source;
            Arguments["_MVP"] = Mvp;
            Arguments["_ViewportSize"] = context.Window.WindowSize;

            source.ConnectedFramebuffer.CopyTo(tempFramebuffer);
            tempFramebuffer.Activate();

            _shader.Draw(Arguments);
            tempFramebuffer.CopyTo(source.ConnectedFramebuffer);
        }
    }
}