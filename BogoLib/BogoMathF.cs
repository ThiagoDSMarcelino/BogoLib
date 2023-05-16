using System;

namespace BogoLib;

public static class BogoMathF
{
    /// <summary>
    /// Returns the square root of a specified number
    /// </summary>
    /// <param name="x">Value to calculate the square root</param>
    /// <returns>
    /// Values ​​referring to the table based on the parameter <paramref name="x" /><br /><br />
    /// <b>Is Positive</b> -- Square root positive or zero<br /><br />
    /// <b>Is Negative</b> -- <see cref="float.NaN" /><br /><br />
    /// <b>Equal <see cref="float.NaN" /></b> -- <see cref="float.NaN" /><br /><br />
    /// <b>Equal <see cref="float.PositiveInfinity" /></b> -- <see cref="float.PositiveInfinity" /><br /><br />
    /// </returns>
    public static float Sqrt(float x)
    {
        if (x < 0 || float.IsNaN(x))
            return float.NaN;

        if (x == float.PositiveInfinity)
            return float.PositiveInfinity;

        long
            min = 0,
            max = (long)x;

        float
            sqrt = Random.Shared.NextInt64(max),
            exp = sqrt * sqrt;

        while (max - min != 1)
        {
            if (exp < x)
                min = (int)sqrt;
            else
                max = (int)sqrt;

            sqrt = Random.Shared.NextInt64(min, max);
            exp = sqrt * sqrt;
        }

        for (int i = 10; i <= 1E9; i *= 10)
        {
            min = 0;
            max = 10;
            float randNum = 0;

            while (max - min != 1)
            {
                if (exp < x)
                    min = (long)randNum;
                else
                    max = (long)randNum;

                randNum = Random.Shared.NextInt64(min, max);
                float aux = sqrt + randNum / i;
                exp = aux * aux;
            }

            sqrt += randNum / i;
        }

        return sqrt;
    }
}