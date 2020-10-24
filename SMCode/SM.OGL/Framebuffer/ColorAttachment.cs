#region usings

using System;
using OpenTK.Graphics.OpenGL4;
using SM.OGL.Texture;

#endregion

namespace SM.OGL.Framebuffer
{
    public class ColorAttachment : TextureBase
    {
        public ColorAttachment(int attachmentId)
        {
            AttachmentID = attachmentId;
        }

        public int AttachmentID { get; }

        public FramebufferAttachment FramebufferAttachment => FramebufferAttachment.ColorAttachment0 + AttachmentID;
        public DrawBufferMode DrawBufferMode => DrawBufferMode.ColorAttachment0 + AttachmentID;
        public ReadBufferMode ReadBufferMode => ReadBufferMode.ColorAttachment0 + AttachmentID;
        public DrawBuffersEnum DrawBuffersEnum => DrawBuffersEnum.ColorAttachment0 + AttachmentID;

        public void Generate(Framebuffer f)
        {
            _id = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, _id);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba8,
                (int) f.Size.X, (int) f.Size.Y,
                0, PixelFormat.Bgra, PixelType.UnsignedByte, IntPtr.Zero);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter,
                (int) TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter,
                (int) TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS,
                (int) TextureParameterName.ClampToEdge);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT,
                (int) TextureParameterName.ClampToEdge);
        }
    }
}