using System.Collections.Generic;

namespace SM.OGL.Shaders
{
    /// <summary>
    /// This class controls uniform array structures.
    /// </summary>
    public class UniformArray : IUniform
    {
        private readonly Dictionary<int, Dictionary<string, Uniform>> storedUniforms = new Dictionary<int, Dictionary<string, Uniform>>();
        internal List<string> UniformNames = new List<string>();

        /// <inheritdoc />
        public int Location { get; internal set; }
        /// <summary>
        /// The name of the uniform.
        /// </summary>
        public string Name { get; internal set; }
        /// <summary>
        /// The uniform collection the uniform is from.
        /// </summary>
        public UniformCollection Parent { get; internal set; }
        /// <summary>
        /// The shader the uniform is from.
        /// </summary>
        public GenericShader ParentShader { get; internal set; }
        /// <summary>
        /// The length of the array.
        /// </summary>
        public int Length => storedUniforms.Count;

        /// <summary>
        /// Returns a dictionary to control the current index inside the array.
        /// </summary>
        public Dictionary<string, Uniform> this[int index] => Get(index);

        /// <summary>
        /// Equivalent to <see cref="this"/>
        /// <para>Returns a dictionary to control the current index inside the array.</para>
        /// </summary>
        public Dictionary<string, Uniform> Get(int index)
        {
            if (!storedUniforms.ContainsKey(index))
            {
                Dictionary<string, Uniform> dic = storedUniforms[index] = new Dictionary<string, Uniform>();

                for (int i = 0; i < UniformNames.Count; i++)
                {
                    dic.Add(UniformNames[i], new Uniform(Name + $"[{index}]." + UniformNames[i], ParentShader, Parent));
                }
            }

            return storedUniforms[index];
        }
    }
}