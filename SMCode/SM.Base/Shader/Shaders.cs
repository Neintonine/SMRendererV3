using System.IO;
using System.Reflection;
using SM.OGL.Shaders;

namespace SM.Base.Shader
{
    public class Shaders
    {
        public static InstanceShader Default = new InstanceShader(new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream("SM.Base.Shader.Files.default.vert")).ReadToEnd(),
            new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream("SM.Base.Shader.Files.default.frag")).ReadToEnd(),
            ((u, context) =>
            {
                u["MVP"].SetMatrix4(context.View * context.World);
                u["ModelMatrix"].SetMatrix4(context.ModelMatrix);
                u["Tint"].SetUniform4(context.Material.Tint);
                u["Texture"].SetTexture(context.Material.Texture, 0, u["UseTexture"]);
            }));
    }
}