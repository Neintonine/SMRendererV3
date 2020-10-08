using System;
using OpenTK;

namespace SM.Utility
{
    /// <summary>
    /// Utilitys for rotations
    /// </summary>
    public class RotationUtility
    {
        /// <summary>
        /// Angle towards an coordinate.
        /// </summary>
        /// <returns></returns>
        public static float TurnTowards(Vector2 origin, Vector2 target)
        {
            return MathHelper.RadiansToDegrees((float)Math.Atan2(target.Y - origin.Y, target.X - origin.X));
        }
    }
}