using System;
using OpenTK;
using OpenTK.Audio.OpenAL;
using OpenTK.Graphics.OpenGL;
using SM.Base.Animation;

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
        /// Interpolates the motion to the target.
        /// </summary>
        /// <param name="duration">How long the interpolation should take.</param>
        /// <param name="to">The value it should interpolate.</param>
        /// <param name="interpolationCurve">The curve how he interpolates. Preset values can be found under <see cref="AnimationCurves"/>. Default: <see cref="AnimationCurves.Linear"/></param>
        /// <param name="autoStart">Auto-starts the interpolation process.</param>
        /// <returns>A handle to control the interpolation process.</returns>
        public InterpolationProcess Interpolate<TInterpolateType>(TimeSpan duration, TInterpolateType to, BezierCurve? interpolationCurve = null, bool autoStart = true)
            where TInterpolateType : struct
        {
            return Interpolate<TInterpolateType>(duration, ConvertToVector4(), to, interpolationCurve, autoStart);
        }

        /// <summary>
        /// Interpolates the motion to the target.
        /// </summary>
        /// <param name="duration">How long the interpolation should take.</param>
        /// <param name="from">The value it should start with.</param>
        /// <param name="to">The value it should interpolate.</param>
        /// <param name="interpolationCurve">The curve how he interpolates. Preset values can be found under <see cref="AnimationCurves"/>. Default: <see cref="AnimationCurves.Linear"/></param>
        /// <param name="autoStart">Auto-starts the interpolation process.</param>
        /// <returns>A handle to control the interpolation process.</returns>
        public InterpolationProcess Interpolate<TInterpolateType>(TimeSpan duration, TInterpolateType from, TInterpolateType to, BezierCurve? interpolationCurve = null, bool autoStart = true)
            where TInterpolateType : struct
        {
            Vector4 start = from switch
            {
                float f => new Vector4(f, 0, 0, 0),
                Vector2 v2 => new Vector4(v2.X, v2.Y, 0, 0),
                Vector3 v3 => new Vector4(v3.X, v3.Y, v3.Z, 0),
                Vector4 v4 => v4,
                _ => throw new Exception("[INTERPOLATION] Only float, OpenTK.Vector2, OpenTK.Vector3, OpenTK.Vector4 are allowed as types.")
            };

            return Interpolate(duration, start, to, interpolationCurve, autoStart);
        }

        internal InterpolationProcess Interpolate<TInterpolateType>(TimeSpan duration, Vector4 from, TInterpolateType to, BezierCurve? interpolationCurve = null, bool autoStart = true)
            where TInterpolateType : struct
        {
            Vector4 target = to switch
            {
                float f => new Vector4(f, 0, 0, 0),
                Vector2 v2 => new Vector4(v2.X, v2.Y, 0, 0),
                Vector3 v3 => new Vector4(v3.X, v3.Y, v3.Z, 0),
                Vector4 v4 => v4,
                _ => throw new Exception("[INTERPOLATION] Only float, OpenTK.Vector2, OpenTK.Vector3, OpenTK.Vector4 are allowed as types.")
            };

            InterpolationProcess process = new InterpolationProcess(this, duration, from, target, interpolationCurve.GetValueOrDefault(AnimationCurves.Linear));
            if (autoStart) process.Start();

            return process;
        }

        /// <summary>
        /// Sets the values of the vector, by providing the values over an array.
        /// </summary>
        /// <param name="parameters"></param>
        public abstract void SetRaw(params float[] parameters);

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