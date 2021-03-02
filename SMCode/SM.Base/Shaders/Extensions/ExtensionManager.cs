#region usings

using SM.OGL.Shaders;

#endregion

namespace SM.Base.ShaderExtension
{
    internal class ExtensionManager
    {
        internal static void InitExtensions()
        {
            ShaderExtensions.AddAssemblyExtensions("SM_base", "SM.Base.Shaders.Extensions");

            ShaderExtensions.Extensions["SM_base_vertex_basic"].StringOverrides["instanceMax"] =
                SMRenderer.MaxInstances.ToString();
        }
    }
}