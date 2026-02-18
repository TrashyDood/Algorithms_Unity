using System;
using UnityEngine;

public static class MathUtils
{
    #region Arithmetic
    public static double Oscillate(double frequency, double x) =>
        Math.Sin(2 * Mathf.PI * x * frequency);

    public static double Oscillate(double frequency, double x, double epsilon) =>
        FloorToNearest(Oscillate(frequency, x), epsilon);

    public static float Oscillate(float frequency, float x) =>
        MathF.Sin(2 * Mathf.PI * frequency * x);

    public static float Oscillate(float frequency, float x, float epsilon) =>
        FloorToNearest(Oscillate(frequency, x), epsilon);

    public static float OscillateDamped(float frequency, float t)
    {
        t = (t < 0) ? 0 : (t > 1) ? 1 : t;
        return Oscillate(frequency, t) * (1f - t);
    }

    public static float RoundToNearest(float value, float increment) =>
        MathF.Round(value / increment) * increment;

    public static float FloorToNearest(float value, float increment) =>
        (int)(value / increment) * increment;

    public static double RoundToNearest(double value, double increment) =>
        Math.Round(value / increment) * increment;

    public static double FloorToNearest(double value, double increment) =>
        ((int)(value / increment)) * increment;
    #endregion
}