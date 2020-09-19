using OpenTK.Graphics.OpenGL4;

namespace SM.OGL.Texture
{
    public class TextureBase : GLObject
    {
        protected override bool AutoCompile { get; } = true;
        public override ObjectLabelIdentifier TypeIdentifier { get; } = ObjectLabelIdentifier.Texture;

        public virtual TextureMinFilter Filter { get; set; }
        public virtual TextureWrapMode WrapMode { get; set; }
    }
}