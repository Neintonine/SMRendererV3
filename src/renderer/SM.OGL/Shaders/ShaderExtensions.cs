#region usings

using System.Collections.Generic;
using System.IO;
using System.Reflection;

#endregion

namespace SM.OGL.Shaders
{
    /// <summary>
    /// Holder for shader extensions
    /// </summary>
    public class ShaderExtensions
    {
        /// <summary>
        /// Holds the extensions.
        /// </summary>
        public static Dictionary<string, ShaderFile> Extensions { get; private set; } =
            new Dictionary<string, ShaderFile>();

        /// <summary>
        /// Adds extensions from the calling assembly
        /// </summary>
        /// <param name="prefix">Prefix for the added extensions.</param>
        /// <param name="path">Path, where the extensions are located.</param>
        public static void AddAssemblyExtensions(string prefix, string path)
        {
            AddAssemblyExtensions(prefix, Assembly.GetCallingAssembly(), path);
        }

        /// <summary>
        /// Adds extensions from the specific assembly
        /// </summary>
        /// <param name="prefix">Prefix for the added extensions.</param>
        /// <param name="assembly">The specific assembly</param>
        /// <param name="path">Path, where the extensions are located.</param>
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

                    if (Extensions.ContainsKey(name)) continue;

                    Extensions.Add(name, new ShaderFile(reader.ReadToEnd()));
                }
            }
        }
    }
}