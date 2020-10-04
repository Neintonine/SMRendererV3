using System.Collections.Generic;
using SM.Base.Contexts;
using SM.Base.Scene;
using SM.OGL.Framebuffer;

namespace SM.Base
{
    /// <summary>
    /// Definition of specific render options.
    /// </summary>
    public abstract class RenderPipeline
    {

        /// <summary>
        /// The framebuffers, that are used in this Pipeline.
        /// </summary>
        protected virtual List<Framebuffer> _framebuffers { get; }
        
        /// <summary>
        /// The default shader for the pipeline.
        /// </summary>
        protected internal virtual IShader _defaultShader { get; }

        /// <summary>
        /// Occurs, when the window is loading.
        /// </summary>
        protected internal virtual void Load()
        {
            foreach (Framebuffer framebuffer in _framebuffers)
                framebuffer.Compile();
        }

        /// <summary>
        /// Occurs, when the window is resizing.
        /// </summary>
        protected internal virtual void Resize()
        { }

        /// <summary>
        /// Occurs, when the pipeline was connected to a window.
        /// </summary>
        protected internal virtual void Activate(GenericWindow window)
        {
        }

        /// <summary>
        /// Occurs, when the window is unloading.
        /// </summary>
        protected internal virtual void Unload()
        {
            foreach (Framebuffer framebuffer in _framebuffers)
            {
                framebuffer.Dispose();
            }
        }
    }

    /// <summary>
    /// Represents a render pipeline.
    /// </summary>
    /// <typeparam name="TScene">The scene type</typeparam>
    public abstract class RenderPipeline<TScene> : RenderPipeline
        where TScene : GenericScene
    {

        /// <summary>
        /// The system to render stuff.
        /// </summary>
        protected internal virtual void Render(ref DrawContext context, TScene scene)
        {
            context.ActivePipeline = this;
        }
    }
}