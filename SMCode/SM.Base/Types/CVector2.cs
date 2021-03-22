#region usings

using OpenTK;

#endregion

namespace SM.Base.Types
{
    /// <summary>
    ///     A two-dimensional vector.
    /// </summary>
    public class CVector2 : CVector1
    {
        /// <summary>
        ///     Creates a vector, where each component is the same value.
        /// </summary>
        /// <param name="uniform">The Value</param>
        public CVector2(float uniform) : base(uniform)
        {
            Y = uniform;
        }

        /// <summary>
        ///     Creates a vector
        /// </summary>
        public CVector2(float x, float y) : base(x)
        {
            Y = y;
        }

        /// <summary>
        ///     Y-component
        /// </summary>
        public float Y { get; set; }

        /// <inheritdoc />
        protected override float GetLengthProcess()
        {
            return base.GetLengthProcess() + Y * Y;
        }

        /// <inheritdoc />
        protected override void NormalizationProcess(float length)
        {
            base.NormalizationProcess(length);
            Y *= length;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return "{"+X+"; "+Y+"}";
        }

        /// <summary>
        ///     Sets each component to the same value
        /// </summary>
        public override void Set(float uniform, bool triggerChanged = true)
        {
            Y = uniform;
            base.Set(uniform, triggerChanged);
        }

        /// <summary>
        ///     Sets each component to the <see cref="Vector2" /> counter-part.
        /// </summary>
        public void Set(Vector2 vector, bool triggerChanged = true)
        {
            Set(vector.X, vector.Y, triggerChanged);
        }

        /// <summary>
        ///     Sets the a own value to each component.
        /// </summary>
        public void Set(float x, float y, bool triggerChanged = true)
        {
            Y = y;
            base.Set(x, triggerChanged);
        }

        /// <inheritdoc />
        public override void Add(float uniform, bool triggerChanged = true)
        {
            Y += uniform;
            base.Add(uniform, triggerChanged);
        }

        /// <summary>
        /// Adds <see cref="Vector2"/> to the CVector.
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="triggerChanged">If false, the event Changed doesn't gets triggered </param>
        public void Add(Vector2 vector, bool triggerChanged = true)
        {
            Add(vector.X, vector.Y, triggerChanged);
        }

        /// <summary>
        /// Adds the values to the CVector.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="triggerChanged">If false, the event Changed doesn't gets triggered </param>
        public void Add(float x, float y, bool triggerChanged = true)
        {
            Y += y;
            base.Add(x, triggerChanged);
        }

        /// <summary>
        ///     Converts to <see cref="Vector2" />
        /// </summary>
        public static implicit operator Vector2(CVector2 vector2)
        {
            return new Vector2(vector2.X, vector2.Y);
        }

        /// <summary>
        ///     Converts from <see cref="Vector2" /> to <see cref="CVector2" />.
        /// </summary>
        public static implicit operator CVector2(Vector2 vector2)
        {
            return new CVector2(vector2.X, vector2.Y);
        }
    }
}