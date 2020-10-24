using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using SM.Base.Objects.Static;
using SM.OGL.Framebuffer;
using SM.OGL.Shaders;

namespace SM.Base.PostProcess
{
    public abstract class PostProcessEffect
    {
        internal static Matrix4 Mvp;

        public virtual ICollection<Framebuffer> RequiredFramebuffers { get; }
        
        public virtual void Init() {}

        public virtual void Init(Framebuffer main)
        {
            Init();
        }

        public virtual void Draw(Framebuffer main)
        {

        }
    }
}