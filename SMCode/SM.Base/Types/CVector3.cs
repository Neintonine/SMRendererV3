#region usings

using OpenTK;

#endregion

namespace SM.Base.Types
{
    /// <summary>
    ///     A three-dimensional vector.
    /// </summary>
    public class CVector3 : CVector2
    {
        /// <summary>
        ///     Creates a vector, where each component is the same value.
        /// </summary>
        /// <param name="uniform">The Value</param>
        public CVector3(float uniform) : base(uniform)
        {
            Z = uniform;
        }

        /// <summary>
        ///     Creates a vector
        /// </summary>
        public CVector3(float x, float y, float z) : base(x, y)
        {
            Z = z;
        }

        /// <summary>
        ///     Z-component
        /// </summary>
        public float Z { get; set; }

        /// <inheritdoc />
        protected override float GetLengthProcess()
        {
            return base.GetLengthProcess() + Z * Z;
        }

        /// <inheritdoc />
        protected override void NormalizationProcess(float length)
        {
            base.NormalizationProcess(length);
            Z *= length;
        }

        /// <inheritdoc />
        public override void Set(float uniform, bool triggerChanged = true)
        {
            Z = uniform;
            base.Set(uniform, triggerChanged);
        }

        /// <summary>
        ///     Sets the a own value to each component.
        /// </summary>
        public void Set(float x, float y, float z, bool triggerChanged = true)
        {
            Z = z;
            base.Set(x, y, triggerChanged);
        }

        /// <summary>
        ///     Sets each component to the <see cref="Vector3" /> counter-part.
        /// </summary>
        public void Set(Vector3 vector, bool triggerChanged = true)
        {
            Set(vector.X, vector.Y, vector.Z, triggerChanged);
        }

        /// <inheritdoc />
        public override void Add(float uniform, bool triggerChanged = true)
        {
            Z += uniform;
            base.Add(uniform, triggerChanged);
        }

        /// <summary>
        /// Adds a <see cref="Vector3"/> to the CVector.
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="triggerChanged">If false, the event Changed doesn't gets triggered </param>
        public void Add(Vector3 vector, bool triggerChanged = true)
        {
            Add(vector.X, vector.Y, vector.Z, triggerChanged);
        }

        /// <summary>
        /// Adds the values to the CVector.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <param name="triggerChanged">If false, the event Changed doesn't gets triggered </param>
        public void Add(float x, float y, float z, bool triggerChanged = true)
        {
            Z += z;
            base.Add(x, y, triggerChanged);
        }

        /// <summary>
        ///     Converts to <see cref="Vector3" />
        /// </summary>
        public static implicit operator Vector3(CVector3 vector)
        {
            return new Vector3(vector.X, vector.Y, vector.Z);
        }

        /// <summary>
        ///     Converts from <see cref="Vector3" /> to <see cref="CVector3" />.
        /// </summary>
        public static implicit operator CVector3(Vector3 vector)
        {
            return new CVector3(vector.X, vector.Y, vector.Z);
        }
    }
}