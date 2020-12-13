using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace SM.OGL.Mesh
{
    public class MeshAttributeList : List<MeshAttribute>
    {
        public VBO this[string name]
        {
            get
            {
                for (int i = 0; i < Count; i++)
                {
                    if (this[i].Name == name)
                    {
                        return this[i].ConnectedVBO;
                    }
                }

                return null;
            }
        }

        public void Add(int id, string name, VBO vbo)
        {
            if (vbo == null) return;
            Add(new MeshAttribute(id, name, vbo));
        }
    }
}