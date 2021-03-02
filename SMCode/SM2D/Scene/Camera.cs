#region usings

using System;
using OpenTK;
using SM.Base;
using SM.Base.Scene;
using SM.Base.Types;
using SM.Base.Windows;

#endregion

namespace SM2D.Scene
{
    public class Camera : GenericCamera
    {
        internal static int ResizeCounter = 0;

        private int _resizeCounter = 0;
        private bool _updateWorldScale = false;
        private Vector2? _requestedWorldScale = null;

        public Vector2? RequestedWorldScale
        {
            get => _requestedWorldScale;
            set
            {
                _requestedWorldScale = value;
                _updateWorldScale = true;
            }
        }

        public Vector2 WorldScale { get; private set; } = Vector2.Zero;

        public event Action<Camera> WorldScaleChanged;

        public CVector2 Position = new CVector2(0);
        public override bool Orthographic { get; } = true;

        protected override Matrix4 ViewCalculation(IGenericWindow window)
        {
            return Matrix4.LookAt(Position.X, Position.Y, 1, Position.X, Position.Y, 0, 0, 1, 0);
        }

        protected override bool WorldCalculation(IGenericWindow window, out Matrix4 world)
        {
            world = Matrix4.Identity;
            if (ResizeCounter != _resizeCounter || _updateWorldScale)
            {
                _updateWorldScale = false;
                _resizeCounter = ResizeCounter;
                CalculateWorldScale(window);

                world = Matrix4.CreateOrthographic(WorldScale.X, WorldScale.Y, .0001f, 1.5f);
                return true;
            }

            return false;
        }

        public void CalculateWorldScale(IGenericWindow window)
        {
            if (RequestedWorldScale.HasValue)
            {
                float aspect = window.Width > window.Height ? window.AspectRatio : window.AspectRatioReverse;
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