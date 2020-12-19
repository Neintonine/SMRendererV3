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
        protected GenericWindow _window { get; private set; }

        /// <summary>
        ///     The framebuffers, that are used in this Pipeline.
        /// </summary>
        protected virtual List<Framebuffer> _framebuffers { get; }

        /// <summary>
        ///     The default shader for the pipeline.
        /// </summary>
        protected internal virtual MaterialShader _defaultShader { get; } = SMRenderer.DefaultMaterialShader;

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
            if (_framebuffers == null) return;

            foreach (var framebuffer in _framebuffers)
                framebuffer.Dispose();

            Thread.Sleep(50);

            foreach (Framebuffer framebuffer in _framebuffers)
            {
                framebuffer.Compile();
            }
        }

        internal void Activate(GenericWindow window)
        {
            _window = window;

            if (!IsInitialized)
            {
                Initialization(window);
                IsInitialized = true;
            }

            Activation(window);
        }

        /// <summary>
        ///     Occurs, when the pipeline was connected to a window.
        /// </summary>
        protected internal virtual void Activation(GenericWindow window)
        {
        }


        /// <summary>
        ///     Occurs, when the pipeline was connected to a window the first time.
        /// </summary>
        /// <param name="window"></param>
        protected internal virtual void Initialization(GenericWindow window)
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
            framebuffer.Compile();
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
        protected internal virtual void Render(ref DrawContext context, TScene scene)
        {
            context.ActivePipeline = this;
        }

        /// <summary>
        ///     Event, that triggers, when the scene in the current window changes.
        /// </summary>
        /// <param name="scene"></param>
        protected internal virtual void SceneChanged(TScene scene)
        {

        }
    }
}