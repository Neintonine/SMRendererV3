using SM.Base.Time;

namespace SM.Base.Drawing.Particles
{
    /// <summary>
    ///     A context, with that the particle system sends the information for the movement function.
    /// </summary>
    public struct ParticleContext
    {
        /// <summary>
        /// The Timer of the particles
        /// </summary>
        public Timer Timer;
        /// <summary>
        /// The current speed of the particles.
        /// </summary>
        public float Speed;
    }
}