#region usings

using System;
using OpenTK;
using SM.Base;
using SM.Base.Scene;
using SM.Base.Types;
using SM.Base.Window;

#endregion

namespace SM2D.Scene
{
    /// <inheritdoc />
    public class Camera : GenericCamera
    {
        internal static int ResizeCounter = 0;
        internal static float Distance = 2F;

        private int _resizeCounter = 0;
        private bool _updateWorldScale = false;
        private Vector2? _requestedWorldScale = null;

        /// <summary>
        /// This vector allows to request a world scale.
        /// <para>Following cases are possible. ("not set" means 0)</para>
        /// <para>None is set: It takes the window size.</para>
        /// <para>X is set: Y get calculated by the aspect ratio of the window.</para>
        /// <para>Y is set: X get calculated by the aspect ratio of the window.</para>
        /// <para>Both are set: Now the system try to keep a (invisible) rectangle in view, by increasing the width or height of the view, if needed.</para>
        /// </summary>
        public Vector2? RequestedWorldScale
        {
            get => _requestedWorldScale;
            set
            {
                _requestedWorldScale = value;
                _updateWorldScale = true;
            }
        }

        /// <summary>
        /// Will always return a updated version of the world scale.
        /// </summary>
        public Vector2 CalculatedWorldScale
        {
            get
            {
                if (ResizeCounter != _resizeCounter || _updateWorldScale)
                {
                    CalculateWorldScale(SMRenderer.CurrentWindow);
                }

                return WorldScale;
            }
        }

        /// <summary>
        /// The world scale that got calculated.
        /// </summary>
        public Vector2 WorldScale { get; private set; } = Vector2.Zero;

        /// <summary>
        /// A event that gets triggered, when the world scale changed.
        /// <para>Possible causes: Window resizes, <see cref="RequestedWorldScale"/> changed</para>
        /// </summary>
        public event Action<Camera> WorldScaleChanged;

        /// <summary>
        /// The position of the camera.
        /// </summary>
        public CVector2 Position = new CVector2(0);

        /// <inheritdoc />
        public override bool Orthographic { get; } = true;

        /// <inheritdoc />
        protected override Matrix4 ViewCalculation(IGenericWindow window)
        {
            return Matrix4.LookAt(Position.X, Position.Y, Distance, Position.X, Position.Y, 0f, 0, 1, 0);
        }

        /// <inheritdoc />
        protected override bool WorldCalculation(IGenericWindow window, out Matrix4 world)
        {
            world = Matrix4.Identity;
            if (ResizeCounter != _resizeCounter || _updateWorldScale)
            {
                _updateWorldScale = false;
                _resizeCounter = ResizeCounter;
                CalculateWorldScale(window);

                world = Matrix4.CreateOrthographic(WorldScale.X, WorldScale.Y, .001f, 3.2f);
                return true;
            }

            return false;
        }

        /// <summary>
        /// This calculates the world scale.
        /// <para>Usually gets called, by the camera itself, but if you need the world scale for additional calculations, you can execute it by yourself.</para>
        /// </summary>
        /// <param name="window"></param>
        public void CalculateWorldScale(IGenericWindow window)
        {
            if (RequestedWorldScale.HasValue)
            {
                float aspect = window.AspectRatio;
                Vector2 requested = RequestedWorldScale.Value;

                if (requested.X > 0 && requested.Y > 0)
                {
                    float requestRatio = requested.X / requested.Y;

                    if (requestRatio > aspect) WorldScale = new Vector2(requested.X, requested.X / aspect);
                    else WorldScale = new Vector2(aspect * requested.Y, requested.Y);
                }
                else if (requested.X > 0) WorldScale = new Vector2(requested.X, requested.X / aspect);
                else if (requested.Y > 0) WorldScale = new Vector2(aspect * requested.Y, requested.Y);
            }
            else
            {
                WorldScale = window.WindowSize;
            }

            WorldScaleChanged?.Invoke(this);
        }
    }
}