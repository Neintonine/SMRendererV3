#region usings

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

#endregion

namespace SM.Utility
{
    /// <summary>
    ///     A global helper class for randomization.
    /// </summary>
    public static class Randomize
    {
        /// <summary>
        ///     The randomizer.
        /// </summary>
        public static Random Randomizer = new Random();

        /// <summary>
        ///     Sets the seed for the randomizer.
        /// </summary>
        /// <param name="seed">The specified seed.</param>
        public static void SetSeed(int seed)
        {
            Randomizer = new Random(seed);
        }

        /// <summary>
        ///     Generates a double and checks if its under the tolerance.
        /// </summary>
        public static bool GetBool(float tolerance)
        {
            return Randomizer.NextDouble() < tolerance;
        }

        /// <summary>
        ///     Generates a integer.
        /// </summary>
        public static int GetInt()
        {
            return Randomizer.Next();
        }

        /// <summary>
        ///     Generates a integer with a maximum.
        /// </summary>
        public static int GetInt(int max)
        {
            return Randomizer.Next(max);
        }

        /// <summary>
        ///     Generates a integer with a minimum and maximum
        /// </summary>
        public static int GetInt(int min, int max)
        {
            return Randomizer.Next(min, max);
        }

        /// <summary>
        ///     Generates a float between 0 and 1.
        /// </summary>
        public static float GetFloat()
        {
            return (float) Randomizer.NextDouble();
        }

        /// <summary>
        ///     Generates a float between 0 and the specified maximum.
        /// </summary>
        public static float GetFloat(float max)
        {
            return (float) Randomizer.NextDouble() * max;
        }

        /// <summary>
        ///     Generates a float between the specified minimum and the specified maximum.
        /// </summary>
        public static float GetFloat(float min, float max)
        {
            return (float) Randomizer.NextDouble() * (max - min) + min;
        }

        public static TSource GetRandomItem<TSource>(this IList<TSource> list)
        {
            return list[GetInt(0, list.Count - 1)];
        }
    }
}