using SM.OGL.Mesh;

namespace SM.Base.Objects
{
    /// <inheritdoc />
    public class Mesh : GenericMesh
    {
        /// <summary>
        /// Contains vertex colors
        /// </summary>
        public virtual VBO Color { get; }

        /// <summary>
        /// While initializing, it will add the <see cref="Color"/> to the data index.
        /// </summary>
        protected Mesh()
        {
            AttribDataIndex.Add(3, Color);
        }
    }
}