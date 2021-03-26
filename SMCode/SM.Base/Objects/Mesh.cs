#region usings

using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using SM.OGL.Mesh;

#endregion

namespace SM.Base.Objects
{
    /// <inheritdoc cref="GenericMesh" />
    public class Mesh : GenericMesh, ILineMesh
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
        public virtual VBO<Color4> Color { get; protected set; }

        /// <inheritdoc />
        public float LineWidth { get; set; } = 1;
    }
}