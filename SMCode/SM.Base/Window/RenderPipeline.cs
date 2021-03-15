using System.Collections.Generic;
using System.Threading;
using SM.Base.Drawing;
using SM.OGL.Framebuffer;
using SM.OGL.Texture;
using SM.Utility;

namespace SM.Base.Windows
{
    public abstract class RenderPipeline : IInitializable
    {
        public IGenericWindow ConnectedWindow { get; internal set; }

        public Framebuffer MainFramebuffer { get; protected set; }

        public List<Framebuffer> Framebuffers { get; } = new List<Framebuffer>();

        public virtual MaterialShader DefaultShader { get; protected set; } = SMRenderer.DefaultMaterialShader;
        public virtual Material DefaultMaterial { get; protected set; }

        public bool IsInitialized { get; set; }

        internal void Render(ref DrawContext context) => RenderProcess(ref context);

        protected abstract void RenderProcess(ref DrawContext context);

        public virtual void Resize()
        {
            if (Framebuffers == null) return;
            foreach(var framebuffer in Framebuffers) 
                framebuffer.Dispose();

            Thread.Sleep(50);

            foreach(var framebuffer in Framebuffers) 
                framebuffer.Compile();
        }

        public virtual void Activate()
        {
            
        }

        public virtual void Initialization()
        {
            if (MainFramebuffer != null) Framebuffers.Add(MainFramebuffer);
            DefaultShader ??= SMRenderer.DefaultMaterialShader;
        }

        public Framebuffer CreateWindowFramebuffer(int multisamples = 0)
        {
            Framebuffer framebuffer = new Framebuffer(window: ConnectedWindow);
            framebuffer.Append("color", new ColorAttachment(0, PixelInformation.RGBA_LDR, multisamples));
            RenderbufferAttachment depthAttach = RenderbufferAttachment.Depth;
            depthAttach.Multisample = multisamples;
            framebuffer.AppendRenderbuffer(depthAttach);
            return framebuffer;
        }
    }
}