#region usings

using OpenTK;
using SM.Base.Windows;

#endregion

namespace SM.Base.Scene
{
    /// <summary>
    ///     Controller for a camera
    /// </summary>
    public abstract class GenericCamera
    {
        /// <summary>
        ///     This defines what is up. (Normalized)
        ///     <para>Default: <see cref="Vector3.UnitY" /></para>
        /// </summary>
        public Vector3 UpVector { get; set; } = Vector3.UnitY;

        /// <summary>
        ///     Returns the world matrix that is connected to this camera.
        /// </summary>
        public Matrix4 World { get; protected set; }

        /// <summary>
        ///     Contains the view matrix of this camera.
        ///     <para>Default: <see cref="Matrix4.Identity" /></para>
        /// </summary>
        public Matrix4 View { get; protected set; } = Matrix4.Identity;

        /// <summary>
        ///     Represents if the camera is orthographic.
        /// </summary>
        public abstract bool Orthographic { get; }

        /// <summary>
        ///     Exposure defines the exposure to the Scene.
        /// </summary>
        public float Exposure = 1;

        /// <summary>
        ///     Calculates the view matrix.
        /// </summary>
        /// <returns>The calculated view matrix. Same as <see cref="View" /></returns>
        internal void CalculateViewMatrix(IGenericWindow window)
        {
            View = ViewCalculation(window);
            if (WorldCalculation(window, out Matrix4 world))
            {
                World = world;
            }
        }

        /// <summary>
        ///     This calculates the view matrix.
        /// </summary>
        /// <returns>
        ///     The new view matrix. This is the returns for <see cref="CalculateViewMatrix" /> and the next value for
        ///     <see cref="View" />.
        /// </returns>
        protected abstract Matrix4 ViewCalculation(IGenericWindow window);

        protected abstract bool WorldCalculation(IGenericWindow window, out Matrix4 world);
    }
}