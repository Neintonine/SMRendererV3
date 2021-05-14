#region usings

using System;
using System.Collections.Generic;
using OpenTK;
using SM.Base.Scene;
using SM.Base.Time;
using SM.Base.Window;
using Stopwatch = System.Diagnostics.Stopwatch;

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
        private float? _continuesIntervalSeconds = null;
        private Interval _continuesInterval;

        /// <summary>
        ///     The stopwatch of the particles.
        /// </summary>
        protected Timer timer;

        /// <summary>
        ///     This contains the different instances for the particles.
        /// </summary>
        protected List<ParticleInstance<TDirection>> instances;

        /// <summary>
        ///     The amount of particles
        /// </summary>
        public int Amount = 32;

        /// <summary>
        /// The base lifetime for particles in seconds.
        /// </summary>
        public float Lifetime;
        /// <summary>
        /// Randomizes the lifetime for particles.
        /// </summary>
        public float LifetimeRandomize = 0;

        /// <summary>
        /// If set to any value (except null), it will create the particles continuously.
        /// </summary>
        public float? ContinuousInterval
        {
            get => _continuesIntervalSeconds;
            set
            {
                if (value.HasValue)
                {
                    _continuesInterval.Target = value.Value;
                }

                _continuesIntervalSeconds = value;
            }
        }

        /// <summary>
        /// If true, the particles will spawn in Worldspace and can't be moved by the transformation.
        /// </summary>
        public bool DetachedParticles;

        /// <summary>
        ///     The maximum speed of the particles
        ///     <para>Default: 25</para>
        /// </summary>
        public float MaxSpeed = 25;

        /// <summary>
        ///     Sets up the timer.
        /// </summary>
        /// <param name="duration">Duration how long the particles should live</param>
        protected ParticleDrawingBasis(TimeSpan duration)
        {
            timer = new Timer(duration);
            _continuesInterval = new Interval(0);
            _continuesInterval.End += CreateContinuesParticles;

            Lifetime = (float) duration.TotalSeconds;
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
        public abstract Func<ParticleInstance<TDirection>, TDirection> MovementCalculation { get; set; }

        /// <inheritdoc />
        public bool UpdateActive { 
            get => timer.Active || _continuesInterval.Active; 
            set { return; } 
        }

        /// <inheritdoc />
        public void Update(UpdateContext context)
        {
            Stopwatch stp = new Stopwatch();
            stp.Start();
            for (int i = 0; i < instances.Count; i++)
            {
                instances[i].Lifetime -= context.Deltatime;
                if (instances[i].Lifetime <= 0)
                {
                    instances.Remove(instances[i]);
                    break;
                }

                instances[i].ModelMatrix = CreateMatrix(instances[i], MovementCalculation(instances[i]));
            }

            Console.WriteLine();
        }

        /// <summary>
        ///     Triggers the particles.
        /// </summary>
        public void Trigger()
        {
            instances = new List<ParticleInstance<TDirection>>();
            if (_continuesIntervalSeconds.HasValue)
            {
                _continuesInterval.Target = _continuesIntervalSeconds.Value;
                _continuesInterval.Start();

                return;
            }

            timer.Start();

            CreateParticles();
        }

        /// <summary>
        /// Stops the particles.
        /// </summary>
        public void Stop()
        {
            if (_continuesInterval.Active)
            {
                _continuesInterval.Stop();
            }

            timer.Stop();
        }

        /// <inheritdoc />
        public override void OnRemoved(object sender)
        {
            base.OnRemoved(sender);

            Stop();
        }

        /// <inheritdoc />
        protected override void DrawContext(ref DrawContext context)
        {
            if (!timer.Active && _continuesInterval != null && !_continuesInterval.Active) return;

            base.DrawContext(ref context);

            if (DetachedParticles) context.ModelMatrix = Matrix4.Identity;

            context.Instances = instances.ConvertAll(a => (Instance)a);

            Material.Draw(context);
        }

        /// <summary>
        ///     Creates the particles.
        /// </summary>
        protected virtual void CreateParticles()
        {
            

            for (int i = 0; i < Amount; i++)
            {
                instances.Add(CreateObject(i));
            }
        }

        private void CreateContinuesParticles(Timer arg1, UpdateContext arg2)
        {
            instances.Add(CreateObject(0));
        }

        /// <summary>
        ///     Creates a particle.
        /// </summary>
        protected abstract ParticleInstance<TDirection> CreateObject(int index);

        /// <summary>
        ///     Generates the desired matrix for drawing.
        /// </summary>
        protected abstract Matrix4 CreateMatrix(ParticleInstance<TDirection> Struct, TDirection relativePosition);
    }
}