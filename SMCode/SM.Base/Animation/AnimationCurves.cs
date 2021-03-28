using OpenTK;

namespace SM.Base.Animation
{
    /// <summary>
    /// Preset Animation curves.
    /// </summary>
    public class AnimationCurves
    {
        /// <summary>
        /// Linear Curve
        /// </summary>
        public static readonly BezierCurve Linear = new BezierCurve(Vector2.Zero, Vector2.One);

        /// <summary>
        /// Smooth curve
        /// </summary>
        public static readonly BezierCurve Smooth = new BezierCurve(Vector2.Zero, new Vector2(.5f, 0), new Vector2(.5f, 1), Vector2.One);
    }
}