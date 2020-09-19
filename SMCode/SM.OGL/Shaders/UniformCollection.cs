using System;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL4;

namespace SM.OGL.Shaders
{
    public class UniformCollection : Dictionary<string, Uniform>
    {
        internal int NextTexture = 0;

        internal GenericShader _parentShader;

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

        public void Add(string key, int location)
        {
            base.Add(key, new Uniform(location, this));
        }
    }
}