using System;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL4;

namespace SM.OGL.Shaders
{
    /// <summary>
    /// Contains and manages the uniform of the parent shader.
    /// </summary>
    public class UniformCollection : Dictionary<string, Uniform>
    {
        /// <summary>
        /// The next texture id for the uniform.
        /// </summary>
        internal int NextTexture = 0;

        /// <summary>
        /// The parent shader.
        /// </summary>
        internal GenericShader _parentShader;

        /// <summary>
        /// Get you the uniform under the variable name.
        /// <para>If it don't find the uniform, it tries to recreate it.</para>
        /// <para>If the variable doesn't exist in the first place, it will after the recreation send everything to -1, what is the void.</para>
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public new Uniform this[string key]
        {
            get
            {
                try
                {
                    return base[key];
                }
                catch (KeyNotFoundException)
                {
                    Console.WriteLine("[Error] Uniform '"+key+"' was not found. Tried to recreate it.");
                    Uniform u = new Uniform(GL.GetUniformLocation(_parentShader, key), this);
                    Add(key, u);
                    return u;
                }
            }
        }
        /// <summary>
        /// Adds a uniform with a location.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="location"></param>
        public void Add(string key, int location)
        {
            base.Add(key, new Uniform(location, this));
        }
    }
}