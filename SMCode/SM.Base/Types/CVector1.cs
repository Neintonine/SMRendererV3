using System;
using OpenTK;

namespace SM.Base.Types
{
    /// <summary>
    /// A One-dimensional Vector (also known as <see cref="float"/>), in a class.
    /// </summary>
    public class CVector1
    {
        /// <summary>
        /// X - Component
        /// </summary>
        public float X
        {
            get;
            set;
        }

        /// <summary>
        /// The length/magnitute of the vector.
        /// </summary>
        public float Length => GetLength();
        /// <summary>
        /// Gets the square of the vector length (magnitude).
        /// </summary>
        /// <remarks>
        /// This property avoids the costly square root operation required by the Length property. This makes it more suitable
        /// for comparisons.
        /// </remarks>
        public float LengthSquared => GetLength(true);

        public event Action Changed; 

        /// <summary>
        /// Creates a class vector
        /// </summary>
        /// <param name="x">X-Component</param>
        public CVector1(float x)
        {
            X = x;
        }

        

        /// <summary>
        /// Get the length of the vector.
        /// </summary>
        /// <param name="squared">If true, it will return the squared product.</param>
        /// <returns></returns>
        public float GetLength(bool squared = false)
        {
            float length = GetLengthProcess();
            if (squared) return length;
            return (float) Math.Sqrt(length);
        }


        /// <summary>
        /// Normalizes the vector.
        /// </summary>
        public void Normalize()
        {
            float length = GetLength();
            NormalizationProcess(length);
        }

        /// <summary>
        /// Sets the X-Component.
        /// </summary>
        /// <param name="x">X-Component</param>
        public virtual void Set(float uniform, bool triggerChanged = true)
        {
            X = uniform;
            if (triggerChanged) TriggerChanged();
        }

        public virtual void Add(float uniform, bool triggerChanged = true)
        {
            X += uniform;
            if (triggerChanged) TriggerChanged();
        }

        /// <summary>
        /// Conversion into <see cref="float"/>
        /// </summary>
        public static implicit operator float(CVector1 vector1) => vector1.X;
        /// <summary>
        /// Conversion from <see cref="float"/> to One-dimensional Vector.
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        //public static implicit operator CVector1(float f) => new CVector1(f);

        protected virtual float GetLengthProcess()
        {
            return X * X;
        }
        protected virtual void NormalizationProcess(float length)
        {
            X *= length;
        }

        protected void TriggerChanged()
        {
            Changed?.Invoke();
        }
    }
}