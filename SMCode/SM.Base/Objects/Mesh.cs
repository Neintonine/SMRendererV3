#region usings

using SM.OGL.Mesh;

#endregion

namespace SM.Base.Objects
{
    /// <inheritdoc />
    public class Mesh : GenericMesh
    {
        /// <summary>
        ///     While initializing, it will add the <see cref="Color" /> to the data index.
        /// </summary>
        protected Mesh()
        {
            AttribDataIndex.Add(3, Color);
        }

        /// <summary>
        ///     Contains vertex colors
        /// </summary>
        public virtual VBO Color { get; }
    }
}