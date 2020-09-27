using System;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL4;

namespace SM.OGL.Shaders
{
    /// <summary>
    /// Contains/Represents a file used in shaders.
    /// </summary>
    public class ShaderFile : GLObject
    {
        private string _data;

        /// <inheritdoc />
        public override ObjectLabelIdentifier TypeIdentifier { get; } = ObjectLabelIdentifier.Shader;

        /// <summary>
        /// Contains overrides, that can be used to import values from the CPU to the shader before it is been send to the GPU.
        /// </summary>
        public Dictionary<string, string> StringOverrides = new Dictionary<string, string>();
        /// <summary>
        /// Contains other shader files to allow access to their functions.
        /// </summary>
        public List<ShaderFile> GLSLExtensions = new List<ShaderFile>();

        /// <summary>
        /// Creates a file.
        /// </summary>
        /// <param name="data">The source file.</param>
        public ShaderFile(string data)
        {
            _data = data;
        }


        private void GenerateSource()
        {
            foreach (KeyValuePair<string, string> kvp in StringOverrides)
                _data = _data.Replace("//! " + kvp.Key, kvp.Value);
        }

        internal void Compile(GenericShader shader, ShaderType type)
        {
            if (_id < 0)
            {
                GenerateSource();

                _id = GL.CreateShader(type);
                GL.ShaderSource(_id, _data);
                GL.CompileShader(_id);
            }
            GL.AttachShader(shader, _id);
        }
    }
}