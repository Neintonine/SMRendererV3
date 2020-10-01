using OpenTK;

namespace SM.Base.Types
{
    /// <summary>
    /// Represents a vector of four floats.
    /// </summary>
    public class CVector4 : CVector3
    {
        /// <summary>
        /// W-component
        /// </summary>
        public float W
        {
            get => _W;
            set => _W = value;
        }

        /// <inheritdoc />
        public CVector4(float uniform) : base(uniform)
        {
        }

        /// <inheritdoc />
        public CVector4(float x, float y, float z, float w) : base(x, y, z, w)
        {
        }

        /// <summary>
        /// Sets the X, Y, Z and W-component.
        /// </summary>
        public new void Set(float x, float y, float z, float w) => base.Set(x, y, z, w);
        /// <summary>
        /// Sets the X, Y, Z and W-component from a <see cref="OpenTK.Vector4"/>.
        /// </summary>
        public new void Set(Vector4 vector) => base.Set(vector);
        /// <summary>
        /// Adds a <see cref="OpenTK.Vector4"/> to the X, Y, Z and W-component.
        /// </summary>
        public new void Add(Vector4 vector) => base.Add(vector);
    }
}