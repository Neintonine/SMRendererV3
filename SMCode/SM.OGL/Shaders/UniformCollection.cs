#region usings

using System;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL4;

#endregion

namespace SM.OGL.Shaders
{
    public class UniformCollection : Dictionary<string, IUniform>
    {
        public int NextTexture = 0;
        internal string KeyString = "";
        public GenericShader ParentShader { get; internal set; }

        public new Uniform this[string key] => Get(key);

        public Uniform Get(string key)
        {
            try
            {
                return (Uniform) base[key];
            }
            catch (KeyNotFoundException)
            {
                GLCustomActions.AtWarning?.Invoke("Uniform '" + KeyString + key + "' at '" + ParentShader.ToString() + "' was not found. Tried to recreate it.");
                var u = new Uniform(GL.GetUniformLocation(ParentShader, KeyString + key), this);
                Add(key, u);
                return u;
            }
        }

        public UniformArray GetArray(string key)
        {
            try
            {
                return (UniformArray) base[key];
            }
            catch (KeyNotFoundException)
            {
                throw new Exception("UniformArray '"+key+"' wasn't found");
            }
        }

        public void Add(string key, int location)
        {
            base.Add(key, new Uniform(location, this));
        }

        internal void Set(string key, IUniform value)
        {
            base[key] = value;
        }

        internal void Import(GenericShader shader)
        {
            GL.GetProgram(shader, GetProgramParameterName.ActiveUniforms, out var uniformCount);
            if (uniformCount < 1)
                GLCustomActions.AtError("No uniforms has been found.");

            var lastArrayKey = "";
            var array = new UniformArray();
            var arrayFilled = false;

            if (GLSettings.InfoEveryUniform) GLCustomActions.AtInfo?.Invoke("Uniforms for: " + shader.GetType());

            for (var i = 0; i < uniformCount; i++)
            {
                var key = GL.GetActiveUniform(shader, i, out _, out _);
                var loc = GL.GetUniformLocation(shader, key);
                if (GLSettings.InfoEveryUniform) GLCustomActions.AtInfo?.Invoke($"{key} - {loc}");

                if (key.Contains("["))
                {
                    var keySplits = key.Split('[', ']');
                    if (keySplits[2] == "")
                    {
                        if (keySplits[1] == "0")
                        {
                            Add(keySplits[0], loc);
                        }

                        continue;
                    }

                    if (keySplits[0] != lastArrayKey)
                    {
                        if (arrayFilled) Add(lastArrayKey, array);

                        array = new UniformArray
                        {
                            Location = loc,
                            Name = keySplits[0],
                            Parent = this,
                            ParentShader = ParentShader
                        };

                        arrayFilled = true;
                        lastArrayKey = keySplits[0];
                    }

                    if (keySplits[1] == "0") 
                        array.uniformNames.Add(keySplits[2].Substring(1));
                }
                else
                {
                    Add(key, loc);
                }
            }

            if (arrayFilled) Add(lastArrayKey, array);
        }
    }
}