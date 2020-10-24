#region usings

using System;
using System.Collections.Generic;

#endregion

namespace SM.OGL.Shaders
{
    public class UniformArray : IUniform
    {
        internal UniformCollection collection;

        internal Dictionary<string, int> Offsets = new Dictionary<string, int>();
        internal int Size;

        internal bool Struct = false;
        public int Location { get; internal set; }
        public GenericShader Parent { get; internal set; }
        public string Name { get; internal set; }

        public UniformArray()
        {
            collection = new UniformCollection()
            {
                ParentShader = Parent
            };
        }

        public void Set(Action<int, Uniform> setAction)
        {
            for (var i = 0; i < Size; i++) setAction(i, new Uniform(Location + i));
        }

        public void Set(Func<int, UniformCollection, bool> setAction)
        {
            collection.ParentShader ??= Parent;

            for (var i = 0; i < Size; i++)
            {
                collection.KeyString = $"{Name}[{i}]";

                foreach (var pair in Offsets)
                    collection.Set(pair.Key, new Uniform(Location + pair.Value + i));

                if (!setAction(i, collection)) break;
            }
        }
    }
}