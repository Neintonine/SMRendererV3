#region usings

using OpenTK.Graphics.OpenGL4;

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

        /// <inheritdoc />
        public override void Dispose()
        {
            GL.DeleteTexture(_id);
            base.Dispose();
        }
    }
}