using System.Drawing;
using System.Linq;
using System.Numerics;

namespace BogoLib;

public static partial class BogoMath
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="points"></param>
    /// <returns></returns>
    public static PointF[] BogoConvexHull(this (int X, int Y)[] points)
    {
        var pointFs = points.Select(p => new PointF(p.X, p.Y)).ToArray();
        var result = BogoConvexHull(pointFs);

        return result;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="points"></param>
    /// <returns></returns>
    public static PointF[] BogoConvexHull(this PointF[] points)
    {
        if (points.Length < 4)
            return points;

        PointF[] result;
        bool isCorrect;

        do
        {
            var indexesUsed = new bool[points.Length];
            int n = Shared.Next(3, points.Length + 1);
            result = new PointF[n];

            for (int i = 0; i < n; i++)
            {
                int randomIndex;

                do
                {
                    randomIndex = Shared.Next(points.Length);
                }
                while (indexesUsed[randomIndex]);

                indexesUsed[randomIndex] = true;

                result[i] = points[randomIndex];
            }

            result = result.Sort();

            var insidePoints = points
                .Where(point =>
                    !result.Any(r => r.X == point.X && r.Y == point.Y))
                .ToArray();

            isCorrect = IsConvexHull(result, insidePoints);
        } while (!isCorrect);

        return result;
    }

    private static bool IsConvexHull(PointF[] edges, PointF[] points)
    {
        int i = 0;

        do
        {
            var count = 0;

            for (int j = 0; j < edges.Length; j++)
            {
                var x = j;
                var y = j + 1;
                var z = j + 2;

                var A = edges[x];
                var B = edges[y < edges.Length ? y : y - edges.Length];
                var C = edges[z < edges.Length ? z : z - edges.Length];

                var result = GetAngle(A, B, C);

                if (result < 0)
                    return false;

                if (i >= points.Length)
                    continue;
                var point = points[i];

                var isWithinRange = (point.Y < A.Y) != (point.Y < B.Y);

                var isOnTheLeft = point.X < A.X + ((point.Y - A.Y) / (B.Y - A.Y)) * (B.X - A.X);

                if (isWithinRange && isOnTheLeft)
                    count++;
            }

            if (count % 2 == 0 && i < points.Length)
                return false;

        } while (i++ < points.Length);


        return true;
    }

    private static PointF[] Sort(this PointF[] points)
    {
        var minX = points.MinBy(p => p.X).X;

        var left = points
            .Where(p => p.X == minX)
            .OrderBy(p => p.Y);

        var right = points
            .Where(p => p.X > minX);

        var crrY = left.Last().Y;

        var top = right
            .Where(p => p.Y >= crrY)
            .OrderBy(p => p.X);

        var bottom = right
            .Where(p => p.Y < crrY)
            .OrderByDescending(p => p.X);

        var result = left
            .Concat(top)
            .Concat(bottom)
            .ToArray();

        return result;
    }

    private static float GetAngle(PointF A, PointF B, PointF C)
    {
        var matrix = new Matrix4x4(
            B.X, B.Y, 1, 0,
            A.X, A.Y, 1, 0,
            C.X, C.Y, 1, 0,
            000, 000, 0, 1
        );

        return matrix.GetDeterminant();
    }
}