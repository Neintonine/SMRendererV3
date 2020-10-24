#region usings

using System.Collections.Generic;
using SM.Base.Contexts;

#endregion

namespace SM.Base.Time
{
    /// <summary>
    ///     Represents a stopwatch.
    /// </summary>
    public class Stopwatch
    {
        private static List<Stopwatch> _activeStopwatches = new List<Stopwatch>();

        /// <summary>
        ///     Contains how much time already has passed. (in seconds)
        /// </summary>
        public float Elapsed { get; private set; }

        /// <summary>
        ///     Starts the stopwatch.
        /// </summary>
        public virtual void Start()
        {
            _activeStopwatches.Add(this);
        }

        /// <summary>
        ///     Performs a tick.
        /// </summary>
        /// <param name="context"></param>
        private protected virtual void Tick(UpdateContext context)
        {
            Elapsed += context.Deltatime;
        }

        /// <summary>
        ///     Stops the stopwatch.
        /// </summary>
        public virtual void Stop()
        {
            _activeStopwatches.Remove(this);
        }

        /// <summary>
        ///     Resets the stopwatch.
        /// </summary>
        public void Reset()
        {
            Elapsed = 0;
        }

        internal static void PerformTicks(UpdateContext context)
        {
            for (var i = 0; i < _activeStopwatches.Count; i++) _activeStopwatches[i].Tick(context);
        }
    }
}