using System;
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

        public static implicit operator OpenTK.Vector2(Vector v) => new OpenTK.Vector2(v._x, v._y);
        public static implicit operator OpenTK.Vector3(Vector v) => new OpenTK.Vector3(v._x, v._y, v._z);
        public static implicit operator OpenTK.Vector4(Vector v) => new OpenTK.Vector4(v._x, v._y, v._z, v._w);



    }
}