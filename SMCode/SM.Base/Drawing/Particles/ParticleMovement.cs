using OpenTK;

namespace SM.Base.Drawing.Particles
{
    /// <summary>
    ///     Contains methods for particle movements.
    /// </summary>
    public class ParticleMovement
    {
        /// <summary>
        ///     Default movement for 2D.
        /// </summary>
        public static Vector2 Default2D(Vector2 direction, ParticleContext context) => direction * (context.Timer.Elapsed * context.Speed);
        /// <summary>
        ///     Default movement for 3D.
        /// </summary>
        public static Vector3 Default3D(Vector3 direction, ParticleContext context) => direction * (context.Timer.Elapsed * context.Speed);
    }
}