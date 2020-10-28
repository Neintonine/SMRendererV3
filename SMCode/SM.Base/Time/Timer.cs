#region usings

using System;
using SM.Base.Contexts;

#endregion

namespace SM.Base.Time
{
    /// <summary>
    ///     Timer-System
    /// </summary>
    public class Timer : Stopwatch
    {
        /// <summary>
        ///     Creates a timer with specified seconds.
        /// </summary>
        /// <param name="seconds"></param>
        public Timer(float seconds)
        {
            Target = seconds;
        }

        /// <summary>
        ///     Creates a timer with a time span.
        /// </summary>
        /// <param name="timeSpan"></param>
        public Timer(TimeSpan timeSpan)
        {
            Target = (float) timeSpan.TotalSeconds;
        }

        /// <summary>
        ///     The target time in seconds.
        /// </summary>
        public float Target { get; private set; }

        /// <summary>
        ///     The already elapsed time but normalized to the target.
        /// </summary>
        public float ElapsedNormalized { get; private set; }

        /// <summary>
        ///     The event, that is triggered when the timer stops.
        /// </summary>
        public event Action<Timer, UpdateContext> EndAction;

        /// <inheritdoc />
        public override void Start()
        {
            base.Start();
            Reset();
        }

        private protected override void Tick(UpdateContext context)
        {
            base.Tick(context);

            ElapsedNormalized = Elapsed / Target;
            if (ElapsedNormalized >= 1) Stopping(context);
        }

        /// <summary>
        ///     Occurs, when the timer tries to stop.
        /// </summary>
        protected virtual void Stopping(UpdateContext context)
        {
            TriggerEndAction(context);
            Stop();
        }

        /// <summary>
        ///     This will trigger <see cref="EndAction" />
        /// </summary>
        /// <param name="context"></param>
        protected void TriggerEndAction(UpdateContext context)
        {
            EndAction?.Invoke(this, context);
        }
    }
}