using System;

namespace BogoLib;

public static class BogoMath
{
    /// <summary>
    /// Returns the square root of a specified number
    /// </summary>
    /// <param name="x">Value to calculate the square root</param>
    /// <returns>
    /// Values ​​referring to the table based on the parameter <paramref name="x" /><br /><br />
    /// <b>Is Positive</b> -- Square root positive or zero<br /><br />
    /// <b>Is Negative</b> -- <see cref="double.NaN" /><br /><br />
    /// <b>Equal <see cref="double.NaN" /></b> -- <see cref="double.NaN" /><br /><br />
    /// <b>Equal <see cref="double.PositiveInfinity" /></b> -- <see cref="double.PositiveInfinity" /><br /><br />
    /// </returns>
    public static double Sqrt(double x)
    {
        if (x < 0 || double.IsNaN(x))
            return double.NaN;
        
        if (x == double.PositiveInfinity)
            return double.PositiveInfinity;

        int
            min = 0,
            max = (int)x;
        double
            sqrt = Random.Shared.Next(max),
            exp = sqrt * sqrt;

        while (max - min != 1)
        {
            if (exp < x)
                min = (int)sqrt;
            else
                max = (int)sqrt;

            sqrt = Random.Shared.Next(min, max);
            exp = sqrt * sqrt;
        }

        for (long i = 10; i <= 1E17; i *= 10)
        {
            min = 0;
            max = 10;
            double randNum = 0;

            while (max - min != 1)
            {
                if (exp < x)
                    min = (int)randNum;
                else
                    max = (int)randNum;

                randNum = Random.Shared.Next(min, max);
                double aux = sqrt + randNum / i;
                exp = aux * aux;
            }

            sqrt += randNum / i;
        }

        return sqrt;
    }
}