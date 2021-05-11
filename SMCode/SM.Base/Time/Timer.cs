﻿#region usings

using System;
using SM.Base.Window;

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
        public float Target { get; set; }

        /// <summary>
        ///     The already elapsed time but normalized to the target.
        /// </summary>
        public float ElapsedNormalized { get; private set; }

        /// <summary>
        ///     The event, that is triggered when the timer stops.
        /// </summary>
        public event Action<Timer, UpdateContext> End;

        /// <inheritdoc />
        public override void Start()
        {
            base.Start();
            Reset();
        }

        private protected override void Ticking(UpdateContext context)
        {
            base.Ticking(context);

            ElapsedNormalized = Math.Min(Elapsed / Target, 1);
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
        ///     This will trigger <see cref="End" />
        /// </summary>
        /// <param name="context"></param>
        protected void TriggerEndAction(UpdateContext context)
        {
            End?.Invoke(this, context);
        }
    }
}