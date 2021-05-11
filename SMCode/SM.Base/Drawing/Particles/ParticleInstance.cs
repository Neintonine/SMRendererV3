using OpenTK;

namespace SM.Base.Drawing.Particles
{
    public class ParticleInstance : Instance
    {
        public float StartLifetime = 0;

        /// <summary>
        ///     The lifetime this particular particle still has. 
        /// </summary>
        public float Lifetime = 0;

        /// <summary>
        ///     A additional matrix to store rotation and scale.
        /// </summary>
        public Matrix4 Matrix;

        /// <summary>
        ///     Speeeeeeeeeed
        /// </summary>
        public float Speed;
    }

    public class ParticleInstance<TValue> : ParticleInstance
        where TValue : struct
    {
        /// <summary>
        ///     A direction, that the particle should travel.
        /// </summary>
        public TValue Direction;

        /// <summary>
        ///     The start position.
        /// </summary>
        public TValue StartPosition;
    }
}