using OpenTK.Graphics.OpenGL4;

namespace SM.OGL.Texture
{
    public abstract class TextureBase : GLObject
    {
        protected override bool AutoCompile { get; } = true;
        public override ObjectLabelIdentifier TypeIdentifier { get; } = ObjectLabelIdentifier.Texture;

        public abstract TextureMinFilter Filter { get; set; }
        public abstract TextureWrapMode WrapMode { get; set; }
    }
}