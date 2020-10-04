using OpenTK.Graphics.OpenGL4;

namespace SM.OGL.Texture
{
    /// <summary>
    /// Works as a basis for textures.
    /// </summary>
    public abstract class TextureBase : GLObject
    {
        /// <inheritdoc />
        protected override bool AutoCompile { get; } = true;

        /// <inheritdoc />
        public override ObjectLabelIdentifier TypeIdentifier { get; } = ObjectLabelIdentifier.Texture;

        /// <summary>
        /// The texture filter.
        /// <para>Default: <see cref="TextureMinFilter.Linear"/></para>
        /// </summary>
        public virtual TextureMinFilter Filter { get; set; } = TextureMinFilter.Linear;

        /// <summary>
        /// The wrap mode.
        /// <para>Default: <see cref="TextureWrapMode.Repeat"/></para>
        /// </summary>
        public virtual TextureWrapMode WrapMode { get; set; } = TextureWrapMode.Repeat;

        /// <summary>
        /// The Width of the texture
        /// </summary>
        public int Width { get; protected set; }
        /// <summary>
        /// The height of the texture
        /// </summary>
        public int Height { get; protected set; }

        public override void Dispose()
        {
            base.Dispose();
            GL.DeleteTexture(_id);
        }
    }
}