using System;

namespace SM.Utility
{
    public class Randomize
    {
        public static Random Randomizer = new Random();

        public static void SetSeed(int seed) { Randomizer = new Random(seed); }

        public static bool GetBool(float tolerance) { return Randomizer.NextDouble() > tolerance; }

        public static int GetInt() { return Randomizer.Next(); }
        public static int GetInt(int max) { return Randomizer.Next(max); }
        public static int GetInt(int min, int max) { return Randomizer.Next(min, max); }

        public static float GetFloat() { return (float)Randomizer.NextDouble(); }
        public static float GetFloat(float max) { return (float)Randomizer.NextDouble() * max; }
        public static float GetFloat(float min, float max) { return (float)Randomizer.NextDouble() * max + min; }

    }
}