using SM.Base.Shaders;
using SM.Base.Utility;
using SM.Base.Window;
using SM.OGL.Shaders;

namespace SM2D.Shader
{
    class ShaderCollection
    {
        /// <summary>
        /// The most basic shader, that renders only one thing and only allows colors and one texture.
        /// </summary>
        public static SimpleShader Basic = new SimpleShader("basic", AssemblyUtility.ReadAssemblyFile("SM2D.Shader.ShaderFiles.basic.glsl"), SetUniforms);
        /// <summary>
        /// The same fragment shader as <see cref="Basic"/>, but allows to be instanced and used in (f.E.) text.
        /// </summary>
        public static SimpleShader Instanced = new SimpleShader("instanced", AssemblyUtility.ReadAssemblyFile("SM2D.Shader.ShaderFiles.basic.glsl"), SetUniforms);

        static void SetUniforms(UniformCollection uniforms, DrawContext context)
        {
            uniforms["Tint"].SetUniform4(context.Material.Tint);
            uniforms["Texture"].SetTexture(context.Material.Texture, uniforms["UseTexture"]);
        }
    }
}