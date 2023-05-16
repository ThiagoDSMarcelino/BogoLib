using System;
using System.Runtime.Intrinsics.X86;

namespace BogoLib;

public static class BogoMath
{
    /// <summary>
    /// Returns the square root of a specified number.
    /// </summary>
    /// <param name="x">Number whose root you want to know</param>
    /// <returns>Returns the root if it is exact or -1 otherwise</returns>
    public static double Sqrt(double x)
    {
        if (x < 0)
            return double.NaN;

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
