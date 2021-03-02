#region usings

using System;
using OpenTK.Graphics.OpenGL4;
using SM.OGL.Texture;

#endregion

namespace SM.OGL.Framebuffer
{
    /// <summary>
    /// Represents a Framebuffer-Color Attachment.
    /// <para>Can be use like a texture.</para>
    /// </summary>
    public class ColorAttachment : TextureBase
    {
        private int _multisamples;
        
        /// <summary>
        /// The ID the attachment was given.
        /// </summary>
        public int AttachmentID { get; }

        /// <summary>
        /// Returns the <see cref="OpenTK.Graphics.OpenGL4.FramebufferAttachment"/> of this ColorAttachment.
        /// </summary>
        public FramebufferAttachment FramebufferAttachment => FramebufferAttachment.ColorAttachment0 + AttachmentID;
        /// <summary>
        /// Returns the <see cref="OpenTK.Graphics.OpenGL4.DrawBufferMode"/> of this ColorAttachment.
        /// </summary>
        public DrawBufferMode DrawBufferMode => DrawBufferMode.ColorAttachment0 + AttachmentID;
        /// <summary>
        /// Returns the <see cref="OpenTK.Graphics.OpenGL4.ReadBufferMode"/> of this ColorAttachment.
        /// </summary>
        public ReadBufferMode ReadBufferMode => ReadBufferMode.ColorAttachment0 + AttachmentID;
        /// <summary>
        /// Returns the <see cref="OpenTK.Graphics.OpenGL4.DrawBuffersEnum"/> of this ColorAttachment.
        /// </summary>
        public DrawBuffersEnum DrawBuffersEnum => DrawBuffersEnum.ColorAttachment0 + AttachmentID;

        public bool IsMultisampled => _multisamples > 0;

        /// <summary>
        /// Creates a attachment with a specific id.
        /// </summary>
        /// <param name="attachmentId"></param>
        public ColorAttachment(int attachmentId) : this(attachmentId, PixelInformation.RGBA_LDR)
        { }

        public ColorAttachment(int attachmentID, PixelInformation pixelInformation, int multisamples = 0)
        {
            AttachmentID = attachmentID;
            PixelInformation = pixelInformation;
            _multisamples = multisamples;
            Target = IsMultisampled ? TextureTarget.Texture2DMultisample : TextureTarget.Texture2D;
        }
        /// <summary>
        /// Generates the attachment.
        /// </summary>
        /// <param name="f"></param>
        public void Generate(Framebuffer f)
        {
            _id = GL.GenTexture();

            if (IsMultisampled) GenerateMultisampledTexture(f);
            else GenerateTexture(f);
        }

        private void GenerateTexture(Framebuffer f)
        {
            GL.BindTexture(TextureTarget.Texture2D, _id);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInformation.InternalFormat,
                (int)f.Size.X, (int)f.Size.Y,
                0, PixelInformation.Format, PixelInformation.DataType, IntPtr.Zero);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter,
                (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter,

                (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS,
                (int)TextureParameterName.ClampToEdge);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT,
                (int)TextureParameterName.ClampToEdge);

            GL.BindTexture(TextureTarget.Texture2D, 0);
        }

        private void GenerateMultisampledTexture(Framebuffer f)
        {
            GL.BindTexture(TextureTarget.Texture2DMultisample, _id);
            GL.TexImage2DMultisample(TextureTargetMultisample.Texture2DMultisample, _multisamples, PixelInformation.InternalFormat, (int)f.Size.X, (int)f.Size.Y, true);
            GL.BindTexture(TextureTarget.Texture2DMultisample, 0);
        }
    }
}