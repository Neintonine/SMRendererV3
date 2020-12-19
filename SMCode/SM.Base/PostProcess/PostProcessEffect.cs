using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
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

        
        /// <summary>
        /// Method, to initialize the shader.
        /// </summary>
        public virtual void Init() {}


        /// <summary>
        /// Method, to initialize the shader.
        /// </summary>
        public virtual void Init(Framebuffer main)
        {
            Init();
        }

        /// <summary>
        /// Method to draw the actual effect.
        /// </summary>
        /// <param name="main">The framebuffer, that was used.</param>
        /// <param name="target">The framebuffer, the system should draw to.</param>
        public abstract void Draw(Framebuffer main, Framebuffer target);

        /// <summary>
        /// Event, when the scene changed.
        /// </summary>
        public virtual void SceneChanged(GenericScene scene)
        {

        }
    }
}