using OpenTK.Graphics.OpenGL4;

namespace SM.OGL.Framebuffer
{
    /// <summary>
    /// Describes a renderbuffer attachment. 
    /// </summary>
    public class RenderbufferAttachment
    {
        /// <summary>
        /// Preset for the depthbuffer attachment.
        /// </summary>
        public static RenderbufferAttachment GenerateDepth() => new RenderbufferAttachment(RenderbufferStorage.Depth24Stencil8, FramebufferAttachment.DepthStencilAttachment);
        
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
        /// The id that was given to the renderbuffer.
        /// </summary>
        public int ID;

        /// <summary>
        /// Constructor
        /// </summary>
        public RenderbufferAttachment(RenderbufferStorage storage, FramebufferAttachment framebufferAttachment, int multisample = 0)
        {
            Storage = storage;
            FramebufferAttachment = framebufferAttachment;
            Multisample = multisample;

            ID = -1;
        }

        /// <summary>
        /// This generates the renderbuffer for the framebuffer to add.
        /// </summary>
        /// <param name="f">The framebuffer</param>
        /// <returns>The ID of the renderbuffer.</returns>
        public void Generate(Framebuffer f)
        {
            ID = GL.GenRenderbuffer();
            
            GL.BindRenderbuffer(RenderbufferTarget.Renderbuffer, ID);
            if (Multisample != 0)
                GL.RenderbufferStorageMultisample(RenderbufferTarget.Renderbuffer, Multisample, Storage, (int)f.Size.X, (int)f.Size.Y);
            else
                GL.RenderbufferStorage(RenderbufferTarget.Renderbuffer, Storage, (int)f.Size.X, (int)f.Size.Y);
            GL.BindRenderbuffer(RenderbufferTarget.Renderbuffer, 0);
        }
        /// <summary>
        /// Disposes the renderbuffer.
        /// </summary>
        public void Dispose()
        {

            GL.DeleteRenderbuffer(ID);
            ID = -1;
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (obj is RenderbufferAttachment ra)
            {
                if (ra.FramebufferAttachment == FramebufferAttachment) return true;

                return false;
            }

            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            int hashCode = -1803239493;
            hashCode = hashCode * -1521134295 + Storage.GetHashCode();
            hashCode = hashCode * -1521134295 + FramebufferAttachment.GetHashCode();
            hashCode = hashCode * -1521134295 + Multisample.GetHashCode();
            hashCode = hashCode * -1521134295 + ID.GetHashCode();
            return hashCode;
        }
    }
}