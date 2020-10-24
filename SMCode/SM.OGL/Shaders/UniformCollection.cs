#region usings

using System;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL4;

#endregion

namespace SM.OGL.Shaders
{
    public class UniformCollection : Dictionary<string, IUniform>
    {
        internal int NextTexture = 0;
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
                GLCustomActions.AtWarning?.Invoke("Uniform '" + KeyString + key + "' was not found. Tried to recreate it.");
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
                    if (keySplits[0] != lastArrayKey)
                    {
                        if (arrayFilled) Add(lastArrayKey, array);

                        array = new UniformArray
                        {
                            Location = loc,
                            Name = keySplits[0],
                            Parent = ParentShader,
                            Struct = keySplits.Length > 2
                        };

                        arrayFilled = true;
                        lastArrayKey = keySplits[0];
                    }

                    var curIndex = int.Parse(keySplits[1]);
                    if (array.Size < curIndex) array.Size = curIndex;

                    if (array.Struct)
                        if (!array.Offsets.ContainsKey(keySplits[2].Trim('.')))
                            array.Offsets.Add(keySplits[2].Trim('.'), loc - array.Location);
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