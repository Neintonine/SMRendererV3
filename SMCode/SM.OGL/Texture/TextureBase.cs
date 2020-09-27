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
        /// </summary>
        public abstract TextureMinFilter Filter { get; set; }
        /// <summary>
        /// The wrap mode.
        /// </summary>
        public abstract TextureWrapMode WrapMode { get; set; }
    }
}