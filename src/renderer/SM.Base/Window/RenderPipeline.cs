#region usings

using System.Collections.Generic;
using System.Threading;
using SM.Base.Drawing;
using SM.Base.PostProcess;
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
        /// All post processing effects should go here, that should be automaticly managed.
        /// </summary>
        protected List<PostProcessEffect> PostProcessEffects = new List<PostProcessEffect>();

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
        { }
        public void Initialization()
        {
            InitializationProcess();

            InitizePostProcessing();
            if (MainFramebuffer != null)
            {
                Framebuffers.Add(MainFramebuffer);
                MainFramebuffer.Name = GetType().Name + ".MainFramebuffer";
            }
            DefaultShader ??= SMRenderer.DefaultMaterialShader;
        }

        /// <inheritdoc/>
        protected virtual void InitializationProcess()
        {
            
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
        public virtual void Resize(IGenericWindow window)
        {
            Recompile();

            foreach (PostProcessEffect effect in PostProcessEffects)
            {
                effect.ScreenSizeChanged(window);
            }
        }

        /// <summary>
        /// Initilizes the collected post processing effects.
        /// </summary>
        protected void InitizePostProcessing()
        {
            foreach (PostProcessEffect effect in PostProcessEffects)
            {
                effect.Initilize(this);
            }
        }

        /// <summary>
        /// Compiles the framebuffers.
        /// </summary>
        public void Compile()
        {
            foreach (var framebuffer in Framebuffers)
                framebuffer.Compile();
        }

        /// <summary>
        /// Recompiles the pipeline.
        /// </summary>
        public void Recompile()
        {
            if (Framebuffers == null) return;
            Dispose();

            Thread.Sleep(100);

            Compile();
        }

        /// <summary>
        /// Disposes unmanaged resources like Framebuffers.
        /// </summary>
        public void Dispose()
        {
            foreach (var framebuffer in Framebuffers)
                framebuffer.Dispose();
        }

        /// <summary>
        /// This creates a finished setup for a framebuffer.
        /// </summary>
        public Framebuffer CreateWindowFramebuffer(int multisamples = 0, PixelInformation? pixelInformation = null, bool depth = true) =>
            CreateWindowFramebuffer(ConnectedWindow, multisamples, pixelInformation, depth);

        /// <summary>
        /// This creates a finished setup for a framebuffer.
        /// </summary>
        public static Framebuffer CreateWindowFramebuffer(IFramebufferWindow window, int multisamples = 0, PixelInformation? pixelInformation = null, bool depth = true)
        {
            Framebuffer framebuffer = new Framebuffer(window);
            framebuffer.Append("color", new ColorAttachment(0, pixelInformation.GetValueOrDefault(PixelInformation.RGBA_LDR), multisamples:multisamples));

            if (depth)
            {
                RenderbufferAttachment depthAttach = RenderbufferAttachment.GenerateDepth();
                depthAttach.Multisample = multisamples;
                framebuffer.AppendRenderbuffer(depthAttach);
            }

            return framebuffer;
        }
    }
}