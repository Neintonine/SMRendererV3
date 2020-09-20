using OpenTK.Graphics;
using SM.Base.Shader;
using SM.OGL.Texture;

namespace SM.Base.Scene
{
    public class Material
    {
        public TextureBase Texture;
        public Color4 Tint = Color4.White;

        public IShader Shader = Shaders.Default;
    }
}