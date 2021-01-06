#region usings

using OpenTK;

#endregion

namespace SM.Base.Scene
{
    /// <summary>
    ///     Controller for a camera
    /// </summary>
    public abstract class GenericCamera
    {
        /// <summary>
        ///     The matrix for the orthographic world.
        /// </summary>
        public static Matrix4 OrthographicWorld { get; protected set; }

        /// <summary>
        ///     The matrix for the perspective world.
        /// </summary>
        public static Matrix4 PerspectiveWorld { get; protected set; }

        /// <summary>
        ///     This defines what is up. (Normalized)
        ///     <para>Default: <see cref="Vector3.UnitY" /></para>
        /// </summary>
        public static Vector3 UpVector { get; set; } = Vector3.UnitY;

        /// <summary>
        ///     Contains the view matrix of this camera.
        ///     <para>Default: <see cref="Matrix4.Identity" /></para>
        /// </summary>
        public Matrix4 ViewMatrix { get; protected set; } = Matrix4.Identity;

        /// <summary>
        ///     Returns the world matrix that is connected to this camera.
        /// </summary>
        public Matrix4 World => Orthographic ? OrthographicWorld : PerspectiveWorld;

        /// <summary>
        ///     Represents if the camera is orthographic.
        /// </summary>
        public abstract bool Orthographic { get; }

        public float Exposure = 1;

        /// <summary>
        ///     Calculates the view matrix.
        /// </summary>
        /// <returns>The calculated view matrix. Same as <see cref="ViewMatrix" /></returns>
        internal Matrix4 CalculateViewMatrix()
        {
            ViewMatrix = ViewCalculation();
            return ViewMatrix;
        }

        /// <summary>
        ///     This calculates the view matrix.
        /// </summary>
        /// <returns>
        ///     The new view matrix. This is the returns for <see cref="CalculateViewMatrix" /> and the next value for
        ///     <see cref="ViewMatrix" />.
        /// </returns>
        protected abstract Matrix4 ViewCalculation();

        /// <summary>
        ///     This will calculate the world.
        ///     <para>This is called on <see cref="GenericWindow{TScene,TCamera}.ViewportCamera" /> to calculate the world.</para>
        /// </summary>
        /// <param name="world">The world scale</param>
        /// <param name="aspect">The aspect ratio from the window.</param>
        public abstract void RecalculateWorld(Vector2 world, float aspect);
    }
}