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

        public List<string> Defines = new List<string>();

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
        
        internal bool Compile(GenericShader shader, ShaderType type)
        {
            if (_id < 0)
            {
                GenerateSource();

                _id = GL.CreateShader(type);
                if (Defines.Count > 0)
                {
                    string defineString = "";
                    foreach(string define in Defines)
                    {
                        defineString += "#define " + define + Environment.NewLine;
                    }

                    GL.ShaderSource(_id, 2, new string[] { defineString, _data }, new int[] { defineString.Length, _data.Length });
                } else GL.ShaderSource(_id, _data);
                GL.CompileShader(_id);
            }

            GL.GetShader(_id, ShaderParameter.CompileStatus, out int compileStatus);
            if (compileStatus != 1)
            {
                GL.GetShader(_id, ShaderParameter.InfoLogLength, out int loglength);

                GLCustomActions.AtWarning?.Invoke($"Shader '{ToString()}' doesn't compile correctly.\nReason:" + GL.GetShaderInfoLog(_id));

                GL.DeleteShader(_id);
                return false;
            }

            GL.AttachShader(shader, _id);


            for (var i = 0; i < GLSLExtensions.Count; i++) { 
                if (!GLSLExtensions[i].Compile(shader, type)) return false; 
            }
            return true;
        }

        /// <inheritdoc />
        public override void Dispose()
        {
            GL.DeleteShader(this);

            base.Dispose();
        }
    }
}