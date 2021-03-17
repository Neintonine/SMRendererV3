#region usings

using System;
using System.Collections.Generic;
using OpenTK;
using SM.Base.Scene;
using SM.Base.Time;
using SM.Base.Window;

#endregion

namespace SM.Base.Drawing.Particles
{
    /// <summary>
    ///     The (drawing) basis for particles
    /// </summary>
    public abstract class ParticleDrawingBasis<TTransform, TDirection> : DrawingBasis<TTransform>, IScriptable
        where TTransform : GenericTransformation, new()
        where TDirection : struct
    {
        /// <summary>
        ///     The amount of particles
        /// </summary>
        public int Amount = 32;

        /// <summary>
        ///     This contains the different instances for the particles.
        /// </summary>
        protected List<Instance> instances;

        /// <summary>
        ///     The maximum speed of the particles
        /// </summary>
        public float MaxSpeed = 1;

        /// <summary>
        ///     This contains all important information for each particle.
        /// </summary>
        protected ParticleStruct<TDirection>[] particleStructs;

        /// <summary>
        ///     The stopwatch of the particles.
        /// </summary>
        protected Timer timer;

        /// <summary>
        ///     Sets up the timer.
        /// </summary>
        /// <param name="duration">Duration how long the particles should live</param>
        protected ParticleDrawingBasis(TimeSpan duration)
        {
            timer = new Timer(duration);
        }

        /// <summary>
        ///     Get/Sets the state of pausing.
        /// </summary>
        public bool Paused
        {
            get => timer.Paused;
            set => timer.Paused = value;
        }

        /// <summary>
        ///     Controls the movement of each particles.
        /// </summary>
        public abstract Func<TDirection, ParticleContext, TDirection> MovementCalculation { get; set; }

        /// <inheritdoc />
        public bool UpdateActive { get; set; }

        /// <inheritdoc />
        public void Update(UpdateContext context)
        {
            if (!timer.Running) return;

            ParticleContext particleContext = new ParticleContext
            {
                Timer = timer
            };

            for (int i = 0; i < Amount; i++)
            {
                particleContext.Speed = particleStructs[i].Speed;
                instances[i].ModelMatrix = CreateMatrix(particleStructs[i],
                    MovementCalculation(particleStructs[i].Direction, particleContext));
            }
        }

        /// <summary>
        ///     Triggers the particles.
        /// </summary>
        public void Trigger()
        {
            timer.Start();

            CreateParticles();
        }

        /// <inheritdoc />
        protected override void DrawContext(ref DrawContext context)
        {
            if (!timer.Active) return;

            base.DrawContext(ref context);

            context.Instances = instances;

            context.Shader.Draw(context);
        }

        /// <summary>
        ///     Creates the particles.
        /// </summary>
        protected virtual void CreateParticles()
        {
            particleStructs = new ParticleStruct<TDirection>[Amount];
            instances = new List<Instance>();
            for (int i = 0; i < Amount; i++)
            {
                particleStructs[i] = CreateObject(i);

                instances.Add(new Instance());
            }
        }

        /// <summary>
        ///     Creates a particle.
        /// </summary>
        protected abstract ParticleStruct<TDirection> CreateObject(int index);

        /// <summary>
        ///     Generates the desired matrix for drawing.
        /// </summary>
        protected abstract Matrix4 CreateMatrix(ParticleStruct<TDirection> Struct, TDirection relativePosition);
    }
}