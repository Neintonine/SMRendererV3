#region usings

using OpenTK.Graphics.OpenGL4;
using SM.OGL.Mesh;

#endregion

namespace SM.Base.Objects
{
    /// <inheritdoc />
    public class Mesh : GenericMesh, ILineMesh
    {

        public float LineWidth { get; set; } = 1;

        /// <summary>
        ///     While initializing, it will add the <see cref="Color" /> to the data index.
        /// </summary>
        public Mesh(PrimitiveType type) : base()
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