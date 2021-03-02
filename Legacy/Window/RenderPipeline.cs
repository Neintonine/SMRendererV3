#region usings

using System.Collections.Generic;
using System.Threading;
using SM.Base.Contexts;
using SM.Base.Drawing;
using SM.Base.Scene;
using SM.OGL.Framebuffer;

#endregion

namespace SM.Base
{
    /// <summary>
    ///     Definition of specific render options.
    /// </summary>
    public abstract class RenderPipeline
    {
        /// <summary>
        /// If true, this pipeline was already once activated.
        /// </summary>
        public bool IsInitialized { get; private set; } = false;

        /// <summary>
        /// The window the pipeline is connected to.
        /// </summary>
        protected IGenericWindow _window { get; private set; }

        /// <summary>
        ///     The framebuffers, that are used in this Pipeline.
        /// </summary>
        public virtual List<Framebuffer> Framebuffers { get; private set; }

        /// <summary>
        ///     The default shader for the pipeline.
        /// </summary>
        protected internal virtual MaterialShader _defaultShader { get; set; }

        public virtual Framebuffer MainFramebuffer { get; protected set; }= Framebuffer.Screen;

        /// <summary>
        ///     Occurs, when the window is loading.
        /// </summary>
        protected internal virtual void Load()
        {
        }

        /// <summary>
        ///     Occurs, when the window is resizing.
        /// </summary>
        protected internal virtual void Resize()
        {
            if (Framebuffers == null) return;

            foreach (var framebuffer in Framebuffers)
                framebuffer.Dispose();

            Thread.Sleep(50);

            foreach (Framebuffer framebuffer in Framebuffers)
            {
                framebuffer.Compile();
            }
        }

        internal void Activate(IGenericWindow window)
        {
            _window = window;

            if (!IsInitialized)
            {
                if (_defaultShader == null) _defaultShader = SMRenderer.DefaultMaterialShader;
                Framebuffers = new List<Framebuffer>();

                Initialization(window);
                
                Framebuffers.Add(MainFramebuffer);

                IsInitialized = true;
            }

            Activation(window);
        }

        /// <summary>
        ///     Occurs, when the pipeline was connected to a window.
        /// </summary>
        protected internal virtual void Activation(IGenericWindow window)
        {
        }


        /// <summary>
        ///     Occurs, when the pipeline was connected to a window the first time.
        /// </summary>
        /// <param name="window"></param>
        protected internal virtual void Initialization(IGenericWindow window)
        {

        }

        /// <summary>
        ///     Occurs, when the window is unloading.
        /// </summary>
        protected internal virtual void Unload()
        {
        }

        /// <summary>
        /// Creates a framebuffer, that has specific (often) required settings already applied.
        /// </summary>
        /// <returns></returns>
        public static Framebuffer CreateWindowFramebuffer()
        {
            Framebuffer framebuffer = new Framebuffer(window: SMRenderer.CurrentWindow);
            framebuffer.Append("color", 0);
            return framebuffer;
        }
    }

    /// <summary>
    ///     Represents a render pipeline.
    /// </summary>
    /// <typeparam name="TScene">The scene type</typeparam>
    public abstract class RenderPipeline<TScene> : RenderPipeline
        where TScene : GenericScene
    {
        /// <summary>
        ///     The system to render stuff.
        /// </summary>
        internal void Render(ref DrawContext context)
        {
            context.ActivePipeline = this;
            if (context.ActiveScene == null) return;

            RenderProcess(ref context, (TScene)context.ActiveScene);
        }

        protected abstract void RenderProcess(ref DrawContext context, TScene scene);

        /// <summary>
        ///     Event, that triggers, when the scene in the current window changes.
        /// </summary>
        /// <param name="scene"></param>
        protected internal virtual void SceneChanged(TScene scene)
        {

        }
    }
}