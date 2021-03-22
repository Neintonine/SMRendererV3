namespace SM.OGL.Mesh
{
    /// <summary>
    /// This represents a attribute of a mesh.
    /// </summary>
    public class MeshAttribute
    {
        /// <summary>
        /// Index of attribute
        /// </summary>
        public int Index;
        /// <summary>
        /// Name of the attribute
        /// </summary>
        public string Name;
        /// <summary>
        /// Connected buffer object.
        /// </summary>
        public VBO ConnectedVBO;

        /// <summary>
        /// Creates a attribute for a mesh.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="name"></param>
        /// <param name="buffer"></param>
        public MeshAttribute(int index, string name, VBO buffer)
        {
            Index = index;
            Name = name;
            ConnectedVBO = buffer;
        }
    }
}