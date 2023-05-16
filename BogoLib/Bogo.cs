using System;

namespace BogoLib;

/// <summary>
/// Represents a collection of bogo algorithms 
/// </summary>
public static class Bogo
{
    /// <summary>
    /// Represents all sorting modes available for BogoSort
    /// </summary>
    public enum SortingMode : byte
    {
        Shuffle,
        OneByOne,
        Checking
    }

    /// <summary>
    /// Sorts the elements in an entire <see cref="Array" /> using the <see cref="IComparable{in T}" />
    /// </summary>
    /// <typeparam name="T">The type of the elements of <paramref name="arr" /></typeparam>
    /// <param name="arr">A array of values to be sorted</param>
    /// <param name="isDescending">Indicates whether the array should be sorted in descending order</param>
    /// <param name="inplace">Indicates whether the font should be modified</param>
    /// <param name="sortingMode">Indicates which BogoSorting mode should be used</param>
    /// <returns>
    /// Sorted <see cref="Array" /> of <typeparamref name="T"> nothing if <paramref name="inplace" /> parameter is true
    /// </returns>
    public static T[] BogoSort<T>(this T[] arr, bool isDescending = false, bool inplace = false, SortingMode sortingMode = SortingMode.Shuffle)
        where T : IComparable
    {
        T[] newArr = inplace ? arr : (T[])arr.Clone();
        bool isSorted = false;

        while (!isSorted)
        {
            isSorted = true;
            for (int i = 0; i < newArr.Length - 1; i++)
            {
                if (newArr[i].CompareTo(newArr[i + 1]) == 1 && !isDescending)
                {
                    isSorted = false;
                    break;
                }

                if (newArr[i].CompareTo(newArr[i + 1]) == -1 && isDescending)
                {
                    isSorted = false;
                    break;
                }
            }

            if (isSorted)
                continue;

            switch (sortingMode)
            {
                case SortingMode.Shuffle:
                    newArr.Shuffle();
                    break;

                case SortingMode.OneByOne:
                    newArr.OneByOne();
                    break;

                case SortingMode.Checking:
                    newArr.Checking(isDescending);
                    break;
            }
        }

        return newArr;
    }

    private static T[] Shuffle<T>(this T[] arr)
    {
        int n = arr.Length;
        while (n > 1)
        {
            int k = Random.Shared.Next(n--);
            arr.Swap(n, k);
        }

        return arr;
    }

    private static T[] OneByOne<T>(this T[] arr)
    {
        int n = Random.Shared.Next(arr.Length);
        int k = Random.Shared.Next(arr.Length);

        while (n == k)
            k = Random.Shared.Next(arr.Length);

        arr.Swap(n, k);

        return arr;
    }

    private static T[] Checking<T>(this T[] arr, bool isDescending)
        where T : IComparable
    {
        for (int i = 0; i < arr.Length - 1; i++)
        {
            if ((arr[i].CompareTo(arr[i + 1]) == 1 && !isDescending) ||
                (arr[i].CompareTo(arr[i + 1]) == -1 && isDescending))
            {
                int newIndex = Random.Shared.Next(i, arr.Length);
                arr.Swap(i, newIndex);
            }
        }

        return arr;
    }

    private static void Swap<T>(this T[] arr, int index1, int index2) =>
        (arr[index1], arr[index2]) = (arr[index2], arr[index1]);

    /// <summary>
    /// Returns the first found index of the <paramref name="target" />
    /// </summary>
    /// <typeparam name="T">The type of the elements of <paramref name="array" /></typeparam>
    /// <param name="array">A array of values to order</param>
    /// <param name="target"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static int BogoFind<T>(this T[] arr, T target)
        where T : IComparable
    {
        if (arr.Length == 0)
            throw new ArgumentException();

        bool allChecked = false;

        while (!allChecked)
        {
            int i = Random.Shared.Next(arr.Length);
            bool[] indexesUsed = new bool[arr.Length];

            while (indexesUsed[i])
                i = Random.Shared.Next(arr.Length);

            indexesUsed[i] = true;

            if (arr[i].CompareTo(target) == 0) return i;

            allChecked = true;
            foreach (bool item in indexesUsed)
            {
                if (!item)
                {
                    allChecked = false;
                    break;
                }
            }
        }

        return -1;
    }

    public struct Point
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="S"></param>
    /// <returns></returns>
    public static Point[] BogoConvexHull(this Point[] S)
    {
        if (S.Length < 4)
            return S;

        int n = Random.Shared.Next(3, S.Length);
        var result = new Point[n];
        bool[] indexesUsed = new bool[S.Length];
        bool isCorrect = false;

        while (!isCorrect)
        {
            for (int i = 0; i < n; i++)
            {
                int randomIndex = Random.Shared.Next(S.Length);

                while (indexesUsed[randomIndex])
                    randomIndex = Random.Shared.Next(S.Length);

                indexesUsed[randomIndex] = true;

                result[i] = S[randomIndex];
            }

            result.OrderPoints();
            Console.WriteLine(CheckPoint(result, S));

            isCorrect = true;
        }

        return result;
    }

    private static void OrderPoints(this Point[] S)
    {
        var angle = Math.Atan2(S[1].Y - S[0].Y, S[1].X - S[0].X) * (180 / Math.PI);
        Console.WriteLine(angle);
    }

    private static bool CheckPoint(Point[] borderPoint, Point[]  allPoints)
    {
        foreach (var point in borderPoint)
        {
            for (int j = 0; j < allPoints.Length; j++)
            {
                Point
                    init = allPoints[j],
                    end = j < allPoints.Length - 1 ? allPoints[j + 1] : allPoints[0],
                    U = new(end.X - init.X, end.Y - init.Y),
                    V = new(point.X - end.X, point.Y - end.Y);

                var F = U.X * V.Y - U.Y * V.X;

                if (!(F > 0))
                    return false;
            }
        }

        return true;
    }

    // TODO Processor Division Algorithm
}