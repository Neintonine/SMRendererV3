#region usings

using OpenTK;
using SM.Base.Scene;
using SM.Base.Window;
using SM.OGL.Framebuffer;

#endregion

namespace SM.Base.PostProcess
{
    /// <summary>
    ///     Basis for a post process effect
    /// </summary>
    public abstract class PostProcessEffect
    {
        /// <summary>
        /// This contains the transformation matrix for post process effects.
        /// </summary>
        public static Matrix4 Mvp = Matrix4.Identity;

        /// <summary>
        /// This contains the pipeline this instance is currently active.
        /// </summary>
        protected RenderPipeline Pipeline;

        /// <summary>
        /// Enables the effect.
        /// <para>Default: true</para>
        /// </summary>
        public bool Enable = true;

        /// <summary>
        ///     Initialize the effect.
        /// </summary>
        /// <param name="pipeline"></param>
        public void Initilize(RenderPipeline pipeline)
        {
            Pipeline = pipeline;
            InitProcess();
        }

        /// <summary>
        ///     Method, to initialize the shader.
        /// </summary>
        protected virtual void InitProcess()
        {
        }

        /// <summary>
        /// This executes 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="context"></param>
        public void Draw(ColorAttachment source, DrawContext context)
        {
            if (Enable) Drawing(source, context);
        }

        /// <summary>
        ///     Method to draw the actual effect.
        /// </summary>
        protected abstract void Drawing(ColorAttachment source, DrawContext context);

        /// <summary>
        ///     Event, when the scene changed.
        /// </summary>
        public virtual void SceneChanged(GenericScene scene)
        {
        }

        /// <summary>
        ///     Event, when the screen size changed.
        /// </summary>
        /// <param name="window">Window that changed</param>
        public virtual void ScreenSizeChanged(IGenericWindow window)
        {
            
        }
    }
}