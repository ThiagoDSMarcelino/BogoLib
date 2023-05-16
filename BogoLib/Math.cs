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

        double r = 1;
        if (x > Int32.MaxValue)
            r = x / Int32.MaxValue;

        int
            min = 0,
            max = (Int32)(x / r);
        double
            sqrt = Random.Shared.Next(max) * r,
            exp = sqrt * sqrt;

        while (max - min != 1)
        {
            if (exp < x)
            {
                if (sqrt > Int32.MaxValue)
                    r = sqrt / Int32.MaxValue;
                else
                    r = 1;

                min = (Int32)(sqrt / r);
            }
            else
            {
                if (sqrt > Int32.MaxValue)
                    r = sqrt / Int32.MaxValue;
                else
                    r = 1;
                    
                max = (Int32)(sqrt / r);
            }

            sqrt = Random.Shared.Next(min, max) * r;
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
                {
                    if (randNum > Int32.MaxValue)
                        r = randNum / Int32.MaxValue;
                    else
                        r = 1;

                    min = (Int32)(randNum / r);
                }
                else
                {
                    if (randNum > Int32.MaxValue)
                        r = randNum / Int32.MaxValue;
                    else
                        r = 1;

                    max = (Int32)(randNum / r);
                }

                randNum = Random.Shared.Next(min, max) * r;
                double aux = sqrt + randNum / i;
                exp = aux * aux;
            }

            sqrt += randNum / i;
        }

        return sqrt;
    }
}
