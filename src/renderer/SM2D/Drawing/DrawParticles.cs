using System;
using OpenTK;
using SM.Base.Drawing.Particles;
using SM.Base.Utility;
using SM2D.Types;

namespace SM2D.Drawing
{
    /// <summary>
    /// Creates particles.
    /// </summary>
    public class DrawParticles : ParticleDrawingBasis<Transformation, Vector2>
    {
        /// <inheritdoc />
        public override Func<ParticleInstance<Vector2>, Vector2> MovementCalculation { get; set; } = ParticleMovement.Default2D;

        /// <summary>
        /// The direction the particles should travel.
        /// </summary>
        public Vector2? Direction;

        /// <summary>
        /// The radius the direction is randomized
        /// </summary>
        public float DirectionRadius = 25;

        /// <inheritdoc />
        public DrawParticles(TimeSpan duration) : base(duration)
        {
        }

        /// <inheritdoc />
        protected override ParticleInstance<Vector2> CreateObject(int index)
        {
            Vector2 dir;
            if (Direction.HasValue)
            {
                Vector2 direction = Direction.Value;

                dir = (new Vector4(direction.X, direction.Y, 1, 1) *
                       Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(Randomize.GetFloat(-DirectionRadius / 2, DirectionRadius / 2)))).Xy;
            }
            else dir = new Vector2(Randomize.GetFloat(-1, 1), Randomize.GetFloat(-1, 1));

            var particle = new ParticleInstance<Vector2>()
            {
                Matrix = DetachedParticles ? Transform.InWorldSpace : Matrix4.CreateScale(1),

                Direction = dir,

                Speed = Randomize.GetFloat(MaxSpeed),
                StartLifetime = Lifetime - Randomize.GetFloat(LifetimeRandomize)
            };
            particle.Lifetime = particle.StartLifetime;

            return particle;
        }

        /// <inheritdoc />
        protected override Matrix4 CreateMatrix(ParticleInstance<Vector2> Struct, Vector2 direction)
        {
            Vector2 pos = direction;

            return Struct.Matrix * Matrix4.CreateTranslation(pos.X, pos.Y, 0);
        }
    }
}