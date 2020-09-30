using System;
using System.Configuration.Assemblies;
using OpenTK;

namespace SM.Base.Types
{
    public abstract class Vector
    {
        private float _x = default;
        private float _y = default;
        private float _z = default;
        private float _w = default;

        protected float _X
        {
            get => _x;
            set => _x = value;
        }
        protected float _Y
        {
            get => _y;
            set => _y = value;
        }
        protected float _Z
        {
            get => _z;
            set => _z = value;
        }
        protected float _W
        {
            get => _w;
            set => _w = value;
        }

        protected Vector(float uniform) : this(uniform, uniform, uniform, uniform)
        { }

        protected Vector(float x, float y, float z, float w)
        {
            _x = x;
            _y = y;
            _z = z;
            _w = w;
        }
        
        #region Set
        protected void Set(float x, float y)
        {
            _X = x;
            _Y = y;
        }

        protected void Set(OpenTK.Vector2 vector)
        {
            Set(vector.X, vector.Y);
        }

        protected void Set(float x, float y, float z)
        {
            Set(x,y);
            _Z = z;
        }

        protected void Set(OpenTK.Vector3 vector)
        {
            Set(vector.X, vector.Y, vector.Z);
        }

        protected void Set(float x, float y, float z, float w)
        {
            Set(x,y,z);
            _W = w;
        }

        protected void Set(OpenTK.Vector4 vector)
        {
            Set(vector.X, vector.Y, vector.Z, vector.W);
        }
        #endregion

        #region Add

        protected void Add(OpenTK.Vector2 vector)
        {
            _X += vector.X;
            _Y += vector.Y;
        }
        protected void Add(OpenTK.Vector3 vector)
        {
            _X += vector.X;
            _Y += vector.Y;
            _Z += vector.Z;
        }
        protected void Add(OpenTK.Vector4 vector)
        {
            _X += vector.X;
            _Y += vector.Y;
            _Z += vector.Z;
            _W += vector.W;
        }

        #endregion
        public static implicit operator OpenTK.Vector2(Vector v) => new OpenTK.Vector2(v._x, v._y);
        public static implicit operator OpenTK.Vector3(Vector v) => new OpenTK.Vector3(v._x, v._y, v._z);
        public static implicit operator OpenTK.Vector4(Vector v) => new OpenTK.Vector4(v._x, v._y, v._z, v._w);



    }
}