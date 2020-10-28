#region usings

using System;
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
        private bool _paused = false;

        public bool Active { get; private set; } = false;

        public bool Paused
        {
            get => _paused;
            set
            {
                if (value)
                    Pause();
                else
                    Resume();
            }
        }

        public bool Running => Active && !Paused;

        /// <summary>
        ///     Contains how much time already has passed. (in seconds)
        /// </summary>
        public float Elapsed { get; protected set; }

        /// <summary>
        ///     Contains the TimeSpan of how much time already passed.
        /// </summary>
        public TimeSpan ElapsedSpan { get; protected set; }

        /// <summary>
        ///     Starts the stopwatch.
        /// </summary>
        public virtual void Start()
        {
            if (Active) return;

            _activeStopwatches.Add(this);
            Active = true;
        }

        

        /// <summary>
        ///     Performs a tick.
        /// </summary>
        /// <param name="context"></param>
        private protected virtual void Tick(UpdateContext context)
        {
            Elapsed += context.Deltatime;
            ElapsedSpan = TimeSpan.FromSeconds(Elapsed);
        }

        /// <summary>
        /// Resumes the timer.
        /// </summary>
        protected virtual void Resume()
        {
            _paused = false;
        }
        
        /// <summary>
        /// Pauses the timer.
        /// </summary>
        protected virtual void Pause()
        {
            _paused = true;
        }

        /// <summary>
        ///     Stops the stopwatch.
        /// </summary>
        public virtual void Stop()
        {
            if (!Active) return;

            Active = false;
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
            for (var i = 0; i < _activeStopwatches.Count; i++)
            {
                if (_activeStopwatches[i].Paused) continue;
                _activeStopwatches[i].Tick(context);
            }
        }
    }
}