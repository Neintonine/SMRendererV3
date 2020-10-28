using OpenTK;
using SM.Base.Types;

namespace SM.Base.Drawing.Particles
{
    /// <summary>
    ///     A particle...
    /// </summary>
    public struct ParticleStruct<TDirection>
        where TDirection : struct
    {
        /// <summary>
        ///     A direction, that the particle should travel.
        /// </summary>
        public TDirection Direction;
        /// <summary>
        ///     A matrix to store rotation and scale.
        /// </summary>
        public Matrix4 Matrix;
        /// <summary>
        ///     Speeeeeeeeeed
        /// </summary>
        public float Speed;
    }
}