using OpenTK;

namespace SM.Base.Types
{
    /// <summary>
    /// A three-dimensional vector.
    /// </summary>
    public class CVector3 : CVector2
    {
        /// <summary>
        /// Z-component
        /// </summary>
        public float Z { get; set; }

        /// <summary>
        /// Creates a vector, where each component is the same value.
        /// </summary>
        /// <param name="uniform">The Value</param>
        public CVector3(float uniform) : base(uniform)
        {
            Z = uniform;
        }
        /// <summary>
        /// Creates a vector
        /// </summary>
        public CVector3(float x, float y, float z) : base(x, y)
        {
            Z = z;
        }

        private protected override float GetLengthProcess()
        {
            return base.GetLengthProcess() + Z * Z;
        }

        private protected override void NormalizationProcess(float length)
        {
            base.NormalizationProcess(length);
            Z *= length;
        }

        /// <inheritdoc />
        public override void Set(float uniform)
        {
            base.Set(uniform);
            Z = uniform;
        }

        /// <summary>
        /// Sets the a own value to each component.
        /// </summary>
        public void Set(float x, float y, float z)
        {
            base.Set(x,y);
            Z = z;
        }
        /// <summary>
        /// Sets each component to the <see cref="Vector3"/> counter-part.
        /// </summary>
        /// <param name="vector"></param>
        public void Set(Vector3 vector)
        {
            Set(vector.X, vector.Y, vector.Z);
        }

        public override void Add(float uniform)
        {
            base.Add(uniform);
            Z += uniform;
        }

        public void Add(Vector3 vector)
        {
            Add(vector.X, vector.Y, vector.Z);
        }

        public void Add(float x, float y, float z)
        {
            base.Add(x,y);
            Z += z;
        }

        /// <summary>
        /// Converts to <see cref="Vector3"/>
        /// </summary>
        public static implicit operator Vector3(CVector3 vector) => new Vector3(vector.X, vector.Y, vector.Z);
        /// <summary>
        /// Converts from <see cref="Vector3"/> to <see cref="CVector3"/>.
        /// </summary>
        public static implicit operator CVector3(Vector3 vector) => new CVector3(vector.X, vector.Y, vector.Z);
    }
}