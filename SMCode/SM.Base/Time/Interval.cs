#region usings

using System;
using SM.Base.Contexts;

#endregion

namespace SM.Base.Time
{
    /// <summary>
    ///     Performs intervals.
    /// </summary>
    public class Interval : Timer
    {
        private bool _stop;

        /// <inheritdoc />
        public Interval(float seconds) : base(seconds)
        {
        }

        /// <inheritdoc />
        public Interval(TimeSpan timeSpan) : base(timeSpan)
        {
        }

        /// <inheritdoc />
        protected override void Stopping(UpdateContext context)
        {
            TriggerEndAction(context);
            if (_stop)
                base.Stop();
            else Reset();
        }

        /// <summary>
        ///     This will tell the interval to stop after the next iteration.
        ///     <para>To stop immediately use <see cref="Cancel" /></para>
        /// </summary>
        public override void Stop()
        {
            _stop = true;
        }

        /// <summary>
        ///     This will stop the interval immediately.
        /// </summary>
        public void Cancel()
        {
            base.Stop();
        }
    }
}