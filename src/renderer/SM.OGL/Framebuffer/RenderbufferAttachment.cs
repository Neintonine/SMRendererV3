using OpenTK.Graphics.OpenGL4;

namespace SM.OGL.Framebuffer
{
    /// <summary>
    /// Describes a renderbuffer attachment. 
    /// </summary>
    public struct RenderbufferAttachment
    {
        /// <summary>
        /// Preset for the depthbuffer attachment.
        /// </summary>
        public static readonly RenderbufferAttachment Depth = new RenderbufferAttachment(RenderbufferStorage.Depth24Stencil8, FramebufferAttachment.DepthStencilAttachment);
        
        /// <summary>
        /// Storage describes the internal format for the renderbuffer.
        /// </summary>
        public RenderbufferStorage Storage;
        /// <summary>
        /// FramebufferAttachment describes the attachment for the framebuffer.
        /// </summary>
        public FramebufferAttachment FramebufferAttachment;

        /// <summary>
        /// This contains the amount of multisampling for the attachment.
        /// </summary>
        public int Multisample;

        /// <summary>
        /// Constructor
        /// </summary>
        public RenderbufferAttachment(RenderbufferStorage storage, FramebufferAttachment framebufferAttachment, int multisample = 0)
        {
            Storage = storage;
            FramebufferAttachment = framebufferAttachment;
            Multisample = multisample;
        }

        /// <summary>
        /// This generates the renderbuffer for the framebuffer to add.
        /// </summary>
        /// <param name="f">The framebuffer</param>
        /// <returns>The ID of the renderbuffer.</returns>
        public int Generate(Framebuffer f)
        {
            int rb = GL.GenRenderbuffer();
            
            GL.BindRenderbuffer(RenderbufferTarget.Renderbuffer, rb);
            if (Multisample != 0)
                GL.RenderbufferStorageMultisample(RenderbufferTarget.Renderbuffer, Multisample, Storage, (int)f.Size.X, (int)f.Size.Y);
            else
                GL.RenderbufferStorage(RenderbufferTarget.Renderbuffer, Storage, (int)f.Size.X, (int)f.Size.Y);
            GL.BindRenderbuffer(RenderbufferTarget.Renderbuffer, 0);

            return rb;
        }
    }
}