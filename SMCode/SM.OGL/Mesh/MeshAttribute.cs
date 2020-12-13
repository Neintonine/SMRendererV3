namespace SM.OGL.Mesh
{
    public struct MeshAttribute
    {
        public int Index;
        public string Name;
        public VBO ConnectedVBO;

        public MeshAttribute(int index, string name, VBO buffer)
        {
            Index = index;
            Name = name;
            ConnectedVBO = buffer;
        }
    }
}