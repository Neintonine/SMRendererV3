#region usings

using System.Collections.Generic;
using System.IO;
using System.Reflection;

#endregion

namespace SM.OGL.Shaders
{
    public class ShaderExtensions
    {
        public static Dictionary<string, ShaderFile> Extensions { get; private set; } =
            new Dictionary<string, ShaderFile>();

        public static void AddAssemblyExtensions(string prefix, string path)
        {
            AddAssemblyExtensions(prefix, Assembly.GetCallingAssembly(), path);
        }

        public static void AddAssemblyExtensions(string prefix, Assembly assembly, string path)
        {
            var paths = assembly.GetManifestResourceNames();
            for (var i = 0; i < paths.Length; i++)
            {
                var filePath = paths[i];
                if (!filePath.StartsWith(path)) continue;

                using (var reader = new StreamReader(assembly.GetManifestResourceStream(filePath)))
                {
                    var name =
                        $"{prefix}{Path.GetFileNameWithoutExtension(filePath.Substring(path.Length)).Replace('.', '_')}";
                    Extensions.Add(name, new ShaderFile(reader.ReadToEnd()));
                }
            }
        }
    }
}