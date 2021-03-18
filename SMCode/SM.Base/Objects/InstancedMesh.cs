#region usings

using OpenTK.Graphics.OpenGL4;
using SM.OGL.Mesh;

#endregion

namespace SM.Base.Objects
{
    /// <summary>
    /// This class allows for fast mesh creation.
    /// </summary>
    public class InstancedMesh : Mesh
    {
        /// <summary>
        /// Generates a mesh, with a primitive and attributes, that are required.
        /// </summary>
        /// <param name="type">The mesh type</param>
        /// <param name="enabledAttibute">A list of (additional) attributes.
        ///     <para> Possible values: uv, normals and color </para>
        /// </param>
        public InstancedMesh(PrimitiveType type, string[] enabledAttibute) : base(type)
        {
            Attributes["vertex"] = Vertex = new VBO();

            foreach (string attribute in enabledAttibute)
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
}