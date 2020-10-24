#region usings

using OpenTK.Graphics.OpenGL4;
using SM.Base.Contexts;
using SM.Base.Scene;
using SM.OGL.Shaders;
using SM.Utility;

#endregion

namespace SM2D.Shader
{
    public class Default2DShader : MaterialShader
    {
        public static Default2DShader MaterialShader = new Default2DShader();

        //protected override bool AutoCompile { get; } = true;

        private Default2DShader() : base(new ShaderFileCollection(
            AssemblyUtility.ReadAssemblyFile("SM2D.Shader.ShaderFiles.default.vert"),
            AssemblyUtility.ReadAssemblyFile("SM2D.Shader.ShaderFiles.default.frag")))
        {
            Load();
        }

        protected override void DrawProcess(DrawContext context)
        {
            // Vertex Uniforms
            Uniforms["MVP"].SetMatrix4(context.ModelMaster * context.View * context.World);
            Uniforms["HasVColor"]
                .SetUniform1(context.Mesh.AttribDataIndex.ContainsKey(3) && context.Mesh.AttribDataIndex[3] != null);

            Uniforms.GetArray("Instances").Set((i, uniforms) =>
            {
                if (i >= context.Instances.Length) return false;

                var instance = context.Instances[i];
                uniforms["ModelMatrix"].SetMatrix4(instance.ModelMatrix);
                uniforms["TextureOffset"].SetUniform2(instance.TexturePosition);
                uniforms["TextureScale"].SetUniform2(instance.TextureScale);

                return true;
            });

            // Fragment Uniforms
            Uniforms["Tint"].SetUniform4(context.Material.Tint);
            Uniforms["Texture"].SetTexture(context.Material.Texture, Uniforms["UseTexture"]);

            DrawObject(context.Mesh, context.Instances.Length);
        }
    }
}