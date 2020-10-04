using OpenTK.Graphics.OpenGL4;
using SM.Base.Contexts;
using SM.Base.Scene;
using SM.OGL.Shaders;
using SM.Utility;

namespace SM2D.Shader
{
    public class Default2DShader : GenericShader, IShader
    {
        public static Default2DShader Shader = new Default2DShader();

        //protected override bool AutoCompile { get; } = true;

        private Default2DShader() : base(new ShaderFileCollection(
            AssemblyUtility.ReadAssemblyFile("SM2D.Shader.ShaderFiles.default.vert"),
            AssemblyUtility.ReadAssemblyFile("SM2D.Shader.ShaderFiles.default.frag")))
        {
            Load();
        }
        public void Draw(DrawContext context)
        {
            GL.UseProgram(this);

            GL.BindVertexArray(context.Mesh);

            // Vertex Uniforms
            Uniforms["MVP"].SetMatrix4(context.View * context.World);
            Uniforms["HasVColor"].SetUniform1(context.Mesh.AttribDataIndex.ContainsKey(3) && context.Mesh.AttribDataIndex[3] != null);

            for (int i = 0; i < context.Instances.Length; i++)
            {
                GL.UniformMatrix4(Uniforms["ModelMatrix"] + i, false, ref context.Instances[i].ModelMatrix);
                GL.Uniform2(Uniforms["TextureOffset"] + i, context.Instances[i].TexturePosition);
                GL.Uniform2(Uniforms["TextureScale"] + i, context.Instances[i].TextureScale);
            }

            // Fragment Uniforms
            Uniforms["Tint"].SetUniform4(context.Material.Tint);
            Uniforms["Texture"].SetTexture(context.Material.Texture, Uniforms["UseTexture"]);

            DrawObject(context.Mesh, context.Instances.Length);

            CleanUp();

            GL.UseProgram(0);
        }
    }
}