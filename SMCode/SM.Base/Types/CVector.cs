using System;
using System.Configuration.Assemblies;
using System.Diagnostics;
using OpenTK;

namespace SM.Base.Types
{
    /// <summary>
    /// Represents a base vector-class
    /// </summary>
    public abstract class CVector
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private float _x = default;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private float _y = default;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private float _z = default;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private float _w = default;

        /// <summary>
        /// The X-component.
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        protected float _X
        {
            get => _x;
            set => _x = value;
        }
        /// <summary>
        /// The Y-component
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        protected float _Y
        {
            get => _y;
            set => _y = value;
        }
        /// <summary>
        /// The Z-component.
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        protected float _Z
        {
            get => _z;
            set => _z = value;
        }
        /// <summary>
        /// The W-component.
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        protected float _W
        {
            get => _w;
            set => _w = value;
        }

        /// <summary>
        /// Creates a vector by setting every component to the same value.
        /// </summary>
        protected CVector(float uniform) : this(uniform, uniform, uniform, uniform)
        { }

        /// <summary>
        /// Creates a vector by setting values for each component.
        /// </summary>
        protected CVector(float x, float y, float z, float w)
        {
            _x = x;
            _y = y;
            _z = z;
            _w = w;
        }
        
        #region Set
        /// <summary>
        /// Sets the X and Y-component.
        /// </summary>
        protected void Set(float x, float y)
        {
            _X = x;
            _Y = y;
        }
        /// <summary>
        /// Sets the X and Y-component from a <see cref="OpenTK.Vector2"/>
        /// </summary>
        protected void Set(OpenTK.Vector2 vector)
        {
            Set(vector.X, vector.Y);
        }

        /// <summary>
        /// Sets the X, Y and Z-component.
        /// </summary>
        protected void Set(float x, float y, float z)
        {
            Set(x,y);
            _Z = z;
        }

        /// <summary>
        /// Sets the X, Y and Z-component from a <see cref="OpenTK.Vector3"/>.
        /// </summary>
        /// <param name="vector"></param>
        protected void Set(OpenTK.Vector3 vector)
        {
            Set(vector.X, vector.Y, vector.Z);
        }

        /// <summary>
        /// Sets the X, Y, Z and W-component.
        /// </summary>
        protected void Set(float x, float y, float z, float w)
        {
            Set(x,y,z);
            _W = w;
        }

        /// <summary>
        /// Sets the X, Y, Z and W-component from a <see cref="OpenTK.Vector4"/>.
        /// </summary>
        protected void Set(OpenTK.Vector4 vector)
        {
            Set(vector.X, vector.Y, vector.Z, vector.W);
        }
        #endregion

        #region Add

        /// <summary>
        /// Adds a <see cref="OpenTK.Vector2"/> to the X and Y-component.
        /// </summary>
        protected void Add(OpenTK.Vector2 vector)
        {
            _X += vector.X;
            _Y += vector.Y;
        }

        /// <summary>
        /// Adds a <see cref="OpenTK.Vector3"/> to the X, Y and Z-component.
        /// </summary>
        protected void Add(OpenTK.Vector3 vector)
        {
            _X += vector.X;
            _Y += vector.Y;
            _Z += vector.Z;
        }

        /// <summary>
        /// Adds a <see cref="OpenTK.Vector4"/> to the X, Y, Z and W-component.
        /// </summary>
        protected void Add(OpenTK.Vector4 vector)
        {
            _X += vector.X;
            _Y += vector.Y;
            _Z += vector.Z;
            _W += vector.W;
        }

        #endregion

        /// <summary>
        /// Transforms a <see cref="CVector"/> to a <see cref="OpenTK.Vector2"/>
        /// </summary>
        public static implicit operator OpenTK.Vector2(CVector v) => new OpenTK.Vector2(v._x, v._y);
        /// <summary>
        /// Transforms a <see cref="CVector"/> to a <see cref="OpenTK.Vector3"/>
        /// </summary>
        public static implicit operator OpenTK.Vector3(CVector v) => new OpenTK.Vector3(v._x, v._y, v._z);
        /// <summary>
        /// Transforms a <see cref="CVector"/> to a <see cref="OpenTK.Vector4"/>
        /// </summary>
        public static implicit operator OpenTK.Vector4(CVector v) => new OpenTK.Vector4(v._x, v._y, v._z, v._w);
    }
}