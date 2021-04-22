using System;
using System.Windows.Forms;
using OpenTK;
using SM.Base.Animation;

namespace SM.Base.Types
{
    /// <inheritdoc />
    public class CVector4 : CVector3
    {
        /// <summary>
        /// The W-component.
        /// </summary>
        public float W { get; set; }

        /// <inheritdoc />
        public CVector4(float uniform) : base(uniform)
        {
            W = uniform;
        }

        /// <inheritdoc />
        public CVector4(float x, float y, float z, float w) : base(x, y, z)
        {
            W = w;
        }

        /// <summary>
        /// Interpolates the motion to the target.
        /// </summary>
        /// <param name="duration">How long the interpolation should take.</param>
        /// <param name="to">The value it should interpolate.</param>
        /// <param name="interpolationCurve">The curve how he interpolates. Preset values can be found under <see cref="AnimationCurves"/>. Default: <see cref="AnimationCurves.Linear"/></param>
        /// <returns>A handle to control the interpolation process.</returns>
        public InterpolationProcess Interpolate(TimeSpan duration, Vector4 to, BezierCurve? interpolationCurve = null)
        {
            InterpolationProcess process = new InterpolationProcess(this, duration, ConvertToVector4(), to, interpolationCurve.GetValueOrDefault(AnimationCurves.Linear));
            process.Start();

            return process;
        }

        /// <inheritdoc />
        public override void Set(float uniform, bool triggerChanged = true)
        {
            W = uniform;
            base.Set(uniform, triggerChanged);
        }


        /// <summary>
        ///     Sets the a own value to each component.
        /// </summary>
        public void Set(float x, float y, float z, float w, bool triggerChanged = true)
        {
            W = w;
            base.Set(x, y, z, triggerChanged);
        }

        /// <summary>
        ///     Sets each component to the <see cref="Vector4" /> counter-part.
        /// </summary>
        public void Set(Vector4 vector, bool triggerChanged = true)
        {
            Set(vector.X, vector.Y, vector.Z, vector.W, triggerChanged);
        }

        /// <inheritdoc />
        public override void SetRaw(params float[] parameters)
        {
            base.SetRaw(parameters);
            W = parameters[3];
        }

        /// <inheritdoc />
        public override void Add(float uniform, bool triggerChanged = true)
        {
            W += uniform;
            base.Add(uniform, triggerChanged);
        }
        /// <summary>
        /// Adds a <see cref="Vector4"/> to the CVector.
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="triggerChanged">If false, the event Changed doesn't gets triggered </param>
        public void Add(Vector4 vector, bool triggerChanged = true)
        {
            Add(vector.X, vector.Y, vector.Z, vector.W, triggerChanged);
        }

        /// <summary>
        /// Adds the values to the CVector.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <param name="w"></param>
        /// <param name="triggerChanged">If false, the event Changed doesn't gets triggered </param>
        public void Add(float x, float y, float z, float w, bool triggerChanged = true)
        {
            W = w;
            base.Add(x, y, z, triggerChanged);
        }
        /// <inheritdoc />
        public override string ToString()
        {
            return "{" + X + "; " + Y + "; " + Z + "; " + W + "}";
        }

        /// <inheritdoc />
        protected override float GetLengthProcess()
        {
            return base.GetLengthProcess() + W * W;
        }

        /// <inheritdoc />
        protected override void NormalizationProcess(float length)
        {
            base.NormalizationProcess(length);
            W *= length;
        }

        /// <inheritdoc />
        protected override Vector4 ConvertToVector4()
        {
            return new Vector4(X, Y, Z, W);
        }

        /// <summary>
        /// Converts a <see cref="CVector4"/> into a <see cref="Vector4"/>.
        /// </summary>
        public static implicit operator Vector4(CVector4 vector) => new Vector4(vector.X, vector.Y, vector.Z, vector.W);
        /// <summary>
        /// Converts a <see cref="Vector4"/> into a <see cref="CVector4"/>.
        /// </summary>

        public static implicit operator CVector4(Vector4 vector) => new CVector4(vector.X, vector.Y, vector.Z, vector.W);
    }
}