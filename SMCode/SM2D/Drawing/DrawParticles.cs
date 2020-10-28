using System;
using OpenTK;
using SM.Base.Drawing.Particles;
using SM.Utility;
using SM2D.Scene;
using SM2D.Types;

namespace SM2D.Drawing
{
    public class DrawParticles : ParticleDrawingBasis<Transformation, Vector2>, I2DShowItem
    {
        public int ZIndex { get; set; }
        public override Func<Vector2, ParticleContext, Vector2> MovementCalculation { get; set; } = ParticleMovement.Default2D;

        public DrawParticles(TimeSpan duration) : base(duration)
        {
        }

        protected override ParticleStruct<Vector2> CreateObject(int index)
        {
            return new ParticleStruct<Vector2>()
            {
                Matrix = Matrix4.CreateScale(1),
                Direction = new Vector2(Randomize.GetFloat(-1, 1), Randomize.GetFloat(-1, 1)),
                Speed = Randomize.GetFloat(MaxSpeed)
            };
        }

        protected override Matrix4 CreateMatrix(ParticleStruct<Vector2> Struct, Vector2 direction)
        {
            return Struct.Matrix * Matrix4.CreateTranslation(direction.X, direction.Y, 0);
        }
    }
}