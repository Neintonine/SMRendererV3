using OpenTK;

namespace SM.Base.Scene
{
    /// <summary>
    /// Contains methods for using transformations right.
    /// </summary>
    public abstract class GenericTransformation
    {
        /// <summary>
        /// Calculates the current matrix.
        /// </summary>
        /// <returns>The current matrix.</returns>
        public abstract Matrix4 GetMatrix();
    }
}