using System;
using OpenTK;

namespace SM.Base.Types
{
    /// <summary>
    /// Basis for the CVector classes
    /// </summary>
    public abstract class CVectorBase
    {
        /// <summary>
        /// This event triggers when a component changed.
        /// </summary>
        public event Action Changed;

        /// <summary>
        ///     The length/magnitute of the vector.
        /// </summary>
        public float Length => GetLength();

        /// <summary>
        ///     Gets the square of the vector length (magnitude).
        /// </summary>
        /// <remarks>
        ///     This property avoids the costly square root operation required by the Length property. This makes it more suitable
        ///     for comparisons.
        /// </remarks>
        public float LengthSquared => GetLength(true);
        
        /// <summary>
        ///     Get the length of the vector.
        /// </summary>
        /// <param name="squared">If true, it will return the squared product.</param>
        /// <returns></returns>
        public float GetLength(bool squared = false)
        {
            float length = GetLengthProcess();
            if (squared) return length;
            return (float)Math.Sqrt(length);
        }

        /// <summary>
        ///     Normalizes the vector.
        /// </summary>
        public void Normalize()
        {
            float length = GetLength();
            NormalizationProcess(length);
        }

        /// <summary>
        /// Sets the values of the vector, by providing the values over an array.
        /// </summary>
        /// <param name="parameters"></param>
        public abstract void Set(params float[] parameters);

        /// <summary>
        /// This triggers the <see cref="Changed"/> event.
        /// </summary>
        protected void TriggerChanged()
        {
            Changed?.Invoke();
        }

        /// <summary>
        ///     Conversion from <see cref="float" /> to One-dimensional Vector.
        /// </summary>
        /// <returns></returns>
        protected abstract float GetLengthProcess();

        /// <summary>
        /// Normalizes the vector.
        /// </summary>
        /// <param name="length"></param>
        protected abstract void NormalizationProcess(float length);

        /// <summary>
        /// Converts the vector to a <see cref="Vector4"/>
        /// </summary>
        /// <returns></returns>
        protected abstract Vector4 ConvertToVector4();
    }
}