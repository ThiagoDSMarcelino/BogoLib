using System.Collections.Generic;
using System.ComponentModel;
using System;
using System.Linq;

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
        [Description("Here is another")]
        Shuffle,
        [Description("Last one")]
        OneByOne,
        [Description("more one")]
        Checking
    }

    /// <summary>
    /// Sorts the elements of a sequence in ascending or descending.
    /// </summary>
    /// <typeparam name="T">The type of the elements of <paramref name="source"/>.</typeparam>
    /// <param name="source">A sequence of values to be sorted.</param>
    /// <param name="isDescending">Indicates whether the sequence should be sorted in descending order</param>
    /// <param name="inplace">Indicates whether the font should be modified</param>
    /// <param name="sortingMode">Indicates which BogoSorting mode should be used</param>
    /// <returns>An <see cref="IOrderedEnumerable{TElement}"/> whose elements are sorted.</returns>
    /// <exception cref="ArgumentException">Invalid SortingType</exception>
    public static T[] BogoSort<T>(this IEnumerable<T> source, bool isDescending = false, bool inplace = false, SortingMode sortingMode = SortingMode.Shuffle)
        where T : IComparable
    {
        T[] newArr = source.ToArray();

        Random rand = Random.Shared;
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
                    newArr.Shuffle(rand);
                    break;

                case SortingMode.OneByOne:
                    newArr.OneByOne(rand);
                    break;

                case SortingMode.Checking:
                    newArr.Checking(rand, isDescending);
                    break;

                default: throw new ArgumentException();
            }
        }
                
        return newArr;
    }

    private static T[] Shuffle<T>(this T[] arr, Random rand)
    {
        int n = arr.Length;
        while (n > 1)
        {
            int k = rand.Next(n--);
            (arr[n], arr[k]) = (arr[k], arr[n]);
        }

        return arr;
    }

    private static T[] OneByOne<T>(this T[] arr, Random rand)
    {
        int n = rand.Next(arr.Length);
        int k = rand.Next(arr.Length);

        while (n == k)
            k = rand.Next(arr.Length);

        (arr[n], arr[k]) = (arr[k], arr[n]);

        return arr;
    }

    private static T[] Checking<T>(this T[] arr, Random rand, bool isDescending)
        where T : IComparable
    {
        for (int i = 0; i < arr.Length - 1; i++)
        {
            if ((arr[i].CompareTo(arr[i + 1]) == 1 && !isDescending) ||
                (arr[i].CompareTo(arr[i + 1]) == -1 && isDescending))
            {
                int newIndex = rand.Next(i, arr.Length);
                (arr[i], arr[newIndex]) = (arr[newIndex], arr[i]);
            }
        }

        return arr;
    }

    /// <summary>
    /// Returns the first found index of the <paramref name="target"/>
    /// </summary>
    /// <typeparam name="T">The type of the elements of <paramref name="source"/>.</typeparam>
    /// <param name="source">A sequence of values to order.</param>
    /// <param name="target"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static int BogoFind<T>(this T[] source, T target)
        where T : IComparable
    {
        if (source.Length == 0)
            throw new ArgumentException();

        Random rand = Random.Shared;

        bool[] indexesUsed = new bool[source.Length];
        bool allChecked = false;

        while (!allChecked)
        {
            int i = rand.Next(source.Length);

            while (indexesUsed[i])
                i = rand.Next(source.Length);

            indexesUsed[i] = true;

            if (source[i].CompareTo(target) == 0)
                return i;

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

    public static (int X, int Y)[] BogoConvexHull(this (int X, int Y)[] S)
    {
        if (S.Length < 4)
            return S;

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

    /// <summary>
    /// Randomly calculate the root of integers
    /// </summary>
    /// <param name="x">Number whose root you want to know</param>
    /// <returns>Returns the root if it is exact or -1 otherwise</returns>
    public static int BogoSqrt(int x)
    {
        Random rand = Random.Shared;
        int min = 0, max = x, sqrt = rand.Next(max), exp = sqrt * sqrt;

        while (exp != x)
        {
            if (max - min == 1)
                return -1;
            else if (exp < x)
                min = sqrt;
            else
                max = sqrt;

            sqrt = rand.Next(min, max);
            exp = sqrt * sqrt;
        }

        return sqrt;
    }
}