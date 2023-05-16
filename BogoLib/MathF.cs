using System;

namespace BogoLib;

public static class BogoMathF
{
    /// <summary>
    /// Returns the square root of a specified number.
    /// </summary>
    /// <param name="x">Number whose root you want to know</param>
    /// <returns>Returns the root if it is exact or -1 otherwise</returns>
    public static float Sqrt(float x)
    {
        if (x < 0)
            return float.NaN;

        int
            min = 0,
            max = (int)x;
        float
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

        for (int i = 10; i <= 1E9; i *= 10)
        {
            min = 0;
            max = 10;
            float randNum = 0;

            while (max - min != 1)
            {
                if (exp < x)
                    min = (int)randNum;
                else
                    max = (int)randNum;

                randNum = Random.Shared.Next(min, max);
                float aux = sqrt + randNum / i;
                exp = aux * aux;
            }

            sqrt += randNum / i;
        }

        return sqrt;
    }
}
