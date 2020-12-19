using SM.Base.Contexts;
using SM.Base.Drawing;
using SM.OGL.Shaders;
using SM.Utility;

namespace SM2D.Shader
{
    public class Basic2DShader : MaterialShader
    {

        public static Basic2DShader Shader = new Basic2DShader();
        private Basic2DShader() : base(AssemblyUtility.ReadAssemblyFile("SM2D.Shader.ShaderFiles.basic.glsl"))
        {
        }

        protected override void DrawProcess(DrawContext context)
        {
            // Vertex Uniforms
            Uniforms["MVP"].SetMatrix4(context.ModelMaster * context.View * context.World);
            Uniforms["HasVColor"]
                .SetUniform1(context.Mesh.Attributes["color"] != null);

            UniformArray instances = Uniforms.GetArray("Instances");
            for (int i = 0; i < context.Instances.Count; i++)
            {
                var shaderInstance = instances[i];
                var instance = context.Instances[i];
                shaderInstance["ModelMatrix"].SetMatrix4(instance.ModelMatrix);
                shaderInstance["TextureOffset"].SetUniform2(instance.TexturePosition);
                shaderInstance["TextureScale"].SetUniform2(instance.TextureScale);
            }

            // Fragment Uniforms
            Uniforms["Tint"].SetUniform4(context.Material.Tint);
            Uniforms["Texture"].SetTexture(context.Material.Texture, Uniforms["UseTexture"]);

            DrawObject(context.Mesh, context.Instances.Count);
        }
    }
}