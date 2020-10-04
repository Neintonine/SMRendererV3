using OpenTK.Graphics;
using SM.OGL.Texture;

namespace SM.Base.Scene
{
    /// <summary>
    /// Represents a material.
    /// </summary>
    public class Material
    {
        /// <summary>
        /// The base texture. (aka. Diffuse Texture)
        /// </summary>
        public TextureBase Texture;
        /// <summary>
        /// The tint or color.
        /// </summary>
        public Color4 Tint = Color4.White;

        /// <summary>
        /// A custom shader, that is used to draw this material.
        /// </summary>
        public IShader CustomShader;
    }
}