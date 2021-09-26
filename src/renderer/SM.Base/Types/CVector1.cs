#region usings

using System;
using System.Runtime.InteropServices.WindowsRuntime;
using OpenTK;
using SM.Base.Animation;

#endregion

namespace SM.Base.Types
{
    /// <summary>
    ///     A One-dimensional Vector (also known as <see cref="float" />), in a class.
    /// </summary>
    public class CVector1 : CVectorBase
    {
        /// <summary>
        ///     Creates a class vector
        /// </summary>
        /// <param name="x">X-Component</param>
        public CVector1(float x)
        {
            X = x;
        }

        /// <summary>
        ///     X - Component
        /// </summary>
        public float X { get; set; }

        /// <summary>
        ///     Sets the X-Component.
        /// </summary>
        public virtual void Set(float uniform, bool triggerChanged = true)
        {
            X = uniform;
            if (triggerChanged) TriggerChanged();
        }

        /// <inheritdoc />
        public override void SetRaw(params float[] parameters)
        {
            X = parameters[0];
        }

        /// <summary>
        /// Adds the value to the components.
        /// </summary>
        public virtual void Add(float uniform, bool triggerChanged = true)
        {
            X += uniform;
            if (triggerChanged) TriggerChanged();
        }

        /// <summary>
        ///     Conversion from <see cref="float" /> to One-dimensional Vector.
        /// </summary>
        /// <returns></returns>
        protected override float GetLengthProcess()
        {
            return X * X;
        }

        /// <summary>
        /// Normalizes the vector.
        /// </summary>
        /// <param name="length"></param>
        protected override void NormalizationProcess(float length)
        {
            X *= length;
        }


        /// <inheritdoc />
        protected override Vector4 ConvertToVector4()
        {
            return new Vector4(X, 0, 0, 0);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return X.ToString();
        }

        /// <summary>
        ///     Conversion into <see cref="float" />
        /// </summary>
        public static implicit operator float(CVector1 vector1)
        {
            return vector1.X;
        }
    }
}