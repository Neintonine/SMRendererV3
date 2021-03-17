using SM.Base.Shaders;
using SM.Base.Utility;
using SM.Base.Window;
using SM.OGL.Shaders;

namespace SM2D.Shader
{
    public class ShaderCollection
    {
        public static SimpleShader Basic = new SimpleShader("basic", AssemblyUtility.ReadAssemblyFile("SM2D.Shader.ShaderFiles.basic.glsl"), SetUniforms);
        public static SimpleShader Instanced = new SimpleShader("instanced", AssemblyUtility.ReadAssemblyFile("SM2D.Shader.ShaderFiles.basic.glsl"), SetUniforms);

        static void SetUniforms(UniformCollection uniforms, DrawContext context)
        {
            uniforms["Tint"].SetUniform4(context.Material.Tint);
            uniforms["Texture"].SetTexture(context.Material.Texture, uniforms["UseTexture"]);
        }
    }
}