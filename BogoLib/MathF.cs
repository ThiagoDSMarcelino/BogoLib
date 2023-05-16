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

        float r = 1;
        if (x > Int32.MaxValue)
            r = x / Int32.MaxValue;

        int
            min = 0,
            max = (Int32)(x / r);
        float
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

        for (int i = 10; i <= 1E9; i *= 10)
        {
            min = 0;
            max = 10;
            float randNum = 0;

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
                float aux = sqrt + randNum / i;
                exp = aux * aux;
            }

            sqrt += randNum / i;
        }

        return sqrt;
    }
}
