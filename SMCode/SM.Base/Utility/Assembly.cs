using System;
using System.IO;
using System.Reflection;

namespace SM.Utility
{
    public class AssemblyUtility
    {
        /// <summary>
        /// Read a file that is saved in a assembly
        /// </summary>
        /// <param name="ass">The assembly that contains the file</param>
        /// <param name="path">The path to the file inside the assembly</param>
        /// <returns></returns>
        public static string ReadAssemblyFile(Assembly ass, string path) { return new StreamReader(GetAssemblyStream(ass, path)).ReadToEnd(); }

        /// <summary>
        /// Read a file that is saved in the calling assembly
        /// </summary>
        /// <param name="path">The path to the file inside the assembly</param>
        /// <returns></returns>
        public static string ReadAssemblyFile(string path) { return ReadAssemblyFile(Assembly.GetCallingAssembly(), path); }


        public static Stream GetAssemblyStream(Assembly ass, string path) { return ass.GetManifestResourceStream(ass.GetName().Name + "." + path) ?? throw new InvalidOperationException("Assembly couldn't find resource at path '" + path + "'."); }

        public static Stream GetAssemblyStream(string path) { return GetAssemblyStream(Assembly.GetCallingAssembly(), path); }
    }
}