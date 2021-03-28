using System;
using OpenTK;
using SM.Base.Time;
using SM.Base.Types;
using SM.Base.Window;

namespace SM.Base.Animation
{
    /// <summary>
    /// A handle to control the interpolation process.
    /// </summary>
    public class InterpolationProcess : Timer
    {
        /// <summary>
        /// The CVector object, that is interpolated.
        /// </summary>
        public CVectorBase TargetVector { get; set; }

        /// <summary>
        /// From where the interpolation process started.
        /// </summary>
        public Vector4 From { get; }
        /// <summary>
        /// To where the interpolation is heading.
        /// </summary>
        public Vector4 To { get; }
        /// <summary>
        /// The direction towards the <see cref="To"/>
        /// </summary>
        public Vector4 Direction { get; }

        /// <summary>
        /// Gets/Sets the interpolation curve.
        /// </summary>
        public BezierCurve InterpolationCurve { get; set; }

        internal InterpolationProcess(CVectorBase targetVector, TimeSpan timeSpan, Vector4 from, Vector4 to, BezierCurve interpolationCurve) : base(timeSpan)
        {
            TargetVector = targetVector;

            From = from;
            To = to;
            InterpolationCurve = interpolationCurve;

            Direction = to - from;
        }

        /// <summary>
        /// Stops the interpolation process.
        /// <para>This keeps the state where the interpolation was.</para>
        /// </summary>
        public new void Stop()
        {
            Stop(true);
        }

        /// <summary>
        /// Stops the interplation process.
        /// </summary>
        /// <param name="keepState">If true, it will not set the state to the <see cref="To"/> state.</param>
        public void Stop(bool keepState)
        {
            if (Active && !keepState) SetTarget(To);

            base.Stop();

        }

        private protected override void Ticking(UpdateContext context)
        {
            base.Ticking(context);

            float posInCurve = InterpolationCurve.CalculatePoint(ElapsedNormalized).Y;
            Vector4 nextPos = From + (Direction * posInCurve);
            SetTarget(nextPos);
        }

        /// <inheritdoc />
        protected override void Stopping(UpdateContext context)
        {
            base.Stopping(context);
            SetTarget(To);
        }

        private void SetTarget(Vector4 vec)
        {
            TargetVector.Set(vec.X, vec.Y, vec.Z, vec.W);
        }
    }
}