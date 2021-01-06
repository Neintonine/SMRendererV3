using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using SM.Base.Contexts;
using SM.Base.Objects.Static;
using SM.Base.Scene;
using SM.OGL.Framebuffer;
using SM.OGL.Shaders;

namespace SM.Base.PostProcess
{
    /// <summary>
    /// Basis for a post process effect
    /// </summary>
    public abstract class PostProcessEffect
    {
        internal static Matrix4 Mvp;
        internal static Matrix4 Model;

        protected RenderPipeline Pipeline;

        /// <summary>
        /// Initialize the effect.
        /// </summary>
        /// <param name="pipeline"></param>
        public void Initilize(RenderPipeline pipeline)
        {
            Pipeline = pipeline;
            InitProcess();
        }
        
        /// <summary>
        /// Method, to initialize the shader.
        /// </summary>
        protected virtual void InitProcess() {}

        
        /// <summary>
        /// Method to draw the actual effect.
        /// </summary>
        public abstract void Draw(DrawContext context);

        /// <summary>
        /// Event, when the scene changed.
        /// </summary>
        public virtual void SceneChanged(GenericScene scene)
        {

        }
    }
}