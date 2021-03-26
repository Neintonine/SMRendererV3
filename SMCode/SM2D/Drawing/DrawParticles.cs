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
        public override Func<Vector2, ParticleContext, Vector2> MovementCalculation { get; set; } = ParticleMovement.Default2D;

        /// <summary>
        /// The direction the particles should travel.
        /// </summary>
        public Vector2? Direction;

        /// <inheritdoc />
        public DrawParticles(TimeSpan duration) : base(duration)
        {
        }

        /// <inheritdoc />
        protected override ParticleStruct<Vector2> CreateObject(int index)
        {
            return new ParticleStruct<Vector2>()
            {
                Matrix = Matrix4.CreateScale(1),
                Direction = Direction.GetValueOrDefault(new Vector2(Randomize.GetFloat(-1, 1), Randomize.GetFloat(-1, 1))),
                Speed = Randomize.GetFloat(MaxSpeed)
            };
        }

        /// <inheritdoc />
        protected override Matrix4 CreateMatrix(ParticleStruct<Vector2> Struct, Vector2 direction)
        {
            return Struct.Matrix * Matrix4.CreateTranslation(direction.X, direction.Y, 0);
        }
    }
}