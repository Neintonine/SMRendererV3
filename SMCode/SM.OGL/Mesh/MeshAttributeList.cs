using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenTK.Graphics.OpenGL;

namespace SM.OGL.Mesh
{
    /// <summary>
    /// List of mesh attributes.
    /// </summary>
    public class MeshAttributeList : List<MeshAttribute>
    {
        /// <summary>
        /// Returns the VBO (or null) that is connected to the specified name.
        /// </summary>
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
            set
            {
                for (int i = 0; i < Count; i++)
                {
                    if (this[i].Name == name)
                    {
                        this[i].ConnectedVBO = value;
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// Adds a new attribute.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="vbo"></param>
        public void Add(int id, string name, VBO vbo)
        {
            //if (vbo == null) return;
            Add(new MeshAttribute(id, name, vbo));
        }

        public bool Has(string name)
        {
            VBO attribute = this[name];
            return attribute != null && attribute.Active;
        }
    }
}