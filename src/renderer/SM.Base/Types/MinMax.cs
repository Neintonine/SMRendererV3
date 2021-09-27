using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Base.Types
{
    /// <summary>
    /// Structure to store Min and Max-values.
    /// </summary>
    public struct MinMax
    {
        /// <summary>
        /// Default Value: 0..1
        /// </summary>
        public static readonly MinMax Default = new MinMax(0, 1);

        /// <summary>
        /// Minimum Value
        /// </summary>
        public float Min;
        /// <summary>
        /// Maximum Value
        /// </summary>
        public float Max;

        /// <summary>
        /// Creates a MinMax-structure with two values.
        /// </summary>
        public MinMax(float min, float max)
        {
            Min = min;
            Max = max;
        }

        /// <summary>
        /// Get a value that is between <see cref="Min"/> and <see cref="Max"/> based on t [0..1]
        /// </summary>
        /// <param name="t"></param>
        public float GetPoint(float t)
        {
            return t * (Max - Min) + Min;
        }

        /// <summary>
        /// Converts to Vector2.
        /// </summary>
        /// <param name="v"></param>
        public static explicit operator Vector2(MinMax v) => new Vector2(v.Min, v.Max);
    }
}
