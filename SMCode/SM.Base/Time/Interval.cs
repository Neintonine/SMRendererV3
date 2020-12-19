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
            Reset();
        }
    }
}