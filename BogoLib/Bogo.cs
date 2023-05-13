using System;

namespace BogoLib;

/// <summary>
/// Represents a collection of bogo algorithms 
/// </summary>
public static class Bogo
{
    public static (int X, int Y)[] BogoConvexHull(this (int X, int Y)[] S)
    {
        if (S.Length < 4) return S;

        Random rand = Random.Shared;
        int n = rand.Next(3, S.Length);
        var result = new (int X, int Y)[n];
        bool[] indexesUsed = new bool[S.Length];
        bool isCorrect = false;

        while (!isCorrect)
        {
            for (int i = 0; i < n; i++)
            {
                int randomIndex = rand.Next(S.Length);

                while (indexesUsed[randomIndex])
                    randomIndex = rand.Next(S.Length);

                indexesUsed[randomIndex] = true;

                result[i] = S[randomIndex];
            }

            Console.WriteLine(CheckPoint(result, S));

            isCorrect = true;
        }

        return result;
    }

    private static bool CheckPoint((int X, int Y)[] borderPoint, (int X, int Y)[]  allPoints)
    {
        int area = 0;


        foreach (var point in borderPoint)
        {
            for (int j = 0; j < allPoints.Length; j++)
            {
                (int X, int Y)
                    init = allPoints[j],
                    end = j < allPoints.Length - 1 ? allPoints[j + 1] : allPoints[0],
                    U = (end.X - init.X, end.Y - init.Y),
                    V = (point.X - end.X, point.Y - end.Y);

                var F = U.X * V.Y - U.Y * V.X;
                area += F;

                if (!(F > 0))
                    return false;
            }
        }

        return true;
    }
}