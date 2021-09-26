#region usings

using OpenTK;
using OpenTK.Graphics.OpenGL4;
using System;

#endregion

namespace SM.OGL.Texture
{
    /// <summary>
    ///     Works as a basis for textures.
    /// </summary>
    public abstract class TextureBase : GLObject

    {
        /// <inheritdoc />
        protected override bool AutoCompile { get; set; } = true;
        

        /// <inheritdoc />
        public override ObjectLabelIdentifier TypeIdentifier { get; } = ObjectLabelIdentifier.Texture;

        /// <summary>
        /// Contains the specific information of each pixel.
        /// </summary>
        public PixelInformation PixelInformation;

        /// <summary>
        /// The target of the texture.
        /// </summary>
        public TextureTarget Target { get; set; } = TextureTarget.Texture2D;

        /// <summary>
        ///     The texture filter.
        ///     <para>Default: <see cref="TextureMinFilter.Linear" /></para>
        /// </summary>
        public virtual TextureMinFilter Filter { get; set; } = TextureMinFilter.Linear;

        /// <summary>
        ///     The wrap mode.
        ///     <para>Default: <see cref="TextureWrapMode.Repeat" /></para>
        /// </summary>
        public virtual TextureWrapMode WrapMode { get; set; } = TextureWrapMode.Repeat;

        /// <summary>
        ///     The Width of the texture
        /// </summary>
        public virtual int Width { get; protected set; }

        /// <summary>
        ///     The height of the texture
        /// </summary>
        public virtual int Height { get; protected set; }

        public Vector2 Size => new Vector2(Width, Height);
        public Vector2 TexelSize => new Vector2(1f / Width, 1f / Height);

        /// <inheritdoc />
        public override void Dispose()
        {
            GL.DeleteTexture(_id);
            base.Dispose();
        }

        protected void GenerateBaseTexture(Vector2 size)
        {
            Width = (int)size.X;
            Height = (int)size.Y;

            GL.BindTexture(TextureTarget.Texture2D, _id);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInformation.InternalFormat, 
                Width, Height, 0,
                PixelInformation.Format, PixelInformation.DataType, IntPtr.Zero);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)Filter);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)Filter);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS,
                (int)WrapMode);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT,
                (int)WrapMode);

            GL.BindTexture(TextureTarget.Texture2D, 0);
        }
    }
}