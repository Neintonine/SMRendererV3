using System.IO;
using System.Reflection;
using OpenTK.Graphics.OpenGL4;
using SM.OGL.Shaders;

namespace SM.Base.Shader
{
    public class Shaders
    {
        public static InstanceShader Default = new InstanceShader(new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream("SM.Base.Shader.Files.default.vert")).ReadToEnd(),
            new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream("SM.Base.Shader.Files.default.frag")).ReadToEnd())
        {

            SetUniformFragment = (u, context) =>
            {
                u["Tint"].SetUniform4(context.Material.Tint);
                u["Texture"].SetTexture(context.Material.Texture, 0, u["UseTexture"]);
            },
            SetUniformVertex = (u, context, i) =>
            {
                GL.UniformMatrix4(u["ModelMatrix"] + i, false, ref context.Instances[i].ModelMatrix);
                GL.Uniform2(u["TextureOffset"] + i, context.Instances[i].TexturePosition);
                GL.Uniform2(u["TextureScale"] + i, context.Instances[i].TextureScale);
            }
        };
    }
}