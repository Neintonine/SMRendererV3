#region usings

using OpenTK;

#endregion

namespace SM.Base.Drawing
{
    /// <summary>
    ///     Contains methods for using transformations right.
    /// </summary>
    public abstract class GenericTransformation
    {
        /// <summary>
        ///     Contains the current model matrix.
        /// </summary>
        protected Matrix4 _modelMatrix { get; private set; }

        /// <summary>
        ///     Contains the last frame the matrix was calculated.
        /// </summary>
        protected ulong _lastFrame { get; private set; }

        /// <summary>
        ///     Returns the current model matrix.
        /// </summary>
        /// <returns></returns>
        public Matrix4 GetMatrix()
        {
            if (_lastFrame != SMRenderer.CurrentFrame)
            {
                _lastFrame = SMRenderer.CurrentFrame;
                _modelMatrix = RequestMatrix();
            }

            return _modelMatrix;
        }

        /// <summary>
        ///     Calculates the current matrix.
        /// </summary>
        /// <returns>The current matrix.</returns>
        protected abstract Matrix4 RequestMatrix();
    }
}