#region usings

using System;
using System.Collections.Generic;

#endregion

namespace SM.OGL.Shaders
{
    public class ShaderPreProcess
    {
        public static Dictionary<string, Action<ShaderFile, string>> Actions =
            new Dictionary<string, Action<ShaderFile, string>>
            {
                {"import", Import}
            };

        private static void Import(ShaderFile file, string param)
        {
            foreach (var extension in param.Split(' '))
                file.GLSLExtensions.Add(ShaderExtensions.Extensions[extension]);
        }
    }
}