#region usings

using OpenTK;

#endregion

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
        public static Vector2 Default2D(ParticleInstance<Vector2> particle)
        {
            return particle.Direction * ((particle.StartLifetime - particle.Lifetime) * particle.Speed);
        }

        /// <summary>
        ///     Default movement for 3D.
        /// </summary>
        public static Vector3 Default3D(ParticleInstance<Vector3> particle)
        {
            return particle.Direction * ((particle.StartLifetime - particle.Lifetime) * particle.Speed);
        }
    }
}