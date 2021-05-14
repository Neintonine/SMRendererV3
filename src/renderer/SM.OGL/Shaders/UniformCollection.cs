#region usings

using System;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL4;

#endregion

namespace SM.OGL.Shaders
{
    /// <summary>
    /// Collects and provied the uniforms of a shader.
    /// </summary>
    public class UniformCollection : Dictionary<string, IUniform>
    {
        internal string KeyString = "";
        /// <summary>
        /// The next uniform-position for textures.
        /// </summary>
        public int NextTexture = 0;
        /// <summary>
        /// The shader this collections is connected to.
        /// </summary>
        public GenericShader ParentShader { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        public new Uniform this[string key] => Get(key);

        /// <summary>
        /// Equivalent to <see cref="this"/>
        /// <para>Gets the uniform with the provied key.</para>
        /// <para>If it can't find it, it will create a warning and a uniform with the location of -1.</para>
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Gets a array.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException">If the key wasn't found, it will throw a exception</exception>
        public UniformArray GetArray(string key)
        {
            if (ContainsKey(key))
                return (UniformArray) base[key];
            else throw new KeyNotFoundException("UniformArray '"+key+"' wasn't found");
        }

        /// <summary>
        /// Adds a uniform to the collection.
        /// </summary>
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
                        array.UniformNames.Add(keySplits[2].Substring(1));
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