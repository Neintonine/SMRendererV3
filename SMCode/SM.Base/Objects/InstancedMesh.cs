#region usings

using OpenTK;
using OpenTK.Graphics;
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
            Attributes["vertex"] = Vertex = new VBO<Vector3>();

            foreach (string attribute in enabledAttibute)
                switch (attribute)
                {
                    case "uv":
                        Attributes["uv"] = UVs = new VBO<Vector2>();
                        break;
                    case "normals":
                        Attributes["normal"] = Normals = new VBO<Vector3>();
                        break;
                    case "color":
                        Attributes["color"] = Color = new VBO<Color4>();
                        break;
                }
        }
    }
}