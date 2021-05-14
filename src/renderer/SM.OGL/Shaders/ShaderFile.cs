#region usings

using System;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL4;

#endregion

namespace SM.OGL.Shaders
{
    /// <summary>
    ///     Contains/Represents a file used in shaders.
    /// </summary>
    public class ShaderFile : GLObject
    {
        private string _data;

        /// <summary>
        ///     Contains other shader files to allow access to their functions.
        /// </summary>
        public List<ShaderFile> GLSLExtensions = new List<ShaderFile>();

        /// <summary>
        ///     Gets/Sets the name for this shader file.
        /// </summary>
        public new string Name;

        /// <summary>
        ///     Contains overrides, that can be used to import values from the CPU to the shader before it is been send to the GPU.
        /// </summary>
        public Dictionary<string, string> StringOverrides = new Dictionary<string, string>();

        /// <summary>
        ///     Creates a file.
        /// </summary>
        /// <param name="data">The source file.</param>
        public ShaderFile(string data)
        {
            _data = data;
        }

        /// <inheritdoc />
        public override ObjectLabelIdentifier TypeIdentifier { get; } = ObjectLabelIdentifier.Shader;


        private void GenerateSource()
        {
            if (!GLSettings.ShaderPreProcessing) return;

            if (_data.Contains("//#"))
            {
                var commandSplits = _data.Split(new[] {"//#"}, StringSplitOptions.RemoveEmptyEntries);
                for (var i = 1; i < commandSplits.Length; i++)
                {
                    var split = commandSplits[i].Split('\r', '\n')[0].Trim();
                    var cmdArgs = split.Split(new[] {' '}, 2);

                    ShaderPreProcess.Actions[cmdArgs[0]]?.Invoke(this, cmdArgs[1]);
                }
            }

            foreach (var kvp in StringOverrides)
                _data = _data.Replace("//!" + kvp.Key, kvp.Value);
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
            GLDebugging.CheckGLErrors($"Error at loading shader file: '{shader.GetType()}', '{type}', %code%");

            for (var i = 0; i < GLSLExtensions.Count; i++) GLSLExtensions[i].Compile(shader, type);
        }

        /// <inheritdoc />
        public override void Dispose()
        {
            GL.DeleteShader(this);

            base.Dispose();
        }
    }
}