using System.Collections.Generic;
using SM.OGL.Framebuffer;

namespace SM.Base.PostProcessing
{
    public abstract class PostProcessingEffect
    {
        public virtual Dictionary<string, int> AdditionalFramebufferOutputs { get; }
        public virtual ICollection<Framebuffer> AdditionalFramebuffers { get; }

        public void ApplyOutputs(Framebuffer mainbuffer)
        {
            mainbuffer.Append();
        }
    }
}