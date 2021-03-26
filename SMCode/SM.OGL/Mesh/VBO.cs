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
    /// <summary>
    /// Represents a vertex buffer object in C#.
    /// </summary>
    public abstract class VBO : GLObject
    {
        private float[] _floatArray;

        /// <inheritdoc/>
        protected override bool AutoCompile { get => false; set { return; } }
        /// <inheritdoc/>
        public override ObjectLabelIdentifier TypeIdentifier => ObjectLabelIdentifier.Buffer;

        /// <summary>
        /// If false, it will not compile the vertex buffer.
        /// <para>Default: true</para>
        /// </summary>
        public bool Active { get; set; } = true;
        /// <summary>
        /// This determents if the object can be updated.
        /// </summary>
        public bool CanBeUpdated { get; set; } = true;

        /// <summary>
        /// Specifies whether fixed-point data values should be normalized (true) or converted directly as fixed-point values (false) when they are accessed.
        /// </summary>
        public bool Normalized { get; protected set; }
        /// <summary>
        /// Specifies a offset of the first component of the first generic vertex attribute in the array in the data store of the buffer currently bound to the GL_ARRAY_BUFFER target. The initial value is 0.
        /// </summary>
        public int PointerOffset { get; protected set; }
        /// <summary>
        /// Specifies the byte offset between consecutive generic vertex attributes. If stride is 0, the generic vertex attributes are understood to be tightly packed in the array. The initial value is 0.
        /// </summary>
        public int PointerStride { get; protected set; }
        /// <summary>
        /// Specifies the number of components per generic vertex attribute. Must be 1, 2, 3, 4. Additionally, the symbolic constant GL_BGRA is accepted by glVertexAttribPointer. 
        /// <para>This value gets usually set by the data type.</para>
        /// </summary>
        public int PointerSize { get; protected set; }
        /// <summary>
        /// Specifies the expected usage pattern of the data store.
        /// </summary>
        public BufferUsageHint UsageHint { get; protected set; }
        /// <summary>
        /// Specifies the data type of each component in the array.
        /// <para>This value gets usually set by the data type of the class.</para>
        /// </summary>
        public VertexAttribPointerType PointerType { get; protected set; }

        /// <summary>
        /// The shader id for the mesh.
        /// </summary>
        public int AttributeID { get; internal set; }

        /// <summary>
        /// The amount of data.
        /// </summary>
        public abstract int Count { get; }
        /// <summary>
        /// The amount of data, when the mesh was compiled.
        /// </summary>
        public int CompiledCount { get; private set; }

        /// <summary>
        /// Creates a vertex buffer object.
        /// </summary>
        /// <param name="bufferUsageHint">Specifies the expected usage pattern of the data store.</param>
        /// <param name="pointerStride">Specifies the byte offset between consecutive generic vertex attributes. If stride is 0, the generic vertex attributes are understood to be tightly packed in the array. The initial value is 0.</param>
        /// <param name="pointerOffset">Specifies a offset of the first component of the first generic vertex attribute in the array in the data store of the buffer currently bound to the GL_ARRAY_BUFFER target. The initial value is 0.</param>
        /// <param name="normalised">Specifies whether fixed-point data values should be normalized (true) or converted directly as fixed-point values (false) when they are accessed.</param>

        public VBO(BufferUsageHint bufferUsageHint = BufferUsageHint.StaticDraw,
            int pointerStride = 0, int pointerOffset = 0, bool normalised = false)
        {
            UsageHint = bufferUsageHint;
            PointerStride = pointerStride;
            PointerOffset = pointerOffset;
            Normalized = normalised;
        }

        /// <inheritdoc />
        public override void Compile()
        {
            base.Compile();

            if (!Active) return;

            float[] data = ToFloat();
            CompiledCount = data.Length;

            _id = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _id);
            GL.BufferData(BufferTarget.ArrayBuffer, data.Length * sizeof(float), data, UsageHint);

            GL.VertexAttribPointer(AttributeID, PointerSize, PointerType, Normalized, PointerStride, PointerOffset);
            GL.EnableVertexAttribArray(AttributeID);

            CanBeUpdated = false;
        }

        /// <summary>
        /// Updates the buffer object.
        /// </summary>
        public void Update()
        {
            float[] data = GetFloats();
            if (data.Length > CompiledCount)
                throw new NotSupportedException("Updating the amount of data is not yet supported.");

            GL.BindBuffer(BufferTarget.ArrayBuffer, _id);
            GL.BufferSubData(BufferTarget.ArrayBuffer, IntPtr.Zero, data.Length * sizeof(float), data);

            CanBeUpdated = false;
        }

        /// <inheritdoc/>
        public override void Dispose()
        {
            GL.DeleteBuffer(_id);
            base.Dispose();
        }

        /// <summary>
        /// Gets the data stored, in a float array.
        /// </summary>
        /// <returns></returns>
        public float[] GetFloats()
        {
            if (_floatArray == null || CanBeUpdated)
            {
                _floatArray = ToFloat();
            }

            return _floatArray;
        }

        /// <summary>
        /// Calculates the data stored in a float array.
        /// </summary>
        /// <returns></returns>
        protected abstract float[] ToFloat();
    }

    /// <inheritdoc/>
    public class VBO<TType> : VBO, IEnumerable<TType>
        where TType : struct
    {
        static readonly Dictionary<Type, Tuple<int, VertexAttribPointerType>> _pointerSizes = new()
        {
            { typeof(float), new Tuple<int, VertexAttribPointerType>(1, VertexAttribPointerType.Float) },
            { typeof(Vector2), new Tuple<int, VertexAttribPointerType>(2, VertexAttribPointerType.Float) },
            { typeof(Vector3), new Tuple<int, VertexAttribPointerType>(3, VertexAttribPointerType.Float) },
            { typeof(Vector4), new Tuple<int, VertexAttribPointerType>(4, VertexAttribPointerType.Float) },
            { typeof(Color4), new Tuple<int, VertexAttribPointerType>(4, VertexAttribPointerType.Float) },
        };
        private readonly List<TType> _values = new();

        /// <inheritdoc/>
        public override int Count => _values.Count;

        /// <summary>
        /// Exposes the indexer of the list to the public.
        /// </summary>
        public TType this[int index]
        {
            get => _values[index];
            set { _values[index] = value; CanBeUpdated = true; }
        }

        /// <inheritdoc/>
        public VBO(BufferUsageHint bufferUsageHint = BufferUsageHint.StaticDraw,
            int pointerStride = 0, int pointerOffset = 0, bool normalised = false) : base(bufferUsageHint, pointerStride, pointerOffset, normalised)
        {
            if (!_pointerSizes.ContainsKey(typeof(TType)))
                throw new NotSupportedException($"The type '{typeof(TType).FullName}' is not applicable for VBOs.");

            PointerSize = _pointerSizes[typeof(TType)].Item1;
            PointerType = _pointerSizes[typeof(TType)].Item2;
        }

        /// <summary>
        /// Adds a new value to the list.
        /// </summary>
        /// <param name="value"></param>
        public void Add(TType value)
        {
            _values.Add(value);
            CanBeUpdated = true;
        }

        /// <summary>
        /// Removes the object at that position.
        /// </summary>
        /// <param name="pos"></param>
        public void RemoveAt(int pos)
        {
            _values.RemoveAt(pos);
            CanBeUpdated = true;
        }

        /// <inheritdoc/>
        public IEnumerator<TType> GetEnumerator()
        {
            return _values.GetEnumerator();
        }

        /// <inheritdoc/>
        protected override float[] ToFloat()
        {
            List<float> floats = new();

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
