#region usings

using OpenTK;

#endregion

namespace SM.Base.Types
{
    /// <summary>
    ///     Represents a Vector of three floats.
    /// </summary>
    public class CVector3 : CVector2
    {
        /// <inheritdoc />
        public CVector3(float uniform) : base(uniform)
        {
        }

        /// <inheritdoc />
        public CVector3(float x, float y, float z) : base(x, y, z, default)
        {
        }

        /// <inheritdoc />
        protected CVector3(float x, float y, float z, float w) : base(x, y, z, w)
        {
        }

        /// <summary>
        ///     Z-component
        /// </summary>
        public float Z
        {
            get => _Z;
            set => _Z = value;
        }

        /// <summary>
        ///     Sets the X, Y and Z-component.
        /// </summary>
        public new void Set(float x, float y, float z)
        {
            base.Set(x, y, z);
        }

        /// <summary>
        ///     Sets the X, Y and Z-component from a <see cref="OpenTK.Vector3" />.
        /// </summary>
        public new void Set(Vector3 vector)
        {
            base.Set(vector);
        }

        /// <summary>
        ///     Adds a <see cref="OpenTK.Vector3" /> to the X, Y and Z-component.
        /// </summary>
        public new void Add(Vector3 vector)
        {
            base.Add(vector);
        }
    }
}