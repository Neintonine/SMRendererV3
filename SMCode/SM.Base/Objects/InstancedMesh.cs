using System;
using OpenTK.Graphics.OpenGL4;
using SM.OGL.Mesh;

namespace SM.Base.Objects
{
    public class InstancedMesh : Mesh, ILineMesh
    {
        public InstancedMesh(PrimitiveType type, string[] enabledAttibute) : base(type)
        {
            Attributes["vertex"] = Vertex = new VBO();

            foreach (string attribute in enabledAttibute)
            {
                switch (attribute)
                {
                    case "uv":
                        Attributes["uv"] = UVs = new VBO(pointerSize: 2);
                        break;
                    case "normals":
                        Attributes["normal"] = Normals = new VBO();
                        break;
                    case "color":
                        Attributes["color"] = Color = new VBO(pointerSize: 4);
                        break;
                }
            }
        }

        public float LineWidth { get; set; } = 1;
    }
}