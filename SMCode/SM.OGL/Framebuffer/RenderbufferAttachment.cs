using OpenTK.Graphics.OpenGL4;

namespace SM.OGL.Framebuffer
{
    public struct RenderbufferAttachment
    {
        public static readonly RenderbufferAttachment Depth = new RenderbufferAttachment(RenderbufferStorage.Depth24Stencil8, FramebufferAttachment.DepthStencilAttachment);

        public RenderbufferStorage Storage;
        public FramebufferAttachment FramebufferAttachment;

        public int Multisample;

        public RenderbufferAttachment(RenderbufferStorage storage, FramebufferAttachment framebufferAttachment, int multisample = 0)
        {
            Storage = storage;
            FramebufferAttachment = framebufferAttachment;
            Multisample = multisample;
        }

        public int Generate(Framebuffer f)
        {
            int rbo = GL.GenRenderbuffer();
            
            GL.BindRenderbuffer(RenderbufferTarget.Renderbuffer, rbo);
            if (Multisample != 0)
                GL.RenderbufferStorageMultisample(RenderbufferTarget.Renderbuffer, Multisample, Storage, (int)f.Size.X, (int)f.Size.Y);
            else
                GL.RenderbufferStorage(RenderbufferTarget.Renderbuffer, Storage, (int)f.Size.X, (int)f.Size.Y);
            GL.BindRenderbuffer(RenderbufferTarget.Renderbuffer, 0);

            return rbo;
        }
    }
}