#region usings

using System;
using OpenTK;
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
        private readonly int _multisamples;
        
        /// <summary>
        /// The ID the attachment was given.
        /// </summary>
        public int AttachmentID { get; }

        /// <summary>
        /// Contains the framebuffer its connected.
        /// <para>Usually the last framebuffer, that called the Compile-method.</para>
        /// </summary>
        public Framebuffer ConnectedFramebuffer { get; private set; }

        /// <summary>
        /// Can contains the size this attachment want to be.
        /// <para>If set, it will ignore the size from the framebuffer.</para>
        /// </summary>
        public Vector2? AttachmentSize = null;

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

        /// <summary>
        /// Returns true, if multisamples are above 0. 
        /// </summary>
        public bool IsMultisampled => _multisamples > 0;

        /// <summary>
        /// Creates a attachment with a specific id.
        /// </summary>
        /// <param name="attachmentId"></param>
        /// <param name="size"></param>
        public ColorAttachment(int attachmentId, Vector2? size = null) : this(attachmentId, PixelInformation.RGBA_LDR, size)
        { }

        /// <summary>
        /// Creates a color attachment with a specific id, specific pixel informations and multisamples.
        /// </summary>
        /// <param name="attachmentID"></param>
        /// <param name="pixelInformation"></param>
        /// <param name="size"></param>
        /// <param name="multisamples"></param>
        public ColorAttachment(int attachmentID, PixelInformation pixelInformation, Vector2? size = null, int multisamples = 0)
        {
            AttachmentID = attachmentID;
            PixelInformation = pixelInformation;
            AttachmentSize = size;

            _multisamples = multisamples;
            Target = IsMultisampled ? TextureTarget.Texture2DMultisample : TextureTarget.Texture2D;

            WrapMode = TextureWrapMode.ClampToEdge;
        }
        /// <summary>
        /// Generates the attachment.
        /// </summary>
        /// <param name="f"></param>
        public void Generate(Framebuffer f)
        {
            _id = GL.GenTexture();
            ConnectedFramebuffer = f;

            if (IsMultisampled) GenerateMultisampledTexture(f);
            else GenerateTexture(f);
        }

        private void GenerateTexture(Framebuffer f)
        {
            Vector2 size = AttachmentSize.GetValueOrDefault(f.Size);

            GenerateBaseTexture(size);
        }

        private void GenerateMultisampledTexture(Framebuffer f)
        {
            Vector2 size = AttachmentSize.GetValueOrDefault(f.Size);

            Width = (int)size.X;
            Height = (int)size.Y;

            const TextureTarget target = TextureTarget.Texture2DMultisample;

            GL.BindTexture(target, _id);
            GL.TexImage2DMultisample((TextureTargetMultisample)target, _multisamples, PixelInformation.InternalFormat,
                Width, Height, true);
            /*
            GL.TexParameter(target, TextureParameterName.TextureMinFilter, (int)Filter);
            GL.TexParameter(target, TextureParameterName.TextureMagFilter, (int)Filter);

            GL.TexParameter(target, TextureParameterName.TextureWrapS,
                (int)WrapMode);
            GL.TexParameter(target, TextureParameterName.TextureWrapT,
                (int)WrapMode);*/

            GL.BindTexture(target, 0);
        }
    }
}