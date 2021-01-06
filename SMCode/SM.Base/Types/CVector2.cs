using OpenTK;

namespace SM.Base.Types
{
    /// <summary>
    /// A two-dimensional vector.
    /// </summary>
    public class CVector2 : CVector1
    {
        /// <summary>
        /// Y-component
        /// </summary>
        public float Y { get; set; }

        /// <summary>
        /// Creates a vector, where each component is the same value.
        /// </summary>
        /// <param name="uniform">The Value</param>
        public CVector2(float uniform) : base(uniform)
        {
            Y = uniform;
        }

        /// <summary>
        /// Creates a vector
        /// </summary>
        public CVector2(float x, float y) : base(x)
        {
            Y = y;
        }

        private protected override float GetLengthProcess()
        {
            return base.GetLengthProcess() + Y * Y;
        }

        private protected override void NormalizationProcess(float length)
        {
            base.NormalizationProcess(length);
            Y *= length;
        }

        /// <summary>
        /// Sets each component to the same value
        /// </summary>
        /// <param name="uniform"></param>
        public override void Set(float uniform)
        {
            base.Set(uniform);
            Y = uniform;
        }

        /// <summary>
        /// Sets each component to the <see cref="Vector2"/> counter-part.
        /// </summary>
        /// <param name="vector"></param>
        public void Set(Vector2 vector)
        {
            Set(vector.X, vector.Y);
        }

        /// <summary>
        /// Sets the a own value to each component.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void Set(float x, float y)
        {
            base.Set(x);
            Y = y;
        }

        public override void Add(float uniform)
        {
            base.Add(uniform);
            Y += uniform;
        }

        public void Add(Vector2 vector)
        {
            Add(vector.X, vector.Y);
        }

        public void Add(float x, float y)
        {
            base.Add(x);
            Y += y;
        }

        /// <summary>
        /// Converts to <see cref="Vector2"/>
        /// </summary>
        public static implicit operator Vector2(CVector2 vector2) => new Vector2(vector2.X, vector2.Y);
        /// <summary>
        /// Converts from <see cref="Vector2"/> to <see cref="CVector2"/>.
        /// </summary>
        public static implicit operator CVector2(Vector2 vector2) => new CVector2(vector2.X, vector2.Y);
    }
}