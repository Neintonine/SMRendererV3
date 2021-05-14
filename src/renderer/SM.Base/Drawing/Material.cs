#region usings

using OpenTK.Graphics;
using SM.Base.Shaders;
using SM.Base.Window;
using SM.OGL.Texture;

#endregion

namespace SM.Base.Drawing
{
    /// <summary>
    ///     Represents a material.
    /// </summary>
    public class Material
    {
        /// <summary>
        ///     A setting to enable Blending.
        /// </summary>
        public virtual bool Blending { get; set; } = false;

        /// <summary>
        ///     A custom shader, that is used to draw this material.
        /// </summary>
        public virtual MaterialShader CustomShader { get; set; }

        /// <summary>
        ///     The base texture. (aka. Diffuse Texture)
        /// </summary>
        public virtual TextureBase Texture { get; set; }

        /// <summary>
        ///     The tint or color.
        /// </summary>
        public virtual Color4 Tint { get; set; } = Color4.White;

        /// <summary>
        /// This allows custom shaders to use own shader arguments.
        /// </summary>
        public ShaderArguments ShaderArguments { get; internal set; } = new ShaderArguments();

        public virtual void Draw(DrawContext context)
        {
            context.Shader.Draw(context);
        }
    }
}