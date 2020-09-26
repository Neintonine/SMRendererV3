using SM.OGL.Mesh;

namespace SM.Base.Objects
{
    public class Mesh : GenericMesh
    {
        public virtual VBO Color { get; }

        protected Mesh()
        {
            AttribDataIndex.Add(3, Color);
        }
    }
}