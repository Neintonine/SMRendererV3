#region usings

using System.Diagnostics;
using OpenTK;

#endregion

namespace SM.Base.Types
{
    /// <summary>
    ///     Represents a base vector-class
    /// </summary>
    public abstract class CVector
    {
        /// <summary>
        ///     Creates a vector by setting every component to the same value.
        /// </summary>
        protected CVector(float uniform) : this(uniform, uniform, uniform, uniform)
        {
        }

        /// <summary>
        ///     Creates a vector by setting values for each component.
        /// </summary>
        protected CVector(float x, float y, float z, float w)
        {
            _X = x;
            _Y = y;
            _Z = z;
            _W = w;
        }

        /// <summary>
        ///     The X-component.
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        protected float _X { get; set; }

        /// <summary>
        ///     The Y-component
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        protected float _Y { get; set; }

        /// <summary>
        ///     The Z-component.
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        protected float _Z { get; set; }

        /// <summary>
        ///     The W-component.
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        protected float _W { get; set; }

        /// <summary>
        ///     Transforms a <see cref="CVector" /> to a <see cref="OpenTK.Vector2" />
        /// </summary>
        public static implicit operator Vector2(CVector v) => new Vector2(v._X, v._Y);

        /// <summary>
        ///     Transforms a <see cref="CVector" /> to a <see cref="OpenTK.Vector3" />
        /// </summary>
        public static implicit operator Vector3(CVector v) => new Vector3(v._X, v._Y, v._Z);

        /// <summary>
        ///     Transforms a <see cref="CVector" /> to a <see cref="OpenTK.Vector4" />
        /// </summary>
        public static implicit operator Vector4(CVector v) => new Vector4(v._X, v._Y, v._Z, v._W);
        
        #region Set

        /// <summary>
        ///     Sets the X and Y-component.
        /// </summary>
        protected void Set(float x, float y)
        {
            _X = x;
            _Y = y;
        }

        /// <summary>
        ///     Sets the X and Y-component from a <see cref="OpenTK.Vector2" />
        /// </summary>
        protected void Set(Vector2 vector)
        {
            Set(vector.X, vector.Y);
        }

        /// <summary>
        ///     Sets the X, Y and Z-component.
        /// </summary>
        protected void Set(float x, float y, float z)
        {
            Set(x, y);
            _Z = z;
        }

        /// <summary>
        ///     Sets the X, Y and Z-component from a <see cref="OpenTK.Vector3" />.
        /// </summary>
        /// <param name="vector"></param>
        protected void Set(Vector3 vector)
        {
            Set(vector.X, vector.Y, vector.Z);
        }

        /// <summary>
        ///     Sets the X, Y, Z and W-component.
        /// </summary>
        protected void Set(float x, float y, float z, float w)
        {
            Set(x, y, z);
            _W = w;
        }

        /// <summary>
        ///     Sets the X, Y, Z and W-component from a <see cref="OpenTK.Vector4" />.
        /// </summary>
        protected void Set(Vector4 vector)
        {
            Set(vector.X, vector.Y, vector.Z, vector.W);
        }

        #endregion

        #region Add

        /// <summary>
        ///     Adds a <see cref="OpenTK.Vector2" /> to the X and Y-component.
        /// </summary>
        protected void Add(Vector2 vector)
        {
            _X += vector.X;
            _Y += vector.Y;
        }

        /// <summary>
        ///     Adds a <see cref="OpenTK.Vector3" /> to the X, Y and Z-component.
        /// </summary>
        protected void Add(Vector3 vector)
        {
            _X += vector.X;
            _Y += vector.Y;
            _Z += vector.Z;
        }

        /// <summary>
        ///     Adds a <see cref="OpenTK.Vector4" /> to the X, Y, Z and W-component.
        /// </summary>
        protected void Add(Vector4 vector)
        {
            _X += vector.X;
            _Y += vector.Y;
            _Z += vector.Z;
            _W += vector.W;
        }

        #endregion
    }
}