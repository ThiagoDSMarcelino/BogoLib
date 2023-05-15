using System;

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
        if (x < 0) return double.NaN;

        int
            min = 0,
            max = (int)x;
        double
            sqrt = Random.Shared.Next(max),
            exp = sqrt * sqrt,
            aux = 0;
        bool
            flag = true;

        while (exp != x)
        {
            if (max - min == 1) flag = false;
            else if (exp < x) min = (int)sqrt;
            else max = (int)sqrt;

            if (flag)
            {
                sqrt = Random.Shared.Next(min, max);
                exp = sqrt * sqrt;
            }
            else
            {
                aux = sqrt + Random.Shared.NextDouble();
                exp = aux * aux;
            }
        }

        sqrt = aux;
        return sqrt;
    }
}
