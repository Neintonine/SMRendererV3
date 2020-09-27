using System;

namespace SM.Utility
{
    public class BezierCurve
    {
        public static float Calculate(float t, params float[] points)
        {
            int pointAmount = points.Length;
            int itterations = pointAmount - 1;
            
            double x = Math.Pow(1 - t, itterations) * points[0];
            for (int i = 1; i < itterations; i++)
            {
                if (i % 2 == 0) x += itterations * (1 - t) * Math.Pow(t, itterations - 1) * points[i];
                else x += itterations * Math.Pow(1 - t, itterations - 1) * t * points[i];
            }

            x += Math.Pow(t, itterations) * points[pointAmount - 1];
            return (float)x;
        }
    }
}