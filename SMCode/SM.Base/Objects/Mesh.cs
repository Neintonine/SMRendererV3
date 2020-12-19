#region usings

using OpenTK.Graphics.OpenGL4;
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
        public Mesh(PrimitiveType type)
        {
            PrimitiveType = type;
            Attributes.Add(3, "color", Color);
        }

        /// <summary>
        ///     Contains vertex colors
        /// </summary>
        public virtual VBO Color { get; protected set; }
    }
}