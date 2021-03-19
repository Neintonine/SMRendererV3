#region usings

using System.Collections.Generic;
using System.Threading;
using SM.Base.Drawing;
using SM.Base.Shaders;
using SM.Base.Utility;
using SM.OGL.Framebuffer;
using SM.OGL.Texture;

#endregion

namespace SM.Base.Window
{
    /// <summary>
    /// This represents a render pipeline.
    /// </summary>
    public abstract class RenderPipeline : IInitializable
    {
        /// <summary>
        /// This contains the windows its connected to.
        /// </summary>
        public IGenericWindow ConnectedWindow { get; internal set; }

        /// <summary>
        /// This should contains the framebuffer, when any back buffer effects are used.
        /// </summary>
        public Framebuffer MainFramebuffer { get; protected set; }

        /// <summary>
        /// This list contains all framebuffers that are required to update.
        /// </summary>
        public List<Framebuffer> Framebuffers { get; } = new List<Framebuffer>();

        /// <summary>
        /// This contains the default shader.
        /// <para>Default: <see cref="SMRenderer.DefaultMaterialShader"/></para>
        /// </summary>
        public virtual MaterialShader DefaultShader { get; protected set; }
        /// <summary>
        /// Here you can set a default material.
        /// </summary>
        public virtual Material DefaultMaterial { get; protected set; }

        /// <inheritdoc/>
        public bool IsInitialized { get; set; }

        /// <inheritdoc/>
        public virtual void Activate()
        {
        }

        /// <inheritdoc/>
        public virtual void Initialization()
        {
            if (MainFramebuffer != null) { 
                Framebuffers.Add(MainFramebuffer);
                MainFramebuffer.Name = GetType().Name + ".MainFramebuffer";
            }
            DefaultShader ??= SMRenderer.DefaultMaterialShader;
        }

        internal void Render(ref DrawContext context)
        {
            RenderProcess(ref context);
        }

        /// <summary>
        /// The process of rendering.
        /// </summary>
        protected abstract void RenderProcess(ref DrawContext context);

        /// <summary>
        /// The event when resizing.
        /// </summary>
        public virtual void Resize()
        {
            if (Framebuffers == null) return;
            foreach (var framebuffer in Framebuffers)
                framebuffer.Dispose();

            Thread.Sleep(50);

            foreach (var framebuffer in Framebuffers)
                framebuffer.Compile();
        }

        /// <summary>
        /// This creates a finished setup for a framebuffer.
        /// </summary>
        /// <param name="multisamples"></param>
        /// <returns></returns>
        public Framebuffer CreateWindowFramebuffer(int multisamples = 0)
        {
            Framebuffer framebuffer = new Framebuffer(ConnectedWindow);
            framebuffer.Append("color", new ColorAttachment(0, PixelInformation.RGBA_LDR, multisamples));
            
            RenderbufferAttachment depthAttach = RenderbufferAttachment.Depth;
            depthAttach.Multisample = multisamples;
            framebuffer.AppendRenderbuffer(depthAttach);

            return framebuffer;
        }
    }
}