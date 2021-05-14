#region usings

using System;
using OpenTK;

#endregion

namespace SM.Base.Utility
{
    /// <summary>
    ///     Utilitys for rotations
    /// </summary>
    public class RotationUtility
    {
        /// <summary>
        ///     Angle towards an coordinate.
        /// </summary>
        /// <returns></returns>
        public static float TurnTowards(Vector2 origin, Vector2 target)
        {
            return MathHelper.RadiansToDegrees((float) Math.Atan2(origin.X - target.X, target.Y - origin.Y));
        }
    }
}