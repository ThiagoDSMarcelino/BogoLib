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
    public static PointF[] BogoConvexHull(this PointF[] points)
    {
        if (points.Length < 4)
            return points;

        var indexesUsed = new bool[points.Length];
        PointF[] result;
        bool isCorrect;

        do
        {
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

            isCorrect = IsConvex(points);
        } while (!isCorrect);

        return result;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="points"></param>
    /// <returns></returns>
    public static PointF[] BogoConvexHull(this (int X, int Y)[] points)
    {
        PointF[] PointFs = new PointF[points.Length];
        for (int i = 0; i < points.Length; i++)
            PointFs[i] = new(points[i].X, points[i].Y);

        var result = BogoConvexHull(PointFs);

        return result;
    }

    private static bool IsConvex(PointF[] points)
    {
        var sortedPoints = points.Sort();

        for (int i = 0; i < sortedPoints.Length; i++)
        {

            var x = i;
            var y = i + 1;
            var z = i + 2;

            var A = sortedPoints[x];
            var B = sortedPoints[y < sortedPoints.Length ? y : y - sortedPoints.Length];
            var C = sortedPoints[z < sortedPoints.Length ? z : z - sortedPoints.Length];

            var result = GetAngle(A, B, C);

            if (result < 0)
                return false;
        }

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