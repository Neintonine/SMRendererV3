using System;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL4;

namespace SM.OGL.Shaders
{
    public class ShaderFile : GLObject
    {
        private string _data;

        public override ObjectLabelIdentifier TypeIdentifier { get; } = ObjectLabelIdentifier.Shader;

        public Dictionary<string, string> StringOverrides = new Dictionary<string, string>();
        public List<ShaderFile> GLSLExtensions = new List<ShaderFile>();

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
            GL.AttachShader(_id, shader);
        }
    }
}