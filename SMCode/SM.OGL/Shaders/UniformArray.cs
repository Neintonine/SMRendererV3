using System.Collections.Generic;
using OpenTK.Graphics.OpenGL;

namespace SM.OGL.Shaders
{
    public class UniformArray : IUniform
    {
        private Dictionary<int, Dictionary<string, Uniform>> storedUniforms = new Dictionary<int, Dictionary<string, Uniform>>();
        internal List<string> uniformNames = new List<string>();

        public int Location { get; internal set; }
        public string Name { get; internal set; }
        public UniformCollection Parent { get; internal set; }
        public GenericShader ParentShader { get; internal set; }

        public Dictionary<string, Uniform> this[int index] => Get(index);

        public Dictionary<string, Uniform> Get(int index)
        {
            if (!storedUniforms.ContainsKey(index))
            {
                Dictionary<string, Uniform> dic = storedUniforms[index] = new Dictionary<string, Uniform>();

                for (int i = 0; i < uniformNames.Count; i++)
                {
                    dic.Add(uniformNames[i], new Uniform(Name + $"[{index}]." + uniformNames[i], ParentShader, Parent));
                }
            }

            return storedUniforms[index];
        }
    }
}