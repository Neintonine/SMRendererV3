#region usings

using OpenTK.Graphics;
using SM.Base.Shaders;
using SM.OGL.Texture;

#endregion

namespace SM.Base.Drawing
{
    /// <summary>
    ///     Represents a material.
    /// </summary>
    public class Material
    {
        public bool Blending = false;

        /// <summary>
        ///     A custom shader, that is used to draw this material.
        /// </summary>
        public MaterialShader CustomShader;

        /// <summary>
        ///     The base texture. (aka. Diffuse Texture)
        /// </summary>
        public TextureBase Texture;

        /// <summary>
        ///     The tint or color.
        /// </summary>
        public Color4 Tint = Color4.White;

        /// <summary>
        /// This allows custom shaders to use own shader arguments.
        /// </summary>
        public ShaderArguments ShaderArguments { get; internal set; } = new ShaderArguments();
    }
}