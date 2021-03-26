using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.OGL.Mesh
{
    public abstract class VBO : GLObject
    {
        private float[] _floatArray;

        protected override bool AutoCompile { get => false; set { return; } }
        public override ObjectLabelIdentifier TypeIdentifier => ObjectLabelIdentifier.Buffer;

        public bool Active { get; set; } = true;
        public bool CanBeUpdated { get; set; } = true;

        public bool Normalized { get; protected set; }
        public int PointerOffset { get; protected set; }
        public int PointerStride { get; protected set; }
        public int PointerSize { get; protected set; }
        public BufferUsageHint UsageHint { get; protected set; }
        public VertexAttribPointerType PointerType { get; protected set; }

        public int AttributeID { get; internal set; }

        public abstract int Count { get; }

        public VBO(BufferUsageHint bufferUsageHint = BufferUsageHint.StaticDraw,
            int pointerStride = 0, int pointerOffset = 0, bool normalised = false)
        {
            UsageHint = bufferUsageHint;
            PointerStride = pointerStride;
            PointerOffset = pointerOffset;
            Normalized = normalised;
        }

        public override void Compile()
        {
            base.Compile();

            if (!Active) return;

            float[] data = ToFloat();

            _id = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _id);
            GL.BufferData(BufferTarget.ArrayBuffer, data.Length * sizeof(float), data, UsageHint);

            GL.VertexAttribPointer(AttributeID, PointerSize, PointerType, Normalized, PointerStride, PointerOffset);
            GL.EnableVertexAttribArray(AttributeID);

            CanBeUpdated = false;
        }

        public void Update()
        {
            float[] data = GetFloats();

            GL.BindBuffer(BufferTarget.ArrayBuffer, _id);
            GL.BufferSubData(BufferTarget.ArrayBuffer, IntPtr.Zero, data.Length * sizeof(float), data);

            CanBeUpdated = false;
        }

        public override void Dispose()
        {
            GL.DeleteBuffer(_id);
            base.Dispose();
        }

        public float[] GetFloats()
        {
            if (_floatArray == null || CanBeUpdated)
            {
                _floatArray = ToFloat();
            }

            return _floatArray;
        }

        protected abstract float[] ToFloat();
    }

    public class VBO<TType> : VBO, IEnumerable<TType>
        where TType : struct
    {
        static Dictionary<Type, Tuple<int, VertexAttribPointerType>> _pointerSizes = new Dictionary<Type, Tuple<int, VertexAttribPointerType>>()
        {
            { typeof(float), new Tuple<int, VertexAttribPointerType>(1, VertexAttribPointerType.Float) },
            { typeof(Vector2), new Tuple<int, VertexAttribPointerType>(2, VertexAttribPointerType.Float) },
            { typeof(Vector3), new Tuple<int, VertexAttribPointerType>(3, VertexAttribPointerType.Float) },
            { typeof(Vector4), new Tuple<int, VertexAttribPointerType>(4, VertexAttribPointerType.Float) },
            { typeof(Color4), new Tuple<int, VertexAttribPointerType>(4, VertexAttribPointerType.Float) },
        };
        private List<TType> _values = new List<TType>();

        public override int Count => _values.Count;

        public VBO(BufferUsageHint bufferUsageHint = BufferUsageHint.StaticDraw,
            int pointerStride = 0, int pointerOffset = 0, bool normalised = false) : base(bufferUsageHint, pointerStride, pointerOffset, normalised)
        {
            if (!_pointerSizes.ContainsKey(typeof(TType)))
                throw new NotSupportedException($"The type '{typeof(TType).FullName}' is not applicable for VBOs.");

            PointerSize = _pointerSizes[typeof(TType)].Item1;
            PointerType = _pointerSizes[typeof(TType)].Item2;
        }

        public void Add(TType value)
        {
            _values.Add(value);
            CanBeUpdated = true;
        }

        public void RemoveAt(int pos)
        {
            _values.RemoveAt(pos);
            CanBeUpdated = true;
        }

        public IEnumerator<TType> GetEnumerator()
        {
            return _values.GetEnumerator();
        }

        protected override float[] ToFloat()
        {
            List<float> floats = new List<float>();

            foreach (TType value in _values)
            {
                switch (value)
                {
                    case float f: floats.Add(f); break;
                    case Vector2 v: floats.AddRange(new[] { v.X, v.Y }); break;
                    case Vector3 v: floats.AddRange(new[] { v.X, v.Y, v.Z }); break;
                    case Vector4 v: floats.AddRange(new[] { v.X, v.Y, v.Z, v.W }); break;
                    case Color4 v: floats.AddRange(new[] { v.R, v.G, v.B, v.A }); break;
                }
            }
            return floats.ToArray();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _values.GetEnumerator();
        }
    }
}
